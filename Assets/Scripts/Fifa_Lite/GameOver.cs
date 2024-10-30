using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public static GameOver instance;
    public TextMeshProUGUI player1Score, player2Score;
    private void Awake() {
        instance = this;
    }
    void Update()
    {
        Player1Score();
        Player2Score();
    }

    void Player1Score() {
        player1Score.text = Goal.instance.player1Score.ToString();
    }

    void Player2Score() {
        player2Score.text = Goal.instance.player2Score.ToString();
    }
}
