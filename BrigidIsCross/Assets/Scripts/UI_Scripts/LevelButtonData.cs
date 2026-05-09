using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LevelButtonData : MonoBehaviour
{
    public int score = 0;
    public bool completed = false;
    public bool completedPrior = false;
    [SerializeField] private GameObject ScoreImage;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private AudioClip wrongSFX;

    private void Awake()
    {
        if (completed)
        {
            ScoreImage.SetActive(true);

            switch (score)
            {
                case 0:
                    ScoreImage.GetComponent<Image>().sprite = sprites[0];
                    return;
                case 1:
                    ScoreImage.GetComponent<Image>().sprite = sprites[1];
                    return;
                case 2:
                    ScoreImage.GetComponent<Image>().sprite = sprites[2];
                    return;
                case 3:
                    ScoreImage.GetComponent<Image>().sprite = sprites[3];
                    return;
                case 4:
                    ScoreImage.GetComponent<Image>().sprite = sprites[4];
                    return;
            }

        }
    }

    public void PlayScene()
    {
        if (completedPrior)
        {
            string s = gameObject.name;
            SceneManager.LoadScene(s);
        }
        else
        {
            gameObject.GetComponent<AudioSource>().PlayOneShot(wrongSFX);
        }
    }

}
