using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;
using UnityEngine.SceneManagement;

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
    public Transform playerPrefab2;
    public Transform player2DPrefab;
    public Transform spawnPoint;
	public float spawnDelay = 2;
	public GameObject spawnPrefab;
    public GameObject smokePrefab;
    public string respawnCoundownSoundName = "RespawnCountdown";
    public string spawnSoundName = "Spawn";
 
    public string gameOverSoundName = "GameOver";

    private float distanceBetweenBackAndFront = -10f;

    GameObject cloneSmoke;


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

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q) && SceneManager.GetActiveScene().name == "EscenaAbstracta" && !player2DPrefab.gameObject.activeSelf)
        {
            
            if(playerPrefab != null && playerPrefab2 != null) {
                
                if (playerPrefab.gameObject.activeSelf)
                {
                   
                    playerPrefab2.gameObject.SetActive(!playerPrefab2.gameObject.activeSelf);
                    playerPrefab2.position = playerPrefab.position;
                    playerPrefab2.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    playerPrefab2.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                    playerPrefab.gameObject.SetActive(false);

                    //Camera.main.GetComponent<VignetteAndChromaticAberration>().chromaticAberration = 50f;


                    GameObject clone = Instantiate(spawnPrefab, playerPrefab.position, playerPrefab.rotation) as GameObject;
                    Destroy(clone, 3f);
                }
                else
                {
                    
                    playerPrefab.gameObject.SetActive(!playerPrefab.gameObject.activeSelf);
                    playerPrefab.position = playerPrefab2.position;
                    playerPrefab2.gameObject.SetActive(false);

                    //Camera.main.GetComponent<VignetteAndChromaticAberration>().chromaticAberration = 25f;

                    GameObject clone = Instantiate(spawnPrefab, playerPrefab2.position, playerPrefab2.rotation) as GameObject;
                    Destroy(clone, 3f);
                }  
            }

        }

        if (Input.GetKeyDown(KeyCode.F) && SceneManager.GetActiveScene().name == "EscenaAbstracta")
        {
            if (playerPrefab != null && playerPrefab2 != null)
            {

                if (playerPrefab.gameObject.activeSelf)
                {
                    if (cloneSmoke != null)
                        Destroy(cloneSmoke, 1f);

                    player2DPrefab.gameObject.SetActive(!player2DPrefab.gameObject.activeSelf);
                    changePosition(player2DPrefab, playerPrefab, false);
                    playerPrefab.gameObject.SetActive(false);

                    //Camera.main.GetComponent<VignetteAndChromaticAberration>().chromaticAberration = 50f;


                    GameObject clone = Instantiate(spawnPrefab, playerPrefab.position, playerPrefab.rotation) as GameObject;
                    Destroy(clone, 3f);

                }else if (playerPrefab2.gameObject.activeSelf)
                {
                    if (cloneSmoke != null)
                        Destroy(cloneSmoke, 1f);

                    player2DPrefab.gameObject.SetActive(!player2DPrefab.gameObject.activeSelf);
                    changePosition(player2DPrefab, playerPrefab2, false);
                    playerPrefab2.gameObject.SetActive(false);

                    //Camera.main.GetComponent<VignetteAndChromaticAberration>().chromaticAberration = 50f;


                    GameObject clone = Instantiate(spawnPrefab, playerPrefab.position, playerPrefab.rotation) as GameObject;
                    Destroy(clone, 3f);

                }
                else
                {
                    
                    if(cloneSmoke == null)
                    {
                        cloneSmoke = Instantiate(smokePrefab, player2DPrefab.position, player2DPrefab.rotation) as GameObject;
                    }
                    else
                    {
                        Destroy(cloneSmoke);
                        cloneSmoke = Instantiate(smokePrefab, player2DPrefab.position, player2DPrefab.rotation) as GameObject;
                    }
                    playerPrefab.gameObject.SetActive(!playerPrefab.gameObject.activeSelf);

                    changePosition(playerPrefab, player2DPrefab, true);
                    //playerPrefab.GetComponent<PlayerAbstracto>().ResetForces();
                    //playerPrefab.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                    player2DPrefab.gameObject.SetActive(false);

                    //Camera.main.GetComponent<VignetteAndChromaticAberration>().chromaticAberration = 25f;

                    GameObject clone = Instantiate(spawnPrefab, playerPrefab2.position, playerPrefab2.rotation) as GameObject;
                    Destroy(clone, 3f);
                }


            }
        }
    }

    public void changePosition(Transform newObject, Transform oldObject, bool toFront)
    {
        newObject.position = oldObject.position;
        float aux;
        if (!toFront)
        {
            aux = Mathf.Abs(distanceBetweenBackAndFront);
        }
        else
        {
            aux = distanceBetweenBackAndFront;
        }
        Vector3 temp = new Vector3(0, 1f, aux);
        newObject.position += temp;
    }

    public void EndGame()
    {
        audioManager.PlaySound(gameOverSoundName);

        Debug.Log("Game over");
        gameOverUI.SetActive(true);
    }

	public IEnumerator _RespawnPlayer(){

        //audioManager.PlaySound(respawnCoundownSoundName);

		yield return new WaitForSeconds (spawnDelay);   //then if we use this we have to return an IEnumerator in this fucntion

        audioManager.PlaySound(spawnSoundName);
        Instantiate (playerPrefab, spawnPoint.position, spawnPoint.rotation);
		GameObject clone = Instantiate (spawnPrefab, spawnPoint.position, spawnPoint.rotation) as GameObject;
		Destroy (clone, 3f);

	}

    public static void KillPlayer(Transform player)
    {
        Destroy(player.gameObject);
        //_remainingLives--;
        //if (_remainingLives <= 0)
        //{
        //    gm.EndGame();
        //}
        //else
        //{
        gm.StartCoroutine(gm._RespawnPlayer());  //startCoRoutine cuz we used yield return 
        //}
        

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
