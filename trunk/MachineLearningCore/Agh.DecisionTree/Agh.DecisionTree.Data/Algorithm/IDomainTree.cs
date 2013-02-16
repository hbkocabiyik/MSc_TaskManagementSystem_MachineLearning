using System.Collections.Generic;

namespace Agh.DecisionTree.Algorithm
{
    public interface IDomainTree
    {
        List<string> Attributes { get; set; }
        List<List<string>> Domain { get; set; }
        ITreeNode Root { get; set; }

        int[] GetAllSymbolicValuesOfAttribute(ISymbolicDomainDataParams symbolicDomainDataParams);
    }
}