using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CameraController_m : MonoBehaviour 
{
	public Transform Player;
	public float m_speed = 0.1f;
	Camera mycam;

    float nextTimeToSearch = 0;

    public void Start()
	{
		mycam = GetComponent<Camera> ();
	}

	public void Update()
	{

        if (Player == null || Player.gameObject.activeSelf == false)
        {
            FindPlayer();
            return;
        }

        mycam.orthographicSize = (Screen.height / 100f) / 0.8f;

		if (Player) 
		{
		
			transform.position = Vector3.Lerp(transform.position, Player.position, m_speed) + new Vector3(0, 0f, -12f);
		}


	}

    void FindPlayer()
    {
        if (nextTimeToSearch <= Time.time)
        {
            //GameObject searchResult = GameObject.FindGameObjectWithTag("Player");
            GameObject[] searchResult = SceneManager.GetActiveScene().GetRootGameObjects();
            if (searchResult != null)
            {
                for (int i = 0; i < searchResult.Length; i++)
                {
                    if (searchResult[i].tag == "Player" && searchResult[i].activeInHierarchy)
                    {
                        Player = searchResult[i].transform;
                        Debug.Log("Target encontrado: " + searchResult[i].name);
                    }

                }


            }
            nextTimeToSearch = Time.time + 0.5f; //search for the player 2 times each second
           
        }
    }
}
