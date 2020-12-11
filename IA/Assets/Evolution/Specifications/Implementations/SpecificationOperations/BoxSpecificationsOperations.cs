using Assets.Evolution.Specifications.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Evolution.Specifications.Implementations.Constraints;

namespace Assets.Evolution.Specifications.Implementations.SpecificationOperations
{
    class BoxSpecificationsOperations : ISpecificationsOperations
    {
        public ISpecifications AddSpecifications(ISpecifications specifications1, ISpecifications specifications2)
        {
            var specs1 = specifications1 as BoxSpecifications;
            var specs2 = specifications2 as BoxSpecifications;
            var mass = RepairMass(specs1.GetMass() + specs2.GetMass());
            var scale = RepairScale(specs1.GetScale() + specs2.GetScale());
            return new BoxSpecifications(specs1.GetBoxNumber(), scale, mass);
        }

        public ISpecifications ScalarMultiplySpecifications(ISpecifications specifications, float scalar)
        {
            var specs = specifications as BoxSpecifications;
            var mass = RepairMass(specs.GetMass() * scalar);
            var scale = RepairScale(specs.GetScale() * scalar);
            return new BoxSpecifications(specs.GetBoxNumber(), scale, mass);
        }
        
        private float RepairScale(float scale)
        {
            if (scale > BoxConstraints.MaxScale)
                scale = BoxConstraints.MaxScale;
            
            if (scale < BoxConstraints.MinScale)
                scale = BoxConstraints.MinScale;
            return scale;
        }

        private float RepairMass(float mass)
        {
            if (mass > BoxConstraints.MaxMass)
                mass = BoxConstraints.MaxMass;
            
            if (mass < BoxConstraints.MinMass)
                mass = BoxConstraints.MinMass;
            return mass;
        }
    }
}
