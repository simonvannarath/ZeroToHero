using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlappyBird
{
    [RequireComponent(typeof(Bird))]
    public class UserController : MonoBehaviour
    {
        private Bird bird;

	    // Use this for initialization
	    void Start ()
        {
            bird = GetComponent<Bird>();
	    }
	
	    // Update is called once per frame
	    void Update ()
        {
            // check for mouse down
		    if (Input.GetMouseButtonDown(0))
            {
                bird.Flap();                    // Bird will now flap wings
            }
	    }
    }

}
