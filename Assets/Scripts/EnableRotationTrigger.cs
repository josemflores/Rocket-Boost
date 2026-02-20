using UnityEngine;
using UnityEngine.UI;

public class TriggerBehavior : MonoBehaviour
{
    [Header("Trigger Settings")]
    public bool enablesRotation = false;
    public float rotationStrength = 100f;

    public bool changesThrust = false;
    public float newThrustValue = 10f;

    [Header("Optional UI")]
    public GameObject tutorialPanel; // Assign panel with rotation instructions
    public Button continueButton;    // Assign the button inside the panel

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return; // Prevent double-triggering
        triggered = true;

        Movement movement = other.GetComponent<Movement>();
        if (movement != null)
        {
            // Handle thrust changes immediately
            if (changesThrust)
            {
                movement.SetThrustStrength(newThrustValue);
            }

            // If rotation should be enabled, pause and wait for player
            if (enablesRotation && tutorialPanel != null && continueButton != null)
            {
                Time.timeScale = 0;
                tutorialPanel.SetActive(true);
                continueButton.onClick.AddListener(() =>
                {
                    ResumeGame(movement);
                });
            }
            else if (enablesRotation)
            {
                movement.SetRotationStrength(rotationStrength);
                Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject); // No UI, just destroy trigger
            }
        }
    }

    private void ResumeGame(Movement movement)
    {
        Time.timeScale = 1;
        tutorialPanel.SetActive(false);
        movement.SetRotationStrength(rotationStrength);

        continueButton.onClick.RemoveAllListeners();
        Destroy(gameObject);
    }
}