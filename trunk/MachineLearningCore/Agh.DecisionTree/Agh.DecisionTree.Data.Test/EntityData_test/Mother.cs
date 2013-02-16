using Agh.DecisionTree.Entity;

namespace Agh.DecisionTree.Data.Test.EntityData_test
{
    public static class Mother
    {
        // Span;Shape;Slab
        //******************************
        // long;square;waffle
        // long;rectangle;waffle
        // short;square;two-way
        // short;rectangle;one-way

        internal static EntityData GetFullEntityData()
        {
            var entityData = new EntityData();
            
            entityData.Attributes.Add("Span");
            entityData.Attributes.Add("Shape");
            entityData.Attributes.Add("Slab");

            entityData.Data.Add(GetFirstValidRow());
            entityData.Data.Add(GetSecondValidDataRow());
            entityData.Data.Add(GetThirdValidDataRow());
            entityData.Data.Add(GetFourthValidDataRow());

            return entityData;
        }

        internal static EntityData GetEntityDataWithEmptyArgument()
        {
            var entityData = new EntityData();

            entityData.Attributes.Add("Span");
            entityData.Attributes.Add("");
            entityData.Attributes.Add("Slab");

            entityData.Data.Add(GetFirstValidRow());
            entityData.Data.Add(GetSecondValidDataRow());
            entityData.Data.Add(GetThirdValidDataRow());
            entityData.Data.Add(GetFourthValidDataRow());

            return entityData;
        }

        internal static EntityData GetEntityDataWithMismatchCountOfAttributesAndValues()
        {
            var entityData = new EntityData();

            entityData.Attributes.Add("Span");
            entityData.Attributes.Add("Shape");
            entityData.Attributes.Add("Slab");

            entityData.Data.Add(GetFirstValidRow());
            entityData.Data.Add(GetSecondValidDataRow());
            entityData.Data.Add(GetThirdInValidDataRow());
            entityData.Data.Add(GetFourthValidDataRow());

            return entityData;
        }

        internal static EntityData GetEntityDataWithSomeMissingValue()
        {
            var entityData = new EntityData();

            entityData.Attributes.Add("Span");
            entityData.Attributes.Add("Shape");
            entityData.Attributes.Add("Slab");

            entityData.Data.Add(GetFirstValidRow());
            entityData.Data.Add(GetSecondValidDataRow());
            entityData.Data.Add(GetThirdValidDataRowWithMissingValue());
            entityData.Data.Add(GetFourthValidDataRow());

            return entityData;
        }

        private static DataRow GetFourthValidDataRow()
        {
            var fourthDataRow = new DataRow();

            fourthDataRow.Values.Add("short");
            fourthDataRow.Values.Add("rectangle");
            fourthDataRow.Values.Add("one-way");
            return fourthDataRow;
        }

        private static DataRow GetThirdInValidDataRow()
        {
            var thirdDataRow = new DataRow();

            thirdDataRow.Values.Add("short");
            thirdDataRow.Values.Add("square");
            thirdDataRow.Values.Add("two-way");
            thirdDataRow.Values.Add("additional invald data");

            return thirdDataRow;
        }

        private static DataRow GetThirdValidDataRowWithMissingValue()
        {
            var thirdDataRow = new DataRow();

            thirdDataRow.Values.Add("short");
            thirdDataRow.Values.Add("");
            thirdDataRow.Values.Add("two-way");

            return thirdDataRow;
        }

        private static DataRow GetThirdValidDataRow()
        {
            var thirdDataRow = new DataRow();

            thirdDataRow.Values.Add("short");
            thirdDataRow.Values.Add("square");
            thirdDataRow.Values.Add("two-way");
            return thirdDataRow;
        }

        private static DataRow GetSecondValidDataRow()
        {
            var secondDataRow = new DataRow();

            secondDataRow.Values.Add("long");
            secondDataRow.Values.Add("rectangle");
            secondDataRow.Values.Add("waffle");
            return secondDataRow;
        }

        private static DataRow GetFirstValidRow()
        {
            var firstDataRow = new DataRow();

            firstDataRow.Values.Add("long");
            firstDataRow.Values.Add("square");
            firstDataRow.Values.Add("waffle");
            return firstDataRow;
        }
    }
}