using Assets.Evolution.Genes.Interfaces;
using Assets.Evolution.Specifications.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Evolution.Genes.Implementations
{
    class BoxGene:IGene
    {

        public BoxGene(ISpecifications specifications, ISpecificationsOperations specificationsOperations)
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
