using System.Collections;
using System.Collections.Generic;
using Assets.Evolution;
using Assets.Models;
using UnityEngine;

public class Test : MonoBehaviour
{
    private GameObject _allCar;
    private List<Car> cars = new List<Car>();
    private List<Chromosome> _chromosomes = new List<Chromosome>();
    public static Test instance;
    private float time = 0;
    private int index = 1;
    
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        _allCar = GameObject.FindGameObjectWithTag("Car");
        cars.Add(new Car(_allCar));
        GenerateChromosomes();
    }

    public List<Car> getCars()
        => cars;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - time > 3)
        {
            time = Time.time;
            if (index < 20)
            {
                cars[index-1].SetActive(false);
                cars[index].SetActive(true);
                CameraController.instance.changeCarBody(cars[index].GetCarBody());
            }
            index++;
        }
    }

    private void GenerateChromosomes()
    {
        for (int i = 0; i < 20; i++)
        {
            var newChromosome = new Chromosome();
            _chromosomes.Add(newChromosome);
            var newCar = constructCar(newChromosome);
            newCar.SetActive(false);
            cars.Add(newCar);
        }
    }

    private Car constructCar(Chromosome chromosome)
    {
        var genes = chromosome.Genes;
        GameObject newCar = GameObject.Instantiate(_allCar);
        foreach(var gene in genes){
            gene.GetSpecifications().ChangeGameObject(newCar);
        }
        return new Car(newCar);
    }
}
