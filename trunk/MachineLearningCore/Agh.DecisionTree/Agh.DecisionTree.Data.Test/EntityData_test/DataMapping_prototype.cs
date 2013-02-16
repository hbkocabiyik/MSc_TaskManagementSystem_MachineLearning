using Agh.DecisionTree.Entity;
using Agh.DecisionTree.Serialization;
using NUnit.Framework;
using System.Linq;

namespace Agh.DecisionTree.Data.Test.EntityData_test
{
    [TestFixture]
    public class DataMapping_prototype
    {
        [Test]
        public void XmlSerialization()
        {
            EntityData entityData = Mother.GetFullEntityData();

            const string FileName = "EntityData.xml";

            DataSerializer.Serialize(entityData,  FileName);

            EntityData deserializedEntityData = DataSerializer.Deserialize<EntityData>(FileName);
            
            Assert.That(deserializedEntityData, Is.Not.Null);

            Assert.That(deserializedEntityData.Attributes.SequenceEqual(entityData.Attributes), Is.True);
            Assert.That(deserializedEntityData.Data.Count, Is.EqualTo(entityData.Data.Count));

            for (int i = 0; i < deserializedEntityData.Data.Count; i++)
                Assert.That(deserializedEntityData.Data[i].Values.SequenceEqual(entityData.Data[i].Values), Is.True);
        }
    }
}
