using Evolution.Specifications.Implementations.Specifications.Constraints;
using Evolution.Specifications.Interfaces;
using UnityEngine;

namespace Evolution.Specifications.Implementations.Specifications
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

        public BoxSpecifications(BoxConstraints.Boxes boxNumber, float scale)
        {
            _scale = scale;
            ResetMass();
            _boxNumber = boxNumber;
        }

        public void ChangeGameObject(GameObject car)
        {
            var box = car.transform.GetChild((int)_boxNumber).gameObject;

            var newScale = new Vector3(_scale, _scale, box.transform.localScale.z);
            box.transform.localScale = newScale;

            box.GetComponent<Rigidbody2D>().mass = _mass;
        }

        public void RegenerateValues()
        {
            _scale = UnityEngine.Random.Range(BoxConstraints.MinScale, BoxConstraints.MaxScale);
            ResetMass();
        }

        private void ResetMass()
        {
            _mass = (_scale * BoxConstraints.StandardMass) / BoxConstraints.MaxScale;
        }

        public BoxConstraints.Boxes GetBoxNumber()
        {
            return _boxNumber;
        }

        public float GetScale()
        {
            return _scale;
        }

        public float GetMass()
        {
            return _mass;
        }
    }
}
