using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Evolution;
using Assets.Models;
using UnityEngine;

public class Test : MonoBehaviour
{
    public static Test instance;

    public GameObject _car;
    private List<Car> _cars = new List<Car>();
    private List<Chromosome> _chromosomes = new List<Chromosome>();
    private float time = 0;
    private double oldPosition = -1;
    private int index = 1;
    private bool resetCarFlag = false;

    public List<Car> getCars() => _cars;

    void Awake()
    {
        instance = this;
        GenerateChromosomes();
    }

    void Update()
    {
        Score.ScoreValue = Convert.ToInt32(_car.transform.GetChild(0).position.x);
        if( ((Time.time - time > 1) && Math.Round(_car.transform.GetChild(0).position.x,1) == oldPosition && resetCarFlag) ||
            Math.Round(_car.transform.GetChild(0).position.x,1) < -1)
        {
            resetCarFlag = false;
            if (index < 10)
            {
                _chromosomes[index - 1].score = Score.ScoreValue;
                _cars[index-1].SetActive(false);
                _car = _cars[index].GetCar();
                _cars[index].SetActive(true);
            }
            else
            {
                for (int i = 0; i < 10; i++)
                {
                    Debug.Log("Masina " + i + " a avut scorul : " + _chromosomes[i].score);
                }
                Application.Quit();
            }
            index++;
        }
        
        if (Time.time - time > 5)
        {
            resetCarFlag = true;
            time = Time.time;
            oldPosition = Math.Round(_car.transform.GetChild(0).position.x, 1);
        }
       
    }

    private void GenerateChromosomes()
    {
        for (int i = 0; i < 10; i++)
        {
            var newChromosome = new Chromosome();
            _chromosomes.Add(newChromosome);
            var newCar = constructCar(newChromosome);
            if (i == 0)
            {
                _car = newCar.GetCar();
            }
            else
            {
                newCar.SetActive(false);
            }

        _cars.Add(newCar);
        }
    }

    private Car constructCar(Chromosome chromosome)
    {
        var genes = chromosome.Genes;
        GameObject newCar = GameObject.Instantiate(_car);
        foreach (var gene in genes)
        {
            gene.GetSpecifications().ChangeGameObject(newCar);
        }

        return new Car(newCar);
    }
}
