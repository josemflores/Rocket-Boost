using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public Button[] buttons; // Assign in Inspector
    public Sprite lockIcon;  // Assign a lock sprite in Inspector
    public Sprite unlockedIcon; // Optional: assign unlocked sprite if you want

    private void Awake()
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        for (int i = 0; i < buttons.Length; i++)
        {
            int levelIndex = i + 1;
            bool isUnlocked = levelIndex <= unlockedLevel;

            buttons[i].interactable = isUnlocked;

            // Optional: change the button's icon depending on lock state
            Image iconImage = buttons[i].GetComponentInChildren<Image>();
            if (iconImage != null)
            {
                iconImage.sprite = isUnlocked && unlockedIcon != null ? unlockedIcon : lockIcon;
            }

            // Assign click listener only if level is unlocked
            if (isUnlocked)
            {
                buttons[i].onClick.AddListener(() => OpenScene(levelIndex));
            }
        }
    }
    public void ResetProgress()
{
    PlayerPrefs.SetInt("UnlockedLevel", 1);
    PlayerPrefs.Save();
    Debug.Log("Progress reset: Only Level 1 is now unlocked.");
}

    private void OpenScene(int level)
    {
        SceneManager.LoadScene("Gal" + level);
    }
}
