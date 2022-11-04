using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public int Sound_Background = 50;
    public int Sound_Effect = 50;

    // option panal
    public GameObject _optionpanel;
    public GameObject _optionpanelfrommain;

    public void setBackgroundSound(Slider slider)
    {
        Sound_Background = (int)slider.value;
        _optionpanel.transform.Find("BackgroundSlider").gameObject.GetComponent<Slider>().value = Sound_Background;
        _optionpanelfrommain.transform.Find("BackgroundSlider").gameObject.GetComponent<Slider>().value = Sound_Background;
    }

    public void setEffectSound(Slider slider)
    {
        Sound_Effect = (int)slider.value;
        _optionpanel.transform.Find("EffectSlider").gameObject.GetComponent<Slider>().value = Sound_Effect;
        _optionpanelfrommain.transform.Find("EffectSlider").gameObject.GetComponent<Slider>().value = Sound_Effect;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
