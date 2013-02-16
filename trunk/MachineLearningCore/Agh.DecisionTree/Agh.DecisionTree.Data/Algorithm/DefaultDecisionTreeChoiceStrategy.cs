using System.Collections.Generic;
using System.Linq;

namespace Agh.DecisionTree.Algorithm
{
    public class DefaultDecisionTreeChoiceStrategy : IDecisionTreeChoiceStrategy
    {
        public static IDecisionTreeChoiceStrategy CreateIt()
        {
            return new DefaultDecisionTreeChoiceStrategy();
        }

        private DefaultDecisionTreeChoiceStrategy()
        {
        }

        public string MakeChoice(IEnumerable<string> categories)
        {
            return categories.FirstOrDefault();
        }
    }
}