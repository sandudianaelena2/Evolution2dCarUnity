using Assets.Evolution.Specifications.Implementations.Constraints;
using Assets.Evolution.Specifications.Interfaces;
using UnityEngine;

namespace Assets.Evolution.Specifications.Implementations
{
    public class CarBodySpecifications:ISpecifications
    {
        private string _tag = "CarBody";
        private readonly GameObject _carBody;
        private float _scaleX;
        private float _scaleY;
        private Vector2 _frontWheelAnchorPos;
        private Vector2 _backWheelAnchorPos;
        private Vector2 _spriteMaxPoint;
        private Vector2 _spriteMinPoint;
        public CarBodySpecifications()
        {
            _carBody = Test.instance._car.transform.GetChild(0).gameObject;
            _frontWheelAnchorPos = new Vector2();
            _backWheelAnchorPos = new Vector2();
            RegenerateValues();
        }

        public void ChangeGameObject(GameObject car)
        {
            GameObject carBody = car.transform.GetChild(0).gameObject;
            carBody.transform.localScale = new Vector3(_scaleX, _scaleY, carBody.transform.localScale.z);
            var frontWheelJoint = carBody.GetComponents<WheelJoint2D>()[0];
            var backWheelJoint = carBody.GetComponents<WheelJoint2D>()[1];
            JointMotor2D jointMotor2D = new JointMotor2D();
            jointMotor2D.motorSpeed = CarBodyConstraints.MotorSpeed;
            jointMotor2D.maxMotorTorque = CarBodyConstraints.MotorTorque;
            frontWheelJoint.motor = jointMotor2D;
            backWheelJoint.motor = jointMotor2D;
            frontWheelJoint.anchor = _frontWheelAnchorPos;
            backWheelJoint.anchor = _backWheelAnchorPos;
        }

        public void RegenerateValues()
        {
            GenerateNewScale();
            RefreshSpriteBounds();
            GenerateNewAnchorPositions();
        }

        public void RemoveObject(GameObject car)
        {
            //Do nothing   
        }
        
        private void RefreshSpriteBounds()
        {
            _spriteMaxPoint = _carBody.GetComponent<SpriteRenderer>().sprite.bounds.max;
            _spriteMinPoint = _carBody.GetComponent<SpriteRenderer>().sprite.bounds.min;
        }

        private void GenerateNewScale()
        {
            _scaleX = Random.Range(CarBodyConstraints.ScaleXmin, CarBodyConstraints.ScaleXmax);
            _scaleY = Random.Range(CarBodyConstraints.ScaleYmin, CarBodyConstraints.ScaleYmax);
        }

        private void GenerateNewAnchorPositions()
        {
            float xMiddlePoint = _spriteMinPoint.x + (_spriteMaxPoint.x - _spriteMinPoint.x) / 2;
            _frontWheelAnchorPos.x = Random.Range(xMiddlePoint, _spriteMaxPoint.x);
            _backWheelAnchorPos.x = Random.Range(_spriteMinPoint.x, xMiddlePoint);
            _frontWheelAnchorPos.y = Random.Range(_spriteMinPoint.y, _spriteMaxPoint.y);
            _backWheelAnchorPos.y = Random.Range(_spriteMinPoint.y, _spriteMaxPoint.y);
        }
    }
}