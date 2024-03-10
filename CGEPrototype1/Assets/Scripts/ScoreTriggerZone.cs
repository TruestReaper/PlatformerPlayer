using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTriggerZone : MonoBehaviour
{
    //create a variable to keep track whether
    //the trigger zone is active
    bool active = true;

    // audio clip to hold score trigger sound
    public AudioSource scoreTrigger;

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if trigger zone is active
        if (active && collision.gameObject.tag == "Player")
        {
            //deactivate trigger zone
            active = false;

            // play score trigger sound when collected
            scoreTrigger.Play();

            //Add 1 to the score when player enters trigger zone
            ScoreManager.score++;
            gameObject.SetActive(false);
        }
    }
}
