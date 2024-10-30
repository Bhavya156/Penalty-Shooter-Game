using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Pool
{
    public string tag;
    public GameObject ballPrefab;
    public int size;
}

public class SpawnBalls : MonoBehaviour
{
    public AudioClip begin;
    public AudioSource audioSource;
    public Animator swapAnimator;
    public GameObject gameOverPanel;
    public GameObject scoreBoard;

    public bool isBallPresent;
    public GameObject ballContainer;

    public int ballsPerPlayer = 5;
    public int ballsUsedByPlayer;
    public GameObject player1;
    public GameObject player2;
    public GameObject goalKeeper1;
    public GameObject goalKeeper2;

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    private void Start()
    {
        audioSource.clip = begin;
        audioSource.Play();
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.ballPrefab);
                obj.SetActive(false);
                // obj.transform.SetParent(ballContainer.transform);
                objectPool.Enqueue(obj);
            }
            poolDictionary.Add(pool.tag, objectPool);
        }
        SpawnFootBall();
    }

    private void Update()
    {
        if (player2.activeSelf && ballsUsedByPlayer >= ballsPerPlayer)
        {
            StartCoroutine(GameOverRoutine());
            return;
        }
        else
        {
            if (!isBallPresent)
            {
                if (ballsUsedByPlayer >= ballsPerPlayer)
                {
                    SwapPlayer();
                    ballsUsedByPlayer = 0;
                }

                Invoke("SpawnFootBall", 2f);
                isBallPresent = true;
            }
        }
    }

    void SwapPlayer()
    {
        player1.SetActive(false);
        StartCoroutine(SwapPlayers());
        player2.SetActive(true);

        goalKeeper1.SetActive(false);
        goalKeeper2.SetActive(true);
    }

    void SpawnFootBall()
    {
        if (pools.Count > 0)
        {
            string defaultTag = pools[0].tag;
            if (!poolDictionary.ContainsKey(defaultTag))
            {
                Debug.LogWarning("Pool with tag " + defaultTag + " doesn't exist");
                return;
            }

            GameObject objectToSpawn = poolDictionary[defaultTag].Dequeue();

            objectToSpawn.SetActive(true);
            objectToSpawn.transform.position = ballContainer.transform.position;
            objectToSpawn.transform.rotation = Quaternion.identity;

            isBallPresent = true;

            poolDictionary[defaultTag].Enqueue(objectToSpawn);
        }
    }

    IEnumerator SwapPlayers() {
        yield return new WaitForSeconds(1f);
        swapAnimator.SetBool("Swap", true);
        yield return new WaitForSeconds(3f);
        swapAnimator.SetBool("Swap", false);
        yield return new WaitForSeconds(1f);
    }

    IEnumerator GameOverRoutine()
    {
        yield return new WaitForSeconds(2.5f);
        Time.timeScale = 0f;
        scoreBoard.SetActive(false);
        gameOverPanel.SetActive(true);
    }
}
