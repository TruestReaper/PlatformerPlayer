using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Must add this to use TMP_Text
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text textbox;
    public string[] sentences;
    private int index;
    public float typingSpeed;

    public GameObject continueButton;
    public GameObject dialogPanel;

    void OnEnable()
    {
        continueButton.SetActive(false);
        StartCoroutine(Type());
    }

    IEnumerator Type()
    {
        textbox.text = "";
        foreach (char letter in sentences[index])
        {
            index++;
            textbox.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextSentence()
    {
        if (index < sentences.Length - 1)
        {
            index++;
            textbox.text = "";
            StartCoroutine(Type());
        } else
        {
            textbox.text = "";
            dialogPanel.SetActive(false);
        }
    }
}
