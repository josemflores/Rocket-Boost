using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TimedLevelUI : MonoBehaviour
{
    [Header("Timer Settings")]
    public float timeLimit = 60f; // Total time for the level
    private float timeRemaining;

    [Header("UI Reference")]
    public TextMeshProUGUI timerText;

    private bool isLevelActive = true;

    void Start()
    {
        timeRemaining = timeLimit;
        UpdateTimerDisplay();
    }

    void Update()
    {
        if (!isLevelActive) return;

        timeRemaining -= Time.deltaTime;
        if (timeRemaining <= 0f)
        {
            timeRemaining = 0f;
            isLevelActive = false;
            UpdateTimerDisplay();
            OnTimeExpired();
        }
        else
        {
            UpdateTimerDisplay();
        }
    }

    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60f);
        int seconds = Mathf.FloorToInt(timeRemaining % 60f);
        timerText.text = $"{minutes:00}:{seconds:00}";
        if (timeRemaining <= 10f)
        {
            timerText.color = Color.red;
        }
        else
        {
            timerText.color = Color.white;
        }

    }

    void OnTimeExpired()
    {
        // Disable player controls or trigger fail sequence
        Debug.Log("Time expired!");
        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_Expired", 1);
        PlayerPrefs.Save();
        SceneManager.LoadScene("Main Menu"); // or a game over scene
    }
}
