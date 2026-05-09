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

        #if UNITY_WEBGL && !UNITY_EDITOR

                bool isMobile =
                    Application.platform == RuntimePlatform.WebGLPlayer &&
                    (SystemInfo.deviceType == DeviceType.Handheld);

                if (isMobile)
                {
                    if (mobileMessageObject != null)
                    {
                        mobileMessageObject.SetActive(true);
                    }

                    StartCoroutine(LoadNextSceneAfterDelay());
                }
                else
                {
                    LoadNextScene();
                }

        #else
                // Non-WebGL
                LoadNextScene();
        #endif
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
