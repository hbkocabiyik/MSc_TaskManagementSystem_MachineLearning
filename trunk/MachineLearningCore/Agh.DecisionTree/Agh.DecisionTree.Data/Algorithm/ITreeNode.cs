using System.Collections.Generic;

namespace Agh.DecisionTree.Algorithm
{
    public interface ITreeNode
    {
        ITreeNode[] Children { get; set; }

        List<IAttributeSymbolicValues> Data { get; set; }

        int TestAttribute { get; set; }

        int TestAttributeValue { get; set; }

        double Entropy { get; set; }

        ITreeNode Parent { get; set; }

        bool IsLeaf();

        bool IsRoot();
    }
}
