using UnityEngine;
using UnityEngine.UI;

public class SettingsUIScript : MonoBehaviour
{
    public Image musicSettingsCrntImage;
    public Image effectsSettingsCrntImage;
    public Image clickedImage;
    public Image notClickedImage;
    public Image backgroundImage;

    // In game settings management and save the changes into PlayerPrefs

    void Start()
    {
        SetupImages();
    }

    public void OnMusicClicked()
    {
        MusicController.Instance.clickSound.Play();
        if (musicSettingsCrntImage.sprite == clickedImage.sprite)
        {
            musicSettingsCrntImage.sprite = notClickedImage.sprite;
            MusicController.Instance.bgMusic.mute = true;
            PlayerPrefs.SetInt("bgSound", 0);
        }
        else if(musicSettingsCrntImage.sprite == notClickedImage.sprite)
        {
            musicSettingsCrntImage.sprite = clickedImage.sprite;
            MusicController.Instance.bgMusic.mute = false;
            MusicController.Instance.bgMusic.Play();
            PlayerPrefs.SetInt("bgSound", 1);
        }
    }

    public void OnEffectsClicked()
    {
        MusicController.Instance.clickSound.Play();
        if (effectsSettingsCrntImage.sprite == clickedImage.sprite)
        {
            effectsSettingsCrntImage.sprite = notClickedImage.sprite;
            PlayerPrefs.SetInt("effectsSound", 0);
        }
        else if (effectsSettingsCrntImage.sprite == notClickedImage.sprite)
        {
            effectsSettingsCrntImage.sprite = clickedImage.sprite;
            PlayerPrefs.SetInt("effectsSound", 1);
        }
        MusicController.Instance.CheckSettings();
    }
    // Setup images for active/inactive settigs
    void SetupImages()
    {
        if (PlayerPrefs.GetInt("bgSound") == 1 || !(PlayerPrefs.HasKey("bgSound")))
        {
            musicSettingsCrntImage.sprite =  clickedImage.sprite;
        }
        else
        {
            musicSettingsCrntImage.sprite = notClickedImage.sprite;
        }
        if (PlayerPrefs.GetInt("effectsSound") == 1 || !(PlayerPrefs.HasKey("effectsSound")))
        {
            effectsSettingsCrntImage.sprite = clickedImage.sprite;
        }
        else 
        {
            effectsSettingsCrntImage.sprite =  notClickedImage.sprite;
        }
    }
}
