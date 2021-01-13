using System.Collections.Generic;
using System.Linq;
using Evolution.Genes.Interfaces;
using Evolution.Specifications.Implementations.SpecificationOperations;
using Evolution.Specifications.Implementations.Specifications;
using Evolution.Specifications.Interfaces;
using UnityEngine;

namespace Evolution.Genes.Implementations
{
    public class WheelGene:IGene
    {
        public WheelGene(ISpecifications specifications)
        {
            Specifications = specifications;
        }

        public ISpecifications Specifications { get; private set; }

        public void Mutate(List<IGene> indivizi, float f)
        {
            var operations = new WheelSpecificationsOperations();
            var wheelGenes = indivizi.Select(gene => gene as WheelGene).ToList();
            var minusSpec =operations
                .ScalarMultiplySpecifications(wheelGenes[2].Specifications, -1);
            var firstAddSpec = operations.AddSpecifications(minusSpec, wheelGenes[1].Specifications);
            var fSpec = operations.ScalarMultiplySpecifications(firstAddSpec, f);
            Specifications = operations.AddSpecifications(wheelGenes[3].Specifications, fSpec);
            var temp = Specifications as WheelSpecifications;
            Debug.Log($"Wheel specs {temp.GetScale()}");
        }

        public ISpecifications GetSpecifications()
        {
            return Specifications;
        }
    }
}