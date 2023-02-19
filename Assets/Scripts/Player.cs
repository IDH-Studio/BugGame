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
        // �÷��̾� ���� �ʱ�ȭ
        transform.position = Vector3.zero;
        hasTools.Clear();
        hasToolTypes.Clear();
        money = 0;
        showSelectTools.SetActive(false);
        showHitPos.SetActive(false);
        showToolsButton.GetComponent<ShowToolsButton>().Init();
        selectTool = null;

        // ������ �ִ� ���� ����
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
        // toolManager���Լ� ������ �޾ƿ��� shopManager���� ������ �޾ƿ� ����� �˷���
        GetTool(toolIndex);
        GameManager.instance.shopManager.UnLockTool(toolIndex); // shopManager���� ������ �ִٰ� �˷���
        SelectTool();
    }

    public GameObject GetTool(TOOL tool, GameObject toolPref=null)
    {
        GameObject toolObj = toolPref == null ? GameManager.instance.toolManager.GiveTool(tool) : toolPref;
        // � �������� ������ ���޹��� �� �ش� ������ �ν��Ͻ�ȭ �ϴ� ����
        GameObject toolObjInstance = Instantiate(toolObj, toolListObj.transform);

        // �ش� ������ �÷��̾ ������ �ִٴ� ���� �˷���
        toolObjInstance.GetComponent<Tool>().HasPlayer();
        
        // ��ȭ�� �� � ������ ������ �ִ��� �˷���
        GameManager.instance.reinforceManager.AddTool(toolObjInstance);

        // �÷��̾ ������ �ִ� ������ �����Ѵ�.
        hasTools.Add(toolObjInstance);
        hasToolTypes.Add(tool);

        // �÷��̾ ������ �� �ֵ��� ��ư�� �߰��Ѵ�.
        GameObject toolButton = GameManager.instance.prefabManager.RequestInstantiate(OBJ_TYPE.PLAYER_HAS);
        toolButton.transform.SetParent(showSelectTools.transform);
        toolButton.transform.localScale = Vector3.one;

        // ��ư �̹��� ����
        toolButton.transform.GetChild(0).GetComponent<Image>().sprite =
            toolObjInstance.GetComponent<SpriteRenderer>().sprite;

        // ��ư Ŭ�� �� �ش� ������ ����
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

        // Hit ��ư�� ���� ����
        ChangeTool();

        // ������ ������ Hit ��ǥ�� ������
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

        // Hit ��ư�� ���� ����
        ChangeTool();

        // ������ ������ Hit ��ǥ�� ������
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

        // Hit ��ư�� ���� ����
        ChangeTool();

        // ������ ������ Hit ��ǥ�� ������
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
        // ������ ���̻� ����� �� �����Ƿ� �ش� ������ ������ ���� ���� ���·� ����� ������ �̸� �˷��ش�
        TOOL tool = toolObj.GetComponent<Tool>().ToolType;
        Vector3 pos = toolObj.transform.position;
        int index = hasToolTypes.IndexOf(tool);
        print("index: " + index);

        // ������ �ִ� �������� ����
        hasToolTypes.Remove(tool);
        hasTools.RemoveAt(index);

        // ���� ������ �ִ� ������ ������Ʈ ����
        Destroy(toolListObj.transform.GetChild(index).gameObject);
        Destroy(showSelectTools.transform.GetChild(index).gameObject);

        // ������ ��ȭ �Ŵ������� ������ ����� �� ���ٴ� ���� �˸�
        GameManager.instance.shopManager.LockTool(tool);
        GameManager.instance.reinforceManager.RemoveTool(toolObj);

        SelectTool(pos);
    }
}