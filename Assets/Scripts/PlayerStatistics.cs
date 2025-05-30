using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public struct LevelInfo
{
    public bool completed;
    public double levelTime;
    public int hintsRequested;
	public int starRating;

    public LevelInfo(bool _completed = false, double _levelTime = 0, int _hintsRequested = 0, int _starRating = 0)
    {
        completed = _completed;
        levelTime = _levelTime;
        hintsRequested = _hintsRequested;
		starRating = _starRating;
    }
}

public static class PlayerStatistics
{
    public static Dictionary<string, LevelInfo> levels = new Dictionary<string, LevelInfo>();
    private static Stopwatch timer;
    private static string currentLevel = "";

    public static void EnterLevel(string levelName)
    {
        currentLevel = levelName;
		LevelInfo thisLevel;
		if(!levels.ContainsKey(levelName))
		{
			thisLevel = new LevelInfo();
		}
		else
		{
			thisLevel = levels[levelName];
			thisLevel.hintsRequested = 0;
			thisLevel.levelTime = 0;
		}
        // if (!levels.ContainsKey(levelName)) levels.Add(levelName, new LevelInfo());

        // LevelInfo thisLevel = levels[levelName];
		levels[levelName] = thisLevel;
        if (!thisLevel.completed) StartLevelTimer(levelName);
    }

    public static void CompletePuzzleLevel()
    {
        if (currentLevel == "") return;
        LevelInfo thisLevel = levels[currentLevel];
        thisLevel.completed = true;

		int newStars = CalculateStars(thisLevel.hintsRequested);
		Debug.Log($"[PlayerStatistics] Stars: {newStars}");
		if(newStars > thisLevel.starRating){
			Debug.Log($"[PlayerStatistics] Updating star rating from {thisLevel.starRating} to {newStars}");
			thisLevel.starRating = newStars;
		}

		else
		{
			Debug.Log($"[PlayerStatistics] Star rating not updated");
		}

        levels[currentLevel] = thisLevel;
    }

	public static int CalculateStars(int hintsUsed)
	{
		if (hintsUsed == 0) return 3;
		if (hintsUsed == 1 || hintsUsed == 2) return 2;
		return 1;
	}


    public static void StartLevelTimer(string levelName)
    {
        timer = new Stopwatch();
        timer.Start();
    }

    public static void StopLevelTimer()
    {
        if (timer != null && currentLevel != "")
        {
            timer.Stop();
            LevelInfo thisLevel = levels[currentLevel];
            thisLevel.levelTime += timer.Elapsed.TotalSeconds;
            levels[currentLevel] = thisLevel;
        }
    }

    public static void HintUsed()
    {
        if (currentLevel == "") return;
        LevelInfo thisLevel = levels[currentLevel];
        ++thisLevel.hintsRequested;
        levels[currentLevel] = thisLevel;
    }
}
