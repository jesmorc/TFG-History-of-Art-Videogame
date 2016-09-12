using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Sound
{

    public string name;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = 0.7f;
    [Range(0.5f, 1.5f)]
    public float pitch = 1f;

    [Range(0f, 0.5f)]
    public float randomVolume = 0.1f;
    [Range(0f, 0.5f)]
    public float randomPitch = 0.1f;

    public bool loop = false;

    private AudioSource source;

    private float length;

    public void SetSource(AudioSource _source)
    {
        source = _source;
        source.clip = clip;
        source.loop = loop;
        length = source.clip.length;
    }

    public float getLength()
    {
        return length;
    }

    public void Play()
    {
        source.volume = volume * (1 + Random.Range(-randomVolume / 2f, randomVolume / 2f));
        source.pitch = pitch * (1 + Random.Range(-randomPitch / 2f, randomPitch / 2f));
        source.Play();
        
    }

    public void Stop()
    {
        source.Stop();
    }

    public bool isPlaying()
    {
        return source.isPlaying;
    }

}

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;

    [SerializeField]
    Sound[] sounds;

    void Awake()
    {
        if (instance != null)
        {
            if(instance != this)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    void Start()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            GameObject _go = new GameObject("Sound_" + i + "_" + sounds[i].name);
            _go.transform.SetParent(this.transform);
            sounds[i].SetSource(_go.AddComponent<AudioSource>());
        }

        switch (SceneManager.GetActiveScene().name)
        {
            case "EscenaMuseo":
                PlaySound("Music");
                break;
            case "EscenaAbstracta":
                PlaySound("AbstractMusic");
                break;
            case "EscenaRenacimiento":
                PlaySound("RenacimientoMusic");
                break;
            case "EscenaXilografia":
                PlaySound("XiloMusic");
                break;
            default:
                PlaySound("Music");
                break;

        }
        
    }


    public void PlaySound(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == _name)
            {
                sounds[i].Play();
                return;
            }
        }

        // no sound with _name
        Debug.LogWarning("AudioManager: Sound not found in list, " + _name);
    }

    public void StopSound(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == _name)
            {
                sounds[i].Stop();
                return;
            }
        }

        // no sound with _name
        Debug.LogWarning("AudioManager: Sound not found in list, " + _name);
    }

    public bool IsPlaying()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].isPlaying())
            {
                return true;
            }
        }
        return false;

    }

    public float getCurrentAudioLength()
    {
        float length = 0f;
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].isPlaying() && sounds[i].name != "Music")
            {
                
                length = sounds[i].getLength();
            }
        }
        return length;
    }


}
