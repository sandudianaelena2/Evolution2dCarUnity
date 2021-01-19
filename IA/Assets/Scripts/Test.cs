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
    private int noMaximumGenerations = 20;
    private bool adaptationPhase = true;
    private bool initialPopulationPhase = true;
    private bool generateNewPopulation = false;
    private Chromosome potentialSpecimen = null;
    public Car activeCar = null;
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
                ResetCarIfBlocked();
                if (!initialPopulationPhase && index >= noOfChromosomes)
                {
                    CreateNewGeneration();
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
            CreateNewPontentialSpecimen();
            Debug.Log("Generat masina pentru chromose " + index);
            activeCar = constructCar(potentialSpecimen);
            activeCar.SetActive(true);
            _car = activeCar.GetCar();
            adaptationPhase = true;
            generateNewPopulation = false;
        }
    }

    /// <summary>
    /// <para>Functie care genereaza individul potential.</para>
    /// </summary>
    private void CreateNewPontentialSpecimen()
    {
        var indivizi = new List<Chromosome>();
        var chromose = _chromosomes[index];
        //se stocheaza individul existent
        indivizi.Add(chromose);
        var temp = 3;
        while (temp > 0)
        {
            int indexRandom;
            do
            {
                //se genereaza alti 3 indivizi diferiti de individul existent in mod aleatoriu
                indexRandom = GetRandomIntNumber(_chromosomes.Count);
            } while (indexRandom == index && !indivizi.Contains(_chromosomes[indexRandom]));

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
                //se genereaza individul potential efectuand operatii asupra
                //celorlalti 3 indivizi
                potentialSpecimen.Genes[gena]
                    .Mutate(indivizi.Select(individ => individ.Genes[gena]).ToList(), f);
            }
            else
            {
                //individul potential va prelua caracteristicile individului existent
                potentialSpecimen.Genes[gena] = indivizi[0].Genes[gena];
            }
        }
    }

    /// <summary>
    /// <para>Functie care genereaza individul potential.</para>
    /// </summary>
    private void CreateNewGeneration()
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

    /// <summary>
    /// <para>Functie care activeaza urmatoarea masina(daca exista) in caz ca s-a blocat.</para>
    /// </summary>
    private void ResetCarIfBlocked()
    {
        resetCarFlag = false;
        if (initialPopulationPhase)
        {
            if (index < noOfChromosomes - 1)
            {
                //se va dezactiva masina actuala si se va activa urmatoarea masina din lista
                _chromosomes[index].score = Score.ScoreValue;
                _cars[index].SetActive(false);
                _car = _cars[index + 1].GetCar();
                _cars[index + 1].SetActive(true);
            }
            else
            {
                //se va dezactiva masina actuala 
                //se pregateste trecerea in faza de generare a unui nou individ
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
            //se compara scorul individului existent cu cel al individului potential
            //functia de adaptare(scor)
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

        //se actualizeaza scorul maxim
        if (Score.ScoreValue > maxScore)
        {
            maxScore = Score.ScoreValue;
        }

        index++;
    }

    /// <summary>
    /// <para>Functie care genereaza populatia initiala.</para>
    /// </summary>
    private void GenerateChromosomes()
    {
        for (int i = 0; i < noOfChromosomes; i++)
        {
            var newChromosome = new Chromosome();
            _chromosomes.Add(newChromosome);
            //se genereaza un individ in mod aleatoriu
            var newCar = constructCar(newChromosome);
            //prima masina se activeaza iar restul se dezactiveaza
            if (i == 0)
            {
                _car = newCar.GetCar();
                activeCar = newCar;
            }
            else
            {
                newCar.SetActive(false);
            }

            _cars.Add(newCar);
        }
    }

    /// <summary>
    /// <para>Functie care genereaza in mod aleatoriu un individ.</para>
    /// </summary>
    private Car constructCar(Chromosome chromosome)
    {
        var genes = chromosome.Genes;
        GameObject newCar = GameObject.Instantiate(prefabCar);
        foreach (var gene in genes)
        {
            //se genereaza un individ in mod aleatoriu
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