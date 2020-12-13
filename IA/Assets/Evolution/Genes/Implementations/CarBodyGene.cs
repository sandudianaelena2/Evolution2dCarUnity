using System.Collections.Generic;
using System.Linq;
using Evolution.Genes.Interfaces;
using Evolution.Specifications.Implementations.SpecificationOperations;
using Evolution.Specifications.Interfaces;

namespace Evolution.Genes.Implementations
{
    public class CarBodyGene: IGene
    {
        public CarBodyGene(ISpecifications specifications)
        {
            Specifications = specifications;
        }

        public ISpecifications Specifications { get; private set; }

        public void Mutate(List<IGene> indivizi, float f)
        {
            var operations = new CarBodySpecificationsOperations();
            var wheelGenes = indivizi.Select(gene => gene as CarBodyGene).ToList();
            var minusSpec =operations
                .ScalarMultiplySpecifications(wheelGenes[2].Specifications, -1);
            var firstAddSpec = operations.AddSpecifications(minusSpec, wheelGenes[1].Specifications);
            var fSpec = operations.ScalarMultiplySpecifications(firstAddSpec, f);
            Specifications = operations.AddSpecifications(wheelGenes[3].Specifications, fSpec);
        }
        
        public ISpecifications GetSpecifications()
        {
            return Specifications;
        }
        
    }
}