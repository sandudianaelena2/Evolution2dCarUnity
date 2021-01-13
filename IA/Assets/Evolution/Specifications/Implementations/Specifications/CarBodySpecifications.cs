using Evolution.Specifications.Implementations.Specifications.Constraints;
using Evolution.Specifications.Interfaces;
using UnityEngine;

namespace Evolution.Specifications.Implementations.Specifications
{
    public class CarBodySpecifications : ISpecifications
    {
        private float _scaleX;
        private float _scaleY;
        
        public System.Tuple<float, float> GetScale()
        {
            return new System.Tuple<float, float>(_scaleX, _scaleY);
        }

        public void SetScale(System.Tuple<float, float> newScale)
        {
            _scaleX = newScale.Item1;
            _scaleY = newScale.Item2;
        }
        public CarBodySpecifications()
        {
            RegenerateValues();
        }

        public void ChangeGameObject(GameObject car)
        {
            var carBody = car.transform.GetChild(0).gameObject;
            carBody.transform.localScale = new Vector3(_scaleX, _scaleY, carBody.transform.localScale.z);
        }

        public void RegenerateValues()
        {
            GenerateNewScale();
        }
        

        private void GenerateNewScale()
        {
            _scaleX = Random.Range(CarBodyConstraints.ScaleXmin, CarBodyConstraints.ScaleXmax);
            _scaleY = Random.Range(CarBodyConstraints.ScaleYmin, CarBodyConstraints.ScaleYmax);
        }
    }
}