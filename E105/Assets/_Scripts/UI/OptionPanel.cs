using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionPanel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setSound()
    {

    }

    public void handelPanel()
    {
        UIManager ui = this.GetComponentInParent<UIManager>();
        ui.pressedESC();
    }
}
