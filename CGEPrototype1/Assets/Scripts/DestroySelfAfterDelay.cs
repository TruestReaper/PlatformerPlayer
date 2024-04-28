using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelfAfterDelay : MonoBehaviour
{
    // the delay before game object is destroyed
    public float delay = 2f;

    // Start is called before the first frame update
    void Start()
    {
        // destroy game object after 2 seconds
        Destroy(gameObject, delay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
