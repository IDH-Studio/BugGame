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
        // ���̽�ƽ �ʱ�ȭ
        //joyStick.Init();

        // �÷��̾� ���� �ʱ�ȭ
        transform.position = Vector3.zero;
        hasTools.Clear();
        hasToolTypes.Clear();
        money = 0;
        showSelectTools.SetActive(false);
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

    private void OnDisable()
    {
        Init();
    }

    private void Update()
    {
        /*
         * ��ȭ ���� �ذ� ��
         * ����� ������ ���̽�ƽ �� ������ ��ư ���� ��
         * ��ư�� ���� ���� ���� ��� ���� �� �׽�Ʈ
        */
        if (GameManager.instance.screenManager.CurrentScreen() != SCREEN.INGAME)
        {
            return;
        }

        if (joyStick.IsTouch)
        {
            selectTool.GetComponent<Tool>().Move(joyStick.MovePosition);
        }

        // PC �׽�Ʈ ����

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
        // toolManager���Լ� ������ �޾ƿ��� shopManager���� ������ �޾ƿ� ����� �˷���
        GetTool(toolIndex);
        GameManager.instance.shopManager.UnLockTool(toolIndex); // shopManager���� ������ �ִٰ� �˷���
        SelectTool();
    }

    public GameObject GetTool(TOOL tool)
    {
        // � �������� ������ ���޹��� �� �ش� ������ �ν��Ͻ�ȭ �ϴ� ����
        GameObject toolObj = GameManager.instance.toolManager.GiveTool(tool);
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

    public GameObject GetTool(TOOL tool, GameObject toolObj)
    {
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