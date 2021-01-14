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
    public static Test instance;
    
    public GameObject _car;
    public GameObject prefabCar;
    private List<Car> _cars = new List<Car>();
    private List<Chromosome> _chromosomes = new List<Chromosome>();
    private float time = 0;
    private double oldPosition = -1;
    public int index = 0;
    private bool resetCarFlag = false;
    private static System.Random random;
    private static object syncObj = new object();

    const float cr = 0.5f;
    const float f = 0.8f;
    public const int noOfChromosomes = 8;
    const int noOfGenes = 5;
    private int noMaximGeneratii = 10;
    

    public List<Car> getCars() => _cars;

    private bool adaptare = true;
    private bool populatieInitiala = true;
    private bool generarePopulatieNoua = false;

    private Chromosome individPotential = null;
    private Car masinaActiva = null;
    private List<Chromosome> populatieNoua = new List<Chromosome>();
    public int noGeneratie = 1;
    private Chromosome solutie;
    public bool terminat = false;
    public int maxScore;
    public int score;
    
    void Awake()
    {
        instance = this;
        
        GenerateChromosomes();
    }

    void Update()
    {
        if (terminat) return;
        if (adaptare)
        {
            Score.ScoreValue = Convert.ToInt32(_car.transform.GetChild(0).position.x);
            score = Score.ScoreValue;
            if (((Time.time - time > 1) && Math.Round(_car.transform.GetChild(0).position.x, 1) == oldPosition &&
                 resetCarFlag) ||
                Math.Round(_car.transform.GetChild(0).position.x, 1) < -1)
            {
                resetCarFlag = false;
                if (populatieInitiala)
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
                        generarePopulatieNoua = true;
                        populatieInitiala = false;
                        adaptare = false;
                    }
                }
                else
                {
                    individPotential.score = Score.ScoreValue;
                    if (individPotential.score > _chromosomes[index].score)
                    {
                        populatieNoua.Add(individPotential);
                    }
                    else
                    {
                        populatieNoua.Add(_chromosomes[index]);
                    }

                    masinaActiva.SetActive(false);
                    adaptare = false;
                    generarePopulatieNoua = true;
                }
                    
                if (Score.ScoreValue > maxScore)
                {
                    maxScore = Score.ScoreValue;
                }

                index++;
                if (!populatieInitiala && index >= noOfChromosomes)
                {
                    noGeneratie++;
                    Debug.Log("Numar generatii" + noGeneratie);
                    if (noGeneratie >= noMaximGeneratii)
                    {
                        terminat = true;
                    }
                    index = 0;
                    _chromosomes = populatieNoua.Select((item) => item).ToList();
                    solutie = _chromosomes.Find((item) => item.score == maxScore);
                    populatieNoua.Clear();
                }
            }

            if (Time.time - time > 5)
            {
                resetCarFlag = true;
                time = Time.time;
                oldPosition = Math.Round(_car.transform.GetChild(0).position.x, 1);;
            }
        }

        if (generarePopulatieNoua)
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

            individPotential = new Chromosome();
            var punctDeDivizare = generateDivingPoint();
            for (int gena = 0; gena < noOfGenes; ++gena)
            {
                var nrAleatoriu = GetRandomDoubleNumber();
                if (gena == punctDeDivizare || nrAleatoriu < cr)
                {
                    individPotential.Genes[gena]
                        .Mutate(indivizi.Select(individ => individ.Genes[gena]).ToList(), f);
                }
                else
                {
                    individPotential.Genes[gena] = indivizi[0].Genes[gena];
                }
            }
            Debug.Log("Generat masina pentru chromose " + index);
            masinaActiva = constructCar(individPotential);
            masinaActiva.SetActive(true);
            _car = masinaActiva.GetCar();
            adaptare = true;
            generarePopulatieNoua = false;
        }
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