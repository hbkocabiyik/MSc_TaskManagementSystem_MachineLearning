using System;
using Agh.DecisionTree.Transformation;

namespace Agh.DecisionTree.DataTransformator
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("----------------------------------------------------------------------------");
                Console.WriteLine("Usage: Agh.DecisionTree.DataTransformator.exe <data_to_transform_path> <Data mapping xml>");
                Console.WriteLine("----------------------------------------------------------------------------");
                return;
            }

            var toTransformFileName = args[0];
            var dataMappingFileName = args[1];

            Console.WriteLine("Transforming file: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(toTransformFileName);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.WriteLine("-------------------");

            Console.WriteLine("Data Mapping file: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(dataMappingFileName);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.WriteLine("-------------------");
            
            try
            {
                var dataTransformator = Transformation.DataTransformator.CreateIt(dataMappingFileName);

                Console.WriteLine("Start transforming the file");

                var createdFileInfoName = dataTransformator.Transform(toTransformFileName);

                Console.WriteLine("Transforming the file is finished");

                Console.WriteLine();
                Console.WriteLine("Entity data file: ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(createdFileInfoName);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
                Console.WriteLine("-------------------");
            }
            catch (DataTransformationException transformationException)
            {
                HandleError(transformationException.Message);
            }
        }

        private static void HandleError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Type <enter> to close a program...");
            Console.ReadLine();
        }
    }
}
