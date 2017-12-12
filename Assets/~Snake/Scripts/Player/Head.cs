using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;

namespace Snake
{
    public class Head : MonoBehaviour
    {
        public float moveRate = 0.3f;                               // Movement interval
        public float sprintRate = 0.1f;                             // Sprint interval
        public GameObject tailPrefab;                               // Prefab of tail to spawn
        public float keyDownDuration = 1f;                          // How long does a key have to be down before sprinting?

        private float keyDownTimer = 0f;                            // How long has any key been pressed?
        private float moveTimer = 0f;                               // Timer to keep track of elapsed time
        public Vector2 direction = Vector2.right;                   // Movement direction of snake (Right by default)
        private bool hasEaten = false;                              // Has the snake eaten?
        private List<Transform> tail = new List<Transform>();       // List to keep track of tails
        private float interval = 0f;                                // moveRate/sprintRate backing store

	    public void Sprint()
        {
                keyDownTimer += Time.deltaTime;                     // Count how long a key is down for

                // If key has been down for a set time (duration)
                if (keyDownTimer >= keyDownDuration)
                {
                    // Snake is now running
                    interval = sprintRate;
                }
            
        }

        public void Walk()
        {
            keyDownTimer = 0f; // Reset the key down timer
            interval = moveRate; // Reset the move speed
        }

        void AppendTail(Vector3 gapPos)
        {
            GameObject clone = Instantiate(tailPrefab, gapPos, Quaternion.identity);    // Load prefab into the world
            tail.Insert(0, clone.transform);                                            // Keep of it in our tail list
            hasEaten = false;                                                            // Reset the flag (the snake is now NEVER satisfied)
        }

        void RefreshTail(Vector3 gapPos)
        {
            // Do we have a tail?
            if (tail.Count > 0)
            {
                tail.Last().position = gapPos;  // Move the last Tail element to where the Head's old position was
                tail.Insert(0, tail.Last());    // Add to fron of list
                tail.RemoveAt(tail.Count - 1);   // Remove from the back
            }
        }

        void Move()
        {
            Vector2 gapPos = transform.position;    // Save current position
            transform.Translate(direction);         // Move head into the new direction

            // Has the snake eaten something?
            if (hasEaten)
            {
                AppendTail(gapPos);                 // Append size of the tail
            }

            else
            {
                RefreshTail(gapPos);                // Refresh the tail position
            }
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            // If collided with Food
            if (other.name.Contains("Food"))
            {
                hasEaten = true;                    // Get longer in the next Move call
                Destroy(other.gameObject);          // Remove the food
                GameManager.Instance.Spawn();       // Tell Manager to spawn things
                GameManager.Instance.AddScore(1);   // Tell GameManager we scored!
            }

            else
            {
                GameManager.Instance.ResetGame();   // Game Over
            }
        }
        
        
        // Update is called once per frame
        void Update ()
        {
            moveTimer += Time.deltaTime;    // Count up the timer

            // Is it time to move?
            if (moveTimer > interval)
            {
                Move();     // Move the snake
                moveTimer = 0f; // Reset the timer
            }
	    }
    }
}

