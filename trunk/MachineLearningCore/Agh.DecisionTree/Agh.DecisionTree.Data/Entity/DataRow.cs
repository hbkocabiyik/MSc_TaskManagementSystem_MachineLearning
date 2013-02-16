using System.Collections.Generic;

namespace Agh.DecisionTree.Entity
{
    public class DataRow
    {
        public DataRow()
        {
            Values = new List<string>();
        }

        public List<string> Values { get; set; }
    }
}