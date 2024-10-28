using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public static Goal instance;
    public Animator animator;
    public AudioClip goalClip;
    public AudioSource audioSource;
    public int player1Score, player2Score;
    public TextMeshProUGUI whoWonText;
    private void Awake() {
        instance = this;
    }
    private void Start() {
        audioSource.clip = goalClip;
    }
    private void Update() {
        WhoWon();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ball")
        {
            // Debug.Log("Goal");
            string playerName = Player.instance.gameObject.name;
            if (playerName == "Player1")
            {
                player1Score++;
                UIManager.instance.Player1(player1Score);
            }
            else if (playerName == "Player2")
            {
                player2Score++;
                UIManager.instance.Player2(player2Score);
            }
            StartCoroutine(GoalAnimation());
        }
    }

    IEnumerator GoalAnimation()
    {
        animator.SetBool("Goal", true);
        audioSource.Play();
        yield return new WaitForSeconds(2f);
        animator.SetBool("Goal", false);
    }
    void WhoWon()
    {
        if (player1Score > player2Score)
        {
            whoWonText.text = "Player 1 won the game";
        }
        else if (player1Score < player2Score)
        {
            whoWonText.text = "Player 2 won the game";
        }
        else
        {
            whoWonText.text = "Nobody Won. Better Luck next time";
        }
    }
}
