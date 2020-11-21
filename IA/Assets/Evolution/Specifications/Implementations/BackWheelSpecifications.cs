using Assets.Evolution.Specifications.Implementations.Constraints;
using Assets.Evolution.Specifications.Interfaces;
using UnityEngine;


namespace Assets.Evolution.Specifications.Implementations
{
    public class BackWheelSpecifications:ISpecifications
    {
        private float _scale;
        public BackWheelSpecifications()
        {
            _scale = Random.Range(WheelConstraints.MinScale, WheelConstraints.MaxScale);
        }

        public void ChangeGameObject(GameObject car)
        {
            var backWheel = car.transform.GetChild(1).gameObject;
            var newScale =  new Vector3(_scale, _scale, backWheel.transform.localScale.z);
            backWheel.transform.localScale = newScale;
        }

        public void RegenerateValues()
        {
            _scale = Random.Range(WheelConstraints.MinScale, WheelConstraints.MaxScale);
        }

        public void RemoveObject(GameObject car)
        {
            GameObject.Destroy(car.transform.GetChild(1).gameObject);
        }
    }
}