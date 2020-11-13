using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject carBody;

    public float moveSpeed = 10.0f;

    public static CameraController instance;

    void Start()
    {
        
    }

    void Awake()
    {
        instance = this;
    }

    public void changeCarBody(GameObject newCarbody)
    {
        this.carBody = newCarbody;
    }

    void Update()
    {
        float interpolation = moveSpeed * Time.deltaTime;

        Vector3 cameraPosition = this.transform.position;

        cameraPosition.y = Mathf.Lerp(this.transform.position.y, carBody.transform.position.y, interpolation);
        cameraPosition.x = Mathf.Lerp(this.transform.position.x, carBody.transform.position.x, interpolation);

        this.transform.position = cameraPosition;
    }
}
