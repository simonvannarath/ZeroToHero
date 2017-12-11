using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlappyBird
{
    public class ColumnSpawner : MonoBehaviour
    {
        public GameObject columnPrefab;     // The column prefab we want to spawn
        public int columnPoolSize = 5;      // How many columns to keep on standby
        public float spawnRate = 3f;        // How quickly each column spawns
        public float columnMin = -1f;       // Minimum y value of the column position
        public float columnMax = 2.5f;      // Maximum y value of the column position
        public Vector3 standbyPos = new Vector3(-15, -25); // Holding position for the unused columns offsceen
        public float spawnXpos = 10f;

        private GameObject[] columns;       // Collection of pooled columns
        private int currentColumn = 0;      // Index of the column in the collection
        private float spawnTimer = 0f;

	    // Use this for initialization
	    void Start ()
        {
            columns = new GameObject[columnPoolSize]; // Initialise column pool

            for (int i = 0; i < columnPoolSize; i++)
            {
                // ... and create individual columns
                columns[i] = Instantiate(columnPrefab, standbyPos, Quaternion.identity);
            }
	    }
	
	    // Update is called once per frame
	    void Update ()
        {
            spawnTimer += Time.deltaTime; // Count up the timer

            // Is game not over AND has spawnTimer reached the spawnRate?
            if (GameManager.Instance.gameOver == false && 
                spawnTimer >= spawnRate)
            {
                spawnTimer = 0f;    // Reset timer
                SpawnColumn();      // Spawn the column i.e. reuse a column
            }
	    }

        void SpawnColumn()
        {
            float spawnYpos = Random.Range(columnMin, columnMax);                   // Set a random y spawn position for the column
            GameObject column = columns[currentColumn];                             // Get current column
            column.transform.position = new Vector2(spawnXpos, spawnYpos);          // Set position of current column
            currentColumn++;                                                        // Increase value of current column

            // If the new currentColumn reaches the end of the array
            if (currentColumn >= columnPoolSize)
            {
                currentColumn = 0;                                                  // ... set it bvack to zero
            }
        }
    }

}
