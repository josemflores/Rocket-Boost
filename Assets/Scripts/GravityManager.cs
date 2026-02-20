using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneGravityManager : MonoBehaviour
{
    public Vector3 customGravity = new Vector3(0, 0, 0);
    private Vector3 defaultGravity;

    public static SceneGravityManager Instance;

    void Awake()
    {
        defaultGravity = Physics.gravity;

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Force apply gravity immediately when awake
            ApplyGravityForCurrentScene();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void ApplyGravityForCurrentScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        Debug.Log("SceneGravityManager: Applying gravity for scene: " + currentSceneName);

        if (currentSceneName == "GAL2")
        {
            Physics.gravity = customGravity;
            Debug.Log("SceneGravityManager: Applied custom gravity: " + customGravity);
        }
        else if (currentSceneName == "GAL3" || currentSceneName == "GAL5")
        {
            Physics.gravity = new Vector3(0, -6f, 0);
            Debug.Log("SceneGravityManager: Applied custom gravity: -6f");
        }
        else if (currentSceneName == "GAL4")
        {
            Physics.gravity = new Vector3(0, 4f, 0);
            Debug.Log("SceneGravityManager: Applied custom gravity: 4f");
        }
        else if (currentSceneName == "GAL6")
        {
            Physics.gravity = new Vector3(0, -9.81f, 0);
            Debug.Log("SceneGravityManager: Applied custom gravity: -9.81f");
        }
        else
        {
            Physics.gravity = defaultGravity;
            Debug.Log("SceneGravityManager: Applied default gravity: " + defaultGravity);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ApplyGravityForCurrentScene();
    }
}
