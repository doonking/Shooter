﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour {

    public Image fadePlane;
    public GameObject gameOverUI;

    public RectTransform newWaveBanner;
    public Text newWaveTitle;

    Spawner spawner;

	// Use this for initialization
	void Start () {
        FindObjectOfType<Player>().OnDeath += OnGameOver;
	}

    void Awake()
    {
        spawner = FindObjectOfType<Spawner>();
        spawner.OnNewWave += OnNewWave;
    }

    void OnNewWave(int waveNumber)
    {
        string[] numbers = { "One", "Two", "Three", "Four", "Five" };
        newWaveTitle.text = "Stage " + numbers[waveNumber - 1];

        StartCoroutine(AnimateNewWaveBanner());
    }
    void OnGameOver()
    {
        StartCoroutine(Fade(Color.clear, Color.black, 1));
        gameOverUI.SetActive(true);
    }

    IEnumerator AnimateNewWaveBanner()
    {
        float delayTime = 1f;
        float speed = 2.5f;
        float animatePercent = 0;
        int dir = 1;

        float endDelayTime = Time.time + 1 / speed + delayTime;
        
        while ( animatePercent >= 0)
        {
            animatePercent += Time.deltaTime * speed * dir;
            
            if (animatePercent >= 0)
            {
                
                if(animatePercent >= 1)
                {
                    animatePercent = 1;
                    if(Time.time > endDelayTime)
                    {
                        dir = -1;
                    }
                }

                newWaveBanner.anchoredPosition = Vector2.up * Mathf.Lerp(-500, 0, animatePercent);
                yield return null;
            }
        }
    }

    IEnumerator Fade(Color from, Color to, float time)
    {
        float speed = 1 / time;
        float percent = 0;

        while (percent < 1)
        {
            percent += Time.deltaTime * speed;
            fadePlane.color = Color.Lerp(from, to, percent);
            yield return null;
        }
    }

    //UI Input
    public void StartNewGame()
    {
        SceneManager.LoadScene("Game");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
