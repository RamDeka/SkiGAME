using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class GameData : MonoBehaviour
{
    public List<float> bestTimes = new List<float>();
    private static GameData instance;
    [SerializeField] private string leaderboardKey = "LeaderboardLVL1-";

    [Header("UI Setup")]
    [SerializeField] private TextMeshProUGUI textPrefab;
    [SerializeField] private Transform layoutGroupContainer;

    public static GameData Instance => instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        LoadLeaderboard();
        UpdateUI();
    }

    private void LoadLeaderboard()
    {
        bestTimes.Clear();
        for (int i = 0; i < 5; i++)
        {
            float time = PlayerPrefs.GetFloat(leaderboardKey + i, 999.99f);
            bestTimes.Add(time);
        }
        bestTimes.Sort();
    }

    private void SaveLeaderboard()
    {
        for (int i = 0; i < 5; i++)
        {
            if (i < bestTimes.Count)
                PlayerPrefs.SetFloat(leaderboardKey + i, bestTimes[i]);
        }
        PlayerPrefs.Save();
    }

    public void AddLevelTime(float time)
    {
        bestTimes.Add(time);
        bestTimes.Sort();
        if (bestTimes.Count > 5) bestTimes.RemoveAt(5);
        SaveLeaderboard();
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (layoutGroupContainer == null || textPrefab == null) return;

        foreach (Transform child in layoutGroupContainer)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < bestTimes.Count; i++)
        {
            if (bestTimes[i] >= 999.99f) continue;
            
            TextMeshProUGUI newText = Instantiate(textPrefab, layoutGroupContainer);
            newText.text = $"{i + 1}. {bestTimes[i]:F2}s";
        }
    }
}