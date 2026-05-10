using UnityEngine;
using System.Collections;

public class EndGame_Manager : MonoBehaviour
{
    //Handles Win/Lose States In the game
    [SerializeField] private int enemyCount;
    [SerializeField] private int leastThrows;
    [SerializeField] private GameObject ScoreScreen;
    [SerializeField] private AudioSource Music;
    private int enemiesKilled;
    private int currentThrows;
    private int mostThrows;
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
            StartCoroutine(LowerMusic());
            ScoreScreen.SetActive(true);
        }
    }

    public void ThrowCheck()
    {
        currentThrows++;

        if (currentThrows == mostThrows)
        {
            Debug.Log("Lose");
            Throw.Instance.enabled = false;
            StartCoroutine(LowerMusic());
            ScoreScreen.SetActive(true);
        }
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
}