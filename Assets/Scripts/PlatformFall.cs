using UnityEngine;
using System.Collections;

public class PlatformFall : MonoBehaviour {

    private Vector3 initialPos;
    // Use this for initialization
    void Start () {
        initialPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	       
	}

    public void Fall()
    {
        StartCoroutine(FallPlatform());
        StartCoroutine(ResetPlatformPosition(initialPos));
        
        
    }

    IEnumerator FallPlatform()
    {
        yield return new WaitForSeconds(1f);
        transform.GetComponent<Rigidbody>().isKinematic = false;
       
    }

    IEnumerator ResetPlatformPosition(Vector3 position)
    {
        yield return new WaitForSeconds(3f);
        transform.GetComponent<Rigidbody>().isKinematic = true;
        transform.position = position;
    }




}
