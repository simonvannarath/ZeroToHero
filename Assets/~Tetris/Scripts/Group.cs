using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tetris
{
    public class Group : MonoBehaviour
    {
        public float fallInterval = 1f;
        public float fastInterval = 0.25f;
        public float holdDuration = 1f;


        private float holdTimer = 0f;
        private float fallTimer = 0f;
        private bool isFallingFaster = false;
        private bool isSpacePressed = false;
        private Spawner spawner;

        // Use this for initialization
        void Start()
        {
            spawner = FindObjectOfType<Spawner>();                              // Find the current spawner in the scene

            // Check if null
            if (spawner == null)
            {
                Debug.LogError("Spawner does not exist in the current scene");  // Display error
                Debug.Break();

            }
            // Check if game over
            if (!IsValidGridPos())
            {
                Grid.GameOver(); // Game is over!

            }
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isSpacePressed = true;
            }

            if (!isSpacePressed)
            {
                MoveLeftOrRight();
                MoveUpOrDown();
                fallTimer += Time.deltaTime;
                float currentInterval = isFallingFaster ? fastInterval : fallInterval; // Ternary operator
                if (fallTimer >= fallInterval)
                {
                    Fall();
                    fallTimer = 0f;
                }
            }

            else
            {
                Fall();
            }
        }

        // Detect if group's position is valid in relation to Grid
        bool IsValidGridPos()
        {
            // Loop through all children in group
            foreach (Transform child in transform)
            {
                Vector2 v = Grid.RoundVec2(child.position);                     // Round the child's position

                // Not inside border?
                if (!Grid.InsideBorder(v))
                {
                    return false;
                }

                // Truncate position
                int x = (int)v.x;
                int y = (int)v.y;

                // If cell is NOT empty AND not part of the same group
                if (Grid.grid[x, y] != null && Grid.grid[x, y].parent != transform)
                {
                    return false;
                }
            }

            return true;                                                        // All other cases return true! Which means it's a valid position
        }

        // Remove all elements in grid (set them to null) and re-add the new positions
        void UpdateGrid()
        {
            //  remove old children from grid
            for (int x = 0; x < Grid.width; x++)
            {
                for (int y = 0; y < Grid.height; y++)
                {
                    if (Grid.grid[x, y] != null && Grid.grid[x, y].parent == transform)
                    {
                        Grid.grid[x, y] = null; // Remove it from grid
                    }
                }
            }

            // Add new children positions to grid
            foreach (Transform child in transform)
            {
                Vector2 v = Grid.RoundVec2(child.position);                     // Round the child's position

                // Truncate position
                int x = (int)v.x;
                int y = (int)v.y;
                Grid.grid[x, y] = child;                                        // Set the coordinate to child
            }
        }

        void MoveLeftOrRight()
        {
            Vector3 moveDir = Vector3.zero;                                     // Direction to move

            // Is going left?
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                moveDir = new Vector3(-1, 0, 0);                                // Move left
            }

            // Is going right?
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {

                moveDir = new Vector3(1, 0, 0);                                 // Move right
            }

            // Is there a movement?
            if (moveDir.magnitude > 0)
            {
                transform.position += moveDir;                                  // Move the group in that direction

                // See if valid
                if (IsValidGridPos())
                {
                    UpdateGrid();                                               // It's valid, update the grid
                }
                else
                {
                    transform.position += -moveDir;                             // It's NOT valid, revert
                }
            }
        }

        void MoveUpOrDown()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                transform.Rotate(0, 0, -90);

                // See if valid
                if (IsValidGridPos())
                {
                    UpdateGrid();                                               // It's valid, update grid
                }

                else
                {
                    transform.Rotate(0, 0, 90);
                }
            }

            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                transform.position += new Vector3(0, -1, 0);                    // Modify position

                // See if valid
                if (IsValidGridPos())
                {
                    UpdateGrid();                                               // It's valid, update grid
                }
                else
                {
                    transform.position += new Vector3(0, 1, 0);                 // It's NOT valid, revert.
                    //DetectFullRow();
                }
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                holdTimer += Time.deltaTime;

                if (holdTimer >= holdDuration)
                {
                    isFallingFaster = true;

                }
            }

            if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                isFallingFaster = false;
                holdTimer = 0;
            }
        }

        void DetectFullRow()
        {
            Grid.DeleteFullRows();                                              // Clear any rows that are filled.
            spawner.SpawnNext();                                                // Spawn the next group
            enabled = false;                                                    // Disable script
        }

        void Fall()
        {
            transform.position += new Vector3(0, -1, 0);                        // Modify position

            // See if valid
            if (IsValidGridPos())
            {
                UpdateGrid();                                                   // It's valid, update grid
            }
            else
            {
                transform.position += new Vector3(0, 1, 0);                     // It's NOT valid, revert.
                DetectFullRow();                                                // Detect full row
            }
        }

    }
}

