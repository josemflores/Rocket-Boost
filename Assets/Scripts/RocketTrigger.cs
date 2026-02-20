using UnityEngine;

public class RocketTrigger : MonoBehaviour
{
    // SerializeField allows you to assign the rocket prefab in the Unity Editor
    [SerializeField] private GameObject rocketPrefab;
    [SerializeField] private GameObject particlesParent; // Optional: Particle system for visual effects
    [SerializeField] private float forceMultiplier = 10f; // Multiplier for the force applied to the rocket
    [SerializeField] private float destroyHeight = 120f;
    private Rigidbody rocketBody;

    private void Awake()
    {
        // Ensure the rocketPrefab is assigned, otherwise log a warning
        if (rocketPrefab == null)
        {
            Debug.LogWarning("Rocket prefab is not assigned in the Inspector.");
            return;
        }

        rocketBody = rocketPrefab.GetComponent<Rigidbody>();
        Debug.Log("Rocket Rigidbody found: " + (rocketBody != null));
    }
    
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that entered the trigger is the player rocket
        Debug.Log("Trigger entered by: " + other.tag);
        if (other.tag == "Player Rocket")
        {
            // Ensure the rocketBody is not null before applying force
            if (rocketBody == null)
            {
                Debug.LogWarning("Rocket Rigidbody is not assigned or found.");
                return;
            }

            // Apply an upward force to the rocket when it enters the trigger
            ApplyForceToRocket();
        }
    }

    private void ApplyForceToRocket()
    {
        // Add logic for when the player rocket enters the trigger
        rocketBody.AddRelativeForce(Vector3.up * forceMultiplier, ForceMode.Impulse);
        Debug.Log("Rocket has entered the trigger zone and received an upward force.");
        if (particlesParent != null)
        {
            foreach (Transform child in particlesParent.transform)
            {
                ParticleSystem particleSystem = child.GetComponent<ParticleSystem>();
                if (particleSystem != null)
                {
                    particleSystem.Play();
                }
            }
        }
    }

    private void Update()
    {
        if (rocketPrefab != null && rocketPrefab.transform.position.y >= destroyHeight)
        {
            Destroy(rocketPrefab);
            Debug.Log($"Rocket destroyed after exceeding {destroyHeight} m.");
        }
    }
}
