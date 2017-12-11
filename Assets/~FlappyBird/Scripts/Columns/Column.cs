using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlappyBird
{
    public class Column : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D other) // Consider OnTriggerExit2D
        {
            if (other.name.StartsWith("Bird"))  // Have we collided with the bird?
            {
                GameManager.Instance.BirdScored();
            }
        }
    }

}
