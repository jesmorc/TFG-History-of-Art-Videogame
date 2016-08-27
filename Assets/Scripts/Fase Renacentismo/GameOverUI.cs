using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour {

    [SerializeField]
    string mouseHoverSoundName = "ButtonHover";

    [SerializeField]
    string buttonPressSound = "ButtonPress";

    AudioManager audioManager;

    void Start()
    {
        audioManager = AudioManager.instance;
        if(audioManager == null)
        {
            Debug.LogError("No audio manager in the scene");
        }
    }

    public void Quit()
    {

        audioManager.PlaySound(buttonPressSound);

        Debug.Log("Application quit");
        Application.Quit();
    }

    public void Retry()
    {
        audioManager.PlaySound(buttonPressSound);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnMouseOver()
    {
        audioManager.PlaySound(mouseHoverSoundName);
    }
}
