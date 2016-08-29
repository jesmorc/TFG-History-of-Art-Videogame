using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {

	public static GameMaster gm;

    [SerializeField]
    private int maxLives = 3;

    private static int _remainingLives;
    public static int RemainingLives
    {
        get { return _remainingLives; }
    }

	void Awake(){
		if (gm == null) {
			gm = GameObject.FindGameObjectWithTag ("GM").GetComponent<GameMaster>();
		}
		
	}

	public Transform playerPrefab;
	public Transform spawnPoint;
	public float spawnDelay = 2;
	public Transform spawnPrefab;
    public string respawnCoundownSoundName = "RespawnCountdown";
    public string spawnSoundName = "Spawn";

    public string gameOverSoundName = "GameOver";

    

    [SerializeField]
    private GameObject gameOverUI;

    //cache
    private AudioManager audioManager;

    void Start()
    {
       

        _remainingLives = maxLives;

        //caching
        audioManager = AudioManager.instance;
        if(audioManager == null)
        {
            Debug.Log("No audio manager found!");
        }
    }
	
    public void EndGame()
    {
        audioManager.PlaySound(gameOverSoundName);

        Debug.Log("Game over");
        gameOverUI.SetActive(true);
    }

	public IEnumerator _RespawnPlayer(){

        audioManager.PlaySound(respawnCoundownSoundName);

		yield return new WaitForSeconds (spawnDelay);   //then if we use this we have to return an IEnumerator in this fucntion

        audioManager.PlaySound(spawnSoundName);
        Instantiate (playerPrefab, spawnPoint.position, spawnPoint.rotation);
		GameObject clone = Instantiate (spawnPrefab, spawnPoint.position, spawnPoint.rotation) as GameObject;
		Destroy (clone, 3f);

	}


	public static void KillPlayer(PlayerRenacentismo player){
		Destroy (player.gameObject);
        _remainingLives--;
        if(_remainingLives <= 0)
        {
            gm.EndGame();
        }else
        {
            gm.StartCoroutine(gm._RespawnPlayer());  //startCoRoutine cuz we used yield return 
        }
		
	}

	public static void KillEnemy(Enemy enemy){
        gm._KillEnemy(enemy);
	}

    public void _KillEnemy(Enemy _enemy)
    {
        //play sound
        audioManager.PlaySound(_enemy.deathSoundName);

        //add particles
        GameObject _clone = Instantiate(_enemy.deathParticles, _enemy.transform.position, Quaternion.identity) as GameObject;
        Destroy(_clone, 5f);
        
        Destroy(_enemy.gameObject);
    }

}
