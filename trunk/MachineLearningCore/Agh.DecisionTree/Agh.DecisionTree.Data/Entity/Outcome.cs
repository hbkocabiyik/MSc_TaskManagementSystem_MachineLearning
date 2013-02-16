namespace Agh.DecisionTree.Entity
{
    public sealed class Outcome
    {
        public bool IsFailure { get; set; }
        public string Message { get; set; }

        public bool IsSuccess
        {
            get { return !IsFailure; }
        }
    }
}