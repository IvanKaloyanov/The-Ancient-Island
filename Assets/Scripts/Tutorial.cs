using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public GameObject uiTutorialStone;
    public Text tutorialText;

    private const float textDisplayDelay = 0.2f;
    private readonly string[] textLines = { "Welcome, traveler ! Are you ready to exercise your memory ?",
        "Pick a stone and try to eliminate the pairs two by two.","Good luck and relax."};
    private Vector3 finalStonePossition = new Vector3(51.28f, -223.15f, -281.23f);

    void Start()
    {
        // The tutorial is called only once (When you first launch te game)
        if (PlayerPrefs.GetInt("startTutorial") == 0 || !(PlayerPrefs.HasKey("startTutorial")))
        {
          uiTutorialStone.SetActive(true);  
          StartTutorial();
        }
    }

    void StartTutorial()
    {
        PlayerPrefs.SetInt("startTutorial", 1);

        StartCoroutine(DisplayTextOverTheStone(textLines));
    }

    // Display text letter by latter
    private IEnumerator DisplayTextOverTheStone(string[] lines, float charPrintDelay = 0.1f,
        float sentencePrintDelay = 2f, float startDelay = 2f)
    {
        yield return new WaitForSeconds(startDelay);
        foreach (var text in lines)
        {
            tutorialText.text = "";
            for (int i = 0; i < text.Length; i++)
            {
                tutorialText.text = string.Concat(tutorialText.text, text[i]);

                MusicController.Instance.typingSound.Play();
                yield return new WaitForSeconds(charPrintDelay);
            }
            yield return new WaitForSeconds(sentencePrintDelay);
        }
        MoveTutorialStone();
    }

    private void MoveTutorialStone()
    {
        StartCoroutine(MoveObjectFromTo(uiTutorialStone.transform, finalStonePossition));
    }

    // Custom method for moving object with delay
    private IEnumerator MoveObjectFromTo(Transform startingTransform, Vector3 finalPossition,
        float speed = 2f, float delayTime = 0f)
    {
        while (startingTransform.localPosition != finalPossition)
        {
            float step;
            step = speed * Time.deltaTime;
            startingTransform.transform.localPosition = Vector3.MoveTowards(startingTransform.transform.localPosition, finalPossition, step);
            yield return new WaitForSeconds(delayTime);
        }
    }
}
