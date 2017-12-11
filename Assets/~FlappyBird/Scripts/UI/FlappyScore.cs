using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FlappyBird
{
    public class FlappyScore : MonoBehaviour
    {
        public Sprite[] numbers;                            // Stores all the flappy digits
        public GameObject scoreTextPrefab;                  // Score prefab text element to create
        public Vector3 standbyPos = new Vector3(-15, 15);   // Position offscreen for standby
        public int maxDigits = 5;                           // The amount of digits to store offscreen for reuse

        private GameObject[] scoreTextPool;
        private int[] digits;

        // Use this for initialization
        void Start()
        {
            scoreTextPool = new GameObject[maxDigits];      // Allocate memory for the score text pool
            for (int i = 0; i < maxDigits; i++)             // Loop through all available digits
            {
                GameObject clone = Instantiate(scoreTextPrefab, standbyPos, Quaternion.identity);   // Create a new gameObject offscreen
                Image img = clone.GetComponent<Image>();                                            // Get the Image component attached to the clone
                img.sprite = numbers[i];                                                            // Set sprite to corresponding number sprite
                clone.transform.SetParent(transform);                                               // Attach to self
                clone.name = i.ToString();                                                          // Set name of text to index
                scoreTextPool[i] = clone;                                                           // Add it to pool
            }

            GameManager.Instance.scoreAdded += UpdateScore; 

            UpdateScore(0);                                                                     // Update score to start on zero
        }

        void UpdateScore(int score)
        {
            int[] digits = GetDigits(score);                    // Convert score into array of digits

            // Loop through all digits
            for (int i = 0; i < digits.Length; i++)
            {

                int value = digits[i];                          // Get value of each digit
                GameObject textElement = scoreTextPool[i];      // Get corresponding text element in pool
                Image img = textElement.GetComponent<Image>();  // Get image component attached to it
                img.sprite = numbers[value];                    // Assign sprite to number using value
                textElement.SetActive(true);                    // Activate text element
            }

            // Loop through all remaining text elements in the pool
            for (int i = digits.Length; i < scoreTextPool.Length; i++)
            {

                scoreTextPool[i].SetActive(false);              // Deactivate that element
            }
        }

        int[] GetDigits(int number)
        {
            List<int> digits = new List<int>();
            while (number >= 10)
            {
                digits.Add(number % 10);
                number /= 10;
            }

            digits.Add(number);
            digits.Reverse();
            return digits.ToArray();
        }
    }
}

