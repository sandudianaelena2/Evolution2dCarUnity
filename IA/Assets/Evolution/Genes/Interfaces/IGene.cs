using Assets.Evolution.Specifications.Interfaces;

namespace Assets.Evolution.Genes.Interfaces
{
    public interface IGene
    {
        void Mutate(double probability);
        ISpecifications GetSpecifications();
    }
}