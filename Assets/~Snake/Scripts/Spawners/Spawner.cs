using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snake
{
    public class Spawner : MonoBehaviour
    {
        public float spawnRate = 4;
        public float startTime = 3f;
        public GameObject foodPrefab;   // Food prefab to spawn
        public Transform borderTop;
        public Transform borderBottom;
        public Transform borderLeft;
        public Transform borderRight;

        // Use this for initialization
        void Start()
        {
            Subscribe();
            // InvokeRepeating("Spawn", startTime, spawnRate);
            // GameManager.Instance.onSpawn += Spawn;      // Subscribe function to this function call 
        }
				
				void OnDestroy()
				{
					UnSubscribe();	// Once object is destroyed, unsubscribe from GameManager's onSpawn delagate
				}

        public void Subscribe()
        {
            GameManager.Instance.onSpawn += Spawn;  // Subscribe function to this function call
        }

        public void UnSubscribe()
        {
            GameManager.Instance.onSpawn -= Spawn;  // Subscribe function to this function call
        }

        // Spawn the food randomly
        void Spawn()
        {
            // Get coordinates of borders
            float left      = borderLeft.position.x;
            float right     = borderRight.position.x;
            float bottom    = borderBottom.position.y;
            float top       = borderTop.position.y;

            // Get random x and y coordinates
            int x = (int)Random.Range(left + .5f, right - .5f);
            int y = (int)Random.Range(top - .5f, bottom + .5f);

            // Spawn food at this point
            Instantiate(foodPrefab, new Vector2(x, y), Quaternion.identity);
        }
    }
}

