using System;
using Evolution.Specifications.Implementations.Specifications;
using Evolution.Specifications.Implementations.Specifications.Constraints;
using Evolution.Specifications.Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

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
            var frontSpeed =
                RepairSpeed(carBodySpecifications1.MotorFrontSpeed + carBodySpecifications2.MotorFrontSpeed);
            var backSpeed = RepairSpeed(carBodySpecifications1.MotorBackSpeed + carBodySpecifications2.MotorBackSpeed);
            var frontTorque =
                RepairTorque(carBodySpecifications1.MotorFrontTorque + carBodySpecifications2.MotorFrontTorque);
            var backTorque =
                RepairTorque(carBodySpecifications1.MotorBackTorque + carBodySpecifications2.MotorBackTorque);
            result.MotorBackSpeed = backSpeed;
            result.MotorBackTorque = backTorque;
            result.MotorFrontSpeed = frontSpeed;
            result.MotorFrontTorque = frontTorque;
            return result;
        }

      
        public ISpecifications ScalarMultiplySpecifications(ISpecifications specifications, float scalar)
        {
            CarBodySpecifications specs = (CarBodySpecifications)specifications;
            var result = new CarBodySpecifications();
            Tuple<float, float> newScale = RepairScale(ScalarMultiplyScale(result, scalar));
            result.SetScale(newScale);
            var frontSpeed = RepairSpeed(scalar * specs.MotorFrontSpeed);
            var backSpeed = RepairSpeed(scalar * specs.MotorBackSpeed);
            var frontTorque = RepairTorque(scalar * specs.MotorFrontTorque);
            var backTorque = RepairTorque(scalar * specs.MotorBackTorque);
            result.MotorBackSpeed = backSpeed;
            result.MotorBackTorque = backTorque;
            result.MotorFrontSpeed = frontSpeed;
            result.MotorFrontTorque = frontTorque;
            return result;
        }

        public Tuple<float, float> RepairScale(Tuple<float, float> scale)
        {
            var scaleX = scale.Item1;
            var scaleY = scale.Item2;

            if (scaleX > CarBodyConstraints.ScaleXmax || scaleX < CarBodyConstraints.ScaleXmin)
            {
                scaleX = Random.Range(CarBodyConstraints.ScaleXmin, CarBodyConstraints.ScaleXmax);
            }

            if (scaleY > CarBodyConstraints.ScaleYmax || scaleY < CarBodyConstraints.ScaleYmin)
            {
                scaleY = Random.Range(CarBodyConstraints.ScaleYmin, CarBodyConstraints.ScaleYmax);;
            }

            scale = new Tuple<float, float>(scaleX, scaleY);

            return scale;
        }

        private float RepairSpeed(float speed)
        {
            if (speed < CarBodyConstraints.MinMotorSpeed || speed > CarBodyConstraints.MaxMotorSpeed)
            {
                speed = Random.Range(CarBodyConstraints.MinMotorSpeed, CarBodyConstraints.MaxMotorSpeed);
            }
            return speed;
        }

        private float RepairTorque(float torque)
        {
            if (torque < CarBodyConstraints.MinMotorTorque || torque > CarBodyConstraints.MaxMotorTorque)
            {
                torque = Random.Range(CarBodyConstraints.MinMotorTorque, CarBodyConstraints.MaxMotorTorque);
            }

            return torque;
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
