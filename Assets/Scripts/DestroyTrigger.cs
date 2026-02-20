using UnityEngine;
using System.Collections.Generic;

public class DestroyOnTrigger : MonoBehaviour
{
    [Tooltip("Add all the GameObjects you want to destroy when the player enters the trigger.")]
    public List<GameObject> objectsToDestroy = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (GameObject obj in objectsToDestroy)
            {
                if (obj != null)
                    Destroy(obj);
            }

            Destroy(gameObject); // Destroy the trigger itself
        }
    }
}