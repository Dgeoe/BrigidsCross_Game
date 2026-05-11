using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectScreen_Manager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private RectTransform StartPos;
    [SerializeField] private GameObject Prefab;

    [Header("Layout Settings")]
    [SerializeField] private float xSpacing = 250f;
    [SerializeField] private float ySpacing = 250f;
    [SerializeField] private int buttonsPerRow = 4;

    private void Start()
    {
        SpawnLevelButtons();
    }

    private void SpawnLevelButtons()
    {
        int sceneCount = SceneManager.sceneCountInBuildSettings;

        // Skip first 2 scenes (Device Check & Main Menu) and stop before last scene (thanks for playing)
        for (int i = 2; i < sceneCount - 1; i++)
        {
            int adjustedIndex = i - 2;

            int column = adjustedIndex % buttonsPerRow;
            int row = adjustedIndex / buttonsPerRow;

            Vector2 pos = new Vector2(StartPos.anchoredPosition.x + (column * xSpacing), StartPos.anchoredPosition.y - (row * ySpacing));

            GameObject buttonObj = Instantiate(Prefab, StartPos.parent);
            buttonObj.GetComponent<LevelButtonData>().sceneIndex = i;
            RectTransform rect = buttonObj.GetComponent<RectTransform>();
            rect.anchoredPosition = pos;

            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
            buttonObj.name = sceneName;
        }
    }
}
