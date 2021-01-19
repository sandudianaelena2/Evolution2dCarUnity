using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CarController : MonoBehaviour
{

	static public CarController instance;

	public bool stop = false;

	public WheelJoint2D frontWheel;
	public WheelJoint2D backWheel;

	private JointMotor2D motorFront;
	private JointMotor2D motorBack;
	
	private void Awake()
	{
		instance = this;
	}

	void Update()
	{
		if (stop)
		{
			motorBack.motorSpeed = 0;
			motorFront.motorSpeed = 0;
			frontWheel.motor = motorFront;
			backWheel.motor = motorBack;
		}
	}
	
}