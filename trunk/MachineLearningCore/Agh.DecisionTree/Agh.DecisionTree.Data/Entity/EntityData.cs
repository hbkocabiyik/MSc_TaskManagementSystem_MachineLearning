using System.Collections.Generic;

namespace Agh.DecisionTree.Entity
{
    public class EntityData
    {
        public EntityData()
        {
            Attributes = new List<string>();
            Data = new List<DataRow>();
        }

        public List<string> Attributes { get; set; }
        public List<DataRow> Data { get; set;}
    }
}