using UnityEngine;

public class TriggerAst : MonoBehaviour
{
    [SerializeField] private GameObject asteroid;
    private Rigidbody rb;
    void OnTriggerEnter(Collider other)
    {
        asteroid.SetActive(true);
        if (asteroid != null)
        {
            rb = asteroid.GetComponent<Rigidbody>();
            rb.AddRelativeForce(Vector3.down * 1000f, ForceMode.Impulse);
            Debug.Log("Asteroid activated and force applied.");

        }
    }
}
