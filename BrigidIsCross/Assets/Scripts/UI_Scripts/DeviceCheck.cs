using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class DeviceCheck : MonoBehaviour
{
    [Header("Mobile Only")]
    public GameObject mobileMessageObject;

    [Header("Delay Before Loading Mobile Scene")]
    public float mobileDelay = 4f;

    private void Awake()
    {
        ContinueGame();
    }
    public void ContinueGame()
    {
        bool isMobile = Application.isMobilePlatform;
        if (isMobile)
        {
            StartCoroutine(LoadNextSceneAfterDelay());
        }
        else
        {
            LoadNextScene();
        }
    }

    private IEnumerator LoadNextSceneAfterDelay()
    {
        Debug.Log("Loading on Mobile");
        yield return new WaitForSeconds(mobileDelay);
        LoadNextScene();
    }

    private void LoadNextScene()
    {
        Debug.Log("Loading");
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
