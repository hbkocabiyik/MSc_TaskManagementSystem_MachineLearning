using System.Collections.Generic;

namespace Agh.DecisionTree.Algorithm
{
    public interface ISymbolicDomainDataParams
    {
        IList<IAttributeSymbolicValues> Data { get; }
        int DomainAttributeId { get; }
    }
}