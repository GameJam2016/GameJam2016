using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    // The current instanc of the audio manager
    public static AudioManager Instance = null;

    [SerializeField]
    private AudioSource m_Source;

    public float Volume = 0.5f;

	// Use this for initialization
	void Start ()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        m_Source = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    /// <summary>
    /// Plays Sound
    /// </summary>
    /// <param name="clip"> clip to play</param>
    public void PlaySound(AudioClip clip)
    {
        if(!clip || !m_Source)
        {
            return;
        }

        m_Source.PlayOneShot(clip, Volume);
    }

    /// <summary>
    /// Plays a sound at location
    /// </summary>
    /// <param name="clip"> clip to play</param>
    /// <param name="position"> position to play the sound at</param>
    public void PlaySoundAtLocation(AudioClip clip, Vector3 position)
    {
        if (!clip || !m_Source)
        {
            return;
        }
        
        AudioSource.PlayClipAtPoint(clip, position, Volume);
    }
}
