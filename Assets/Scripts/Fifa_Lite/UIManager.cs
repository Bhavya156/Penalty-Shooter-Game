using TMPro;
using UnityEngine;
public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public TextMeshProUGUI player1Score, player2Score;
    private void Awake() {
        instance = this;
    }

    public void Player1(int score) {
        player1Score.text = score.ToString();
    }

    public void Player2(int score) {
        player2Score.text = score.ToString();
    }
}
