using System.Collections.Generic;

namespace Agh.DecisionTree.Algorithm
{
    public class TreeNode : ITreeNode
    {
        private TreeNode()
        {
            Data = new List<IAttributeSymbolicValues>();
        }

        #region ITreeNode Members

        public ITreeNode[] Children { get; set; }

        public List<IAttributeSymbolicValues> Data { get; set; }

        public int TestAttribute { get; set; }

        public int TestAttributeValue { get; set; }

        public double Entropy { get; set; }

        public ITreeNode Parent { get; set; }

        public bool IsLeaf()
        {
            return Children == null;
        }

        public bool IsRoot()
        {
            return Parent == null;
        }

        #endregion

        public static TreeNode CreateIt()
        {
            return new TreeNode();
        }
    }
}
