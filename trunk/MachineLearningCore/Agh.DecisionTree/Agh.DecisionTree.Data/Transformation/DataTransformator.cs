using Agh.DecisionTree.Entity;
using Agh.DecisionTree.Mapping;
using Agh.DecisionTree.Serialization;
using System;
using System.Collections.Generic;
using System.IO;

namespace Agh.DecisionTree.Transformation
{
    public class DataTransformator : IDataTransformator
    {
        private readonly DataMapping _dataMapping;

        private DataTransformator(string dataMappingFileName)
        {
            try
            {
                _dataMapping = DataSerializer.Deserialize<DataMapping>(dataMappingFileName);
            }
            catch (Exception e)
            {
                var exceptionMessage = String.Format("Unable to open data file: {0}\n{1}", dataMappingFileName, e);
                throw new DataTransformationException(exceptionMessage);
            }

            if (_dataMapping.IsValid())
                return;

            var validationMessage = String.Format("Data mapping file is invalid: {0}\n", dataMappingFileName);
            throw new DataTransformationException(validationMessage);
        }

        #region IDataTransformator Members

        public string Transform(string toTransformFileName)
        {
            FileStream fileToTransformStream;

            try
            {
                var inputFile = new FileInfo(toTransformFileName);
                fileToTransformStream = new FileStream(inputFile.FullName, FileMode.Open);
            }
            catch (Exception e)
            {
                var exceptionMessage = String.Format("Unable to open data file: {0}\n{1}", toTransformFileName, e);
                throw new DataTransformationException(exceptionMessage);
            }

            var entity = new EntityData {
                                            Attributes = _dataMapping.Attributes
                                        };

            using (var input = new StreamReader(fileToTransformStream))
            {
                EvalInputFrom(input, entity);
            }

            SetCategoryAsALastAttribute(entity);

            var fileWithoutExtension = Path.GetFileNameWithoutExtension(toTransformFileName);

            var transformedDataFileName = String.Format("{0}.xml", fileWithoutExtension);
            DataSerializer.Serialize(entity, transformedDataFileName);

            return transformedDataFileName;
        }

        #endregion

        public static IDataTransformator CreateIt(string dataMappingFileName)
        {
            return new DataTransformator(dataMappingFileName);
        }

        private void SetCategoryAsALastAttribute(EntityData entity)
        {
            var indexOfCategory = entity.Attributes.IndexOf(_dataMapping.Category);
            var lastAttributeIndex = entity.Attributes.Count - 1;

            if (indexOfCategory == lastAttributeIndex)
                return;

            ReverseElementsOf(entity.Attributes, indexOfCategory, lastAttributeIndex);

            foreach (var dataRow in entity.Data)
                ReverseElementsOf(dataRow.Values, indexOfCategory, lastAttributeIndex);
        }

        private static void ReverseElementsOf(IList<string> attributes, int indexOfCategory, int lastAttributeIndex)
        {
            var category = attributes[indexOfCategory];
            attributes[indexOfCategory] = attributes[lastAttributeIndex];
            attributes[lastAttributeIndex] = category;
        }

        private void EvalInputFrom(TextReader reader, EntityData entity)
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                if (line.StartsWith("//"))
                    continue;
                if (line.Equals(""))
                    continue;

                var values = SplitInput(line);

                if (values.Length != entity.Attributes.Count)
                {
                    var exceptionMessage = String.Format("Read {0} data\nLast line read: {1}\nExpecting {2} attributes", values.Length, line,
                                                         entity.Attributes.Count);

                    throw new DataTransformationException(exceptionMessage);
                }

                var dataRow = new DataRow();

                for (var attributeId = 0; attributeId < entity.Attributes.Count; attributeId++)
                    dataRow.Values.Add(values[attributeId]);

                entity.Data.Add(dataRow);
            }
        }

        private string[] SplitInput(string line)
        {
            return line.Split(new[] {_dataMapping.Delimiter}, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
