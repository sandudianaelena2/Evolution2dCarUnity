using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinalScript : MonoBehaviour
{
    public static FinalScript instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CarBody"))
        {
            Test.instance.algorithmIsFinish = true;
            StopMotors();
        }
    }

    private void StopMotors()
    {
        Test.instance._car.transform.GetChild(0).GetComponent<CarController>().stop = true;
        var rigidBody = Test.instance._car.transform.GetChild(0).GetComponent<Rigidbody2D>();
        rigidBody.constraints = RigidbodyConstraints2D.FreezePosition;
    }

}
