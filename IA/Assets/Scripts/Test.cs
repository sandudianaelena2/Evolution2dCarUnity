using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Evolution;
using Models;
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
    private static System.Random random;
    private static object syncObj = new object();

    const float cr = 0.9f;
    const float f = 0.8f;
    const int noOfChromosomes = 30;
    const int noOfGenes = 5;
    

    public List<Car> getCars() => _cars;

    private bool adaptare = true;
    private bool generarePopulatieNoua = false;


    void Awake()
    {
        instance = this;
        GenerateChromosomes();
    }

    void Update()
    {
        if (adaptare)
        {
            Score.ScoreValue = Convert.ToInt32(_car.transform.GetChild(0).position.x);
            if( ((Time.time - time > 1) && Math.Round(_car.transform.GetChild(0).position.x,1) == oldPosition && resetCarFlag) ||
                Math.Round(_car.transform.GetChild(0).position.x,1) < -1)
            {
                resetCarFlag = false;
                if (index < noOfChromosomes)
                {
                    _chromosomes[index - 1].score = Score.ScoreValue;
                    _cars[index-1].SetActive(false);
                    _car = _cars[index].GetCar();
                    _cars[index].SetActive(true);
                }
                else
                {
                    index = 0;
                    generarePopulatieNoua = true;
                    adaptare = false;
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
            var individPotential = new Chromosome();
            var punctDeDivizare = generateDivingPoint();
            for(int gena=0;gena<noOfGenes; ++gena)
            {
                var nrAleatoriu = GetRandomDoubleNumber();
                if (gena == punctDeDivizare || nrAleatoriu < cr)
                {
                    individPotential.Genes[gena].Mutate(indivizi.Select(individ => individ.Genes[gena]).ToList(), f);
                }
                else
                {
                    individPotential.Genes[gena] = indivizi[0].Genes[gena];
                }
            }
            index++;
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
        GameObject newCar = GameObject.Instantiate(_car);
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

    //private GameObject Mutation()
    //{

    //    foreach(var chromosome in _chromosomes)
    //    {
    //        List<Chromosome> individuals = new List<Chromosome>();

    //        individuals.Add(chromosome);
    //        for (int i = 0; i < 3; ++i)
    //        {
    //            var randomIndx = GetRandomIntNumber(noOfChromosomes);
    //            individuals.Add(_chromosomes[randomIndx]);
    //        }

    //        var potentialIndiv = new Chromosome();
    //        var dividingPoint = GetRandomIntNumber(noOfGenes - 1);

    //        for(int geneNo = 0; geneNo < noOfGenes; ++geneNo)
    //        {
    //            if(geneNo == dividingPoint || GetRandomDoubleNumber() < cr)
    //            {

    //            }
    //            else
    //            {

    //            }
    //        }

    //    }

    //}
    
}
