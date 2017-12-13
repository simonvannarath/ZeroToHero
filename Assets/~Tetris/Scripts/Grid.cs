using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Tetris
{
    public class Grid : MonoBehaviour
    {
        public UnityEvent onGameOver;
        public static int width = 10, height = 20;
        public static Grid Instance = null;
        public static Transform[,] grid = new Transform[width, height];


        public delegate void RowsClearedCallback(int rows);
        public static RowsClearedCallback onRowsCleared;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }
        void OnDrawGizmos()
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Gizmos.DrawWireCube(new Vector2(x, y), Vector2.one);
                }
            }
        }

        public static Vector2 RoundVec2(Vector2 v)
        {
            float roundX = Mathf.Round(v.x);
            float roundY = Mathf.Round(v.y);
            return new Vector2(roundX, roundY);
        }

        public static bool InsideBorder(Vector2 pos)
        {
            // Truncate the vector
            int x = (int)pos.x;
            int y = (int)pos.y;

            if (x >= 0 && x < width &&
                y >= 0)
            {
                return true; // Inside border
            }

            return false; // Outside border
        }

        // Deletes a row with a given y coordinate
        public static void DeleteRow(int y)
        {
            // Look through the row using x - width
            for (int x = 0; x < width; x++)
            {
                Destroy(grid[x, y].gameObject); // Destroy each element
                grid[x, y] = null; // Return each grid element back to null
            }
        }

        public static void DecreaseRow(int y)
        {
            // Loop through entire column
            for (int x = 0; x < width; x++)
            {
                // Check if index is not null
                if (grid[x, y] != null)
                {
                    grid[x, y - 1] = grid[x, y]; // Move one towards bottom, set grid element to one above
                    grid[x, y] = null;
                    grid[x, y - 1].position += new Vector3(0, -1, 0);   // Update block position
                }
            }
        }

        // Shift the rows abve from given y
        public static void DecreaseRowsAbove(int y)
        {
            // Loop through each row
            for (int i = y; i < height; i++)
            {
                DecreaseRow(i); // Decrease each row
            }
        }

        // Check if we have a full row
        public static bool IsRowFull(int y)
        {
            // Loop through each column
            for (int x = 0; x < width; x++)
            {
                // If cell is empty
                if (grid[x, y] == null)
                {
                    return false;
                }
            }
            return true; // The row is full!
        }

        // Delete the full rows
        public static int DeleteFullRows()
        {
            int rows = 0;
            // Loop through all rows
            for (int y = 0; y < height; y++)
            {
                // Is the row full?
                if (IsRowFull(y))
                {
                    rows++;                                                 // Add row to count
                    DeleteRow(y);                                           // Delete entire row
                    DecreaseRowsAbove(y + 1);                               // Decrease the rows above (so we don't skip the next row)
                    y--;
                }
            }

            // If there are rows that were cleared AND
            // Functions are subscribed to onRowsCleared
            if (rows > 0 && onRowsCleared != null)
            {
                onRowsCleared.Invoke(rows);
            }

            // Return counted rows
            return rows;
        }

        public static void GameOver()
        {
            // If there are functions subscribed
            if (Instance.onGameOver != null)
            {
                // Invoke all subscribed functions
                Instance.onGameOver.Invoke();
            }
        }
    } 
}
