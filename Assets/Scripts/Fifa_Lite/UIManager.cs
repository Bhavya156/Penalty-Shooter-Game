using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public TextMeshProUGUI player1Score, player2Score;
    private void Awake() {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Player1(int score) {
        player1Score.text = score.ToString();
    }

    public void Player2(int score) {
        player2Score.text = score.ToString();
    }
}
