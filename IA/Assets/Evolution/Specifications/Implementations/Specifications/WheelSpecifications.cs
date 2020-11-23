using Assets.Evolution.Specifications.Implementations.Constraints;
using Assets.Evolution.Specifications.Interfaces;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Evolution.Specifications.Implementations
{
    public class WheelSpecifications:ISpecifications
    {
        private float _scale;
        private readonly WheelConstraints.Wheels _wheelNumber;
        public WheelSpecifications(WheelConstraints.Wheels wheelNumber)
        {
            _scale = UnityEngine.Random.Range(WheelConstraints.MinScale, WheelConstraints.MaxScale);
            _wheelNumber = wheelNumber;
        }

        public void ChangeGameObject(GameObject car)
        {
            var wheel = car.transform.GetChild((int)_wheelNumber).gameObject;
            var newScale =  new Vector3(_scale, _scale, wheel.transform.localScale.z);
            wheel.transform.localScale = newScale;
        }

        public void RegenerateValues()
        {
            _scale = UnityEngine.Random.Range(WheelConstraints.MinScale, WheelConstraints.MaxScale);
        }

        public void RemoveObject(GameObject car)
        { 

        }
    }
}