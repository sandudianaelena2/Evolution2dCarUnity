using Assets.Evolution.Genes.Interfaces;
using Assets.Evolution.Specifications.Interfaces;

namespace Assets.Evolution.Genes.Implementations
{
    public class CarBodyGene: IGene
    {
        public CarBodyGene(ISpecifications specifications, ISpecificationsOperations specificationsOperations)
        {
            Specifications = specifications;
            SpecificationsOperations = specificationsOperations;
        }

        public ISpecifications Specifications { get; }
        public ISpecificationsOperations SpecificationsOperations { get; }

        public void Mutate(double probability)
        {
            throw new System.NotImplementedException();
        }
        
        public ISpecifications GetSpecifications()
        {
            SpecificationsOperations.AddSpecifications(Specifications,Specifications);
            return Specifications;
        }
        
    }
}