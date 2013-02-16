using System.Collections.Generic;

namespace Agh.DecisionTree.Algorithm
{
    public class SymbolicDomainDataParams : ISymbolicDomainDataParams
    {
        public static ISymbolicDomainDataParams CreateIt(IList<IAttributeSymbolicValues> data, int domainAttributeId)
        {
            return new SymbolicDomainDataParams(data, domainAttributeId);
        }

        private readonly IList<IAttributeSymbolicValues> _data;
        private readonly int _domainAttributeId;

        private SymbolicDomainDataParams(IList<IAttributeSymbolicValues> data, int domainAttributeId)
        {
            _data = data;
            _domainAttributeId = domainAttributeId;
        }

        public IList<IAttributeSymbolicValues> Data
        {
            get { return _data; }
        }

        public int DomainAttributeId
        {
            get { return _domainAttributeId; }
        }
    }
}