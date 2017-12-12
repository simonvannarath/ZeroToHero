using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Snake
{
    public class GameManager : MonoBehaviour
    {
        [Header("UI")]
        public Text scoreText;

        public static GameManager Instance = null;
        public delegate void SpawnCallback();
        public SpawnCallback onSpawn;

        public delegate void ScoreCallback(int score);
        public ScoreCallback onScoreAdded;

        public int currentLevel = 0;   // Store for the current level
        public int score = 0;          // Keep record of the score

	    // Use this for initialization
	    private void Awake ()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            SceneManager.sceneLoaded += OnSceneWasLoaded; // Subscribe to scene changes
        }

        public void AddScore(int scoreToAdd)
        {
            if (currentLevel <= 0)
            {
                scoreText.text = "";
            }
            else
            {
                score += scoreToAdd;
                scoreText.text = "Score: " + score.ToString();
            }
        }

        public void Spawn()
        {
            // If there are subscribed functions
            if (onSpawn != null)
            {
                onSpawn.Invoke();   // Invoke them
            }
        }

        // Update is called once per frame
        public void ResetGame ()
        {
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name);
	    }

        public void LoadNextLevel()
        {
            currentLevel++;     // Increment next level

            // Check if next scene is valid
            if (currentLevel >= SceneManager.sceneCountInBuildSettings)
            {
                currentLevel = 0;                       // Load MainMenu otherwise
            }
            SceneManager.LoadScene(currentLevel);       // Loads next level
        }

        public void OnSceneWasLoaded(Scene scene, LoadSceneMode sceneMode)
        {
            if (scene.name == "MainMenu")
            {
                currentLevel = 0;
            }
        }
    }


}
