using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource sfxAudioSource, musicAudioSource;

    // Esto indica que la instancia puede ser llamada desde cualquier lado, y para protegerla solo 
    // vamos a poder conseguir info de aqui pero no podemos modificar infomacion mas que desde este mismo script
    public static AudioManager Instance { get; private set; }

    // Para asegurarse de que solo hay una sola instancia del audiomanager
    private void Awake()
    {
        // Se limita las instancias a una sola
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
            // No le afectan los cambios de escena
            DontDestroyOnLoad(this);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M)) ToggleMusic();
    }
    public void PlaySound(AudioClip audioClip)
    {
        sfxAudioSource.PlayOneShot(audioClip);
    }

    private void ToggleMusic()
    {
        musicAudioSource.mute = !musicAudioSource.mute;
    }
}
