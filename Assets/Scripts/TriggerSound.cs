using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

public class TriggerSound : MonoBehaviour {

    
    private AudioManager audioManager;
    public string soundName;
    public bool moveAvail = true;
    public float secondsToChangeScene = 0;
    public float secondsToShowPortal = 0;
    public Transform particulasHumo;
    public Transform portalParticulas;
    private GameObject meninas;
    private bool audioPlayed = false;
    private GameObject dialogBox;
    private GameObject dialogText;
    private bool repeat = true;
    public float secondsToCloseDialog;
    public bool psychedelic = false;

    public string textToShow = "";

    void Awake()
    {
        dialogBox = GameObject.FindGameObjectWithTag("DialogBox");
        dialogText = GameObject.FindGameObjectWithTag("DialogText");

    }


    // Use this for initialization
    void Start () {
        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("No audio manager in scene");
        }
        if(SceneManager.GetActiveScene().name == "EscenaMuseo")
            meninas = GameObject.FindGameObjectWithTag("Meninas");

        dialogBox.SetActive(!dialogBox.activeSelf);      
        
    }
	
	
	void Update () {
        //Debug.Log("ARGHHHHH" + dialogBox.name + dialogBox.activeSelf + dialogBox.activeInHierarchy);
    }

    void OnTriggerEnter2D(Collider2D otherObj)
    {
        if (otherObj.tag != "Player")
            return;

        if (psychedelic)
        {
            Camera.main.GetComponent<VignetteAndChromaticAberration>().chromaticAberration = 50f;
        }

        if (repeat && this.tag!= "Stop")
        {
            StartCoroutine(DisableDialogBox(secondsToCloseDialog));
        }
//        repeat = false;
        Debug.Log("TRIGGER ON!");
        if (!dialogBox.activeSelf)
        {
                dialogBox.SetActive(!dialogBox.activeSelf);
        }
        
        dialogText.GetComponent<Text>().text = textToShow;
        //set font size
        if (textToShow.Length > 90)
        {
            
            dialogText.GetComponent<Text>().fontSize = 20;

        }
        else if (textToShow.Length > 50 && textToShow.Length < 90)
        {
           
           
            dialogText.GetComponent<Text>().fontSize = 25;
        }
        else
        {
            
            
            dialogText.GetComponent<Text>().fontSize = 30;
        }

        if (!audioPlayed)
        {
            audioManager.PlaySound(soundName);
        }           

            audioPlayed = true;
            if(this.tag == "Stop")
            {
               

                if (otherObj.transform.root == otherObj.transform)
                {
                    Animator anim = otherObj.gameObject.GetComponent<Animator>();
                    anim.SetFloat("VelX", 0f);
                    anim.SetTrigger("Stop");
                    otherObj.gameObject.GetComponent<MainCharacterController>().enabled = false;
                }
               
                StartCoroutine(WaitAndShowPortal(secondsToShowPortal));
                StartCoroutine(WaitAndChangeScene(secondsToChangeScene));
                
            }
    }
    


    public IEnumerator DisableDialogBox(float seconds)
    {
        Debug.Log("Seconds" + seconds);
        
        yield return new WaitForSeconds(seconds);
        gameObject.SetActive(false);
        dialogBox.SetActive(false);
        audioPlayed = false;
        


    }



    private IEnumerator WaitAndChangeScene(float seconds)
    {
     
        yield return new WaitForSeconds(seconds);
        audioManager.StopSound("Music");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
   
    }

    private IEnumerator WaitAndShowPortal(float seconds)
    {

        yield return new WaitForSeconds(seconds);
        GameObject clone = Instantiate(particulasHumo, meninas.transform.position, meninas.transform.rotation) as GameObject;
        Destroy(clone, 7f);
        GameObject clone2 = Instantiate(portalParticulas, meninas.transform.position, meninas.transform.rotation) as GameObject;
        Destroy(clone2, 7f);

    }
}
