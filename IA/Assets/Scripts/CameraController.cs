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

    void Update()
    {
        if (Test.instance.isRunning)
        {
            carBody = Test.instance._car.transform.GetChild(0).gameObject;

            if (carBody != null)
            {

                float interpolation = moveSpeed * Time.deltaTime;

                Vector3 cameraPosition = this.transform.position;

                cameraPosition.y = Mathf.Lerp(this.transform.position.y, carBody.transform.position.y + 0.5f,
                    interpolation);
                cameraPosition.x = Mathf.Lerp(this.transform.position.x, carBody.transform.position.x, interpolation);

                this.transform.position = cameraPosition;
            }
        }
    }
}
