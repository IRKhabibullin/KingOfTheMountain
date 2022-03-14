using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;

public class RatingController : MonoBehaviour
{
    [SerializeField] private GameObject scoreInstancePrefab;
    [SerializeField] private Transform scoresList;

    private const string SAVE_FILE_PATH = "rating";
    private List<ScoreResult> scoresRating;

    public void DisplayRating()
    {
        if (scoresRating == null)
        {
            LoadRating();
        }

        ClearScoreList();

        foreach (var scoreResult in scoresRating)
        {
            AddScoreInstance(scoreResult);
        }
    }

    private void ClearScoreList()
    {
        foreach (Transform child in scoresList)
        {
            Destroy(child.gameObject);
        }
    }

    private void AddScoreInstance(ScoreResult scoreResult)
    {
        var scoreInstance = Instantiate(scoreInstancePrefab, scoresList);
        scoreInstance.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = scoreResult.playerName;
        scoreInstance.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = scoreResult.score.ToString();
    }

    public void SaveScore(string playerName, int score)
    {
        if (scoresRating == null)
        {
            LoadRating();
        }
        scoresRating.Add(new ScoreResult(playerName, score));
        scoresRating.Sort((a, b) => { return b.score.CompareTo(a.score); });

        Stream stream = File.Open(SAVE_FILE_PATH, FileMode.OpenOrCreate);
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(stream, scoresRating);
        stream.Close();
    }

    public void LoadRating()
    {
        try
        {
            if (HasRatingFile())
            {
                Stream stream = File.Open(SAVE_FILE_PATH, FileMode.Open);
                BinaryFormatter formatter = new BinaryFormatter();
                scoresRating = (List<ScoreResult>)formatter.Deserialize(stream);
                stream.Close();
            }
            else
            {
                scoresRating = new List<ScoreResult>();
            }
        }
        catch (Exception) { }
    }

    public static bool HasRatingFile()
    {
        return File.Exists(SAVE_FILE_PATH);
    }
}

[Serializable]
public class ScoreResult
{
    public string playerName;
    public int score;

    public ScoreResult(string playerName, int newScore)
    {
        this.playerName = playerName;
        this.score = newScore;
    }
}
