﻿using System;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts
{
    public class GameData : MonoBehaviour
    {
        public static GameData Singleton;

        public Text scoreText;
        public Text highscoreText;
        private int _score;
        private int _highScore;

        private void Awake()
        {
            // If this scene creates a new version of the game object, destroy it.
            // This solves the problem of creating a new instance of the game object
            // whenever we enter a scene that contains a reference to it.
            var gd = GameObject.FindGameObjectsWithTag("GameData");
            if (gd.Length > 1)
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);
            Singleton = this;

            PlayerPrefs.SetInt("score", 0);
        }

        public void AddScore(int value)
        {
            _score += value;
            PlayerPrefs.SetInt("score", _score);
            PlayerPrefs.SetInt("lastScore", _score);
            UpdateScoreDisplay();
            UpdateHighscore();
        }

        private void UpdateHighscore()
        {
            if (!PlayerPrefs.HasKey("highscore"))
            {
                _highScore = 0;
                PlayerPrefs.SetInt("highscore", _highScore);
                return;
            }

            var oldHighscore = PlayerPrefs.GetInt("highscore");
            _highScore = Math.Max(oldHighscore, _score);
            PlayerPrefs.SetInt("highscore", _highScore);
        }

        public void ResetScore()
        {
            _score = 0;
            PlayerPrefs.SetInt("score", 0);
            UpdateScoreDisplay();
        }

        private void UpdateScoreDisplay()
        {
            if (scoreText != null)
            {
                scoreText.text = $"Score: {_score}";
            }

            if (highscoreText != null)
            {
                highscoreText.text = $"Highscore: {_highScore}";
            }
        }
    }
}