using Evolution.Specifications.Interfaces;

namespace Evolution.Genes.Interfaces
{
    public interface IGene
    {
        void Mutate(double probability);
        ISpecifications GetSpecifications();
    }
}