using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.GetChild(5).gameObject.GetComponent<Text>().text = Test.instance.noGeneratie.ToString();
        transform.GetChild(6).gameObject.GetComponent<Text>().text = Test.noOfChromosomes.ToString();
        transform.GetChild(7).gameObject.GetComponent<Text>().text = Test.instance.index.ToString();
        transform.GetChild(8).gameObject.GetComponent<Text>().text = Test.instance.score.ToString();
        transform.GetChild(9).gameObject.GetComponent<Text>().text = Test.instance.maxScore.ToString();




    }
}
