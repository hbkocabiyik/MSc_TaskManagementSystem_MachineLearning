using System;
using System.Collections.Generic;
using Agh.DecisionTree.Algorithm;
using Agh.DecisionTree.Entity;
using Agh.DecisionTree.Serialization;

namespace Agh.DecisionTree.Loading
{
    public class DataLoader : IDataLoader
    {
        public static IDataLoader CreateIt(IEntityDataValidator validator)
        {
            return new DataLoader(validator);
        }

        private readonly IEntityDataValidator _validator;

        private DataLoader(IEntityDataValidator validator)
        {
            _validator = validator;
        }

        #region IDataLoader Members

        public IDomainTree LoadFromFile(string fileName)
        {
            var entity = DataSerializer.Deserialize<EntityData>(fileName);

            return Load(entity);
        }

        public IDomainTree Load(EntityData entityData)
        {
            var outcome = _validator.Validate(entityData);

            if (outcome.IsFailure)
                throw new DataValidationException(outcome.Message);

            IDomainTree domainTree = new DomainTree();

            var domainAttributesCount = entityData.Attributes.Count;

            var attributesBuffer = new string[domainAttributesCount];
            entityData.Attributes.CopyTo(attributesBuffer);
            domainTree.Attributes = new List<string>(attributesBuffer);

            for (var i = 0; i < domainAttributesCount; i++)
                domainTree.Domain.Add(new List<string>());

            EvalInputFrom(entityData, domainTree);

            return domainTree;
        }

        #endregion

        private static void EvalInputFrom(EntityData entityData, IDomainTree domainTree)
        {
            foreach (var dataRow in entityData.Data)
            {
                var domainSymbolicAttributes = AttributeSymbolicValues.CreateIt(domainTree.Attributes.Count);

                for (var attributeId = 0; attributeId < domainTree.Attributes.Count; attributeId++)
                    domainSymbolicAttributes.Values[attributeId] = GetSymbolValue(domainTree, attributeId,
                                                                                              dataRow.Values[attributeId]);

                domainTree.Root.Data.Add(domainSymbolicAttributes);
            }
        }

        /// <summary>
        ///   This function returns an integer corresponding to the symbolic value of the attribute.
        ///   If the symbol does not exist in the domain, the symbol is added to the domain of the attribute
        /// </summary>
        /// <param name = "domainTree"></param>
        /// <param name = "domainAttributeId"></param>
        /// <param name = "symbol"></param>
        /// <returns></returns>
        private static int GetSymbolValue(IDomainTree domainTree, int domainAttributeId, String symbol)
        {
            var index = domainTree.Domain[domainAttributeId].IndexOf(symbol);

            if (index < 0)
            {
                domainTree.Domain[domainAttributeId].Add(symbol);
                return domainTree.Domain[domainAttributeId].Count - 1;
            }

            return index;
        }
    }
}
