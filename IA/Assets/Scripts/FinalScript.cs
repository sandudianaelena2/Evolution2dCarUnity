using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalScript : MonoBehaviour
{

    public string stopCar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        stopCar = collision.tag;
        if (collision.tag == "CarBody")
        {
            CarController.instance.Stop();
        }
    }

}
