using System.Linq;
using UnityEngine;

namespace Assets.Models
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

        public void CopyScheme(GameObject car)
        {
            this._car = Object.Instantiate(car);
        }

        public void SetMotor(JointMotor2D motor2D)
        {
            GameObject carBody = _car.transform.GetChild(0).gameObject;
            carBody.GetComponents<WheelJoint2D>()[0].motor = motor2D;
            carBody.GetComponents<WheelJoint2D>()[1].motor = motor2D;
        }

        public void SetActive(bool active)
        {
            _car.SetActive(active);
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

        public void MoveWheel(GameObject newWheel, float x, float y)
        {
            GameObject frontWheel = _car.transform.GetChild(2).gameObject;
            GameObject carBody = _car.transform.GetChild(0).gameObject;
            WheelJoint2D wheelJoint2D = carBody.GetComponents<WheelJoint2D>().First((
                joint2D => joint2D.connectedBody == frontWheel.GetComponent<Rigidbody2D>()));
            wheelJoint2D.anchor = new Vector2(wheelJoint2D.anchor.x + x, wheelJoint2D.anchor.y + y);
            newWheel.transform.position = frontWheel.transform.position;
            wheelJoint2D.connectedBody = newWheel.GetComponent<Rigidbody2D>();
            GameObject.Destroy(frontWheel);
            newWheel.transform.parent = _car.transform;
        }
    }
}