using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    private int score = 0;

    public void IncreaseScore()
    {
        score++;
        scoreText.text = $"Score: {score}";
    }
}
