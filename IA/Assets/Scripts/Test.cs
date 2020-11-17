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
    private int index = 1;

    public List<Car> getCars() => _cars;

    void Awake()
    {
        instance = this;
        //GenerateChromosomes();
    }

    void Update()
    {/*
        if (Time.time - time > 3)
        {
            time = Time.time;
            if (index < 20)
            {
                _cars[index-1].SetActive(false);
                _car = _cars[index].GetCar();
                _cars[index].SetActive(true);
            }
            index++;
        }*/
    }

    private void GenerateChromosomes()
    {
        for (int i = 0; i < 20; i++)
        {
            var newChromosome = new Chromosome();
            _chromosomes.Add(newChromosome);
            var newCar = constructCar(newChromosome);
            if (i != 0)
            {
                _car = newCar.GetCar();
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
