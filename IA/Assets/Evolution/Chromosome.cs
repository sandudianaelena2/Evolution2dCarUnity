using System.Collections.Generic;
using Assets.Evolution.Genes.Implementations;
using Assets.Evolution.Genes.Interfaces;
using Assets.Evolution.Specifications.Implementations;

namespace Assets.Evolution
{
    public class Chromosome
    {
        public Chromosome()
        {
            Genes = new List<IGene>
            {
                new CarBodyGene(new CarBodySpecifications()),
                new FrontWheelGene(new FrontWheelSpecifications()),
                new BackWheelGene(new BackWheelSpecifications())
            };
        }

        public List<IGene> Genes { get; set; }

        public Chromosome cross(Chromosome other)
        {
            //TODO:Implement a method of crossing chromosomes
            return new Chromosome();
        }
    }
}