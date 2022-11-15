using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class Sound
{
    public string soundName;
    public AudioClip clip;
}

public class SoundManager : MonoBehaviour
{
    [Header("사운드 등록")]
    [SerializeField] Sound[] bgmSounds;


    [Header("사운드 플레이어")]
    [SerializeField] AudioSource bgmPlayer;

    private float Sound_Background = 0.5f;
    private float Sound_Effect = 0.5f;

    private UIManager _UIManager;
    public AudioSource effectSoundPlayer;
    public AudioClip getItem;
    public AudioClip drinking;
    public AudioClip eating;

    private bool canplay = true;
    public AudioSource playerSoundPlayer;
    public AudioClip moving;
    public AudioClip axing;
    public AudioClip picking;
    // option panal
    // public GameObject _optionpanel;
    // public GameObject _optionpanelfrommain;


    void Start()
    {
        _UIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        // playBGM1();
    }

    public void playBGM1()  //정화
    {
        bgmPlayer.Stop();
        bgmPlayer.clip = bgmSounds[0].clip;
        bgmPlayer.Play();
    }

    public void playBGM2()  //황폐화
    {
        bgmPlayer.Stop();
        bgmPlayer.clip = bgmSounds[1].clip;
        bgmPlayer.Play();
    }

    public void playBGM3()  //숲
    {
        bgmPlayer.Stop();
        bgmPlayer.clip = bgmSounds[2].clip;
        bgmPlayer.Play();
    }

    public void playBGM4()  //제단
    {
        bgmPlayer.Stop();
        bgmPlayer.clip = bgmSounds[3].clip;
        bgmPlayer.Play();
    }

    public void playRandom()  //음악가가 랜덤음악 재생
    {
        bgmPlayer.Stop();
        int random = Random.Range(0, 2);

        switch (random)
        {
            case 0:
                bgmPlayer.clip = bgmSounds[4].clip;
                break;
            case 1:
                bgmPlayer.clip = bgmSounds[5].clip;
                break;
        }

        bgmPlayer.Play();

        //UIManager에서 BGMChanger(string bgmName) 호출하기
        //_UIManager.BGMChanger(bgmName);
    }

    public void setBackgroundSound(Slider volume)
    {
        // Sound_Background = volume.value;
        // bgmPlayer.volume = Sound_Background;
    }

    public void setEffectSound(Slider volume)
    {
        // Sound_Effect = volume.value;
    }

    public float getBackgroundSound()
    {
        return Sound_Background;
    }

    public float getEffectSound()
    {
        return Sound_Effect;
    }

    public void playEffectSound(string action)
    {
        switch(action) {
            case "GETITEM":
                effectSoundPlayer.clip = getItem;
                break;
            case "DRINKING":
                effectSoundPlayer.clip = drinking;
                break;
            case "EATING":
                effectSoundPlayer.clip = eating;
                break;
            case "AXING":
                effectSoundPlayer.clip = axing;
                break;
            case "PICKING":
                effectSoundPlayer.clip = picking;
                break;
        }
        effectSoundPlayer.Play();
    }

    public void playerEffectSound(string action)
    {
        if(canplay){
            switch(action) {
                case "MOVING":
                    playerSoundPlayer.clip = moving;
                    canplay = false;
                    Invoke("CanPlayTrue", 0.4f);
                    break;
                case "RUNNING":
                    playerSoundPlayer.clip = moving;
                    canplay = false;
                    Invoke("CanPlayTrue", 0.35f);
                    break;
                case "AXING":
                    playerSoundPlayer.clip = axing;
                    break;
            }
            playerSoundPlayer.Play();
        }
    }

    private void CanPlayTrue() {
        canplay = true;
    }
}
