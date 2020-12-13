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
                new CarBodyGene(new CarBodySpecifications(), new CarBodySpecificationsOperations()),
                new WheelGene(new WheelSpecifications(WheelConstraints.Wheels.BackWheel), new WheelSpecificationsOperations()),
                new WheelGene(new WheelSpecifications(WheelConstraints.Wheels.FrontWheel), new WheelSpecificationsOperations()),
                new BoxGene(new BoxSpecifications(BoxConstraints.Boxes.LeftBox), new BoxSpecificationsOperations()),
                new BoxGene(new BoxSpecifications(BoxConstraints.Boxes.RightBox), new BoxSpecificationsOperations())
            };
        }

        public List<IGene> Genes { get; set; }
        public int Fitness { get; set; }
    }
}