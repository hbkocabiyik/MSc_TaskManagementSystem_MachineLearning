using System.Collections.Generic;
using System.Linq;

namespace Agh.DecisionTree.Algorithm
{
    public class DomainTree : IDomainTree
    {
        public DomainTree()
        {
            Attributes = new List<string>();
            Domain = new List<List<string>>();
            Root = TreeNode.CreateIt();
        }

        #region IDomainTree Members

        public List<string> Attributes { get; set; }

        public List<List<string>> Domain { get; set; }

        public ITreeNode Root { get; set; }

        public int[] GetAllSymbolicValuesOfAttribute(ISymbolicDomainDataParams symbolicDomainDataParams)
        {
            if (symbolicDomainDataParams.Data == null || symbolicDomainDataParams.Data.Count == 0)
                return new int[0];

            var values =
                symbolicDomainDataParams.Data.Select(
                    domainSymbolicAttributes => GetSymbolOf(symbolicDomainDataParams.DomainAttributeId, domainSymbolicAttributes)).Distinct()
                    .ToList();

            var array = new int[values.Count];
            for (var i = 0; i < array.Length; i++)
            {
                var symbol = values[i];
                array[i] = Domain[symbolicDomainDataParams.DomainAttributeId].IndexOf(symbol);
            }
            return array;
        }

        #endregion

        private string GetSymbolOf(int domainAttributeId, IAttributeSymbolicValues point)
        {
            return Domain[domainAttributeId][point.Values[domainAttributeId]];
        }
    }
}
