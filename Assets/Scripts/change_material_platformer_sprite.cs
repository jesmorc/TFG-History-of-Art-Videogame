using UnityEngine;
using System.Collections;

public class change_material_platformer_sprite : MonoBehaviour {
    SpriteRenderer mySpriteRenderer;
    float nextFlick= 5f;
    bool is_black = true;

    // Use this for initialization
    void Start () {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {

        if (Time.time > nextFlick)
        {
            if (is_black)
            {
                change_to_white();
                is_black = false;
            } else
            {
                change_to_black();
                is_black = true;
            }
            nextFlick = Time.time + Mathf.Floor(Random.Range(1, 5)); ;
        }
    }

    void change_to_white()
    {
        Shader myShader;

        myShader = Shader.Find("Standard");

        mySpriteRenderer.material.shader = myShader;
    }

    void change_to_black()
    {
        Shader myShader;

        myShader = Shader.Find("Sprites/Default");

        mySpriteRenderer.material.shader = myShader;
    }
}
