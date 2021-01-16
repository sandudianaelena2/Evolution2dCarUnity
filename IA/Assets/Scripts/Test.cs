using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Evolution;
using Models;
using UnityEngine;

public class Test : MonoBehaviour
{
    const float cr = 0.5f;
    const float f = 0.8f;
    const int noOfGenes = 5;
    
    public static Test instance;
    public GameObject _car;
    public GameObject prefabCar;
    public int noGeneratie = 1;
    public bool algorithmIsFinish = false;
    public int maxScore;
    public int score;
    public int index = 0;
    public const int noOfChromosomes = 8;
    public List<Car> getCars() => _cars;
 
    private List<Car> _cars = new List<Car>();
    private List<Chromosome> _chromosomes = new List<Chromosome>();
    private float time = 0;
    private double oldPosition = -1;
    private bool resetCarFlag = false;
    private static System.Random random;
    private static object syncObj = new object();
    private int noMaximumGenerations = 10;
    private bool adaptationPhase = true;
    private bool initialPopulationPhase = true;
    private bool generateNewPopulation = false;
    private Chromosome potentialSpecimen = null;
    private Car activeCar = null;
    private List<Chromosome> newPopulation = new List<Chromosome>();
    private Chromosome solutie;
    
    void Awake()
    {
        instance = this;
        
        GenerateChromosomes();
    }

    void Update()
    {
        if (algorithmIsFinish) return;
        if (adaptationPhase)
        {
            Score.ScoreValue = Convert.ToInt32(_car.transform.GetChild(0).position.x);
            score = Score.ScoreValue;
            if (((Time.time - time > 1) && Math.Round(_car.transform.GetChild(0).position.x, 1) == oldPosition &&
                 resetCarFlag) ||
                Math.Round(_car.transform.GetChild(0).position.x, 1) < -1)
            {
                resetCarIfBlocked();
                if (!initialPopulationPhase && index >= noOfChromosomes)
                {
                    createNewGeneration();
                }
            }

            if (Time.time - time > 5)
            {
                resetCarFlag = true;
                time = Time.time;
                oldPosition = Math.Round(_car.transform.GetChild(0).position.x, 1);;
            }
        }

        if (generateNewPopulation)
        {
            createNewPontentialSpecimen();
            Debug.Log("Generat masina pentru chromose " + index);
            activeCar = constructCar(potentialSpecimen);
            activeCar.SetActive(true);
            _car = activeCar.GetCar();
            adaptationPhase = true;
            generateNewPopulation = false;
        }
    }

    private void createNewPontentialSpecimen()
    {
        var indivizi = new List<Chromosome>();
        var chromose = _chromosomes[index];
        indivizi.Add(chromose);
        var temp = 3;
        while (temp > 0)
        {
            int indexRandom;
            do
            {
                indexRandom = GetRandomIntNumber(_chromosomes.Count);
            } while (indexRandom == index);

            indivizi.Add(_chromosomes[indexRandom]);
            temp--;
        }

        potentialSpecimen = new Chromosome();
        var punctDeDivizare = generateDivingPoint();
        for (int gena = 0; gena < noOfGenes; ++gena)
        {
            var nrAleatoriu = GetRandomDoubleNumber();
            if (gena == punctDeDivizare || nrAleatoriu < cr)
            {
                potentialSpecimen.Genes[gena]
                    .Mutate(indivizi.Select(individ => individ.Genes[gena]).ToList(), f);
            }
            else
            {
                potentialSpecimen.Genes[gena] = indivizi[0].Genes[gena];
            }
        }
    }

    private void createNewGeneration()
    {
        noGeneratie++;
        Debug.Log("Numar generatii" + noGeneratie);
        if (noGeneratie >= noMaximumGenerations)
        {
            algorithmIsFinish = true;
        }
        index = 0;
        _chromosomes = newPopulation.Select((item) => item).ToList();
        solutie = _chromosomes.Find((item) => item.score == maxScore);
        newPopulation.Clear();
    }
    private void resetCarIfBlocked()
    {
        resetCarFlag = false;
        if (initialPopulationPhase)
        {
            if (index < noOfChromosomes - 1)
            {
                _chromosomes[index].score = Score.ScoreValue;
                _cars[index].SetActive(false);
                _car = _cars[index + 1].GetCar();
                _cars[index + 1].SetActive(true);
            }
            else
            {
                _cars[index].SetActive(false);
                _chromosomes[index].score = Score.ScoreValue;
                noGeneratie++;
                index = -1;
                generateNewPopulation = true;
                initialPopulationPhase = false;
                adaptationPhase = false;
            }
        }
        else
        {
            potentialSpecimen.score = Score.ScoreValue;
            if (potentialSpecimen.score > _chromosomes[index].score)
            {
                newPopulation.Add(potentialSpecimen);
            }
            else
            {
                newPopulation.Add(_chromosomes[index]);
            }

            activeCar.SetActive(false);
            adaptationPhase = false;
            generateNewPopulation = true;
        }

        if (Score.ScoreValue > maxScore)
        {
            maxScore = Score.ScoreValue;
        }

        index++;
    }

    private void GenerateChromosomes()
    {
        for (int i = 0; i < noOfChromosomes; i++)
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
        GameObject newCar = GameObject.Instantiate(prefabCar);
        foreach (var gene in genes)
        {
            gene.GetSpecifications().ChangeGameObject(newCar);
        }

        return new Car(newCar);
    }
    
    private int generateDivingPoint()
    {
        return GetRandomIntNumber(noOfGenes);
    }

    private int GetRandomIntNumber(int max)
    {
        lock (syncObj)
        {
            if (random == null)
                random = new System.Random();
            return random.Next(max);
        }
    }

    private double GetRandomDoubleNumber()
    {
        lock (syncObj)
        {
            if (random == null)
                random = new System.Random();
            return random.NextDouble();
        }
    }
}