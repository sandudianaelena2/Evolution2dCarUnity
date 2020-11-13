using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Assets.Models;

public class CarController : MonoBehaviour
{

	static public CarController instance;
	
	public bool isMoving = true;

	private Car currentCar;

	private void Awake()
	{
		instance = this;
		AudioController.instance.PlayMusic();
	}

	private void Start()
	{
		
	}

	void Update()
	{
		if (isMoving)
		{
			
		}
	}

	public void setCurrentCar(Car car)
	{
		this.currentCar = car;
	}

	public void Stop()
	{
		currentCar.Stop();
		currentCar.SetActive(false);
		AudioController.instance.StopMusic();
	}

}