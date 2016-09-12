using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class doorNextScene : MonoBehaviour {
    private GameObject character;

    public string nameNextScene = "GameScene";
    public LayerMask characterMask;

    private AudioManager audioManager;

    // Use this for initialization
    void Start()
    {
        character = GameObject.FindGameObjectsWithTag("Player")[0];
        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("Panic, no audio manager in scene");
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (character)
        {
            if (Input.GetKeyDown("e") && Physics2D.OverlapCircle(transform.position, 1f, characterMask))
            {
                GetComponent<AudioSource>().Play();
                Invoke("loadLevel", GetComponent<AudioSource>().clip.length);
            }

        }
        //soundPlayed = false;
        //  }
    }

    void loadLevel()
    {
        SceneManager.LoadScene(nameNextScene);
        audioManager.StopSound("RenacimientoMusic");

    }
}
