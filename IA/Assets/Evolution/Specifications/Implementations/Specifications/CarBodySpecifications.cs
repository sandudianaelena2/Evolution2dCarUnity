using Evolution.Specifications.Implementations.Specifications.Constraints;
using Evolution.Specifications.Interfaces;
using Models;
using UnityEngine;

namespace Evolution.Specifications.Implementations.Specifications
{
    public class CarBodySpecifications : ISpecifications
    {
        private float _scaleX;
        private float _scaleY;
        private float _motorFrontSpeed;
        private float _motorBackSpeed;
        private float _motorFrontTorque;
        private float _motorBackTorque;
        private float _mass;
        
        public CarBodySpecifications()
        {
            RegenerateValues();
        }

        public void ChangeGameObject(GameObject car)
        {
            var carBody = car.transform.GetChild(0).gameObject;
            carBody.transform.localScale = new Vector3(_scaleX, _scaleY, carBody.transform.localScale.z);
            carBody.GetComponent<Rigidbody2D>().mass = _mass;
            var joints = carBody.GetComponents<WheelJoint2D>();
            JointMotor2D frontMotor2D = new JointMotor2D(), backMotor = new JointMotor2D();
            frontMotor2D.motorSpeed = - _motorFrontSpeed;
            frontMotor2D.maxMotorTorque = _motorFrontTorque;
            backMotor.motorSpeed = - _motorBackSpeed;
            backMotor.maxMotorTorque = _motorBackTorque;
            joints[0].motor = frontMotor2D;
            joints[1].motor = backMotor;
        }

        public void RegenerateValues()
        {
            GenerateNewScale();
            GenerateSpeeds();
            GenerateTorque();
        }

        private void GenerateSpeeds()
        {
            _motorFrontSpeed = Random.Range(CarBodyConstraints.MinMotorSpeed, CarBodyConstraints.MaxMotorSpeed);
            _motorBackSpeed = Random.Range(CarBodyConstraints.MinMotorSpeed, CarBodyConstraints.MaxMotorSpeed);
        }

        private void GenerateTorque()
        {
            _motorFrontTorque = Random.Range(CarBodyConstraints.MinMotorTorque, CarBodyConstraints.MaxMotorTorque);
            _motorBackTorque = Random.Range(CarBodyConstraints.MinMotorTorque, CarBodyConstraints.MaxMotorTorque);
        }

        private void GenerateNewScale()
        {
            _scaleX = Random.Range(CarBodyConstraints.ScaleXmin, CarBodyConstraints.ScaleXmax);
            _scaleY = Random.Range(CarBodyConstraints.ScaleYmin, CarBodyConstraints.ScaleYmax);
            ResetMass();
        }
        
        public System.Tuple<float, float> GetScale()
        {
            return new System.Tuple<float, float>(_scaleX, _scaleY);
        }

        public void SetScale(System.Tuple<float, float> newScale)
        {
            _scaleX = newScale.Item1;
            _scaleY = newScale.Item2;
            ResetMass();
        }

        private void ResetMass()
        {
            _mass = (CarBodyConstraints.StandardMass * _scaleX * _scaleY) / (CarBodyConstraints.ScaleXmax * CarBodyConstraints.ScaleYmax);
        }

        public float MotorFrontSpeed
        {
            get => _motorFrontSpeed;
            set => _motorFrontSpeed = value;
        }

        public float MotorFrontTorque
        {
            get => _motorFrontTorque;
            set => _motorFrontTorque = value;
        }

        public float MotorBackSpeed
        {
            get => _motorBackSpeed;
            set => _motorBackSpeed = value;
        }

        public float MotorBackTorque
        {
            get => _motorBackTorque;
            set => _motorBackTorque = value;
        }
    }
}