using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelButtonData : MonoBehaviour
{
    public int sceneIndex;

    public int score = 0;
    public bool completed = false;
    public bool completedPrior = false;

    [SerializeField] private GameObject ScoreImage;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private AudioClip wrongSFX;

    private void Start()
    {
        score = SaveSystem.Instance.GetLevelScore(sceneIndex);
        completed = SaveSystem.Instance.IsLevelBeaten(sceneIndex);
        completedPrior = SaveSystem.Instance.IsLevelUnlocked(sceneIndex);

        ScoreImage.SetActive(completed);
        if (completed)
        {
            Image image = ScoreImage.GetComponent<Image>();

            score = Mathf.Clamp(score, 0, 4);

            if (score == 0) image.sprite = sprites[score];
            else
            {
                image.sprite = sprites[score+1];
            }

            if (score == 4) image.color = new Color32(255, 215, 0, 255);
        }
    }

    public void PlayScene()
    {
        if (completedPrior)
        {
            SceneManager.LoadScene(sceneIndex);
        }
        else
        {
            GetComponent<AudioSource>().PlayOneShot(wrongSFX);
        }
    }
}