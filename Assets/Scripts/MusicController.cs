using UnityEngine;

public class MusicController : MonoBehaviour
{
    // Improvised Singleton
    public static MusicController Instance = null;

    public AudioSource bgMusic;
    public AudioSource hoverSound;
    public AudioSource clickSound;
    public AudioSource spinStoneSound;
    public AudioSource twoSameSound;
    public AudioSource levelCompleteSound;
    public AudioSource typingSound;
    //Audio source controller which is one for the entire game sesion (DontDestroyOnLoad(this) method)

    void Start()
    {
        Instance = this;
        CheckSettings();
        DontDestroyOnLoad(this);
    }
    // Check if the player have made some custon changes to the settings and set them
    public void CheckSettings()
    {
        if(PlayerPrefs.GetInt("bgSound") == 0)
        {
            bgMusic.mute = true;
        }
        if ((PlayerPrefs.GetInt("bgSound") == 1 || !(PlayerPrefs.HasKey("bgSound"))) && !bgMusic.isPlaying)
        {
            bgMusic.mute = false;
            bgMusic.Play();
        }
        if (PlayerPrefs.GetInt("effectsSound")==0)
        {
            hoverSound.mute = true;
            clickSound.mute = true;
            spinStoneSound.mute = true;
            twoSameSound.mute = true;
            levelCompleteSound.mute = true;
            typingSound.mute = true;
        }
        if (PlayerPrefs.GetInt("effectsSound") == 1 || !(PlayerPrefs.HasKey("effectsSound")))
        {
            hoverSound.mute = false;
            clickSound.mute = false;
            spinStoneSound.mute = false;
            twoSameSound.mute = false;
            levelCompleteSound.mute = false;
            typingSound.mute = false;
        }
    }
}
