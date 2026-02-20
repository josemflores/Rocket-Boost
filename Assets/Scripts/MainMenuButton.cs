using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class ButtonFunctionality : MonoBehaviour
{
    private UIDocument uiDocument;
    private VisualElement rootVisualElement;
    private Button startButton;
    private Button settingsButton;
    private Button quitButton;

    private void OnEnable()
    {
        // Get UIDocument from this GameObject
        uiDocument = GetComponent<UIDocument>();

        if (uiDocument == null)
        {
            Debug.LogError("❌ UIDocument not found on this GameObject.");
            return;
        }

        rootVisualElement = uiDocument.rootVisualElement;

        // Query buttons by name
        startButton = rootVisualElement.Q<Button>("StartButton");
        settingsButton = rootVisualElement.Q<Button>("SettingsButton");
        quitButton = rootVisualElement.Q<Button>("QuitButton");

        // Register button click events
        if (startButton != null) startButton.clicked += OnStartButtonClicked;
        if (settingsButton != null) settingsButton.clicked += OnSettingsButtonClicked;
        if (quitButton != null) quitButton.clicked += OnQuitButtonClicked;
    }

    public void OnStartButtonClicked()
    {
        Debug.Log("Start button clicked!");
        LoadLevelMenu();
    }

    public void OnSettingsButtonClicked()
    {
        Debug.Log("Settings button clicked!");
        // Implement settings logic here
    }

    public void OnQuitButtonClicked()
    {
        Debug.Log("Quit button clicked!");
        Application.Quit();
    }

    private void LoadLevelMenu()
    {
        SceneManager.LoadScene("Level Menu");
    }
}
