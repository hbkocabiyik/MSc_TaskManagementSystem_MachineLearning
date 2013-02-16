using System;
using Agh.DecisionTree.Algorithm;
using Agh.DecisionTree.Entity;
using Agh.DecisionTree.Loading;
using Agh.DecisionTree.Utils;

namespace Agh.DecisionTree.ID3.Program
{
    public class Program
    {
        public static void Main(String[] args)
        {
            var num = args.Length;
            if (num != 1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Error.WriteLine("You need to specify the name of the datafile at the command line ");
                return;
            }

            IDomainTree data = ReadData(args);

            InvokeID3Algorithm(data);

            IDomainTree data2 = ReadData(args);

            InvokeID3Optimized(data2);

            IDomainTree data3 = ReadData(args);
            InvokeID3Algorithm(data3);

            IDomainTree data4 = ReadData(args);

            InvokeID3Optimized(data4);

            Console.WriteLine("Press <enter> to finish program ...");
            Console.ReadLine();
        }

        private static IDomainTree ReadData(string[] args)
        {
            var startTime = DateTime.Now.Ticks; //  To print the time taken to process the data

            IDataLoader loader = DataLoader.CreateIt(EntityDataValidator.CreateIt());
            var data = loader.LoadFromFile(args[0]);

            var endTime = DateTime.Now.Ticks;
            var totalTime = (endTime - startTime) / 1000;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Reading tree: {0} ms", totalTime);
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            return data;
        }

        private static void InvokeID3Optimized(IDomainTree data)
        {
            var meOptimized = ID3Algorithm.CreateIt(data, true);

            long startTime = DateTime.Now.Ticks;

            meOptimized.BuildDecisionTree();

            long endTime = DateTime.Now.Ticks;
            long totalTime = (endTime - startTime) / 1000;
            
            TreePrinter.PrintTree(meOptimized.DomainTree);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            Console.WriteLine("Generating tree (optimized): {0} ms", totalTime);
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
        }

        private static void InvokeID3Algorithm(IDomainTree data)
        {
            var me = ID3Algorithm.CreateIt(data);

            long startTime = DateTime.Now.Ticks;

            me.BuildDecisionTree();

            long endTime = DateTime.Now.Ticks;
            long totalTime = (endTime - startTime) / 1000;

            TreePrinter.PrintTree(me.DomainTree);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            Console.WriteLine("Generating tree: {0} ms", totalTime);
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}