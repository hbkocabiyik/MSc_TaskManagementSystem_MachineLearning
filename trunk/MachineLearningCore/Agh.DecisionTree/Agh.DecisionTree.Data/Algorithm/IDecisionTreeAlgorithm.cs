using System.Collections.Generic;

namespace Agh.DecisionTree.Algorithm
{
    public interface IDecisionTreeAlgorithm
    {
        IDomainTree DomainTree { get; }

        void BuildDecisionTreeFrom(ITreeNode root);

        void BuildDecisionTree();

        string Classify(IList<string> data);
    }
}