using UnityEngine;

public class EnterCar : MonoBehaviour
{
    private bool playerNear = false;
    public GameObject player;
    public CarSteering carSteering;

    private bool isInsideCar = false;

    public Transform exitPoint;

    private bool canEnter = true;

    void Start()
    {
        carSteering.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!isInsideCar && playerNear)   
                EnterVehicle();
            else if (isInsideCar)            
                ExitVehicle();
        }
    }

    void EnterVehicle()
    {
        player.SetActive(false);
        carSteering.enabled = true;
        isInsideCar = true;
    }

    void ExitVehicle()
    {
        player.SetActive(true);

        if (exitPoint != null)
            player.transform.position = exitPoint.position;
        else
            player.transform.position = transform.position + new Vector3(1f, 0, 0);

        carSteering.enabled = false;
        isInsideCar = false;

        canEnter = false;
        Invoke(nameof(EnableEnter), 0.5f);
    }

    private void EnableEnter()
    {
        canEnter = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canEnter && collision.CompareTag("Player"))
        {
            playerNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerNear = false;
        }
    }
}
