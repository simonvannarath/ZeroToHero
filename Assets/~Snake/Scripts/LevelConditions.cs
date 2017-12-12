using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

namespace Snake
{
    [System.Serializable]
    public class Level
    {
        public enum Condition
        {
            ScoreAndTimer   = 0,
            ScoreOrTimer    = 1,
            JustScore       = 2,
            JustTimer       = 3
        }

        public Condition condition;
        public int scoreRequired = 2;
        public float timerRequired = 5;

        public bool IsTrue(int currentScore, float currentTimer)
        {
            switch (condition)
            {
                case Condition.ScoreAndTimer:
                    return currentScore >= scoreRequired && currentTimer >= timerRequired;
                case Condition.ScoreOrTimer:
                    return currentScore >= scoreRequired || currentTimer >= timerRequired;
                case Condition.JustScore:
                    return currentScore >= scoreRequired;
                case Condition.JustTimer:
                    return currentTimer >= timerRequired;
                default:
                    break;
            }

            return false;       // Return false if error
        }
    }

    

    public class LevelConditions : MonoBehaviour
    {
        public Level[] levels;
        private float elapsedTime = 0f;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            int currentLevel = GameManager.Instance.currentLevel;
            if (currentLevel <= 0 || currentLevel > levels.Length)
                return;
            int score = GameManager.Instance.score;

            Level l = levels[currentLevel - 1];
            if(l.IsTrue(score, elapsedTime))
            {
                GameManager.Instance.LoadNextLevel();
            }
        }

        void OnSceneWasLoaded(Scene scene, LoadSceneMode sceneMode)
        {
            // Reset on scene load
            elapsedTime = 0f;
        }
    }
}