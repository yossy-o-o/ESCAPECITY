using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [SerializeField] public AudioSource BGMSource;
    [SerializeField] public AudioSource ambienceSource;
    [SerializeField] public AudioSource medicalclip;

    private void Awake()
    {
        // シングルトンパターンの実装
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    //MainMenuに行った場合のみ破棄する
    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            BGMSource.Stop();
            ambienceSource.Stop();
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        BGMSource.clip = clip;
        BGMSource.Play();

        ambienceSource.clip = clip;
        ambienceSource.Play();
    }

    public void PlayMedicalSFX(AudioClip clip)
    {
        ambienceSource.clip = clip;
        ambienceSource.Play();
    }
}
