using Assets.Evolution.Specifications.Implementations.Constraints;
using Assets.Evolution.Specifications.Interfaces;
using System;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Evolution.Specifications.Implementations
{
    class BoxSpecifications:ISpecifications
    {

        private float _scale;
        private float _mass;

        private readonly BoxConstraints.Boxes _boxNumber;

        public BoxSpecifications(BoxConstraints.Boxes boxNumber)
        {
            _boxNumber = boxNumber;
            RegenerateValues();         
        }

        public void ChangeGameObject(GameObject car)
        {
            if (car.transform.childCount > (int)_boxNumber)
            {
                var box = car.transform.GetChild((int)_boxNumber).gameObject;

                var newScale = new Vector3(_scale, _scale, box.transform.localScale.z);
                box.transform.localScale = newScale;

                box.GetComponent<Rigidbody2D>().mass = _mass;
            }
        }

        public void RegenerateValues()
        {
            _scale = UnityEngine.Random.Range(BoxConstraints.MinScale, BoxConstraints.MaxScale);
            _mass = BoxConstraints.MinScale * _scale / BoxConstraints.MinMass;
        }

        public void RemoveObject(GameObject car)
        {

        }

        Dictionary<string, Tuple<float, float>> ISpecifications.GetSpecifications()
        {
            throw new NotImplementedException();
        }
    }
}
