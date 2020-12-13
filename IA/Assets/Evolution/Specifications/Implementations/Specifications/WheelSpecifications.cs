using Evolution.Specifications.Implementations.Specifications.Constraints;
using Evolution.Specifications.Interfaces;
using UnityEngine;

namespace Evolution.Specifications.Implementations.Specifications
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

        public WheelConstraints.Wheels GetWheelNumber()
        {
            return _wheelNumber;
        }

        public float GetScale()
        {
            return _scale;
        }

        public void SetScale(float scale)
        {
            _scale = scale;
        }
    }
}