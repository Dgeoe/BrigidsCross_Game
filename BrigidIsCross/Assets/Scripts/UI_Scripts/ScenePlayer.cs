using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePlayer : MonoBehaviour
{
    private Animator buttonAnimator;
    [SerializeField] private Animator cameraAnimator;
    [SerializeField] private GameObject Credits, LevelSelect, Settings;
    private void Awake()
    {
        buttonAnimator = GetComponent<Animator>();
    }
    public void PlayButton()
    {
        buttonAnimator.SetBool("pressMain", true);
        cameraAnimator.SetBool("zoom", true);
        StartCoroutine(PlayScene("Puzzle 1 Tutorial"));
    }

    public void LevelSelectButton()
    {
        buttonAnimator.SetBool("pressMain", true);
        StartCoroutine(turnOnAfter(LevelSelect));
    }

    public void SettingsButton()
    {
        buttonAnimator.SetBool("pressMain", true);
        StartCoroutine(turnOnAfter(Settings));
    }

    public void CreditsButton()
    {
        buttonAnimator.SetBool("pressMain", true);
        StartCoroutine(turnOnAfter(Credits));
    }

    public void BackButton()
    {
        Credits.SetActive(false);
        LevelSelect.SetActive(false); 
        Settings.SetActive(false);
        buttonAnimator.SetBool("pressMain", false);
    }

    private IEnumerator PlayScene(string sceneName)
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(sceneName);
    }
    private IEnumerator turnOnAfter(GameObject g)
    {
        yield return new WaitForSeconds(1.5f);
        g.SetActive(true);
    }

    public void RePlay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMain()
    {
        SceneManager.LoadScene("Menu");
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
