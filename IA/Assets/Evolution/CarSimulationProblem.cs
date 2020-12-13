using Models;
using UnityEngine;

namespace Evolution
{
    public class CarSimulationProblem
    {
        private static Car ConstructCar(Chromosome chromosome)
        {
            Debug.Log("Construiesc o noua masina!");
            var genes = chromosome.Genes;
            GameObject newCar = GameObject.Instantiate(Test.instance.carPrefab);
            foreach (var gene in genes)
            {
                gene.GetSpecifications().ChangeGameObject(newCar);
            }
            return new Car(newCar);
        }

        public static void CalculateFitnessValueForChromosome(Chromosome chromosome)
        {
            Debug.Log("Inceput Calcul functie fitness!");
            var newCar = ConstructCar(chromosome);
            Debug.Log("Generate new car!");
            Test.instance._car = newCar.GetCar();
            Test.instance.isRunning = true;
            while (Test.instance.isRunning) { }
        
            chromosome.Fitness = Score.ScoreValue;
            GameObject.Destroy(newCar.GetCar());
        }
    }
}