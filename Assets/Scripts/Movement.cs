// Movement.cs
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    [SerializeField] float thrustStrength = 0f;
    [SerializeField] float rotationStrength = 0f;
    [SerializeField] float maxYPosition = 100f;

    [Header("Audio")]
    [SerializeField] AudioClip mainEngineSFX;
    [SerializeField] AudioSource heightWarningSource;
    [SerializeField] AudioClip heightWarningClip;

    [Header("Particles")]
    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem rightThrustParticles;
    [SerializeField] ParticleSystem leftThrustParticles;

    [Header("UI")]
    [SerializeField] GameObject heightWarningText;

    Rigidbody rb;
    AudioSource audioSource;
    float warningCooldown = 3f;
    float lastWarningTime = -Mathf.Infinity;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        if (audioSource != null)
        {
            audioSource.playOnAwake = false;
        }
        else
        {
            Debug.LogWarning("Movement script requires an AudioSource component.", this);
        }
    }

    void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
    }

    void OnDisable()
    {
        thrust.Disable();
        rotation.Disable();
    }

    void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (thrust.IsPressed())
        {
            Vector3 worldThrustDirection = transform.TransformDirection(Vector3.up);
            bool isAboveMaxY = transform.position.y >= maxYPosition;
            bool allowThrust = !isAboveMaxY || (isAboveMaxY && worldThrustDirection.y < 0);

            if (allowThrust)
            {
                StartThrusting();
            }
            else
            {
                StopThrusting();
                ShowHeightWarning();
            }
        }
        else
        {
            StopThrusting();
        }
    }

    void ShowHeightWarning()
    {
        if (Time.time - lastWarningTime > warningCooldown)
        {
            lastWarningTime = Time.time;

            if (heightWarningText != null)
            {
                heightWarningText.SetActive(true);
                Invoke("HideHeightWarningText", 2f);
            }

            if (heightWarningSource != null && heightWarningClip != null)
            {
                heightWarningSource.PlayOneShot(heightWarningClip);
            }
        }
    }

    void HideHeightWarningText()
    {
        if (heightWarningText != null)
        {
            heightWarningText.SetActive(false);
        }
    }

    void ProcessRotation()
    {
        float rotationInput = rotation.ReadValue<float>();

        if (rotationInput < 0)
        {
            RotateRight();
        }
        else if (rotationInput > 0)
        {
            RotateLeft();
        }
        else
        {
            StopRotating();
        }
    }

    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * thrustStrength * Time.fixedDeltaTime);

        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.clip = mainEngineSFX;
            audioSource.Play();
        }

        if (mainEngineParticles != null && !mainEngineParticles.isPlaying)
        {
            mainEngineParticles.Play();
        }
    }

    void StopThrusting()
    {
        if (audioSource != null && audioSource.isPlaying && audioSource.clip == mainEngineSFX)
        {
            audioSource.Stop();
        }

        if (mainEngineParticles != null && mainEngineParticles.isPlaying)
        {
            mainEngineParticles.Stop();
        }
    }

    void RotateRight()
    {
        ApplyRotation(rotationStrength);

        if (rightThrustParticles != null && !rightThrustParticles.isPlaying)
        {
            if (leftThrustParticles != null) leftThrustParticles.Stop();
            rightThrustParticles.Play();
        }
    }

    void RotateLeft()
    {
        ApplyRotation(-rotationStrength);

        if (leftThrustParticles != null && !leftThrustParticles.isPlaying)
        {
            if (rightThrustParticles != null) rightThrustParticles.Stop();
            leftThrustParticles.Play();
        }
    }

    void StopRotating()
    {
        if (rightThrustParticles != null) rightThrustParticles.Stop();
        if (leftThrustParticles != null) leftThrustParticles.Stop();
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.fixedDeltaTime);
        rb.freezeRotation = false;
    }

    // --- External control methods ---
    public void SetRotationStrength(float value)
    {
        rotationStrength = value;
    }

    public void SetThrustStrength(float value)
    {
        thrustStrength = value;
    }
}