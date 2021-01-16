using Evolution.Specifications.Implementations.Specifications;
using Evolution.Specifications.Implementations.Specifications.Constraints;
using Evolution.Specifications.Interfaces;
using UnityEngine;

namespace Evolution.Specifications.Implementations.SpecificationOperations
{
    class BoxSpecificationsOperations : ISpecificationsOperations
    {
        public ISpecifications AddSpecifications(ISpecifications specifications1, ISpecifications specifications2)
        {
            var specs1 = specifications1 as BoxSpecifications;
            var specs2 = specifications2 as BoxSpecifications;
            var scale = RepairScale(specs1.GetScale() + specs2.GetScale());
            return new BoxSpecifications(specs1.GetBoxNumber(), scale);
        }

        public ISpecifications ScalarMultiplySpecifications(ISpecifications specifications, float scalar)
        {
            var specs = specifications as BoxSpecifications;
            var scale = RepairScale(specs.GetScale() * scalar);
            return new BoxSpecifications(specs.GetBoxNumber(), scale);
        }
        
        private float RepairScale(float scale)
        {
            if (scale > BoxConstraints.MaxScale || scale < BoxConstraints.MinScale)
                scale = Random.Range(BoxConstraints.MinScale, BoxConstraints.MaxScale);
            return scale;
        }
        
    }
}
