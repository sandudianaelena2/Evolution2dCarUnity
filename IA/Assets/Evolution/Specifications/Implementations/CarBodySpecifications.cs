using Assets.Evolution.Specifications.Implementations.Constraints;
using Assets.Evolution.Specifications.Interfaces;
using UnityEngine;

namespace Assets.Evolution.Specifications.Implementations
{
    public class CarBodySpecifications:ISpecifications
    {
        private string _tag = "CarBody";
        private GameObject _carBody;
        private float _scaleX;
        private float _scaleY;
        private Vector2 _frontWheelPos;
        private Vector2 _backWheelPos;
        public CarBodySpecifications()
        {
            _carBody = GameObject.FindGameObjectWithTag(_tag);
            _scaleX = Random.Range(CarBodyConstraints.ScaleXmin, CarBodyConstraints.ScaleXmax);
            _scaleY = Random.Range(CarBodyConstraints.ScaleYmin, CarBodyConstraints.ScaleYmax);
            var xMin = _carBody.GetComponent<SpriteRenderer>().sprite.bounds.min.x;
            var xMax = _carBody.GetComponent<SpriteRenderer>().sprite.bounds.max.x;
            var yMin = _carBody.GetComponent<SpriteRenderer>().sprite.bounds.min.y;
            var yMax = _carBody.GetComponent<SpriteRenderer>().sprite.bounds.max.y;
            _frontWheelPos = new Vector2();
            _backWheelPos = new Vector2();
            _frontWheelPos.x = Random.Range(xMin, xMax);
            _backWheelPos.x = Random.Range(xMin, xMax);
            _frontWheelPos.y = Random.Range(yMin, yMax);
            _backWheelPos.y = Random.Range(yMin, yMax);
            Debug.unityLogger.Log("Xmin:" + xMin + " Xmax:"+xMax);
            Debug.unityLogger.Log("Ymin:" + yMin + " Ymax:"+yMax);
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
            frontWheelJoint.anchor = _frontWheelPos;
            backWheelJoint.anchor = _backWheelPos;
        }

        public void RegenerateValues()
        {
            _scaleX = Random.Range(CarBodyConstraints.ScaleXmin, CarBodyConstraints.ScaleXmax);
            _scaleY = Random.Range(CarBodyConstraints.ScaleYmin, CarBodyConstraints.ScaleYmax);
            var xMin = (_carBody.GetComponent<SpriteRenderer>().sprite.bounds.min.x * _scaleX)/ _carBody.transform.localScale.x;
            var xMax = (_carBody.GetComponent<SpriteRenderer>().sprite.bounds.max.x * _scaleX)/ _carBody.transform.localScale.x;
            var yMin = (_carBody.GetComponent<SpriteRenderer>().sprite.bounds.min.y * _scaleY)/ _carBody.transform.localScale.y;
            var yMax = (_carBody.GetComponent<SpriteRenderer>().sprite.bounds.max.y * _scaleY)/ _carBody.transform.localScale.y;
            _frontWheelPos.x = Random.Range(xMin, xMax);
            _backWheelPos.x = Random.Range(xMin, xMax);
            _frontWheelPos.y = Random.Range(yMin, yMax);
            _backWheelPos.y = Random.Range(yMin, yMax);
        }

        public void RemoveObject(GameObject car)
        {
            
        }
    }
}