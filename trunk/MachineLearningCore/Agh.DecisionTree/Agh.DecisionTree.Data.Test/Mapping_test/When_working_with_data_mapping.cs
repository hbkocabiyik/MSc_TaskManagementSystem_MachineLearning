using Agh.DecisionTree.Mapping;
using Agh.DecisionTree.Serialization;

namespace Agh.DecisionTree.Data.Test.Mapping_test
{
    using System.Linq;
    using NUnit.Framework;

    // Span;Shape;Slab
    //******************************
    // long;square;waffle
    // long;rectangle;waffle
    // short;square;two-way
    // short;rectangle;one-way

    [TestFixture]
    public class When_working_with_data_mapping
    {
        private const string CategoryAttribute = "Slab";

        [Test]
        public void It_should_be_invalid_after_initialization()
        {
            var dataMapping = new DataMapping();

            Assert.That(dataMapping.IsValid(), Is.False);
        }

        [Test]
        public void It_should_be_invalid_after_initialization_and_setting_only_attributes()
        {
            var dataMapping = new DataMapping();

            dataMapping.Attributes.Add("Span");
            dataMapping.Attributes.Add("Shape");
            dataMapping.Attributes.Add(CategoryAttribute);

            Assert.That(dataMapping.IsValid(), Is.False);
        }

        [Test]
        public void It_should_be_valid_after_initialization_and_setting_only_attributes_and_category()
        {
            var dataMapping = new DataMapping();

            dataMapping.Attributes.Add("Span");
            dataMapping.Attributes.Add("Shape");
            dataMapping.Attributes.Add(CategoryAttribute);

            dataMapping.Category = CategoryAttribute;

            Assert.That(dataMapping.IsValid(), Is.True);
            Assert.That(dataMapping.Delimiter, Is.EqualTo(DataMapping.DefaultDelimiter));
        }

        [Test]
        public void It_should_be_valid_after_initialization_and_setting_attributes_and__valid_category_and_delimiter()
        {
            var dataMapping = GetValidDataMapping();

            Assert.That(dataMapping.IsValid(), Is.True);
        }

        private static DataMapping GetValidDataMapping()
        {
            var dataMapping = new DataMapping();

            dataMapping.Attributes.Add("Span");
            dataMapping.Attributes.Add("Shape");
            dataMapping.Attributes.Add(CategoryAttribute);

            dataMapping.Category = CategoryAttribute;
            dataMapping.Delimiter = " ";
            return dataMapping;
        }

        [Test]
        public void It_should_be_invalid_after_initialization_and_setting_attributes_and__invalid_category_and_delimiter()
        {
            var dataMapping = new DataMapping();

            dataMapping.Attributes.Add("Span");
            dataMapping.Attributes.Add("Shape");
            dataMapping.Attributes.Add(CategoryAttribute);

            dataMapping.Category = "invalidcategory";
            dataMapping.Delimiter = ";";

            Assert.That(dataMapping.IsValid(), Is.False);
        }

        [Test]
        public void It_should_generate_xml_configuration_and_load_it_with_the_same_values()
        {
            const string FileName = "MappingData.xml";
            var dataMapping = GetValidDataMapping();

            DataSerializer.Serialize(dataMapping, FileName);
            DataMapping deserializedDataMapping = DataSerializer.Deserialize<DataMapping>(FileName);

            Assert.That(deserializedDataMapping, Is.Not.Null);

            Assert.That(deserializedDataMapping.Attributes.SequenceEqual(dataMapping.Attributes), Is.True);
            Assert.That(deserializedDataMapping.Category, Is.EqualTo(dataMapping.Category));
            Assert.That(deserializedDataMapping.Delimiter, Is.EqualTo(dataMapping.Delimiter));
        }
    }
}
