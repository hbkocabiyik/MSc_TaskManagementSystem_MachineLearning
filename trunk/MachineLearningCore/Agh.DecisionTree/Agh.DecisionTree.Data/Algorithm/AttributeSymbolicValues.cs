namespace Agh.DecisionTree.Algorithm
{
    /// <summary>
    ///   The class to represent a data point consisting of 'count' values of attributes
    /// </summary>
    public class AttributeSymbolicValues : IAttributeSymbolicValues
    {
        public static AttributeSymbolicValues CreateIt(int count)
        {
            return new AttributeSymbolicValues(count);
        }

        private readonly int[] _values;

        private AttributeSymbolicValues(int count)
        {
            _values = new int[count];
        }

        public int[] Values
        {
            get { return _values; }
        }
    }
}
