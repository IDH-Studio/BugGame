using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using CESCO;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameObject toolListObj;
    [SerializeField] private TOOL toolIndex = TOOL.HAND;
    [SerializeField] private GameObject joyStickObj;
    private JoyStick joyStick;
    [SerializeField] private Button hitButton;
    [SerializeField] private GameObject showSelectTools;

    private List<GameObject> hasTools;
    private List<TOOL> hasToolTypes;
    private GameObject selectTool = null;
    private int money;

    private bool[] isTouch;

    public List<GameObject> HasTools
    {
        get { return hasTools; }
    }

    public int Money
    {
        get { return money; }
        set { money = value; }
    }

    void Awake()
    {
        isTouch = new bool[]{ false, false };
        hasTools = new List<GameObject>();
        hasToolTypes = new List<TOOL>();
        money = 0;

        hitButton.onClick.AddListener(() =>
        {
            selectTool.GetComponent<Tool>().Hit();
        });

        joyStick = joyStickObj.GetComponent<JoyStick>();

        showSelectTools.SetActive(false);
        joyStickObj.SetActive(false);
    }

    private void OnEnable()
    {
        toolIndex = TOOL.HAND;
        BringTool();
        transform.position = Vector3.zero;
    }

    public void Init()
    {
        // 조이스틱 초기화
        //joyStick.Init();

        // 플레이어 정보 초기화
        transform.position = Vector3.zero;
        hasTools.Clear();
        hasToolTypes.Clear();
        money = 0;
        showSelectTools.SetActive(false);
        selectTool = null;

        // 가지고 있던 도구 삭제
        for(int i = 0; i < toolListObj.transform.childCount; ++i)
        {
            Destroy(toolListObj.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < showSelectTools.transform.childCount; ++i)
        {
            Destroy(showSelectTools.transform.GetChild(i).gameObject);
        }
    }

    private void OnDisable()
    {
        Init();
    }

    private void Update()
    {
        /*
         * 강화 버그 해결 후
         * 모바일 용으로 조이스틱 및 때리는 버튼 제작 후
         * 버튼을 통해 도구 변경 기능 제작 후 테스트
        */
        if (GameManager.instance.screenManager.CurrentScreen() != SCREEN.INGAME)
        {
            return;
        }

        if (joyStick.IsTouch)
        {
            selectTool.GetComponent<Tool>().Move(joyStick.MovePosition);
        }

        // PC 테스트 전용

        Vector2 movePos = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        selectTool.GetComponent<Tool>().Move(movePos.normalized);

        if (Input.GetKeyDown(KeyCode.Space)) selectTool.GetComponent<Tool>().Hit();

        //if (joyStick.IsTouch)
        //{
        //    selectTool.GetComponent<Tool>().Move(joyStick.MovePosition);
        //}

        //if (Input.GetMouseButtonDown(0)) selectTool.GetComponent<Tool>().Hit();
    }

    public void BringTool()
    {
        // toolManager에게서 도구를 받아오며 shopManager에게 도구를 받아온 사실을 알려줌
        GetTool(toolIndex);
        GameManager.instance.shopManager.UnLockTool(toolIndex); // shopManager에게 도구가 있다고 알려줌
        SelectTool();
    }

    public GameObject GetTool(TOOL tool)
    {
        // 어떤 도구인지 정보를 전달받은 후 해당 도구를 인스턴스화 하는 과정
        GameObject toolObj = GameManager.instance.toolManager.GiveTool(tool);
        GameObject toolObjInstance = Instantiate(toolObj, toolListObj.transform);

        // 해당 도구를 플레이어가 가지고 있다는 것을 알려줌
        toolObjInstance.GetComponent<Tool>().HasPlayer();

        // 강화할 때 어떤 도구를 가지고 있는지 알려줌
        GameManager.instance.reinforceManager.AddTool(toolObjInstance);
        
        // 플레이어가 가지고 있는 도구를 저장한다.
        hasTools.Add(toolObjInstance);
        hasToolTypes.Add(tool);

        // 플레이어가 선택할 수 있도록 버튼을 추가한다.
        GameObject toolButton = GameManager.instance.prefabManager.RequestInstantiate(OBJ_TYPE.PLAYER_HAS);
        toolButton.transform.SetParent(showSelectTools.transform);
        toolButton.transform.localScale = Vector3.one;
        // 버튼 이미지 수정
        toolButton.transform.GetChild(0).GetComponent<Image>().sprite =
            toolObjInstance.GetComponent<SpriteRenderer>().sprite;
        // 버튼 클릭 시 해당 도구로 변경
        toolButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            SelectTool(tool);
        });

        return toolObjInstance;
    }

    public GameObject GetTool(TOOL tool, GameObject toolObj)
    {
        // 어떤 도구인지 정보를 전달받은 후 해당 도구를 인스턴스화 하는 과정
        GameObject toolObjInstance = Instantiate(toolObj, toolListObj.transform);

        // 해당 도구를 플레이어가 가지고 있다는 것을 알려줌
        toolObjInstance.GetComponent<Tool>().HasPlayer();
        
        // 강화할 때 어떤 도구를 가지고 있는지 알려줌
        GameManager.instance.reinforceManager.AddTool(toolObjInstance);

        // 플레이어가 가지고 있는 도구를 저장한다.
        hasTools.Add(toolObjInstance);
        hasToolTypes.Add(tool);

        // 플레이어가 선택할 수 있도록 버튼을 추가한다.
        GameObject toolButton = GameManager.instance.prefabManager.RequestInstantiate(OBJ_TYPE.PLAYER_HAS);
        toolButton.transform.SetParent(showSelectTools.transform);
        toolButton.transform.localScale = Vector3.one;
        // 버튼 이미지 수정
        toolButton.transform.GetChild(0).GetComponent<Image>().sprite =
            toolObjInstance.GetComponent<SpriteRenderer>().sprite;
        // 버튼 클릭 시 해당 도구로 변경
        toolButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            SelectTool(tool);
        });

        return toolObjInstance;
    }

    void SelectTool()
    {
        if (selectTool != null)
        {
            selectTool.SetActive(false);
        }

        int index = hasToolTypes.IndexOf(toolIndex);
        selectTool = hasTools[index];
        selectTool.SetActive(true);
    }
    
    void SelectTool(TOOL tool)
    {
        if (selectTool != null)
        {
            selectTool.SetActive(false);
        }

        int index = hasToolTypes.IndexOf(tool);
        selectTool = hasTools[index];
        selectTool.SetActive(true);
    }

    //public void ChangeToolHand()
    //{
    //    toolIndex = TOOL.HAND;
    //    SelectTool();
    //}

    //public void ChangeToolFlySwatter()
    //{
    //    toolIndex = TOOL.FLY_SWATTER;
    //    SelectTool();
    //}

    public void ChangeMoney(int money)
    {
        this.money += money;
    }

    public List<GameObject> GetHasTools()
    {
        return hasTools;
    }

    public bool hasTool(TOOL tool)
    {
        return hasToolTypes.IndexOf(tool) != -1 ? true : false;
    }

    public void Hit()
    {
        selectTool.GetComponent<Tool>().Hit();
    }
}