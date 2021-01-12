using UnityEngine;

namespace Models
{
    public class Car
    {
        private GameObject _car;

        public Car()
        {
            
        }
        
        public Car(GameObject car)
        {
            _car = car;
        }
        

        public void SetActive(bool active)
        {
            _car.SetActive(active);
        }

        public void Destroy()
        {
            Object.Destroy(_car);
        }

        public void Stop()
        {
            GameObject carBody = _car.transform.GetChild(0).gameObject;
            JointMotor2D stopMotor = new JointMotor2D();
            stopMotor.motorSpeed = 0;
            stopMotor.maxMotorTorque = 0;
            carBody.GetComponents<WheelJoint2D>()[0].motor = stopMotor;
            carBody.GetComponents<WheelJoint2D>()[1].motor = stopMotor;
        }

        public GameObject GetCarBody()
        {
            return _car.transform.GetChild(0).gameObject;
        }

        public GameObject GetCar()
        {
            return _car;
        }

        
        
    }
}