using System.Collections;
using UnityEngine;

public class GoalKeeper : MonoBehaviour
{
    public static GoalKeeper instance;
    public Animator animator;
    private Transform _ball;
    private Vector3 newPos;
    public bool isBallShot;
    private void Awake() {
        instance = this;
    }

    void Update()
    {
        if (isBallShot)
            Movement();
    }

    void Movement() {
        newPos = _ball.position - transform.position;

        //Actions
        if (newPos.magnitude < 3.8f) {
            PlayRandomAnimation();
        }
    }

    void PlayRandomAnimation() {
        string[] animations = {"SideStep", "DiveRight", "Catch"};

        string playAnimation = animations[Random.Range(0, animations.Length)];

        animator.SetBool(playAnimation, true);
    }

    public void ReactToShot(Transform ballTranform) {
        _ball = ballTranform;
        isBallShot = true;

        StartCoroutine(ResetBallRoutine());
    }

    IEnumerator ResetBallRoutine() {
        yield return new WaitForSeconds(4f);

        isBallShot = false;
        _ball = null;
        ResetAnimation();
    }

    void ResetAnimation() {
        animator.SetBool("SideStep", false);
        animator.SetBool("DiveRight", false);
        animator.SetBool("Catch", false);
    }
}
