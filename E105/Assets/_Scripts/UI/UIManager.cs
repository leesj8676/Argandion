using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    // Panel var
    private GameObject _baseuipanel;
    private GameObject _mapuipanel;
    private MainPagePanel _mainpage;
    private OptionPanel _optionpanel;
    private OptionPanel _optionfrommain;

    private CreateCharacter _createcharacter;

    private ConversationPanel _conversationpanel;

    private CraftingPanel _craftingpanel;
    private CookingPanel _cookingpanel;

    private BuildEventPanel _buildeventpanel;

    private TransactionAnimalPanel _transactionanimalpanel;
    private TransactionPanel _transactionpanel;
    private InventoryPanel _inventorypanel;
    private GameObject _storagepanel;
    private TradeModal _trademodal;
    private GameObject _inventory;
    private TextMeshProUGUI _invenMoney;

    private GameObject _notificationpanel;
    private ResultNotificationPanel _resultnotificationpanel;
    private TransactionDoubleCheck _transactiondoublecheck;

    public GameObject _eventAnnounce;
    public TextMeshProUGUI _announceTitle;
    public TextMeshProUGUI _announceText;

    private GameObject _nowequip;

    [SerializeField] private SystemManager _systemmanager;
    [SerializeField] private PlayerSystem _playersystem;
    [SerializeField] private Item _itemmanager;
    [SerializeField] private WorldTree _worldtree;
    [SerializeField] private TeleportAltar _alterdown;
    [SerializeField] private TeleportAltar _alterup;

    private Slider _healthbar;
    private Slider _energybar;
    public RectTransform _timer;
    public GameObject _daytime;

    // 상태 저장 데이터
    public Quaternion rotateZero = Quaternion.Euler(new Vector3(0, 0, 0));     // 회전값 기본값 세팅

    public int conversationNPC;
    private int selectCharacter;
    private bool isPressESC;
    private bool isMyHome;

    // 패널 오픈 여부 변수
    [SerializeField] private bool isTransactionOpen;
    [SerializeField] private bool isInventoryOpen;
    [SerializeField] private bool isInvenRightModal;
    [SerializeField] private bool isStorageOpen;

    // 주연 추가
    public GameObject _eventpanel;
    // private EventManager _eventmanager;
    public FoodManager _foodmanager;

    private Dictionary<int, Sprite> Dic = new Dictionary<int, Sprite>();

    public Sprite getItemIcon(int key)
    {
        if (Dic.ContainsKey(key))
        {
            return Dic[key];
        }
        Sprite icon = Resources.Load<Sprite>("Sprites/" + key);
        Dic.Add(key, icon);
        return icon;
    }
    // Start is called before the first frame update
    void Start()
    {
        conversationNPC = 0;
        selectCharacter = -1;
        isPressESC = false;
        isMyHome = false;
        isTransactionOpen = false;
        isInventoryOpen = false;
        isStorageOpen = false;

        _systemmanager = GameObject.Find("SystemManager").GetComponent<SystemManager>();
        _playersystem = GameObject.Find("PlayerObject").GetComponent<PlayerSystem>();
        _itemmanager = GameObject.Find("ItemManager").GetComponent<Item>();
        _foodmanager = GameObject.Find("FoodManager").GetComponent<FoodManager>();
        _worldtree = GameObject.Find("WorldTree").GetComponent<WorldTree>();
        // _alterdown = GameObject.Find("teleportDown").GetComponent<TeleportAltar>();
        // _alterup = GameObject.Find("teleportUp").GetComponent<TeleportAltar>();
        // _eventmanager = GameObject.Find("EventManager").GetComponent<EventManager>();


        _systemmanager.setPlayerGold(245000);

        _baseuipanel = gameObject.transform.Find("BaseUIPanel").gameObject;
        _healthbar = _baseuipanel.transform.GetChild(0).GetComponent<Slider>();
        _energybar = _baseuipanel.transform.GetChild(1).GetComponent<Slider>();
        _eventpanel = _baseuipanel.transform.GetChild(4).gameObject;
        _daytime = _baseuipanel.transform.GetChild(2).GetChild(1).gameObject;
        _foodmanager._eventPanel = _eventpanel.GetComponent<EventPanel>();
        // _food.settingEventPanel();
        // _eventmanager.setting();

        _nowequip = GameObject.Find("NowEquip").gameObject;
        _baseuipanel.SetActive(false);

        _mapuipanel = GameObject.Find("MapUIPanel");
        _mapuipanel.SetActive(false);

        _mainpage = gameObject.transform.Find("MainPagePanel").GetComponent<MainPagePanel>();

        _optionpanel = gameObject.transform.Find("OptionPanel").GetComponent<OptionPanel>();
        _optionpanel.gameObject.SetActive(false);

        _optionfrommain = gameObject.transform.Find("OptionPanelFromMainPage").GetComponent<OptionPanel>();
        _optionfrommain.gameObject.SetActive(false);

        _createcharacter = gameObject.transform.Find("CreateCharacter").GetComponent<CreateCharacter>();
        _createcharacter.gameObject.SetActive(false);

        _conversationpanel = gameObject.transform.Find("ConversationPanel").GetComponent<ConversationPanel>();
        _conversationpanel.gameObject.SetActive(false);

        _cookingpanel = gameObject.transform.Find("CookingPanel").GetComponent<CookingPanel>();
        _cookingpanel.gameObject.SetActive(false);

        _craftingpanel = gameObject.transform.Find("CraftingPanel").GetComponent<CraftingPanel>();
        _craftingpanel.gameObject.SetActive(false);

        _buildeventpanel = gameObject.transform.Find("BuildEventPanel").GetComponent<BuildEventPanel>();
        _buildeventpanel.gameObject.SetActive(false);

        _transactionanimalpanel = gameObject.transform.Find("TransactionAnimalPanel").GetComponent<TransactionAnimalPanel>();
        _transactionanimalpanel.gameObject.SetActive(false);

        _transactionpanel = gameObject.transform.Find("TransactionPanel").GetComponent<TransactionPanel>();
        _transactionpanel.gameObject.SetActive(false);

        _inventorypanel = gameObject.transform.Find("InventoryPanel").GetComponent<InventoryPanel>();
        _inventorypanel.gameObject.SetActive(false);

        _storagepanel = gameObject.transform.Find("StoragePanel").gameObject;
        _storagepanel.SetActive(false);

        _trademodal = gameObject.transform.Find("TradeModal").GetComponent<TradeModal>();
        _trademodal.gameObject.SetActive(false);

        _inventory = gameObject.transform.Find("Inventory").gameObject;
        _invenMoney = _inventory.transform.GetChild(2).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
        _invenMoney.text = _systemmanager.getPlayerGold().ToString();
        _inventory.gameObject.SetActive(false);

        _notificationpanel = GameObject.Find("NotificationPanel");
        _notificationpanel.SetActive(false);
        _resultnotificationpanel = gameObject.transform.Find("ResultNotificationPanel").GetComponent<ResultNotificationPanel>();
        _resultnotificationpanel.gameObject.SetActive(false);
        _transactiondoublecheck = gameObject.transform.Find("TransactionDoubleCheckModal").GetComponent<TransactionDoubleCheck>();
        _transactiondoublecheck.gameObject.SetActive(false);

        _eventAnnounce = GameObject.Find("EventUIAnnounce");
        _announceTitle = _eventAnnounce.transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>();
        _announceText = _eventAnnounce.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>();
        _eventAnnounce.SetActive(false);

        ItemObject item1 = findItem(2);
        acquireItem(item1, 98);
        ItemObject item2 = findItem(3);
        acquireItem(item2, 99);
        ItemObject item3 = findItem(106);
        acquireItem(item3, 30);
        ItemObject item4 = findItem(127);
        acquireItem(item4, 99);
        acquireItem(item4, 99);
        acquireItem(item4, 10);
        ItemObject item5 = findItem(302);
        acquireItem(item5, 1);
        ItemObject item10 = findItem(110);
        acquireItem(item10, 1);
        ItemObject item11 = findItem(120);
        acquireItem(item11, 1);

        ItemObject item6 = findItem(54);
        acquireItem(item6, 6);
        ItemObject item7 = findItem(4);
        acquireItem(item7, 35);
        ItemObject item8 = findItem(21);
        acquireItem(item8, 15);
        ItemObject item9 = findItem(12);
        acquireItem(item9, 15);

        _storagepanel.transform.GetChild(1).GetChild(4).GetComponent<Storage>().AcquireItem(findItem(2), 9990);
    }

    // Update is called once per frame
    void Update()
    {
        setTimer();

        if (Input.GetButtonDown("optionKey"))
        {
            pressedESC();
        }

        if (Input.GetButtonDown("InventoryKey"))
        {
            if (getGameState())
            {
                OnInventoryPanel();
            }
        }
        if (Input.GetButtonDown("mapKey"))
        {
            if (getGameState() && !isMyHome)
            {
                OnMapUIPanel();
            }
        }

        // if (Input.GetButtonDown("interactionKey") && _conversationpanel.GetComponent<ConversationPanel>().getIsOn())
        // {
        //     _conversationpanel.GetComponent<ConversationPanel>().secondConversation();
        // }

        if (Input.GetButtonDown("interactionKey") && isInvenRightModal)
        {
            closeInvenRightClickModal();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            // test buildEvent
            if (_buildeventpanel.GetComponent<BuildEventPanel>().isOnPanel)
            {
                _buildeventpanel.GetComponent<BuildEventPanel>().closeWindow();
            }
            else
            {
                int random = Random.Range(1, 7);
                OnBuildEventPanel(random);
                // OnBuildEventPanel(3);
            }
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            int randNum = Random.Range(1, 7);
            if (randNum == 3 || randNum == 5)
            {
                randNum++;
            }
            if (!_craftingpanel.gameObject.activeSelf)
            {
                OnCraftingPanel(randNum);
            }
            else
            {
                _craftingpanel.GetComponent<CraftingPanel>().closePanel();
            }
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            OnCookingPanel();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            // test conversation
            int conversationCnt = _conversationpanel.GetComponent<ConversationPanel>().getConversationCnt();
            if (_conversationpanel.GetComponent<ConversationPanel>().getIsConversation())
            {
                _conversationpanel.GetComponent<ConversationPanel>().conversation();
            }
            else
            {
                int randomNum = Random.Range(1, 12);
                // randomNum = 1;
                // randomNum = 2;
                // randomNum = 3;
                // randomNum = 4;
                // randomNum = 5;
                // randomNum = 6;
                // randomNum = 9;
                // randomNum = 10;
                randomNum = Random.Range(11, 13);
                switch (conversationCnt)
                {
                    case -1:
                        OnConversationPanel(randomNum);
                        break;
                    case 0:
                        if (conversationNPC == 9)
                        {
                            break;
                        }
                        _conversationpanel.GetComponent<ConversationPanel>().secondConversation();
                        break;
                    case 1:
                        _conversationpanel.GetComponent<ConversationPanel>().thirdConversation();
                        break;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            _conversationpanel.conversationWhenAlterBuff();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            OnStoragePanel();
            OnInventory(3);
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            _playersystem.changeHealth(30);
            _playersystem.changeEnergy(30);
        }
    }

    // ======================== UI 호출 함수 Start

    public void OnBaseUIPanel()
    {
        _baseuipanel.SetActive(true);
    }
    public void OnTransactionPanel()
    {
        _transactionpanel.GetComponent<TransactionPanel>().OnPanel(conversationNPC);
        _conversationpanel.GetComponent<ConversationPanel>().resetConversationPanel();
        // conversationNPC = 0;
        stopControllPlayer();
        OnInventory(2);
    }

    public void OnTransactionAnimalPanel()
    {
        _transactionanimalpanel.GetComponent<TransactionAnimalPanel>().onPanel();
        _conversationpanel.GetComponent<ConversationPanel>().resetConversationPanel();
        conversationNPC = 0;
        stopControllPlayer();
    }

    public void OnCraftingPanel(int value)
    {
        stopControllPlayer();
        _craftingpanel.GetComponent<CraftingPanel>().OnPanel(value);
    }

    public void OnCookingPanel()
    {
        Debug.Log("온쿠킹");
        stopControllPlayer();
        Debug.Log("스탑");
        _cookingpanel.GetComponent<CookingPanel>().openCooking();
    }

    public void OnBuildEventPanel(int value)
    {
        stopControllPlayer();
        // int step = _systemmanager.getPuriCount();
        _buildeventpanel.GetComponent<BuildEventPanel>().OnPanel(value, 1);
    }

    public void OnStoragePanel()
    {
        stopControllPlayer();
        _storagepanel.GetComponent<StoragePanel>().handlePanel();
    }

    public void OnInventoryPanel()
    {
        if (_inventorypanel.gameObject.activeSelf)
        {
            runControllPlayer();
        }
        else
        {
            stopControllPlayer();
        }
        _inventorypanel.GetComponent<InventoryPanel>().handlePanel();
    }

    public void OnConversationPanel(int value)
    {
        _conversationpanel.GetComponent<ConversationPanel>().setConversationNPC(value);
        stopControllPlayer();
    }

    public void OnCreateCharacter()
    {
        _createcharacter.gameObject.SetActive(true);
    }

    public void OnMainPagePanel()
    {
        _mainpage.gameObject.SetActive(true);
        setGameState(false);
        // isGameStart = false;
    }

    public void OnMapUIPanel()
    {
        if (getGameState())
        {
            if (_mapuipanel.activeSelf)
            {
                _mapuipanel.SetActive(false);
            }
            else
            {
                _mapuipanel.SetActive(true);
            }
        }
    }

    public void OnNotificationPanel()
    {
        _notificationpanel.SetActive(true);
    }

    public void OnResultNotificationPanel(string text)
    {
        _resultnotificationpanel.GetComponent<ResultNotificationPanel>().handelNoti(text);
    }

    public void OnTransactionDoubleCheckPanel(string name, int store, int itemIdx, int itemCode)
    {
        // Debug.Log("============ " + itemIdx);
        _transactiondoublecheck.setData(name, store, itemIdx, itemCode);
        _transactiondoublecheck.handleModal();
    }

    public void OnTradeModal(string name, string iconName, int maxCnt, int cost, int checkMod, int storeIdx, int itemIdx)
    {
        int _storeKey = storeIdx;
        if (_storeKey == -1)
        {
            _storeKey = conversationNPC;
        }
        // Debug.Log(_storeKey);
        _trademodal.GetComponent<TradeModal>().setModal(name, iconName, maxCnt, cost, checkMod, _storeKey, itemIdx);
    }

    public void closeTradeModal()
    {
        _trademodal.GetComponent<TradeModal>().closeModal();
    }

    public void OnInventory(int value)
    {
        switch (value)
        {
            case 1:
                _inventory.GetComponent<RectTransform>().SetLocalPositionAndRotation(new Vector3(110, -30, 0), rotateZero);
                break;
            case 2:
                _inventory.GetComponent<RectTransform>().SetLocalPositionAndRotation(new Vector3(110, -15.06f, 0), rotateZero);
                break;
            case 3:
                _inventory.GetComponent<RectTransform>().SetLocalPositionAndRotation(new Vector3(-211.66f, -5.58f, 0), rotateZero);
                _inventory.transform.GetChild(1).GetComponent<Image>().color = new Color(225, 225, 225, 0);
                _inventory.transform.GetChild(1).GetChild(1).GetComponent<Image>().color = new Color(225, 225, 225, 0);
                _inventory.transform.GetChild(1).GetChild(2).GetComponent<Image>().color = new Color(225, 225, 225, 0);
                break;
        }
        _inventory.gameObject.SetActive(!_inventory.gameObject.activeSelf);
    }

    // ======================= UI 호출 함수 End

    // 패널 오픈 여부 함수
    public bool getIsOpenTransaction()
    {
        return isTransactionOpen;
    }

    public void setIsOpenTransaction(bool value)
    {
        isTransactionOpen = value;
    }

    public bool getIsOpenInventory()
    {
        return isInventoryOpen;
    }

    public void setIsOpenInventory(bool value)
    {
        isInventoryOpen = value;
    }

    public bool getIsOpenStorage()
    {
        return isStorageOpen;
    }

    public void setIsOpenStorage(bool value)
    {
        isStorageOpen = value;
    }

    // 캐릭터 선택 관련 함수
    public void setCharacterValue(int value)
    {
        selectCharacter = value;
    }

    // ======================= Base UI 관련 함수
    public void setHealthBar(float value)
    {
        _healthbar.value = value;
    }

    public void setEnergyBar(float value)
    {
        _energybar.value = value;
    }

    public void setTimer()
    {
        float angle = (_systemmanager._hour_display - 6) * 15 + (_systemmanager._minute_display / 4);
        _timer.rotation = Quaternion.Euler(180, 0, angle);
    }

    // ======================= Base UI 관련 함수 끝

    // 동물 수 동기화 함수
    public void syncAnimalPanel(int capacity, int sheepCnt, int chickenCnt, int cowCnt)
    {
        _transactionanimalpanel.syncRanchData(capacity, sheepCnt, chickenCnt, cowCnt);
    }

    // ESC 클릭 시 동작
    public void pressedESC()
    {
        isPressESC = !isPressESC;

        if (isPressESC)
        {
            if (getGameState())
            {
                _optionpanel.gameObject.SetActive(true);
            }
            else
            {
                _optionfrommain.gameObject.SetActive(true);
            }
            _playersystem._canMove = false;
        }
        else
        {
            if (getGameState())
            {
                _optionpanel.gameObject.SetActive(false);
            }
            else
            {
                _optionfrommain.gameObject.SetActive(false);
            }
            _playersystem._canMove = true;
        }
    }

    // inventory 접근 함수
    public bool checkInventory(ItemObject _item, int _count)
    {
        Inventory inven = _inventory.transform.GetChild(1).GetComponent<Inventory>();
        return inven.CheckInven(_item, _count);
    }

    public void acquireItem(ItemObject _item, int _count)
    {
        _inventory.transform.GetChild(1).GetComponent<Inventory>().AcquireItem(_item, _count);
    }

    public void reductItem(ItemObject _item, int _count)
    {
        _inventory.transform.GetChild(1).GetComponent<Inventory>().ReductItem(_item, _count);
    }

    public void sellItem(int slotIdx, int _count, int _key)
    {
        if (_key == 1)
        {
            _inventory.transform.GetChild(1).GetComponent<Inventory>().SellInventoryItem(slotIdx, _count);
        }
        else if (_key == 2)
        {
            _inventory.transform.GetChild(0).GetComponent<Quickslot>().SellQuickslotItem(slotIdx, _count);
        }
        else if (_key == 3)
        {
            switch (slotIdx)
            {
                case 0:
                    _inventorypanel.transform.GetChild(0).GetChild(2).GetChild(1).GetComponent<Slot>().SetSlotCount(-1);
                    break;
                case 1:
                    _inventorypanel.transform.GetChild(0).GetChild(2).GetChild(2).GetComponent<Slot>().SetSlotCount(-1);
                    break;
                case 2:
                    _inventorypanel.transform.GetChild(0).GetChild(2).GetChild(3).GetComponent<Slot>().SetSlotCount(-1);
                    break;
                case 3:
                    _inventorypanel.transform.GetChild(0).GetChild(2).GetChild(4).GetComponent<Slot>().SetSlotCount(-1);
                    break;
            }
        }
    }

    public void onSlotOverModal(string _text, Vector3 _position)
    {
        _inventory.transform.GetChild(3).gameObject.SetActive(true);
        _inventory.transform.GetChild(3).GetComponentInChildren<TextMeshProUGUI>().text = _text;
        _inventory.transform.GetChild(3).transform.position = _position;
    }

    public void offSlotOverModal()
    {
        _inventory.transform.GetChild(3).gameObject.SetActive(false);
    }

    // 인벤 마우스 우클릭
    public void clickRightSlotModal(int _key, Vector3 _position, ItemObject _item, int _count, int _index)
    {
        isInvenRightModal = true;
        Debug.LogWarning("마우스 우클릭 모달 호출");
        _inventory.transform.GetChild(4).gameObject.SetActive(true);
        _inventory.transform.GetChild(4).transform.position = _position;
        switch (_key)
        {
            case 1:
                _inventory.transform.GetChild(4).GetChild(0).gameObject.SetActive(true);
                _inventory.transform.GetChild(4).GetChild(0).GetComponent<Button>().onClick.RemoveAllListeners();
                _inventory.transform.GetChild(4).GetChild(0).GetComponent<Button>().onClick.AddListener(() => rightEquip(_item, _count, _index));
                break;
            case 2:
                _inventory.transform.GetChild(4).GetChild(2).gameObject.SetActive(true);
                _inventory.transform.GetChild(4).GetChild(2).GetComponent<Button>().onClick.RemoveAllListeners();
                _inventory.transform.GetChild(4).GetChild(2).GetComponent<Button>().onClick.AddListener(() => rightQuick(_item, _count, _index));
                break;
            case 3:
                _inventory.transform.GetChild(4).GetChild(1).gameObject.SetActive(true);
                _inventory.transform.GetChild(4).GetChild(1).GetComponent<Button>().onClick.RemoveAllListeners();
                _inventory.transform.GetChild(4).GetChild(1).GetComponent<Button>().onClick.AddListener(() => rightUse(_item, _count, _index));
                break;
            case 4:
                _inventory.transform.GetChild(4).GetChild(3).gameObject.SetActive(true);
                _inventory.transform.GetChild(4).GetChild(3).GetComponent<Button>().onClick.RemoveAllListeners();
                _inventory.transform.GetChild(4).GetChild(3).GetComponent<Button>().onClick.AddListener(() => rightDismiss(_item, _count, _index));
                break;

        }
    }

    // 인벤 우클릭 모달 close
    public void closeInvenRightClickModal()
    {
        isInvenRightModal = false;
        _inventory.transform.GetChild(4).gameObject.SetActive(false);
        for (int i = 0; i < 4; i++)
        {
            _inventory.transform.GetChild(4).GetChild(i).gameObject.SetActive(false);
        }
    }

    // 인벤토리 아이템 처리 - 우클릭
    public void rightEquip(ItemObject _item, int _count, int invenIdx)
    {
        // Debug.Log("여기 몇 번 동작해?");
        ItemObject itemObj = null;
        int equiptCnt = -1;
        switch (_item.ItemCode)
        {
            case 400:
                equiptCnt = _inventorypanel.transform.GetChild(0).GetChild(2).GetChild(1).GetComponent<Slot>().getSlotItemCount();
                if (equiptCnt > 0)
                {
                    itemObj = _inventorypanel.transform.GetChild(0).GetChild(2).GetChild(1).GetComponent<Slot>().getSlotItemData();
                }

                _inventorypanel.transform.GetChild(0).GetChild(2).GetChild(1).GetComponent<Slot>().AddItem(_item);
                sellItem(invenIdx, _count, 1);
                setPlayerEquipSlot(_item.ItemCode, 0);
                break;
            case 401:
                equiptCnt = _inventorypanel.transform.GetChild(0).GetChild(2).GetChild(2).GetComponent<Slot>().getSlotItemCount();
                if (equiptCnt > 0)
                {
                    itemObj = _inventorypanel.transform.GetChild(0).GetChild(2).GetChild(2).GetComponent<Slot>().getSlotItemData();
                }

                _inventorypanel.transform.GetChild(0).GetChild(2).GetChild(2).GetComponent<Slot>().AddItem(_item);
                sellItem(invenIdx, _count, 1);
                setPlayerEquipSlot(_item.ItemCode, 1);
                break;
            case 402:
                equiptCnt = _inventorypanel.transform.GetChild(0).GetChild(2).GetChild(3).GetComponent<Slot>().getSlotItemCount();
                if (equiptCnt > 0)
                {
                    itemObj = _inventorypanel.transform.GetChild(0).GetChild(2).GetChild(3).GetComponent<Slot>().getSlotItemData();
                }

                _inventorypanel.transform.GetChild(0).GetChild(2).GetChild(3).GetComponent<Slot>().AddItem(_item);
                sellItem(invenIdx, _count, 1);
                setPlayerEquipSlot(_item.ItemCode, 2);
                break;
            case 403:
            case 404:
                equiptCnt = _inventorypanel.transform.GetChild(0).GetChild(2).GetChild(4).GetComponent<Slot>().getSlotItemCount();
                if (equiptCnt > 0)
                {
                    itemObj = _inventorypanel.transform.GetChild(0).GetChild(2).GetChild(4).GetComponent<Slot>().getSlotItemData();
                }

                _inventorypanel.transform.GetChild(0).GetChild(2).GetChild(4).GetComponent<Slot>().AddItem(_item);
                sellItem(invenIdx, _count, 1);
                setPlayerEquipSlot(_item.ItemCode, 3);
                break;
            case 502:
            case 503:
            case 504:
                _baseuipanel.transform.GetChild(3).GetChild(1).GetComponentInChildren<Slot>().AddItem(_item, _count);
                setPlayerQuickSlot(8, _item.ItemCode, _count);
                break;
            default:
                break;
        }

        if (itemObj != null)
        {
            acquireItem(itemObj, 1);
        }

        closeInvenRightClickModal();
    }

    public void rightQuick(ItemObject _item, int _count, int _index)
    {
        if (_inventory.transform.GetChild(0).GetComponent<Quickslot>().CheckInven(_item, _count))
        {
            _inventory.transform.GetChild(0).GetComponent<Quickslot>().AcquireItem(_item, _count);
            sellItem(_index, _count, 1);
        }
        else
        {
            OnResultNotificationPanel("퀵슬롯에 빈 공간이 없습니다.");
        }
        closeInvenRightClickModal();
    }

    public void rightDismiss(ItemObject _item, int _count, int _index)
    {
        if (checkInventory(_item, _count))
        {
            if (_item.Category == "옷")
            {
                sellItem(_index, _count, 3);
            }
            else
            {
                sellItem(_index, _count, 2);
            }
            acquireItem(_item, _count);
        }
        closeInvenRightClickModal();
    }

    public void rightUse(ItemObject _item, int _count, int _index)
    {
        Debug.Log(_item.ItemCode);
        _foodmanager.UseFood(_item.ItemCode);
        sellItem(_index, 1, 1);
        closeInvenRightClickModal();
    }

    public void quickUse(int _itemCode, int _count, int _slotIdx)
    {
        if (findItem(_itemCode).Category == "식량")
        {
            _foodmanager.UseFood(_itemCode);
        }

        if (_slotIdx == 8)
        {
            reductItem(findItem(_itemCode), -1 * _count);
        }
        else
        {
            sellItem(_slotIdx, _count, 2);
        }
    }

    public Slot[] getInventorySlots()
    {
        return _inventory.transform.GetChild(1).GetComponent<Inventory>().getInventorySlots();
    }

    // 창고 관련
    public void addToStorage(ItemObject _item, int _count, int slotIdx)
    {
        int value = _storagepanel.transform.GetChild(1).GetChild(4).GetComponent<Storage>().checkStorage(_item, _count);
        if (value <= 0)
        {
            _storagepanel.transform.GetChild(1).GetChild(4).GetComponent<Storage>().AcquireItem(_item, _count);
            _inventory.transform.GetChild(1).GetComponent<Inventory>().SellInventoryItem(slotIdx, _count);
        }
        else if (value == _count)
        {
            OnResultNotificationPanel("창고에 " + _item.Name + "이(가) 가득 찼습니다.");
        }
        else
        {
            _storagepanel.transform.GetChild(1).GetChild(4).GetComponent<Storage>().AcquireItem(_item, _count - value);
            _inventory.transform.GetChild(1).GetComponent<Inventory>().SellInventoryItem(slotIdx, _count - value);
        }
    }

    public void removeToStorage(ItemObject _item, int _count, int slotIdx)
    {
        int nowCnt = _count;
        while (nowCnt > 99)
        {
            if (checkInventory(_item, 99))
            {
                acquireItem(_item, 99);
                _storagepanel.transform.GetChild(1).GetChild(4).GetComponent<Storage>().AcquireItem(_item, -99);
                nowCnt -= 99;
            }
        }
        if (checkInventory(_item, nowCnt))
        {
            acquireItem(_item, nowCnt);
            _storagepanel.transform.GetChild(1).GetChild(4).GetComponent<Storage>().AcquireItem(_item, -1 * nowCnt);
        }
    }

    // 제작관련 함수
    public CraftingPanel getCraftPanel()
    {
        return _craftingpanel;
    }

    // 플레이어 함수 관련
    private void stopControllPlayer()
    {
        _playersystem._canMove = false;
    }

    public void runControllPlayer()
    {
        _playersystem._canMove = true;
    }

    public void runCookingAnimation()
    {
        Debug.LogWarning("======== ui cooking call ========");
        // _playersystem._playerAnimator.SetInteger("action", 6);
        _playersystem.setAnimator(6, 5.0f);
        Debug.LogWarning(_playersystem._playerAnimator);
        Debug.LogWarning(_playersystem._playerAnimator.GetInteger("action"));
    }

    // input 관련 함수
    public void toggleCanInteract()
    {
        _playersystem.toggleCanInteract();
    }

    // item 관련 함수
    public ItemObject findItem(int value)
    {
        return _itemmanager.FindItem(value);
    }

    // 퀵슬롯 변경 함수
    public void setEquipPointer(int num)
    {
        _nowequip.transform.SetLocalPositionAndRotation(new Vector3((num - 1) * 31 + 2, 0, 0), rotateZero);
    }

    // 퀵슬롯 동기 함수
    public void syncQuickSlot()
    {
        Slot[] baseuiQuick = _baseuipanel.transform.GetChild(3).GetChild(0).GetChild(0).GetComponentsInChildren<Slot>();
        Slot[] quickSlotData = _inventory.transform.GetChild(0).GetComponent<Quickslot>().getInventorySlots();

        for (int i = 0; i < 7; i++)
        {
            if (quickSlotData[i].itemCount <= 0)
            {
                baseuiQuick[i].SetSlotCount(-1 * baseuiQuick[i].itemCount);
            }
            else
            {
                baseuiQuick[i].AddItem(quickSlotData[i].item, quickSlotData[i].itemCount);
            }
        }
    }

    // 집인지 확인하는 코드
    public void setIsHome(bool value)
    {
        isMyHome = value;
        _baseuipanel.transform.GetChild(2).gameObject.SetActive(!value);
    }

    // 소지금 관련
    public int getPlayerGold()
    {
        int gold = _systemmanager.getPlayerGold();
        _invenMoney.text = gold.ToString();
        return gold;
    }

    public void addPlayerGold(int value)
    {
        _systemmanager.addPlayerGold(value);
        _invenMoney.text = getPlayerGold().ToString();
    }

    // 플레이어 선택
    public void selectPlayer()
    {
        _playersystem.ChangePlayerCharacter(selectCharacter);
    }

    public void setPlayerName(string _name)
    {
        _playersystem.setPlayerName(_name);
    }


    // 플레이어 체력 / 기력
    public void healPlayer()
    {
        _playersystem.changeHealth(-10000);
        _playersystem.changeEnergy(-10000);
        _conversationpanel.selectHeal();
    }

    // 플레이어 장비 관련
    public void setPlayerQuickSlot(int index, int itemCode, int count)
    {
        _playersystem.setQuickItem(index, itemCode, count);
    }

    public void setPlayerEquipSlot(int itemCode, int idx)
    {
        _playersystem.setEquipItem(itemCode, idx);
    }

    // 사운드 관련
    public void playRandomBGM()
    {
        // GameObject.Find("SoundManager").GetComponent<SoundManager>().playRandom();
    }
    // public void BGMChanger(string _bgmName)
    // {
    //     _conversationpanel.selectMusic(_bgmName);
    // }

    // 제사 관련 코드
    public void prayToAltar(int _nowFlowerCode, int _newFlowerCode)
    {

    }

    // 세계수 텔포
    public void doTeleport(int value)
    {
        _worldtree.doTeleport(value);
        _conversationpanel.resetConversationPanel();
    }

    // 제단 텔포
    public void upTeleport()
    {
        _alterdown.goUp();
    }

    public void downTeleport()
    {
        // _alterup.goDown();
    }

    // 게임 시작 종료
    public void setGameState(bool value)
    {
        _systemmanager.setGameState(value);
        // isGameStart = value;
    }

    public bool getGameState()
    {
        return _systemmanager.getGameState();
        // return isGameStart;
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    public void DayStart()
    {
        Transform trans = _daytime.GetComponent<RectTransform>().transform;
        Image img = _daytime.GetComponent<Image>();
        if (_systemmanager._buffManager.whitePray || _systemmanager._buffManager.whiteSpirit)
        {
            trans.SetLocalPositionAndRotation(trans.localPosition, Quaternion.Euler(180, 180, 15));
            img.fillAmount = 0.83333f;
        }
        else
        {
            trans.SetLocalPositionAndRotation(trans.localPosition, Quaternion.Euler(180, 180, 0));
            img.fillAmount = 0.70833f;
        }
    }
}
