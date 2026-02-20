using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class TimedLevelCrashHandler : MonoBehaviour
{
    [Header("Audio & VFX")]
    [SerializeField] AudioClip crashSFX;
    [SerializeField] AudioClip successSFX;
    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] TextMeshProUGUI crashMessageText;
    
    [Header("UI Elements")]
    [SerializeField] GameObject successPanel;  // Assign in Inspector

    AudioSource audioSource;
    
    Rigidbody rb;
    bool hasEnded = false;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
         rb = GetComponent<Rigidbody>();
    }
    void OnCollisionEnter(Collision other)
    {
        if (hasEnded) return;

        switch (other.gameObject.tag)
        {
            case "Friendly":
                // No action needed
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    void StartCrashSequence()
    {
        hasEnded = true;

        if (crashSFX != null)
        {
            audioSource.Stop();
            audioSource.PlayOneShot(crashSFX);
        }

        if (crashParticles != null)
        {
            crashParticles.Play();
        }
        if (crashMessageText != null)
        {
            crashMessageText.gameObject.SetActive(true);
            StartCoroutine(AnimateCrashText());
        }

        GetComponent<Movement>().enabled = false;

        Invoke("ReturnToMainMenu", 2f);
    }

    void StartSuccessSequence()
    {
        hasEnded = true;
        Time.timeScale = 0f;

        if (successSFX != null)
        {
            audioSource.Stop();
            audioSource.PlayOneShot(successSFX);
        }

        if (successParticles != null)
        {
            successParticles.Play();
        }

        GetComponent<Movement>().enabled = false;

        if (successPanel != null)
        {
            successPanel.SetActive(true); // Show the success message/UI
        }
    }

    void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void LoadSecretScene()
    {
        SceneManager.LoadScene("GavinSecretScene");
    }
    public void PauseRocket()
    {
        GetComponent<Movement>().enabled = false;

        // Freeze movement and disable gravity
        rb.useGravity = false;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    public void Home()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void ResumeRocket()
    {
        GetComponent<Movement>().enabled = true;

        // Unfreeze movement and enable gravity
        rb.useGravity = true;
        rb.constraints = RigidbodyConstraints.None;
    }
    IEnumerator AnimateCrashText()
    {
        float duration = 1.5f; // How long the animation takes
        float time = 0f;

        Vector3 startScale = new Vector3(0.1f, 0.1f, 0.1f);
        Vector3 endScale = Vector3.one;

        crashMessageText.rectTransform.localScale = startScale;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = Mathf.SmoothStep(0, 1, time / duration);
            crashMessageText.rectTransform.localScale = Vector3.Lerp(startScale, endScale, t);
            yield return null;
        }

        crashMessageText.rectTransform.localScale = endScale;
    }

}
