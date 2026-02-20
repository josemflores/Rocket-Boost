using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using System.Collections; // For using IEnumerator

public class MainMenuLogic : MonoBehaviour
{
    public GameObject introAnimationObject;
    public float animationDuration = 8f;
    public float fadeDuration = 1f; // Duration of the fade-in effect
    public UIDocument uiDocument; // Ensure this is assigned correctly
    private Animator introAnimator;
    private VisualElement rootVisualElement;
    private Button startButton;
    private Button settingsButton;
    private Button quitButton;

    void Start()
    {
        // Ensure UIDocument is assigned before continuing
        if (uiDocument == null)
        {
            Debug.LogError("UIDocument component is missing on the current GameObject!");
            return;
        }

        if (introAnimationObject == null)
        {
            Debug.LogError("introAnimationObject is not assigned!");
            return;
        }

        introAnimator = introAnimationObject.GetComponent<Animator>();
        if (introAnimator == null)
        {
            Debug.LogError("Animator component not found on introAnimationObject!");
            return;
        }

        rootVisualElement = uiDocument.rootVisualElement;

        // Make sure the UI is visible but start with opacity 0 (transparent)
        if (rootVisualElement != null)
        {
            rootVisualElement.style.opacity = 0f; // Start with fully transparent
            rootVisualElement.style.display = DisplayStyle.Flex; // Ensure the UI is displayed
        }

        // Get references to the buttons
        startButton = rootVisualElement.Q<Button>("StartButton");
        settingsButton = rootVisualElement.Q<Button>("SettingsButton");
        quitButton = rootVisualElement.Q<Button>("QuitButton");

        // Add click listeners to the buttons
        if (startButton != null)
            startButton.clicked += OnStartButtonClicked;
        if (settingsButton != null)
            settingsButton.clicked += OnSettingsButtonClicked;
        if (quitButton != null)
            quitButton.clicked += OnQuitButtonClicked;

        // Start the process of showing the UI with intro animation and fade-in
        StartCoroutine(InitializeUI());
    }

    private IEnumerator InitializeUI()
    {
        // Play the intro animation
        introAnimator.Play("MainMenuClip");
        yield return new WaitForSeconds(animationDuration);

        // Perform fade-in effect
        if (rootVisualElement != null)
        {
            float elapsedTime = 0f;
            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                rootVisualElement.style.opacity = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
                yield return null;
            }
            rootVisualElement.style.opacity = 1f; // Ensure it's fully opaque after fade
        }

        introAnimator.enabled = false; // Optionally disable the intro animation to prevent it from playing again
    }

    // Button functionality (Start, Settings, Quit)
    public void OnStartButtonClicked()
    {
        Debug.Log("Start button clicked!");
        LoadLevelMenu(); // Call method to load the next scene
    }

    public void OnSettingsButtonClicked()
    {
        Debug.Log("Settings button clicked!");
        // Add logic for settings
    }

    public void OnQuitButtonClicked()
    {
        Debug.Log("Quit button clicked!");
        Application.Quit(); // Quit the game
    }

    // Method to load the next scene
    private void LoadLevelMenu()
    {
        // For example, this will load the next scene in the build order.
         SceneManager.LoadScene("Level Menu");
    }
}
