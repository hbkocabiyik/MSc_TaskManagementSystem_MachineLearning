using System.Collections.Generic;

namespace Agh.DecisionTree.Algorithm
{
    public interface IDecisionTreeChoiceStrategy
    {
        string MakeChoice(IEnumerable<string> categories);
    }
}