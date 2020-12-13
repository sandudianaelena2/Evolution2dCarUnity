using System.Collections.Generic;
using UnityEngine;

namespace Evolution
{
    public class EvolutionaryAlgorithm : MonoBehaviour
    {
        private static List<Chromosome> _population = new List<Chromosome>();
        
        public static Chromosome Solve(int populationSize, int maxGeneration, double crossoverRate)
        {
            GenerateChromosomes(populationSize);
            return new Chromosome();
        }
        
        private static void GenerateChromosomes(int noOfChromosomes)
        {
            for (var i = 0; i < noOfChromosomes; i++)
            {
                Debug.Log("Generez chromosome-ul "+i.ToString());
                var newChromosome = new Chromosome();
                _population.Add(newChromosome);
                CarSimulationProblem.CalculateFitnessValueForChromosome(newChromosome);
            }
        }
    }
}