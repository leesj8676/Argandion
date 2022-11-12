using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundChange : MonoBehaviour
{
    bool current_purifier;
    // Start is called before the first frame update
    void Start()
    {
        current_purifier = true;   //시작은 정화된 지역에서
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other) {
        if( other.tag == "Sector")
        {

            //정화된 지역인지
            if(other.GetComponent<SectorObject>()._purifier) 
            {
                if(!current_purifier){  //황폐화 -> 정화
                    
                }
            }
            else{

            }

            //황폐화 지역인지 

        }
    }
}
