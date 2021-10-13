using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class soundManager : MonoBehaviour
{
    public AudioMixer mixer;
    public AudioSource BackgroundSound;
    public static soundManager instance;

    public AudioClip[] bglist;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OnSceneLoaded(Scene arg0, LoadSceneMode arg2)
    {
        for (int i = 0; i < bglist.Length; i++)
        {
            if (arg0.name == bglist[i].name)
            {   
                BackgroundSoundPlay(bglist[i]);
            }
        }

    }
    public void SFXPlay(string sfxname, AudioClip clip)
    {
        GameObject go = new GameObject(sfxname + "Sound");
        AudioSource audioSource = go.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = mixer.FindMatchingGroups("SFX")[0];
        audioSource.clip = clip;
        audioSource.Play();

        //public AudioClip clip; 선언 후 해당 스크립트 인스펙터에서 오디오 클립 삽입
        //SoundManager.instance.SFXPlay("이름" + clip); SFX 플레이 할 부분에서 이 코드 삽입

        Destroy(go, clip.length);
    }

    public void BackgroundSoundPlay(AudioClip clip)
    {
        BackgroundSound.outputAudioMixerGroup = mixer.FindMatchingGroups("BGSound")[0];
        BackgroundSound.clip = clip;
        BackgroundSound.loop = true;
        BackgroundSound.volume = 0.1f;
        BackgroundSound.Play();
    }
}
