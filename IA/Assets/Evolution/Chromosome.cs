using System.Collections.Generic;
using Assets.Evolution.Genes.Implementations;
using Assets.Evolution.Genes.Interfaces;
using Assets.Evolution.Specifications.Implementations;
using Assets.Evolution.Specifications.Implementations.Constraints;
using Assets.Evolution.Specifications.Implementations.SpecificationOperations;

namespace Assets.Evolution
{
    public class Chromosome
    {
        public Chromosome()
        {
            Genes = new List<IGene>
            {
                new CarBodyGene(new CarBodySpecifications(), new CarBodySpecificationsOperations()),
                new WheelGene(new WheelSpecifications(WheelConstraints.Wheels.BackWheel), new WheelSpecificationsOperations()),
                new WheelGene(new WheelSpecifications(WheelConstraints.Wheels.FrontWheel), new WheelSpecificationsOperations()),
                new BoxGene(new BoxSpecifications(BoxConstraints.Boxes.LeftBox), new BoxSpecificationsOperations()),
                new BoxGene(new BoxSpecifications(BoxConstraints.Boxes.RightBox), new BoxSpecificationsOperations())
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