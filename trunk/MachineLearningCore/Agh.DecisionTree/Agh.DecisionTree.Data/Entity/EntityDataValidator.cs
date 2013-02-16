using System.Linq;

namespace Agh.DecisionTree.Entity
{
    public class EntityDataValidator : IEntityDataValidator
    {
        public static readonly string ArgumentsCannotBeNullOrEmpty = "Arguments cannot be null or empty.";

        public static readonly string ValuesMustHaveTheSameAmountOfValuesAsTheAttributes =
            "Values must have the same amount of values as the attributes.";

        public static readonly string ValuesCannotBeNullOrEmpty = "Values cannot be null or empty.";

        private EntityDataValidator()
        {
        }

        #region IEntityDataValidator Members

        public Outcome Validate(EntityData entityData)
        {
            var outcome = new Outcome
                          {
                              IsFailure = entityData.Attributes.Any(string.IsNullOrEmpty)
                          };

            if (outcome.IsFailure)
            {
                outcome.Message = ArgumentsCannotBeNullOrEmpty;
                return outcome;
            }

            var attributesCount = entityData.Attributes.Count;

            outcome.IsFailure = entityData.Data.Any(d => d.Values.Count != attributesCount);

            if (outcome.IsFailure)
            {
                outcome.Message = string.Format("{0} Attributes count: {1}.", ValuesMustHaveTheSameAmountOfValuesAsTheAttributes,
                                                attributesCount);
                return outcome;
            }

            outcome.IsFailure = entityData.Data.SelectMany(d => d.Values).Any(string.IsNullOrEmpty);
            if (outcome.IsFailure)
            {
                outcome.Message = ValuesCannotBeNullOrEmpty;
                return outcome;
            }

            return outcome;
        }

        #endregion

        public static IEntityDataValidator CreateIt()
        {
            return new EntityDataValidator();
        }
    }
}
