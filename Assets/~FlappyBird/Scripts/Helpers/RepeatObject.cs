using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlappyBird
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class RepeatObject : MonoBehaviour
    {
        public float padding = 0.01f;

        private BoxCollider2D box;
        private float width;

	    // Use this for initialization
	    void Start()
        {
            box = GetComponent<BoxCollider2D>();            // Get boxcollider component
            width = box.size.x * transform.localScale.x;    // Store size of collider along horizontal axis
	    }
	
	    // Update is called once per frame
	    void Update()
        {
            Vector3 pos = transform.position; // Store the position in a smaller variable

            // Is the position on the x outside the camera?
            if (pos.x < -width)
            {
                Repeat(); // Repeat the object
            }
	    }

        void Repeat()
        {
            Vector3 groundOffset = new Vector3((width - padding) * 2, 0, 0); // Offset of the ground to be placed outside the screen
            transform.position += groundOffset;
        }
    }
}
