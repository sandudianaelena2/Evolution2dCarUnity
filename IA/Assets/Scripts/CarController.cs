using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CarController : MonoBehaviour
{

	static public CarController instance;

	public bool isMoving = true;

	public WheelJoint2D frontWheel;
	public WheelJoint2D backWheel;

	JointMotor2D motorFront;

	public float frontSpeed;
	public float frontTorque;

	private void Awake()
	{
		instance = this;
		AudioController.instance.PlayMusic();

	}

	void Update()
	{
		if (isMoving)
		{
			
			motorFront.motorSpeed = frontSpeed * -1;
			motorFront.maxMotorTorque = frontTorque;

			frontWheel.motor = motorFront;
			backWheel.motor = motorFront;
		}
	}

	public void Stop()
	{
		frontSpeed = 0;

		AudioController.instance.StopMusic();
	}

}