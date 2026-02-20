using UnityEngine;

public class TriggerAnimation : MonoBehaviour
{
    public Transform playerRocket;   // Reference to the rocket's transform
    public float triggerX = 10f;       // The x position where the animation starts
    private Animator animator;         // The Animator component
    private bool animationStarted = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!animationStarted && playerRocket.position.x >= triggerX)
        {
            // Trigger the animation, for example by using a trigger parameter:
            animator.SetTrigger("StartAnimation");
            animationStarted = true;
        }
    }
}
