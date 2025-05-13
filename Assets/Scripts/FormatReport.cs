using System.Collections.Generic;
using UnityEngine;
using System;

public static class ReportFormatter
{
    public static string GetFormattedReport()
    {
        string report = "Puzzle Report:\n\n";

        foreach (var level in PlayerStatistics.levels)
        {
            string name = level.Key;
            string time = SecondsToString(level.Value.levelTime);
            string hints = level.Value.hintsRequested.ToString();
            string completed = level.Value.completed ? "Yes" : "No";

            report += $"Level: {name}\nTime: {time}\nHints: {hints}\nCompleted: {completed}\n\n";
        }

        return report;
    }

    private static string SecondsToString(double totalSeconds)
    {
        int hours = Mathf.FloorToInt((float)totalSeconds / 3600);
        int minutes = Mathf.FloorToInt((float)totalSeconds / 60);
        int seconds = Mathf.FloorToInt((float)totalSeconds) % 60;
        return String.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
    }
}

