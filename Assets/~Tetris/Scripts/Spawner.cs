using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tetris
{
    public class Spawner : MonoBehaviour
    {
        public GameObject[] groups;
        public int nextIndex = 0;

        // Use this for initialization
        void Start()
        {
            SpawnNext(); // Spawn the initial group
        }

        // Spawns the next random group element
        public void SpawnNext()
        {
            Instantiate(groups[nextIndex], transform.position, Quaternion.identity); // Spawn the random group
            nextIndex = Random.Range(0, groups.Length); // Get next random index
            RemoveEmptyParents();
        }

        void RemoveEmptyParents()
        {
            // Check for any parents without children
            Group[] groups = GameObject.FindObjectsOfType<Group>();
            foreach (Group g in groups)
            {
                // If the group doesn't have any children
                if (g.transform.childCount == 0)
                {
                    // Destroy the parent
                    Destroy(g.gameObject);
                }
            }
        }
    }
}

