using System;
using System.Collections.Generic;
using System.Linq;

namespace Agh.DecisionTree.Algorithm
{
    public static class Entropy
    {
        public static double CalculateEntropy(ISymbolicDomainDataParams symbolicDomainDataParams, int attributesCount)
        {
            if (symbolicDomainDataParams.Data.Count == 0)
                return 0;

            var entropy = 0.0;

            for (var i = 0; i < attributesCount; i++)
            {
                var domainAttributeValue = i;
                var count =
                    symbolicDomainDataParams.Data.Count(
                        record => record.Values[symbolicDomainDataParams.DomainAttributeId] == domainAttributeValue);

                if (count <= 0)
                    continue;

                var probability = count / (double)symbolicDomainDataParams.Data.Count;
                entropy += -probability * Math.Log(probability, 2);
            }

            return entropy;
        }

        public static double CalculateGain(double rootEntropy, IList<double> subEntropies, IList<int> nodeSubsetsSizes, int attributesCount)
        {
            return rootEntropy -
                   subEntropies.Select((entropy, index) => (nodeSubsetsSizes[index] / (double)attributesCount) * entropy).Sum();
        }

        public static double CalculateGainRatio(IList<int> nodeSubsetsSizes, int dataCount)
        {
            var sum = 0.0;
            foreach (var setSize in nodeSubsetsSizes)
            {
                if (setSize == 0)
                    sum += -1;
                else
                    sum += -(setSize / (double)dataCount) * Math.Log(setSize / (double)dataCount, 2);
            }

            return sum;
        }
    }
}
