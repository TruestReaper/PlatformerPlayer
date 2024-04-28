using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Add this to work with TextMeshPro
using TMPro;
//Add this to work with SceneManager to load or reload scenes
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    //notice public static variables can be used in any other script
    //but cannot be seen in the Inspector
    public static bool gameOver;
    public static bool won;
    public static int score;
    public int scoreToWin;

    //Set this in the Inspector
    public TMP_Text textbox;

    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
        won = false;
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver)
        {
            textbox.text = "Apples: " + score;
        }

        if (score >= scoreToWin) 
        {
            won = true;
            gameOver = true;
        }

        if (gameOver)
        {
            if (won)
            {
                textbox.text = "You Win!\nPress R to Try Again!";
            }
            else
            {
                textbox.text = "You Lose!\nPress R to Try Again!";
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}
