using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonsController : MonoBehaviour
{
    public Image backgroundImage;

    //UI Buttons Controller
    public void OnGazeEnter()
    {
        MusicController.Instance.hoverSound.Play();
        backgroundImage.color = Color.gray;
    }
    public void OnGazeExit()
    {
        backgroundImage.color = Color.white;
    }
}
