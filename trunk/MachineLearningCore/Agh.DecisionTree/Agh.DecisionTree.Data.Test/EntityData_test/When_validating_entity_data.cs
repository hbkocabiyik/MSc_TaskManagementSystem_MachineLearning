using Agh.DecisionTree.Entity;
using NUnit.Framework;

namespace Agh.DecisionTree.Data.Test.EntityData_test
{
    [TestFixture]
    public class When_validating_entity_data
    {
        [Test]
        public void It_should_return_succes_for_valid_data()
        {
            var fullEntityData = Mother.GetFullEntityData();
            var entityDataValidator = EntityDataValidator.CreateIt();

            var outcome = entityDataValidator.Validate(fullEntityData);

            Assert.That(outcome.IsSuccess, Is.True);
            Assert.That(outcome.Message, Is.Null);
        }

        [Test]
        public void It_should_return_failure_for_data_with_empty_argument()
        {
            var entityDataWithEmptyArgument = Mother.GetEntityDataWithEmptyArgument();
            var entityDataValidator = EntityDataValidator.CreateIt();

            var outcome = entityDataValidator.Validate(entityDataWithEmptyArgument);

            Assert.That(outcome.IsSuccess, Is.False);
            Assert.That(outcome.Message, Is.EqualTo(EntityDataValidator.ArgumentsCannotBeNullOrEmpty));
        }

        [Test]
        public void It_should_return_failure_for_data_with_count_mismatch_between_attributes_and_values()
        {
            var entityDataWithMismatchCountOfAttributesAndValues = Mother.GetEntityDataWithMismatchCountOfAttributesAndValues();
            var entityDataValidator = EntityDataValidator.CreateIt();

            var outcome = entityDataValidator.Validate(entityDataWithMismatchCountOfAttributesAndValues);

            Assert.That(outcome.IsSuccess, Is.False);
            Assert.That(outcome.Message, Is.StringStarting(EntityDataValidator.ValuesMustHaveTheSameAmountOfValuesAsTheAttributes));
        }

        [Test]
        public void It_should_return_failure_for_data_with_missing_value_when_missingData_is_set_to_false()
        {
            var entityDataWithSomeMissingValue = Mother.GetEntityDataWithSomeMissingValue();
            var entityDataValidator = EntityDataValidator.CreateIt();

            var outcome = entityDataValidator.Validate(entityDataWithSomeMissingValue);

            Assert.That(outcome.IsSuccess, Is.False);
            Assert.That(outcome.Message, Is.EqualTo(EntityDataValidator.ValuesCannotBeNullOrEmpty));
        }
    }
}