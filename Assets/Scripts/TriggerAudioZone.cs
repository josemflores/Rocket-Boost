using UnityEngine;
using TMPro;
using System.Collections;

public class AudioAndTextTrigger : MonoBehaviour
{
    [Header("Audio Settings")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private float maxVolume = 1f;

    [Header("Text Settings")]
    [SerializeField] private TextMeshProUGUI textToFlash;
    [SerializeField] private float blinkDuration = 0.5f;

    private Coroutine fadeCoroutine;

    private void Awake()
    {
        // Validate audio
        if (audioSource == null || audioClip == null)
        {
            Debug.LogError("AudioSource or AudioClip not assigned.");
            enabled = false;
            return;
        }

        // Setup AudioSource
        audioSource.clip = audioClip;
        audioSource.loop = true;
        audioSource.playOnAwake = false;
        audioSource.volume = 0f;

        // Validate and disable text
        if (textToFlash != null)
        {
            textToFlash.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Optional: if (!other.CompareTag("Player")) return;

        if (!audioSource.isPlaying)
            audioSource.Play();

        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeAudio(0f, maxVolume, fadeDuration));

        if (textToFlash != null)
            StartCoroutine(BlinkTextThreeTimes());
    }

    private void OnTriggerExit(Collider other)
    {
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeAudio(audioSource.volume, 0f, fadeDuration));
    }

    private IEnumerator FadeAudio(float startVolume, float targetVolume, float duration)
    {
        float time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, targetVolume, time / duration);
            yield return null;
        }

        audioSource.volume = targetVolume;

        if (Mathf.Approximately(targetVolume, 0f))
            audioSource.Stop();
    }

    private IEnumerator BlinkTextThreeTimes()
    {
        textToFlash.enabled = true;

        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(blinkDuration);
            textToFlash.enabled = false;
            yield return new WaitForSeconds(blinkDuration);
            textToFlash.enabled = true;
        }

        yield return new WaitForSeconds(blinkDuration);
        textToFlash.enabled = false;
    }
}
