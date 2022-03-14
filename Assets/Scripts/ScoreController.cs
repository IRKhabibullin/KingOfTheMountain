using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI finalScoreText;

    private int score = 0;

    public int Score {
        get { return score; }
        private set {
            score = value;
            scoreText.text = $"Score: {score}";
            finalScoreText.text = scoreText.text;
        }
    }

    public void ResetScore()
    {
        Score = 0;
    }

    public void IncreaseScore()
    {
        Score++;
    }
}
