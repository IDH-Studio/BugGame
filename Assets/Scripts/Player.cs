using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using CESCO;
using UnityEngine.UI;
using UnityEditor;

public class Player : MonoBehaviour
{
    [SerializeField] private TOOL toolIndex = TOOL.HAND;
    [SerializeField] private GameObject joyStickObj;
    [SerializeField] private Button hitButton;
    [SerializeField] private GameObject showSelectTools;
    [SerializeField] private GameObject showHitPos;
    [SerializeField] private GameObject showToolsButton;
    public GameObject toolListObj;

    private JoyStick joyStick;
    [SerializeField] private List<GameObject> hasTools;
    [SerializeField] private List<TOOL> hasToolTypes;
    private GameObject selectTool = null;
    private int money;
    private EventTrigger.Entry buttonDown;
    private EventTrigger.Entry buttonUp;

    private bool[] isTouch;

    public JoyStick JoyStick { get { return joyStick; } }

    public List<GameObject> HasTools { get { return hasTools; } }

    public int Money { get { return money; } set { money = value; } }

    public GameObject CurrentSelectTool { get { return selectTool; } }

    public GameObject CurrentHitPos { get { return showHitPos; } }

    void Awake()
    {
        isTouch = new bool[]{ false, false };
        hasTools = new List<GameObject>();
        hasToolTypes = new List<TOOL>();
        money = 0;

        buttonDown = new EventTrigger.Entry();
        buttonDown.eventID = EventTriggerType.PointerDown;
        buttonUp = new EventTrigger.Entry();
        buttonUp.eventID = EventTriggerType.PointerUp;

        ChangeTool();

        joyStick = joyStickObj.GetComponent<JoyStick>();

        showSelectTools.SetActive(false);
        showHitPos.SetActive(false);
        joyStickObj.SetActive(false);
    }

    private void OnEnable()
    {
        if (GameManager.instance.GameState == GAME_STATE.RUNNING)
        {
            toolIndex = TOOL.HAND;
            BringTool();
            transform.position = Vector3.zero;
            showHitPos.SetActive(true);
        }
    }

    private void OnDisable()
    {
        if (GameManager.instance.GameState == GAME_STATE.START) Init();
    }

    public void Init()
    {
        // 플레이어 정보 초기화
        transform.position = Vector3.zero;
        hasTools.Clear();
        hasToolTypes.Clear();
        money = 0;
        showSelectTools.SetActive(false);
        showHitPos.SetActive(false);
        showToolsButton.GetComponent<ShowToolsButton>().Init();
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
    
    public void BringTool()
    {
        // toolManager에게서 도구를 받아오며 shopManager에게 도구를 받아온 사실을 알려줌
        GetTool(toolIndex);
        GameManager.instance.shopManager.UnLockTool(toolIndex); // shopManager에게 도구가 있다고 알려줌
        SelectTool();
    }

    public GameObject GetTool(TOOL tool, GameObject toolPref=null)
    {
        GameObject toolObj = toolPref == null ? GameManager.instance.toolManager.GiveTool(tool) : toolPref;
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

    void ChangeTool()
    {
        hitButton.GetComponent<EventTrigger>().triggers.Clear();

        buttonDown.callback.AddListener((data) => selectTool.GetComponent<Tool>().HitButtonDown());
        buttonUp.callback.AddListener((data) => selectTool.GetComponent<Tool>().HitButtonUp());

        hitButton.GetComponent<EventTrigger>().triggers.Add(buttonDown);
        hitButton.GetComponent<EventTrigger>().triggers.Add(buttonUp);
    }
    
    void SelectTool(Vector3 prevPos)
    {
        if (selectTool != null)
        {
            selectTool.SetActive(false);
        }

        //int index = hasToolTypes.IndexOf(toolIndex);
        int index = hasToolTypes.Count - 1;
        selectTool = hasTools[index];
        selectTool.GetComponent<Tool>().ChangePos(prevPos);
        selectTool.GetComponent<Tool>().SetTool();
        selectTool.SetActive(true);

        // Hit 버튼의 도구 변경
        ChangeTool();

        // 선택한 도구의 Hit 좌표를 보여줌
        showHitPos.GetComponent<ShowHitPos>().SetCurTool(selectTool);
    }
    
    void SelectTool()
    {
        Vector3 prevPos = Vector3.zero;
        if (selectTool != null)
        {
            prevPos = selectTool.transform.position;
            selectTool.SetActive(false);
        }

        //int index = hasToolTypes.IndexOf(toolIndex);
        int index = hasToolTypes.Count - 1;
        selectTool = hasTools[index];
        selectTool.GetComponent<Tool>().ChangePos(prevPos);
        selectTool.GetComponent<Tool>().SetTool();
        selectTool.SetActive(true);

        // Hit 버튼의 도구 변경
        ChangeTool();

        // 선택한 도구의 Hit 좌표를 보여줌
        showHitPos.GetComponent<ShowHitPos>().SetCurTool(selectTool);
    }
    
    void SelectTool(TOOL tool)
    {
        if (selectTool.GetComponent<Tool>().ToolType == tool) return;

        Vector3 prevPos = Vector3.zero;
        if (selectTool != null)
        {
            prevPos = selectTool.transform.position;
            selectTool.SetActive(false);
        }

        int index = hasToolTypes.IndexOf(tool);
        selectTool = hasTools[index];
        selectTool.GetComponent<Tool>().ChangePos(prevPos);
        selectTool.GetComponent<Tool>().SetTool();
        selectTool.SetActive(true);

        // Hit 버튼의 도구 변경
        ChangeTool();

        // 선택한 도구의 Hit 좌표를 보여줌
        showHitPos.GetComponent<ShowHitPos>().SetCurTool(selectTool);
    }

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
    
    public void CantUse(GameObject toolObj)
    {
        // 도구를 더이상 사용할 수 없으므로 해당 도구를 가지고 있지 않은 상태로 만들고 상점에 이를 알려준다
        TOOL tool = toolObj.GetComponent<Tool>().ToolType;
        Vector3 pos = toolObj.transform.position;
        int index = hasToolTypes.IndexOf(tool);
        print("index: " + index);

        // 가지고 있는 정보에서 삭제
        hasToolTypes.Remove(tool);
        hasTools.RemoveAt(index);

        // 현재 가지고 있는 도구의 오브젝트 삭제
        Destroy(toolListObj.transform.GetChild(index).gameObject);
        Destroy(showSelectTools.transform.GetChild(index).gameObject);

        // 상점과 강화 매니저에게 도구를 사용할 수 없다는 것을 알림
        GameManager.instance.shopManager.LockTool(tool);
        GameManager.instance.reinforceManager.RemoveTool(toolObj);

        SelectTool(pos);
    }
}