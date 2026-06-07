using System;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using TMPro;

public class GameManager : MonoBehaviour
{
    public delegate void TimerEvent();

    private DateTime raceStart;
    private TimeSpan raceTime;
    private TimeSpan bestTime;
    private TimeSpan penaltyTime;
    private bool raceFinish = false;
    private bool racing = false;
    
    [SerializeField] private TMP_Text timerText, bestTimeText;
    
    [SerializeField] private string bestTimeKey = "BestTimeLVL1";

    private void Start()
    {
        int bestTimeInt = PlayerPrefs.GetInt(bestTimeKey, int.MaxValue);
        bestTime = new TimeSpan(bestTimeInt);
        
        bestTimeText.text = "Best time: "+ bestTime.ToString("mm\\:ss");
    }
    
    
    private void OnEnable()
    {
        FinishGate.FinishRace += FinishRace;
        StartGate.StartRace += StartRace;
        SlalomFlag.RacePenalty += AddRacePenalty;
    }
    
    private void OnDisable()
    {
        FinishGate.FinishRace -= FinishRace;
        StartGate.StartRace -= StartRace;
        SlalomFlag.RacePenalty -= AddRacePenalty;
    }

    void AddRacePenalty()

    {
        penaltyTime += new TimeSpan(0, 0, 3);
    }

    void FinishRace()
    {
        raceFinish = false;
        Debug.Log("Finish Race");
        GameData.Instance.AddLevelTime((float)raceTime.TotalMilliseconds / 1000);
        if (raceTime < bestTime)
        {
            bestTimeText.text = "Best time: " + raceTime.ToString("mm\\:ss");
            PlayerPrefs.SetInt(bestTimeKey, (int) raceTime.Ticks);
            PlayerPrefs.Save();
        }
    }

    void StartRace()
    {
        racing = true;
        raceStart = DateTime.Now;
        Debug.Log("Start Race");
    }

    void Update()
    {
     if(racing) 
         raceTime = DateTime.Now - raceStart + penaltyTime;
     timerText.text = "Time:"+ raceTime.ToString("mm\\:ss");
    }
}
