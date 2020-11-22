using System.Collections.Generic;
using Assets.Evolution.Genes.Implementations;
using Assets.Evolution.Genes.Interfaces;
using Assets.Evolution.Specifications.Implementations;
using Assets.Evolution.Specifications.Implementations.Constraints;

namespace Assets.Evolution
{
    public class Chromosome
    {
        public Chromosome()
        {
            Genes = new List<IGene>
            {
                new CarBodyGene(new CarBodySpecifications()),
                new BackWheelGene(new WheelSpecifications(WheelConstraints.Wheels.BackWheel)),
                new FrontWheelGene(new WheelSpecifications(WheelConstraints.Wheels.FrontWheel))
            };
        }

        public List<IGene> Genes { get; set; }

        public Chromosome Cross(Chromosome other)
        {
            //TODO:Implement a method of crossing chromosomes
            return new Chromosome();
        }
    }
}