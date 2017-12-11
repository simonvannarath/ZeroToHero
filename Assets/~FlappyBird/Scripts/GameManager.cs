using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlappyBird
{
    public class GameManager : MonoBehaviour
    {
        public bool gameOver = false;
        public float scrollSpeed = - 1.5f;
        public int score = 0;

        public static GameManager Instance = null;

        public delegate void ScoreAddedCallback(int score);
        public ScoreAddedCallback scoreAdded;

	    // Use this for initialization
	    void Awake ()
        {
		    if (Instance == null)
            {
                Instance = this;
            }
	    }
	
	    // Update is called once per frame
	    void Update ()
        {
		
	    }

        public void BirdScored()
        {
            // Ths bird cannot score if the game is over
            if (gameOver)
            {
                return;                     // Exit the function
            }

            score++;                        // Increase the score

            // If there is a function subscribed
            if (scoreAdded != null)
            {
                scoreAdded.Invoke(score);       // Call an event to state that a score has been added
            }
        }

        public void BirdDied()
        {
            gameOver = true;                // Set gameOver to true
        }
    }
}

