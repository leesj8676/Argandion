using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldTree : MonoBehaviour
{
    private SystemManager _systemManager;
    private UIManager _uiManager;
    private PlayerSystem _playerSystem;

    [SerializeField] private GameObject[] _teleportOutside = new GameObject[2]; // 시작 전 넣어주기 // 텔포할 곳

    void Start()
    {
        _systemManager = GameObject.Find("SystemManager").GetComponent<SystemManager>();
        _uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        _playerSystem = GameObject.Find("PlayerObject").GetComponent<PlayerSystem>();
    }

    public void Interaction()
    {
        _uiManager.OnConversationPanel(9);
    }

    // '발전도' 변하면 call하기
    public void ChangeTreeLevel()
    {
        int devLevel = _systemManager._development_level; // 변한 발전도
        int season = _systemManager._season; // 변한 계절
        gameObject.transform.GetChild(devLevel - 1).gameObject.SetActive(false);
        gameObject.transform.GetChild(devLevel).gameObject.SetActive(true);
        for (int i = 0; i < 4; i++)
        {
            gameObject.transform.GetChild(devLevel).gameObject.transform.GetChild(i).gameObject.SetActive(false);
        }
        gameObject.transform.GetChild(devLevel).gameObject.transform.GetChild(season).gameObject.SetActive(true);
    }

    // '계절' 변하면 call하기
    public void ChangeSeason()
    {
        int season = _systemManager._season; ; // 변한 계절
        if (season == 0)
        {
            gameObject.transform.GetChild(3).gameObject.SetActive(false);
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            gameObject.transform.GetChild(season - 1).gameObject.SetActive(false);
            gameObject.transform.GetChild(season).gameObject.SetActive(true);
        }

    }

    // 세계수 텔레포트 코드
    public void doTeleport(int num)
    {
        GameObject tp = _teleportOutside[num];
        _playerSystem.transform.position = new Vector3(tp.transform.position.x, tp.transform.position.y, tp.transform.position.z);
    }

}