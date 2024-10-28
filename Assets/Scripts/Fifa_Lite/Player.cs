using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    public AudioClip kick;
    public AudioSource audioSource;
    public Animator animator;
    
    private void Awake() {
        instance = this;
    }
    private void Start() {
        audioSource.clip = kick;
    }
    public void PlayAnimation() {
        animator.SetBool("Kick", true);
        audioSource.Play();
    }

    public void StopAnimation() {
        animator.SetBool("Kick", false);
    }
}
