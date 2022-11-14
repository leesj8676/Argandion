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
    public AudioClip getItemSound;
    public AudioSource effectSoundPlayer;
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
                effectSoundPlayer.clip = getItemSound;
                break;
        }
        effectSoundPlayer.Play();
    }
}
