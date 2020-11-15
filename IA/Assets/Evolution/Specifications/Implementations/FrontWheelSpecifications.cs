using Assets.Evolution.Specifications.Implementations.Constraints;
using Assets.Evolution.Specifications.Interfaces;
using UnityEngine;

namespace Assets.Evolution.Specifications.Implementations
{
    public class FrontWheelSpecifications:ISpecifications
    {
        private float _scale;
        public FrontWheelSpecifications()
        {
            _scale = Random.Range(WheelConstraints.MinScale, WheelConstraints.MaxScale);
        }

        public void ChangeGameObject(GameObject car)
        {
            var frontWheel = car.transform.GetChild(2).gameObject;
            var newScale =  new Vector3(_scale, _scale, frontWheel.transform.localScale.z);
            frontWheel.transform.localScale = newScale;
        }

        public void RegenerateValues()
        {
            _scale = Random.Range(WheelConstraints.MinScale, WheelConstraints.MaxScale);
        }

        public void RemoveObject(GameObject car)
        {
            GameObject.Destroy(car.transform.GetChild(2).gameObject);
        }
    }
}