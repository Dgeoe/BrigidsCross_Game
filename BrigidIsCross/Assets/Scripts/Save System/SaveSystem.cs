using System.Collections.Generic;
using System.IO;
using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem Instance;

    private string savePath;

    public SaveData saveData;

    private const int FIRST_PLAYABLE_SCENE = 2;
    private int LastPlayableScene => SceneManager.sceneCountInBuildSettings - 2;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        savePath = Path.Combine(Application.persistentDataPath, "save.json");

        CreateNewSave();
    }

    private void CreateNewSave()
    {
        saveData = new SaveData();

        int playableLevelCount =
            LastPlayableScene - FIRST_PLAYABLE_SCENE + 1;

        saveData.levels = new List<LevelData>();

        for (int i = 0; i < playableLevelCount; i++)
        {
            LevelData level = new LevelData();

            level.beaten = false;
            level.score = 0;

            level.unlocked = (i == 0);

            saveData.levels.Add(level);
        }

        SaveGame();
    }

    public void SaveGame()
    {
        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(savePath, json);
    }

    public void LoadGame()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            saveData = JsonUtility.FromJson<SaveData>(json);
        }
        else
        {
            CreateNewSave();
        }
    }

    public void CompleteLevel(int sceneIndex, int score)
    {
        int levelIndex = sceneIndex - FIRST_PLAYABLE_SCENE;

        if (levelIndex < 0 || levelIndex >= saveData.levels.Count)
            return;

        LevelData current = saveData.levels[levelIndex];

        current.beaten = true;

        if (score > current.score) current.score = Mathf.Clamp(score, 0, 4);

        if (levelIndex + 1 < saveData.levels.Count)
        {
            saveData.levels[levelIndex + 1].unlocked = true;
        }

        SaveGame();
    }

    public bool IsLevelUnlocked(int sceneIndex)
    {
        int levelIndex = sceneIndex - FIRST_PLAYABLE_SCENE;

        if (levelIndex < 0 || levelIndex >= saveData.levels.Count)
            return false;

        return saveData.levels[levelIndex].unlocked;
    }

    public bool IsLevelBeaten(int sceneIndex)
    {
        int levelIndex = sceneIndex - FIRST_PLAYABLE_SCENE;

        if (levelIndex < 0 || levelIndex >= saveData.levels.Count)
            return false;

        return saveData.levels[levelIndex].beaten;
    }

    public int GetLevelScore(int sceneIndex)
    {
        int levelIndex = sceneIndex - FIRST_PLAYABLE_SCENE;

        if (levelIndex < 0 || levelIndex >= saveData.levels.Count)
            return 0;

        return saveData.levels[levelIndex].score;
    }
    public void DeleteSave()
    {
        if (File.Exists(savePath)) File.Delete(savePath);

        CreateNewSave();
    }
}