using UnityEngine;
using System.Collections;

public class Scroll : MonoBehaviour {

    public Transform[] backgrounds;
    private float[] paralaxScales;
    public float smoothing;

    private Vector3 previousCameraPosition;

    // Use this for initialization
    void Start () {
        previousCameraPosition = transform.position;

        paralaxScales = new float[backgrounds.Length];
        for (int i=0; i< paralaxScales.Length; i++)
        {
            paralaxScales[i] = backgrounds[i].position.z * -1;
        } 
    }

 

    // Update is called once per frame
    void LateUpdate () {
      for (int i=0; i<backgrounds.Length; i++)
        {
            Vector3 parallax = (previousCameraPosition - transform.position) * (paralaxScales[i] / smoothing);
            backgrounds[i].position = new Vector3(backgrounds[i].position.x + parallax.x, backgrounds[i].position.y /*+ (parallax.y)*0.25f*/, backgrounds[i].position.z);
        }
        previousCameraPosition = transform.position;
    }
}
