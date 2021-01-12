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

            Tuple<float, float> newScale = GetScaleSum(carBodySpecifications1, carBodySpecifications2);
            Tuple<Vector2, Vector2> newWheelAnchorPosition = GetWheelAnchorPosSum(carBodySpecifications1, carBodySpecifications2);
            Tuple<Vector2, Vector2> newBoxAnchorPosition = GetBoxAnchorPosSum(carBodySpecifications1, carBodySpecifications2);

            RepairScale(newScale);
            result.SetScale(newScale);

            result.SetWheelAnchorPosition(newWheelAnchorPosition);

            result.SetBoxAnchorPosition(newBoxAnchorPosition);

            return result;
        }

      
        public ISpecifications ScalarMultiplySpecifications(ISpecifications specifications, float scalar)
        {

            CarBodySpecifications result = (CarBodySpecifications)specifications;

            Tuple<float, float> newScale = ScalarMultiplyScale(result, scalar);
            Tuple<Vector2, Vector2> newWheelAnchorPosition = ScalarMultiplyWheelAnchorPos(result, scalar);
            Tuple<Vector2, Vector2> newBoxAnchorPosition = ScalarMultiplyBoxAnchorPos(result, scalar);

            result.SetScale(newScale);
            result.SetWheelAnchorPosition(newWheelAnchorPosition);
            result.SetBoxAnchorPosition(newBoxAnchorPosition);

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

        public Tuple<Vector2, Vector2> ScalarMultiplyWheelAnchorPos(CarBodySpecifications carBodySpecifications, float scalar)
        {
            var backWheelAnchorPos = carBodySpecifications.GetWheelAnchorPosition().Item1 * scalar;
            var frontWheelPos = carBodySpecifications.GetWheelAnchorPosition().Item2 * scalar;

            Tuple<Vector2, Vector2> newAnchorPosition = new Tuple<Vector2, Vector2>(backWheelAnchorPos, frontWheelPos);

            return newAnchorPosition;
        }

        public Tuple<Vector2, Vector2> ScalarMultiplyBoxAnchorPos(CarBodySpecifications carBodySpecifications, float scalar)
        {
            var backWheelAnchorPos = carBodySpecifications.GetBoxAnchorPosition().Item1 * scalar;
            var frontWheelPos = carBodySpecifications.GetBoxAnchorPosition().Item2 * scalar;

            Tuple<Vector2, Vector2> newAnchorPosition = new Tuple<Vector2, Vector2>(backWheelAnchorPos, frontWheelPos);

            return newAnchorPosition;
        }

        public Tuple<float, float> GetScaleSum(CarBodySpecifications carBodySpecifications1, CarBodySpecifications carBodySpecifications2)
        {
            var newScaleX = carBodySpecifications1.GetScale().Item1 + carBodySpecifications2.GetScale().Item1;
            var newScaleY = carBodySpecifications1.GetScale().Item2 + carBodySpecifications2.GetScale().Item2;

            Tuple<float, float> newScale = new Tuple<float, float>(newScaleX, newScaleY);

            return newScale;
        }

        public Tuple<Vector2, Vector2> GetWheelAnchorPosSum(CarBodySpecifications carBodySpecifications1, CarBodySpecifications carBodySpecifications2)
        {
            var backWheelAnchorPos = carBodySpecifications1.GetWheelAnchorPosition().Item1 + carBodySpecifications1.GetWheelAnchorPosition().Item1;
            var frontWheelPos = carBodySpecifications1.GetWheelAnchorPosition().Item2 + carBodySpecifications2.GetWheelAnchorPosition().Item2;

            Tuple<Vector2, Vector2> newAnchorPosition = new Tuple<Vector2, Vector2>(backWheelAnchorPos, frontWheelPos);

            return newAnchorPosition;
        }

        public Tuple<Vector2, Vector2> GetBoxAnchorPosSum(CarBodySpecifications carBodySpecifications1, CarBodySpecifications carBodySpecifications2)
        {
            var backWheelAnchorPos = carBodySpecifications1.GetBoxAnchorPosition().Item1 + carBodySpecifications1.GetBoxAnchorPosition().Item1;
            var frontWheelPos = carBodySpecifications1.GetBoxAnchorPosition().Item2 + carBodySpecifications2.GetBoxAnchorPosition().Item2;

            Tuple<Vector2, Vector2> newAnchorPosition = new Tuple<Vector2, Vector2>(backWheelAnchorPos, frontWheelPos);

            return newAnchorPosition;
        }


    }
}
