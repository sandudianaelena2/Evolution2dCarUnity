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
	JointMotor2D motorBack;

	public float frontSpeed;
	public float frontTorque;


	private void Awake()
	{
		instance = this;
		//AudioController.instance.PlayMusic();
	}

	void Update()
	{
		
	}

	public void Stop()
	{
		frontSpeed = 0;

		//AudioController.instance.StopMusic();
	}
}