using UnityEngine;
using System.Collections;

public class setMusic : MonoBehaviour {

    AudioManager audioManager;
    public string soundName;
	// Use this for initialization
	void Start () {

        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("No audio manager found");
        }
        audioManager.PlaySound(soundName);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
