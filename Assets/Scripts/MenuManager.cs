using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    [SerializeField]
    string hoverOverSound = "ButtonHover";

    [SerializeField]
    string pressButtonSound = "ButtonPress";

    [SerializeField]
    string backgroundMusic = "TitleMusic";

    AudioManager audioManager;


    void Start()
    {
        audioManager = AudioManager.instance;
        if(audioManager == null)
        {
            Debug.LogError("No audio manager found");
        }

        //audioManager.PlaySound(backgroundMusic);
    }

	public void StartGame()
    {
        audioManager.PlaySound(pressButtonSound);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //audioManager.StopSound(backgroundMusic);
    }

    public void QuitGame()
    {
        audioManager.PlaySound(pressButtonSound);
        Application.Quit();
    }

    public void OnMouseOver()
    {
        audioManager.PlaySound(hoverOverSound);
    }

}
