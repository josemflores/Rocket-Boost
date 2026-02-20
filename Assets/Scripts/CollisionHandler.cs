using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip successSFX;
    [SerializeField] AudioClip crashSFX;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] TextMeshProUGUI crashMessageText;
    [SerializeField] GameObject crashPanel;



    AudioSource audioSource;

    bool isControllable = true;
    bool isCollidable = true;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // void Update()
    // {
    //     RespondToDebugKeys();
    // }

    // void RespondToDebugKeys()
    // {
    //     if (Keyboard.current != null && Keyboard.current.lKey.wasPressedThisFrame)
    //     {
    //         LoadNextLevel();
    //     }
    //     else if (Keyboard.current != null && Keyboard.current.cKey.wasPressedThisFrame)
    //     {
    //         isCollidable = !isCollidable;
    //         Debug.Log("Ckey was pressed");
    //     }
    // }

    void OnCollisionEnter(Collision other)
    {
        if (!isControllable || !isCollidable){return;}

        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Everything is looking good");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;

        }
    }
    
    void StartSuccessSequence()
    {
        isControllable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(successSFX);
        successParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);
        UnlockNewLevel();
    }

    void StartCrashSequence()
    {
        isControllable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(crashSFX);
        crashParticles.Play();
        GetComponent<Movement>().enabled = false;
        if (crashMessageText != null)
    {
        crashMessageText.gameObject.SetActive(true);
        StartCoroutine(AnimateCrashText());
    }
        if (crashPanel != null)
    {
        StartCoroutine(AnimateCrashText());
    }
        StartCoroutine(ShowCrashPanelWithDelay(1.5f));

    }
    IEnumerator ShowCrashPanelWithDelay(float delay)
    {
    yield return new WaitForSeconds(delay);

    if (crashPanel != null)
    {
        crashPanel.SetActive(true);
    }
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

    void LoadNextLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene + 1;

        if (nextScene == SceneManager.sceneCountInBuildSettings)
        {
            nextScene = 0;
        }

        SceneManager.LoadScene(nextScene);
    }
    
void UnlockNewLevel()
{
    string sceneName = SceneManager.GetActiveScene().name;

    if (sceneName.StartsWith("GAL"))
    {
        string levelNumString = sceneName.Substring(3);

        if (int.TryParse(levelNumString, out int currentLevel))
        {
            int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

            if (currentLevel + 1 > unlockedLevel)
            {
                PlayerPrefs.SetInt("UnlockedLevel", currentLevel + 1);
                PlayerPrefs.Save();
            }
        }
    }
}
    
    public void ReloadLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

    public void Home()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void StopTime()
    {
        Time.timeScale = 0;
    }
     public void ResumeTime()
    {
        Time.timeScale = 1;
    }
}
