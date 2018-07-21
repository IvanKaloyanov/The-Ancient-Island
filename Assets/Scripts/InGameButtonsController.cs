using UnityEngine.SceneManagement;
using UnityEngine;

public class InGameButtonsController : MonoBehaviour
{
    //Sceene buttons controller
    public void RestartLevel()
    {
        MusicController.Instance.clickSound.Play();
        SceneManager.LoadScene("MainScene");
    }
    public void BackToMenuFromGameplayScene()
    {
        MusicController.Instance.clickSound.Play();
        SceneManager.LoadScene("MenuScene");
    }
}
