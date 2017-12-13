using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tetris
{
    public class ScoreUI : MonoBehaviour
    {
        public int value = 100;
        public int score;
        public Text scoreText;



        // Use this for initialization
        void Start()
        {
            Grid.onRowsCleared += OnRowsClear;
        }

        // Update is called once per frame
        void Update()
        {

        }

        // Gets called everytime a row gets cleared
        void OnRowsClear(int rows)
        {
            score += value * rows;
            scoreText.text = "Score: " + score.ToString();
        }
    }
}

