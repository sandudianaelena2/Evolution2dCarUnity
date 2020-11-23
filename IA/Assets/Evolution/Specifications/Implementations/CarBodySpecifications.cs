using Assets.Evolution.Specifications.Implementations.Constraints;
using Assets.Evolution.Specifications.Interfaces;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Evolution.Specifications.Implementations
{
    public class CarBodySpecifications:ISpecifications
    {
        private readonly GameObject _carBody;

        private float _scaleX;
        private float _scaleY;

        private Vector2 _frontWheelAnchorPos;
        private Vector2 _backWheelAnchorPos;

        private Vector2 _spriteMaxPoint;
        private Vector2 _spriteMinPoint;

        private Vector2 _leftBoxAnchorPos;
        private Vector2 _rightBoxAnchorPos;

        private int childCount;
        public Dictionary<string, Tuple<float,float>> GetSpecifications()
        {
            Dictionary<string, Tuple<float, float>> specifications = new Dictionary<string,Tuple<float, float>>();

            Tuple<float, float> scale = new Tuple<float, float>(_scaleX, _scaleY);
            specifications.Add("scale", scale);


            return specifications;
        }

        public CarBodySpecifications()
        {
            _carBody = Test.instance._car.transform.GetChild(0).gameObject;
            childCount = _carBody.transform.childCount;

            _frontWheelAnchorPos = new Vector2();
            _backWheelAnchorPos = new Vector2();

            _leftBoxAnchorPos = new Vector2();
            _rightBoxAnchorPos = new Vector2();

            RegenerateValues();
        }

        public void ChangeGameObject(GameObject car)
        {
            _carBody.transform.localScale = new Vector3(_scaleX, _scaleY, _carBody.transform.localScale.z);

            var frontWheelJoint = _carBody.GetComponents<WheelJoint2D>()[0];
            var backWheelJoint = _carBody.GetComponents<WheelJoint2D>()[1];

            JointMotor2D jointMotor2D = new JointMotor2D();
            jointMotor2D.motorSpeed = CarBodyConstraints.MotorSpeed;
            jointMotor2D.maxMotorTorque = CarBodyConstraints.MotorTorque;

            frontWheelJoint.motor = jointMotor2D;
            backWheelJoint.motor = jointMotor2D;

            frontWheelJoint.anchor = _frontWheelAnchorPos;
            backWheelJoint.anchor = _backWheelAnchorPos;

            WheelJoint2D leftBoxJoint;
            WheelJoint2D rightBoxJoint;

            if(childCount == 4)
            {
                leftBoxJoint = _carBody.GetComponents<WheelJoint2D>()[0];
                leftBoxJoint.connectedAnchor = new Vector2(0f, 0f);
            }
            else if(childCount == 5)
            {
                leftBoxJoint = _carBody.GetComponents<WheelJoint2D>()[0];
                rightBoxJoint = _carBody.GetComponents<WheelJoint2D>()[1];

                leftBoxJoint.connectedAnchor = new Vector2(0f, 0f);
                rightBoxJoint.connectedAnchor = new Vector2(0f, 0f);
            }
        }

        public void RegenerateValues()
        {
            GenerateNewScale();
            RefreshSpriteBounds();
            GenerateNewAnchorPositionsForWheels();
            GenerateNewAnchorPositionsForBoxes();
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

        private void GenerateNewAnchorPositionsForWheels()
        {
            float xMiddlePoint = _spriteMinPoint.x + (_spriteMaxPoint.x - _spriteMinPoint.x) / 2;

            _frontWheelAnchorPos.x = Random.Range(xMiddlePoint, _spriteMaxPoint.x);
            _backWheelAnchorPos.x = Random.Range(_spriteMinPoint.x, xMiddlePoint);

            _frontWheelAnchorPos.y = Random.Range(_spriteMinPoint.y, _spriteMaxPoint.y);
            _backWheelAnchorPos.y = Random.Range(_spriteMinPoint.y, _spriteMaxPoint.y);
        }

        private void GenerateNewAnchorPositionsForBoxes()
        {
            float xMiddlePoint = _spriteMinPoint.x + (_spriteMaxPoint.x - _spriteMinPoint.x) / 2;

            if (childCount == 4)
            {
                _leftBoxAnchorPos.x = Random.Range(_spriteMinPoint.x + BoxConstraints.MaxScale, xMiddlePoint);
            }
            else if(childCount == 5)
            {
                _leftBoxAnchorPos.x = Random.Range(_spriteMinPoint.x + BoxConstraints.MaxScale, xMiddlePoint);

                _rightBoxAnchorPos.x = Random.Range(xMiddlePoint, _spriteMaxPoint.x-BoxConstraints.MinScale);
            }
        }

     
    }
}