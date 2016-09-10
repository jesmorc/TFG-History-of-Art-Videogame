using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class VideoPlayer : MonoBehaviour {

    MovieTexture video;

	void Start () {
        video = ((MovieTexture)GetComponent<Renderer>().material.mainTexture);
        video.Play();
        StartCoroutine(FindEnd());
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Return))
        {

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

    }

    private IEnumerator FindEnd()
    {
        while (video.isPlaying)
        {
            yield return 0;
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        yield break;
    }
}
