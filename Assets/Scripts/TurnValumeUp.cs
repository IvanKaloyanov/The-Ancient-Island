using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnValumeUp : MonoBehaviour
{
    private static bool isStarted = false;

    public static bool IsStarted
    {
        get
        {
            return isStarted;
        }
        set
        {
            isStarted = value;
        }
    }

    void Start ()
    {
        if (IsStarted == false)
        {
            IsStarted = true;
            StartCoroutine(ValueUp());
        }
    }
    // Turn the value overtime
    private IEnumerator ValueUp()
    {
        AudioListener.volume = 0f;
        float value;
        float time = 6f;

        while (AudioListener.volume != 1f)
        {
            value = Mathf.Lerp(0, 1, AudioListener.volume + Time.deltaTime/time);
            AudioListener.volume = value;
            yield return null;
        }
    }
}
