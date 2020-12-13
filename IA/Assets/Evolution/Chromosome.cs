using System.Collections.Generic;
using Evolution.Genes.Implementations;
using Evolution.Genes.Interfaces;
using Evolution.Specifications.Implementations.SpecificationOperations;
using Evolution.Specifications.Implementations.Specifications;
using Evolution.Specifications.Implementations.Specifications.Constraints;

namespace Evolution
{
    public class Chromosome
    {
        public Chromosome()
        {
            Genes = new List<IGene>
            {
                new CarBodyGene(new CarBodySpecifications()),
                new WheelGene(new WheelSpecifications(WheelConstraints.Wheels.BackWheel)),
                new WheelGene(new WheelSpecifications(WheelConstraints.Wheels.FrontWheel)),
                new BoxGene(new BoxSpecifications(BoxConstraints.Boxes.LeftBox)),
                new BoxGene(new BoxSpecifications(BoxConstraints.Boxes.RightBox))
            };
        }

        public List<IGene> Genes { get; set; }
        public int score { get; set; }

        public Chromosome Cross(Chromosome other)
        {
            //TODO:Implement a method of crossing chromosomes
            return new Chromosome();
        }
    }
}