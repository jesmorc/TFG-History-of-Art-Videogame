using UnityEngine;
using System.Collections;

public class asbtractRespawn : MonoBehaviour {

    private AudioManager audioManager;
    public int fallBoundary = -20;
    public string spawnAbstractSoundName = "SpawnAbstract";

	// Use this for initialization
	void Start () {

        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("Panic, no audio manager in scene");
        }
	
	}
	
	// Update is called once per frame
	void Update () {

        if (transform.position.y <= fallBoundary)
        {
            transform.position = GameMaster.gm.transform.GetChild(1).position;
            audioManager.PlaySound(spawnAbstractSoundName);

        }

	}
}
