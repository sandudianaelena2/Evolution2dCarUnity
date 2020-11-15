using Assets.Evolution.Genes.Interfaces;
using Assets.Evolution.Specifications.Interfaces;

namespace Assets.Evolution.Genes.Implementations
{
    public class CarBodyGene: IGene
    {
        public CarBodyGene(ISpecifications specifications)
        {
            Specifications = specifications;
        }

        public ISpecifications Specifications { get; }

        public void Mutate(double probability)
        {
            throw new System.NotImplementedException();
            Specifications.RegenerateValues();
        }

        public bool IsActive()
        {
            return true;
        }
        
        public ISpecifications GetSpecifications()
        {
            return Specifications;
        }
        
    }
}