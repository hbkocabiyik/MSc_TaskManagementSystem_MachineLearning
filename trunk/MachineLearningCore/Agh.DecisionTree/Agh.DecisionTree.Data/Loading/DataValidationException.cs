using System;

namespace Agh.DecisionTree.Loading
{
    public class DataValidationException : Exception
    {
        public DataValidationException(string message) : base(message)
        {
        }
    }
}