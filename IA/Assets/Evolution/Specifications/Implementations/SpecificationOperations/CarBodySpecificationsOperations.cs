using System;
using Evolution.Specifications.Implementations.Specifications;
using Evolution.Specifications.Implementations.Specifications.Constraints;
using Evolution.Specifications.Interfaces;
using UnityEngine;

namespace Evolution.Specifications.Implementations.SpecificationOperations
{
    class CarBodySpecificationsOperations : ISpecificationsOperations
    {
        public ISpecifications AddSpecifications(ISpecifications specifications1, ISpecifications specifications2)
        {
            CarBodySpecifications carBodySpecifications1 = (CarBodySpecifications)specifications1;
            CarBodySpecifications carBodySpecifications2 = (CarBodySpecifications)specifications2;
            CarBodySpecifications result = new CarBodySpecifications();
            Tuple<float, float> newScale = RepairScale(GetScaleSum(carBodySpecifications1, carBodySpecifications2));
            result.SetScale(newScale);
            return result;
        }

      
        public ISpecifications ScalarMultiplySpecifications(ISpecifications specifications, float scalar)
        {

            CarBodySpecifications result = (CarBodySpecifications)specifications;
            Tuple<float, float> newScale = RepairScale(ScalarMultiplyScale(result, scalar));
            result.SetScale(newScale);
            return result;
        }

        public Tuple<float, float> RepairScale(Tuple<float, float> scale)
        {
            var scaleX = scale.Item1;
            var scaleY = scale.Item2;

            if (scaleX > CarBodyConstraints.ScaleXmax)
            {
                scaleX = CarBodyConstraints.ScaleXmax;
            }
            else if (scaleX < CarBodyConstraints.ScaleXmin)
            {
                scaleX = CarBodyConstraints.ScaleXmin;
            }

            if (scaleY > CarBodyConstraints.ScaleYmax)
            {
                scaleY = CarBodyConstraints.ScaleYmax;
            }
            else if (scaleY < CarBodyConstraints.ScaleYmin)
            {
                scaleY = CarBodyConstraints.ScaleYmin;
            }

            scale = new Tuple<float, float>(scaleX, scaleY);

            return scale;
        }

        public Tuple<float, float> ScalarMultiplyScale(CarBodySpecifications carBodySpecifications, float scalar)
        {
            var newScaleX = carBodySpecifications.GetScale().Item1 * scalar;
            var newScaleY = carBodySpecifications.GetScale().Item2 * scalar;

            Tuple<float, float> newScale = new Tuple<float, float>(newScaleX, newScaleY);

            return newScale;
        }
        

        public Tuple<float, float> GetScaleSum(CarBodySpecifications carBodySpecifications1, CarBodySpecifications carBodySpecifications2)
        {
            var newScaleX = carBodySpecifications1.GetScale().Item1 + carBodySpecifications2.GetScale().Item1;
            var newScaleY = carBodySpecifications1.GetScale().Item2 + carBodySpecifications2.GetScale().Item2;

            Tuple<float, float> newScale = new Tuple<float, float>(newScaleX, newScaleY);

            return newScale;
        }
    }
}
