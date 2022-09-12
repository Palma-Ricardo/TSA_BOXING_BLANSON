using UnityEngine;

public class MusicClass : MonoBehaviour
{
    public AudioSource _audioSource;
    public AudioClip Menu;
    public AudioClip Arena;

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        _audioSource = GetComponent<AudioSource>();
        GameObject.FindWithTag("Music").GetComponent<MusicClass>().PlayMusic(1);
    }

    public void PlayMusic(int source)
    {
        if(source == 1)
        {
            if (_audioSource.isPlaying) return;
            _audioSource.clip = Menu;
            _audioSource.Play();
        }
        else
        {
            if (_audioSource.isPlaying) return;
            _audioSource.clip = Arena;
            _audioSource.Play();
            
        }
        
    }

    public void StopMusic(int source)
    {
        if (source == 1)
        {
            if (_audioSource.isPlaying) return;
            _audioSource.Stop();
        }
        else
        {
            if (_audioSource.isPlaying) return;
            _audioSource.Stop();
        }
    }
}