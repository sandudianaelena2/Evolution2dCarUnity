using Evolution.Specifications.Implementations.Specifications;
using Evolution.Specifications.Implementations.Specifications.Constraints;
using Evolution.Specifications.Interfaces;
using UnityEngine;

namespace Evolution.Specifications.Implementations.SpecificationOperations
{
    class WheelSpecificationsOperations : ISpecificationsOperations
    {
        public ISpecifications AddSpecifications(ISpecifications specifications1, ISpecifications specifications2)
        {
            var specs1 = specifications1 as WheelSpecifications;
            var specs2 = specifications2 as WheelSpecifications;
            var newSpecs = new WheelSpecifications(specs1.GetWheelNumber());
            var newScale = RepairScale(specs1.GetScale() + specs2.GetScale());
            newSpecs.SetScale(newScale);
            return newSpecs;
        }

        public ISpecifications ScalarMultiplySpecifications(ISpecifications specifications, float scalar)
        {
            var specs = specifications as WheelSpecifications;
            var newScale = RepairScale(specs.GetScale() * scalar);
            var newSpecs = new WheelSpecifications(specs.GetWheelNumber());
            newSpecs.SetScale(newScale);
            return newSpecs;
        }
        
        private float RepairScale(float scale)
        {
            if (scale > WheelConstraints.MaxScale)
            {
                scale = WheelConstraints.MaxScale;
            }

            if (scale < WheelConstraints.MinScale)
            {
                scale = WheelConstraints.MinScale;
            }

            return scale;
        }
        
    }
}
