using System.Collections.Generic;

namespace Agh.DecisionTree.Mapping
{
    public class DataMapping
    {
        public static readonly string DefaultDelimiter = " ";

        public DataMapping()
        {
            Attributes = new List<string>();
            TransformingEmptyValuesMode = TransformingEmptyValuesMode.Remove;
        }

        public List<string> Attributes { get; set; }

        
        public TransformingEmptyValuesMode TransformingEmptyValuesMode { get; set; }
        public string Category { get; set; }
        
        private string _delimiter = DefaultDelimiter;
        public string Delimiter
        {
            get { return string.IsNullOrEmpty(_delimiter) ? DefaultDelimiter : _delimiter; }
            set { _delimiter = value; }
        }

        public bool IsValid()
        {
            if (Attributes.Count == 0)
                return false;

            if (string.IsNullOrEmpty(Category) || !Attributes.Contains(Category))
                return false;

            return !string.IsNullOrEmpty(Delimiter);
        }
    }
}
