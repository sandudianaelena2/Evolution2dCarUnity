using System;
using Assets.Evolution.Genes.Interfaces;
using Assets.Evolution.Specifications.Interfaces;

namespace Assets.Evolution.Genes.Implementations
{
    public class WheelGene:IGene
    {
        public WheelGene(ISpecifications specifications, ISpecificationsOperations specificationsOperations)
        {
            Specifications = specifications;
            SpecificationsOperations = specificationsOperations;
        }

        public ISpecifications Specifications { get; }
        public ISpecificationsOperations SpecificationsOperations { get; }

        public void Mutate(double probability)
        {
            Specifications.RegenerateValues();
        }

        public ISpecifications GetSpecifications()
        {
            return Specifications;
        }
    }
}