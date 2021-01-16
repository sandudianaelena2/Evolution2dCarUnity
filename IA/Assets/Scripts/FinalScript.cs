using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinalScript : MonoBehaviour
{
    public static FinalScript instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CarBody"))
        {
            Test.instance.algorithmIsFinish = true;
            StopMotors();
        }
    }

    private void StopMotors()
    {
        JointMotor2D motor2D = new JointMotor2D();
        motor2D.motorSpeed = 0;
        motor2D.maxMotorTorque = 0;
        var joints = Test.instance._car.GetComponents<WheelJoint2D>();
        joints[0].motor = motor2D;
        joints[1].motor = motor2D;
    }

}
