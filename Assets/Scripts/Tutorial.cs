
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    public void OpenScene()
    {
        SceneManager.LoadScene("Tutorial Level");
    }
}
