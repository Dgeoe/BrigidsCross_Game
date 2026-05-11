using UnityEngine;
using System.Collections;

public class EndGame_Manager : MonoBehaviour
{
    //Handles Win/Lose States In the game
    [SerializeField] private int enemyCount;
    [SerializeField] private int leastThrows;
    [SerializeField] private GameObject ScoreScreen;
    [SerializeField] private AudioSource Music;
    [SerializeField] private Animator scoreAnimator;
    [SerializeField] private GameObject levelButtons;
    [SerializeField] public AudioSource scoreSounds;
    [SerializeField] public AudioClip[] sounds; //0 = fail, 1 = 1 , 2 = 2, 3 = 3, 4 = 4, 5 = gold
    private int enemiesKilled;
    private int currentThrows;
    private int mostThrows;
    private int score;
    public static EndGame_Manager Instance;

    private void Start()
    {
        if (Instance == null) Instance = this;
        mostThrows = Throw.Instance.shurikens;
    }

    public void Kill()
    {
        enemiesKilled++;

        if (enemiesKilled == enemyCount)
        {
            Debug.Log("WIN");
            Throw.Instance.enabled = false;
            CalculateScore();
            StartCoroutine(LowerMusic());
            ScoreScreen.SetActive(true);
        }
    }

    public void ThrowCheck()
    {
        currentThrows++;

        if (currentThrows == mostThrows + 1)
        {
            Debug.Log("Lose");
            score = 0;
            Throw.Instance.enabled = false;
            StartCoroutine(LowerMusic());
            ScoreScreen.SetActive(true);
            StartCoroutine(PrintScore(0));
        }
    }

    private void CalculateScore()
    {
        if (currentThrows <= leastThrows)
        {
            score = 4;
            Debug.Log("Gold");
            StartCoroutine(PrintScore(4));
            return;
        }

        int extraThrows = mostThrows - leastThrows;
        int usedExtraThrows = currentThrows - leastThrows;

        float percentage = 1f - ((float)usedExtraThrows / extraThrows);

        if (percentage >= 0.66f)
        {
            score = 3;
            StartCoroutine(PrintScore(3));
        }
        else if (percentage >= 0.33f)
        {
            score = 2;
            StartCoroutine(PrintScore(2));
            scoreAnimator.SetBool("One", true);
            scoreAnimator.SetBool("Two", true);
            scoreAnimator.SetBool("Three", true);
        }
        else
        {
            score = 1;
            StartCoroutine(PrintScore(1));
        }

        Debug.Log("Score: " + score);
    }

    private IEnumerator LowerMusic()
    {
        float startVolume = Music.volume;
        float time = 0f;

        while (time < 2.2f)
        {
            time += Time.deltaTime;
            Music.volume = Mathf.Lerp(startVolume, 0f, time / 2.2f);
            yield return null;
        }

        Music.volume = 0f;
    }

    private IEnumerator PrintScore(int score)
    {
        if (SaveSystem.Instance != null) SaveSystem.Instance.CompleteLevel(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex, score);
        yield return new WaitForSeconds(3f);

        switch (score)
        {
            case 0:
                scoreAnimator.SetBool("One", true);
                scoreAnimator.SetBool("Fail", true);

                yield return new WaitForSeconds(0.8f);
                levelButtons.SetActive(true);
                break;

            case 1:
                scoreAnimator.SetBool("One", true);
                yield return new WaitForSeconds(0.8f);

                scoreAnimator.SetBool("Two", true);

                yield return new WaitForSeconds(0.8f);
                levelButtons.SetActive(true);
                break;

            case 2:
                scoreAnimator.SetBool("One", true);
                yield return new WaitForSeconds(0.8f);

                scoreAnimator.SetBool("Two", true);
                yield return new WaitForSeconds(0.8f);

                scoreAnimator.SetBool("Three", true);

                yield return new WaitForSeconds(0.8f);
                levelButtons.SetActive(true);
                break;

            case 3:
                scoreAnimator.SetBool("One", true);
                yield return new WaitForSeconds(0.8f);

                scoreAnimator.SetBool("Two", true);
                yield return new WaitForSeconds(0.8f);

                scoreAnimator.SetBool("Three", true);
                yield return new WaitForSeconds(0.8f);

                scoreAnimator.SetBool("Four", true);

                yield return new WaitForSeconds(0.8f);
                levelButtons.SetActive(true);
                break;

            case 4:
                scoreAnimator.SetBool("One", true);
                yield return new WaitForSeconds(0.8f);

                scoreAnimator.SetBool("Two", true);
                yield return new WaitForSeconds(0.8f);

                scoreAnimator.SetBool("Three", true);
                yield return new WaitForSeconds(0.8f);

                scoreAnimator.SetBool("Four", true);
                yield return new WaitForSeconds(0.8f);

                scoreAnimator.SetBool("Gold", true);

                yield return new WaitForSeconds(0.8f);
                levelButtons.SetActive(true);
                break;
        }
    }
}