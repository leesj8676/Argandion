using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dirt : MonoBehaviour
{
    private int howMany;
    public int watered;
    public int minusWater;
    public int temp;
    public bool isReady = false;
    public bool fullWater = false;
    public GameObject[] _nearObjects = new GameObject[25];
    ParticleSystem particleObject;

    public SystemManager _system;
    public GameObject _buffManagerObject;
    private BuffManager _buff;

    void Start()
    {
        _system = GameObject.Find("SystemManager").GetComponent<SystemManager>();
        particleObject = GetComponent<ParticleSystem>();
        temp = _system._day;
        _buffManagerObject = GameObject.Find("BuffManager");
        _buff = _buffManagerObject.GetComponent<BuffManager>();
    }

    void Update()
    {
        DayStart();
    }
    
    void DayStart()
    {
        if (temp != _system._day) {
            NewDay();
            temp = _system._day;
        }
    }

    public void Ready()
    {
        isReady = true;
    }

    void NewDay()
    {
        if (watered > 0)
        {
            Debug.Log("물빠짐!");
            watered -= minusWater;
            fullWater = false;
            if (_buff.bluePray)
            {
                watered += 25 * howMany;
                if (watered >= 1500) {
                    watered = 1500;
                    fullWater = true;
                }
            }
            if (watered < 0)
            {
                watered = 0;
                fullWater = false;
                particleObject.Stop();
            }
            else
            {
                for (int i = 0; i < _nearObjects.Length; i++)
                {
                    if (_nearObjects[i] != null)
                    {
                        Crop crop = _nearObjects[i].GetComponent<Crop>();
                        crop.growUp();
                    }
                    else
                    {
                    }
                }
            }
        }
    }

    public void Water()
    {
        fullWater = true;
        watered = 15000;
        particleObject.Play();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("crop"))
        {
            if (!other.gameObject.GetComponent<Crop>().isIn)
            {
                for (int idx = 0; idx < 25; idx++)
                {
                    if (_nearObjects[idx] == null)
                    {
                        Debug.Log(idx + "에 넣었어!");
                        _nearObjects[idx] = other.gameObject;
                        other.gameObject.GetComponent<Crop>().isIn = true;
                        howMany += 1;
                        break;
                    }
                }
            }
        }
    }

    public void CropGrowUp(GameObject crop)
    {
        for (int idx = 0; idx < 25; idx++)
        {
            if (_nearObjects[idx] == crop)
            {
                _nearObjects[idx] = null;
                howMany -= 1;
                break;
            }
        }
    }
}
