using System.Collections.Generic;
using Evolution.Specifications.Interfaces;

namespace Evolution.Genes.Interfaces
{
    public interface IGene
    {
        void Mutate(List<IGene> indivizi, float f);
        ISpecifications GetSpecifications();
    }
}