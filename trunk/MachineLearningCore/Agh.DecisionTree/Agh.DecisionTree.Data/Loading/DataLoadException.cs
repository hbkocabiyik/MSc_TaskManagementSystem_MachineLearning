using System;

namespace Agh.DecisionTree.Loading
{
    public class DataLoadException : Exception
    {
        public DataLoadException(string message) : base(message)
        {
        }
    }
}
