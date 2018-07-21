using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreenController : MonoBehaviour
{
    public Text loadingText;

    private const float fadeOutTime = 2f;

	void Start ()
    {
        StartCoroutine(FadeOutText(fadeOutTime));
    }
    // Fade out method after some time
    private IEnumerator FadeOutText(float t)
    {
        AudioListener.volume = 0f;
        loadingText.color = new Color(loadingText.color.r, loadingText.color.g, loadingText.color.b, 1);
        while (loadingText.color.a > 0.0f)
        {
            loadingText.color = new Color(loadingText.color.r, loadingText.color.g,
                loadingText.color.b, loadingText.color.a - (Time.deltaTime / t));
            yield return null;
        }
        SceneManager.LoadScene("MenuScene"); 
    }
}
