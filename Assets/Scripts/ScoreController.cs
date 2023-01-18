using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [SerializeField] private TMP_Text uiScoreText;
    [SerializeField] private BallController ballController;
    private int _score;
    
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        var direction = gameObject.CompareTag("RightGoal") ? Vector2.left : Vector2.right;
        AddScore();
        ballController.Serve(direction);
    }

    private void AddScore()
    {
        _score++;
        uiScoreText.text = _score.ToString();
    }
    
}
