using UnityEngine;
using System.Collections;

public class Fading : MonoBehaviour {

	public Texture2D fadeOutTexture;
	public float fadeSpeed = 0.8f;

	private int drawDepth = -1000;
	private float alpha = 1.0f;
	private int fadeDir = 1;  //referring to scene

	// Use this for initialization
	void onGUI(){
		alpha += fadeDir * fadeSpeed * Time.deltaTime;
		//force (clamp) to be between 0 and 1
		alpha = Mathf.Clamp01(alpha);

		//set color of the GUI(in this case the texture)
		GUI.color = new Color(GUI.color.r , GUI.color.g, GUI.color.b, alpha);
		GUI.depth = drawDepth;
		GUI.DrawTexture( new Rect ( 0, 0, Screen.width, Screen.height), fadeOutTexture);

	}

	public float BeginFade(int direction){
		fadeDir = direction;
		return (fadeSpeed);
	}

	void OnLevelWasLoaded(){
		//alpha = 1
		BeginFade(-1);
	}
}
