using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtonsController : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject levelMenu;
    public GameObject settingsMenu;
    public GameObject helpMenu;

    // Load gameplay sceene with different amount of stones
    public void OnEasyLevelClicked()
    {
        MusicController.Instance.clickSound.Play();
        StonesLoader.StonesCount = 18;
        SceneManager.LoadScene("MainScene");
    }
    public void OnNormalLevelClicked()
    {
        MusicController.Instance.clickSound.Play();
        StonesLoader.StonesCount = 24;
        SceneManager.LoadScene("MainScene");
    }
    public void OnHardLevelClicked()
    {
        MusicController.Instance.clickSound.Play();
        StonesLoader.StonesCount = 30;
        SceneManager.LoadScene("MainScene");
    }
    // Main menu buttons controller
    public void OnBackToMenuClicked()
    {
        MusicController.Instance.clickSound.Play();
        SceneManager.LoadScene("MenuScene");
    }

    public void OnPlayClicked()
    {
        MusicController.Instance.clickSound.Play();
        levelMenu.SetActive(true);

        settingsMenu.SetActive(false);
        mainMenu.SetActive(false);
        helpMenu.SetActive(false);
    }
    public void OnBackToMainMenuClicked()
    {
        MusicController.Instance.clickSound.Play();
        mainMenu.SetActive(true);

        settingsMenu.SetActive(false);
        levelMenu.SetActive(false);
        helpMenu.SetActive(false);
    }
    public void OnSettingsMenuClicked()
    {
        MusicController.Instance.clickSound.Play();
        settingsMenu.SetActive(true);

        levelMenu.SetActive(false);
        mainMenu.SetActive(false);
        helpMenu.SetActive(false);
    }
    public void OnHelpMenuClicked()
    {
        MusicController.Instance.clickSound.Play();
        helpMenu.SetActive(true);

        settingsMenu.SetActive(false);
        levelMenu.SetActive(false);
        mainMenu.SetActive(false);
    }
}
