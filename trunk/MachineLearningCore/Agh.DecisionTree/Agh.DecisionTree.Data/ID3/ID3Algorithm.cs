using System;
using System.Collections.Generic;
using System.Linq;
using Agh.DecisionTree.Algorithm;

namespace Agh.DecisionTree.ID3
{
    public class ID3Algorithm : IDecisionTreeAlgorithm
    {
        private readonly bool _optimized;

        private IDecisionTreeChoiceStrategy _choiceStrategy;

        private ID3Algorithm(IDomainTree domainTree, bool optimized = false)
        {
            _optimized = optimized;
            DomainTree = domainTree;
            _choiceStrategy = DefaultDecisionTreeChoiceStrategy.CreateIt();
        }

        public IDecisionTreeChoiceStrategy ChoiceStrategy
        {
            get { return _choiceStrategy; }
            set { _choiceStrategy = value ?? DefaultDecisionTreeChoiceStrategy.CreateIt(); }
        }

        #region IDecisionTreeAlgorithm Members

        public IDomainTree DomainTree { get; private set; }

        public void BuildDecisionTreeFrom(ITreeNode root)
        {
            var categoryId = DomainTree.Attributes.Count - 1;
            var categoriesCount = DomainTree.Domain[categoryId].Count;

            var symbolicDomainDataParams = SymbolicDomainDataParams.CreateIt(root.Data, categoryId);
            root.Entropy = Entropy.CalculateEntropy(symbolicDomainDataParams, categoriesCount);

            if (Math.Abs(root.Entropy - 0.0) < 0.000001)
                return;

            var bestAttributeId = CalculateBestAttributeId(root, categoriesCount, categoryId);

            if (bestAttributeId == -1)
                return;

            var bestAttributeValuesCount = DomainTree.Domain[bestAttributeId].Count;

            root.TestAttribute = bestAttributeId;
            root.Children = new TreeNode[bestAttributeValuesCount];

            for (var id = 0; id < bestAttributeValuesCount; id++)
                AssignNewTreeNode(root, bestAttributeId, id);

            foreach (var treeNode in root.Children)
                BuildDecisionTreeFrom(treeNode);
        }

        public void BuildDecisionTree()
        {
            BuildDecisionTreeFrom(DomainTree.Root);
        }

        public string Classify(IList<string> data)
        {
            return ClassifyInternal(data, DomainTree.Root);
        }

        #endregion

        public static IDecisionTreeAlgorithm CreateIt(IDomainTree domainTree, bool optimized = false)
        {
            return new ID3Algorithm(domainTree, optimized);
        }

        private static void AssignNewTreeNode(ITreeNode root, int bestAttributeId, int id)
        {
            root.Children[id] = TreeNode.CreateIt();
            root.Children[id].Parent = root;

            var symbolicDomainDataParams = SymbolicDomainDataParams.CreateIt(root.Data, bestAttributeId);
            root.Children[id].Data = GetSubsetOf(symbolicDomainDataParams, id);

            root.Children[id].TestAttributeValue = id;
        }

        private int CalculateBestAttributeId(ITreeNode root, int categoriesCount, int categoryId)
        {
            var bestAttributeId = -1;
            var bestGainFactor = 0.0;

            for (var attributeId = 0; attributeId < categoryId; attributeId++)
            {
                if (IsAttributeAlreadyUsed(root, attributeId))
                    continue;

                var entropies = new List<double>();
                var nodeSubsetsSizes = new List<int>();

                var attributeSymbolicValuesCount = DomainTree.Domain[attributeId].Count;

                for (var attributeSymbolicValue = 0; attributeSymbolicValue < attributeSymbolicValuesCount; attributeSymbolicValue++)
                {
                    var symbolicDomainDataParams = SymbolicDomainDataParams.CreateIt(root.Data, attributeId);

                    var nodeSubset = GetSubsetOf(symbolicDomainDataParams, attributeSymbolicValue);
                    nodeSubsetsSizes.Add(nodeSubset.Count);

                    if (nodeSubset.Count == 0)
                        continue;

                    var symbolicDomainDataParamsForNodeSubset = SymbolicDomainDataParams.CreateIt(nodeSubset, categoryId);
                    var entropy = Entropy.CalculateEntropy(symbolicDomainDataParamsForNodeSubset, categoriesCount);
                    entropies.Add(entropy);
                }

                var gainFactor = CalculateGainFactor(root, nodeSubsetsSizes, entropies);

                if (gainFactor <= bestGainFactor)
                    continue;

                bestGainFactor = gainFactor;
                bestAttributeId = attributeId;
            }

            return bestAttributeId;
        }

        private double CalculateGainFactor(ITreeNode root, IList<int> nodeSubsetsSizes, IList<double> entropies)
        {
            return _optimized
                       ? Entropy.CalculateGain(root.Entropy, entropies, nodeSubsetsSizes, root.Data.Count)
                       : Entropy.CalculateGain(root.Entropy, entropies, nodeSubsetsSizes, root.Data.Count) /
                         Entropy.CalculateGainRatio(nodeSubsetsSizes, root.Data.Count);
        }

        private string ClassifyInternal(IList<string> data, ITreeNode evaluatedNode)
        {
            var outputAttributeId = DomainTree.Attributes.Count - 1;

            if (evaluatedNode.IsLeaf())
            {
                var values =
                    DomainTree.GetAllSymbolicValuesOfAttribute(SymbolicDomainDataParams.CreateIt(evaluatedNode.Data, outputAttributeId));

                var choicedValue =
                    _choiceStrategy.MakeChoice(values.Select(symbolicValue => DomainTree.Domain[outputAttributeId][symbolicValue]));

                return choicedValue;
            }

            var categories = evaluatedNode.Children.Where((treeNode, index) => AreAttributeSymbolicValuesEqual(evaluatedNode, index, data))
                .Select(treeNode => ClassifyInternal(data, treeNode));

            return _choiceStrategy.MakeChoice(categories);
        }

        private bool AreAttributeSymbolicValuesEqual(ITreeNode node, int i, IList<string> data)
        {
            return DomainTree.Domain[node.TestAttribute][i] == data[node.TestAttribute];
        }

        private static List<IAttributeSymbolicValues> GetSubsetOf(ISymbolicDomainDataParams symbolicDomainDataParams, int symboicValue)
        {
            return
                symbolicDomainDataParams.Data.Where(
                    attributeSymbolicValues => attributeSymbolicValues.Values[symbolicDomainDataParams.DomainAttributeId] == symboicValue).
                    ToList();
        }

        private static bool IsAttributeAlreadyUsed(ITreeNode node, int attributeToDecompose)
        {
            return !node.IsLeaf() && node.TestAttribute == attributeToDecompose ||
                   !node.IsRoot() && IsAttributeAlreadyUsed(node.Parent, attributeToDecompose);
        }
    }
}
