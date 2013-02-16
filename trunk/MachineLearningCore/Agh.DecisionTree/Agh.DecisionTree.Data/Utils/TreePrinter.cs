using System;
using Agh.DecisionTree.Algorithm;

namespace Agh.DecisionTree.Utils
{
    public static class TreePrinter
    {
        public static void PrintTree(IDomainTree domainTree)
        {
            PrintTree(domainTree, domainTree.Root);
        }

        private static void PrintTree(IDomainTree domainTree, ITreeNode node, String tab = "")
        {
            var outputAttributeId = domainTree.Attributes.Count - 1;

            if (node.IsLeaf())
            {
                var values = domainTree.GetAllSymbolicValuesOfAttribute(SymbolicDomainDataParams.CreateIt(node.Data, outputAttributeId));
                if (values.Length == 0)
                {
                    Console.WriteLine("{0}\t{1} = \"null\";", tab, domainTree.Attributes[outputAttributeId]);
                }
                else
                {
                    Console.WriteLine("{0}\t{1} = \"{2}\";", tab, domainTree.Attributes[outputAttributeId],
                                  domainTree.Domain[outputAttributeId][values[0]]);    
                }
                
                return;
            }

            var numvalues = node.Children.Length;
            for (var i = 0; i < numvalues; i++)
            {
                Console.WriteLine(tab + "if( " + domainTree.Attributes[node.TestAttribute] + " == \"" +
                                  domainTree.Domain[node.TestAttribute][i] + "\") {");
                PrintTree(domainTree, node.Children[i], tab + "\t");

                if (i != numvalues - 1)
                    Console.Write(tab + "} else ");
                else
                    Console.WriteLine(tab + "}");
            }
        }
    }
}