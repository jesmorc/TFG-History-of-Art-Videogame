using UnityEngine;
using System.Collections;

public class dog_static : MonoBehaviour {
    public GameObject character;
    public GameObject limit_fall_death;
    private float minDistance = 6f;
    public LayerMask characterMask;
    public int numer_bones_this_level = 5;
    public GameObject dog_running;

    public GameObject conversation_nothing;
    public GameObject conversation_meninaKey;
    public GameObject conversation_bones;
    public GameObject conversation_allFound;

    void Start()
    {
        conversation_nothing.SetActive(false);
        conversation_meninaKey.SetActive(false);
        conversation_bones.SetActive(false);
        conversation_allFound.SetActive(false);

    }

// Update is called once per frame
void Update()
    {
        if (character)
        {
            if (Input.GetKeyDown(KeyCode.E) && Physics2D.OverlapCircle(transform.position, 2f, characterMask))
            {
                speak();
            }
        }
    }


    private void speak()
    {

        if (character.GetComponent<MainCharacterController>().numer_bones >= numer_bones_this_level && character.GetComponent<MainCharacterController>().menina_found)
        {
            conversation_allFound.SetActive(true);
            limit_fall_death.SetActive(false);
            dog_running.SetActive(true);
            gameObject.SetActive(false);
         }
        else if (character.GetComponent<MainCharacterController>().numer_bones >= numer_bones_this_level && !character.GetComponent<MainCharacterController>().menina_found)
        {
            conversation_meninaKey.SetActive(true);
        }
        else if (character.GetComponent<MainCharacterController>().numer_bones < numer_bones_this_level && character.GetComponent<MainCharacterController>().menina_found)
        {
            conversation_bones.SetActive(true);
        }
        else {
            conversation_nothing.SetActive(true);

        }

    }
}
