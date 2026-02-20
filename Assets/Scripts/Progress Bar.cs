using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] GameObject finishGO;
    [SerializeField] GameObject playerGO;
    [SerializeField] GameObject startGO; // Add a start point GameObject

    Image progressBar;
    float maxDistance;

    void Start()
    {
        progressBar = GetComponent<Image>();
        
        // Use absolute value of the distance between start and finish
        maxDistance = Mathf.Abs(finishGO.transform.position.x - startGO.transform.position.x);
    }

    void Update()
    {
        // Distance player has traveled from the start
        float currentDistance = Mathf.Abs(playerGO.transform.position.x - startGO.transform.position.x);

        // Clamp the fill amount to stay between 0 and 1
        progressBar.fillAmount = Mathf.Clamp01(currentDistance / maxDistance);
    }
}
