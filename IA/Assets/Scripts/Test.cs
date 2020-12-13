using System;
using System.Collections;
using System.Collections.Generic;
using Evolution;
using UnityEngine;

public class Test : MonoBehaviour
{
    public static Test instance;
    public GameObject carPrefab;
    public GameObject _car;
    public bool isRunning = false;
    private float time = 0;
    private double oldPosition = -1;
    private bool start = true;
    private bool resetCarFlag = false;
    

    void Awake()
    {
        instance = this;
        Debug.Log("Am apelat solve");
        EvolutionaryAlgorithm.Solve(100, 100, 0.8);
    }

    void Update()
    {
        if (isRunning)
        {
            Score.ScoreValue = Convert.ToInt32(_car.transform.GetChild(0).position.x);
            if (((Time.time - time > 1) && Math.Round(_car.transform.GetChild(0).position.x, 1) == oldPosition &&
                 resetCarFlag) ||
                Math.Round(_car.transform.GetChild(0).position.x, 1) < -1)
            {
                resetCarFlag = false;
                isRunning = false;
            }

            if (Time.time - time > 5)
            {
                resetCarFlag = true;
                time = Time.time;
                oldPosition = Math.Round(_car.transform.GetChild(0).position.x, 1);
            }
        }
    }
}
