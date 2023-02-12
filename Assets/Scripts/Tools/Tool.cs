using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CESCO;
using UnityEditor;

public class Tool : MonoBehaviour
{
    // ���� �ӵ�
    // �ſ� ����, ����, ����, ����, �ſ� ����
    //     1,     0.7,  0.5,  0.3,     0.1
    [SerializeField] float hitDelay = .5f;
    [SerializeField] private TOOL_SPEED toolRate;
    public TOOL_SPEED ToolRate
    {
        get { return toolRate; }
        set { toolRate = value; }
    }

    // ����
    // ����, ����, ŭ
    //  0.5,  1,   1.5
    [SerializeField] float radius = 1f;
    [SerializeField] private TOOL_RADIUS toolRadius;
    public TOOL_RADIUS ToolRadius
    {
        get { return toolRadius; }
        set { toolRadius = value; }
    }

    // �̵� �ӵ�
    // �ſ� ����, ����, ����, ����, �ſ� ����
    //     0.5,    0.7,   1,   1.3,    1.5
    [SerializeField] float speed = 10f;
    public float Speed { get { return speed; } }
    [SerializeField] private TOOL_SPEED toolSpeed;
    public TOOL_SPEED ToolSpeed
    {
        get { return toolSpeed; }
        set { toolSpeed = value; }
    }

    [SerializeField] GameObject HitObj;
    [SerializeField] GameObject HitCheckObj;
    [SerializeField] TOOL tool;
    public TOOL ToolType
    {
        get { return tool; }
    }

    public bool hasPlayer = false;

    private bool canHit = true;
    [SerializeField] private string toolName;

    public string ToolName
    {
        get { return toolName; }
    }

    // �ſ� ����, ����, ����, ����, �ſ� ����
    private float[] rates = { 1f, 0.7f, 0.5f, 0.3f, 0.1f };
    // ����, ����, ŭ
    private float[] radiuses = { 0.8f, 1.2f, 1.6f };
    // �ſ� ����, ����, ����, ����, �ſ� ����
    private float[] speeds = { 3f, 5f, 8f, 10f, 12f };

    // �Ӽ� getter, setter
    public string GetRate()
    {
        switch (toolRate)
        {
            case TOOL_SPEED.SUPER_SLOW:
                return "�ſ� ����";
            case TOOL_SPEED.SLOW:
                return "����";
            case TOOL_SPEED.NORMAL:
                return "����";
            case TOOL_SPEED.FAST:
                return "����";
            case TOOL_SPEED.SUPER_FAST:
                return "�ſ� ����";
            default:
                return "";
        }
    }

    public string GetRadius()
    {
        switch (toolRadius)
        {
            case TOOL_RADIUS.SMALL:
                return "����";
            case TOOL_RADIUS.MEDIUM:
                return "����";
            case TOOL_RADIUS.LARGE:
                return "ŭ";
            default:
                return "";
        }
    }
    
    public string GetSpeed()
    {
        switch (toolSpeed)
        {
            case TOOL_SPEED.SUPER_SLOW:
                return "�ſ� ����";
            case TOOL_SPEED.SLOW:
                return "����";
            case TOOL_SPEED.NORMAL:
                return "����";
            case TOOL_SPEED.FAST:
                return "����";
            case TOOL_SPEED.SUPER_FAST:
                return "�ſ� ����";
            default:
                return "";
        }
    }

    public void SetRate()
    {
        //switch (toolRate)
        //{
        //    case TOOL_SPEED.SUPER_SLOW:
        //        hitDelay = rates[0];
        //        break;
        //    case TOOL_SPEED.SLOW:
        //        hitDelay = rates[1];
        //        break;
        //    case TOOL_SPEED.NORMAL:
        //        hitDelay = rates[2];
        //        break;
        //    case TOOL_SPEED.FAST:
        //        hitDelay = rates[3];
        //        break;
        //    case TOOL_SPEED.SUPER_FAST:
        //        hitDelay = rates[4];
        //        break;
        //}
        hitDelay = rates[(int)toolRate];
    }

    private void SetRate(TOOL_SPEED attr)
    {
        //switch (attr)
        //{
        //    case TOOL_SPEED.SUPER_SLOW:
        //        hitDelay = rates[0];
        //        break;
        //    case TOOL_SPEED.SLOW:
        //        hitDelay = rates[1];
        //        break;
        //    case TOOL_SPEED.NORMAL:
        //        hitDelay = rates[2];
        //        break;
        //    case TOOL_SPEED.FAST:
        //        hitDelay = rates[3];
        //        break;
        //    case TOOL_SPEED.SUPER_FAST:
        //        hitDelay = rates[4];
        //        break;
        //}
        hitDelay = rates[(int)attr];
    }

    public void SetRadius()
    {
        //switch (toolRadius)
        //{
        //    case TOOL_RADIUS.SMALL:
        //        radius = radiuses[0];
        //        break;
        //    case TOOL_RADIUS.MEDIUM:
        //        radius = radiuses[1];
        //        break;
        //    case TOOL_RADIUS.LARGE:
        //        radius = radiuses[2];
        //        break;
        //}
        radius = radiuses[(int)toolRadius];
        transform.localScale = new Vector3(radius, radius, radius);
    }

    private void SetRadius(TOOL_RADIUS attr)
    {
        //switch (attr)
        //{
        //    case TOOL_RADIUS.SMALL:
        //        radius = radiuses[0];
        //        break;
        //    case TOOL_RADIUS.MEDIUM:
        //        radius = radiuses[1];
        //        break;
        //    case TOOL_RADIUS.LARGE:
        //        radius = radiuses[2];
        //        break;
        //}
        radius = radiuses[(int)attr];
        transform.localScale = new Vector3(radius, radius, radius);
    }

    public void SetSpeed()
    {
        //switch (toolSpeed)
        //{
        //    case TOOL_SPEED.SUPER_SLOW:
        //        speed = speeds[0];
        //        break;
        //    case TOOL_SPEED.SLOW:
        //        speed = speeds[1];
        //        break;
        //    case TOOL_SPEED.NORMAL:
        //        speed = speeds[2];
        //        break;
        //    case TOOL_SPEED.FAST:
        //        speed = speeds[3];
        //        break;
        //    case TOOL_SPEED.SUPER_FAST:
        //        speed = speeds[4];
        //        break;
        //}
        speed = speeds[(int)toolSpeed];
    }

    private void SetSpeed(TOOL_SPEED attr)
    {
        //switch (attr)
        //{
        //    case TOOL_SPEED.SUPER_SLOW:
        //        speed = speeds[0];
        //        break;
        //    case TOOL_SPEED.SLOW:
        //        speed = speeds[1];
        //        break;
        //    case TOOL_SPEED.NORMAL:
        //        speed = speeds[2];
        //        break;
        //    case TOOL_SPEED.FAST:
        //        speed = speeds[3];
        //        break;
        //    case TOOL_SPEED.SUPER_FAST:
        //        speed = speeds[4];
        //        break;
        //}
        speed = speeds[(int)attr];
    }

    private void Init()
    {
        transform.localScale = new Vector3(radius, radius, radius);
        HitObj.transform.localScale = new Vector3(radius, radius, radius);
        HitCheckObj.transform.localScale = new Vector3(radius, radius, radius);
        canHit = true;
    }

    protected void Awake()
    {
        SetRate();
        SetRadius();
        SetSpeed();
        Init();
    }

    protected void Awake(TOOL_SPEED hitDelay, string toolName)
    {
        this.toolName = toolName;
        SetRate(hitDelay);
        SetRadius();
        SetSpeed();

        Init();
    }

    protected void Awake(TOOL_SPEED hitDelay, TOOL_RADIUS radius, string toolName)
    {
        this.toolName = toolName;
        SetRate(hitDelay);
        SetRadius(radius);
        SetSpeed();

        Init();
    }

    protected void Awake(TOOL_SPEED hitDelay, TOOL_RADIUS radius, TOOL_SPEED speed, string toolName)
    {
        this.toolName = toolName;
        SetRate(hitDelay);
        SetRadius(radius);
        SetSpeed(speed);

        Init();
    }

    private void OnEnable()
    {
        Init();
        //setHitObj(radius);
    }

    public void Move(Vector3 movePosition)
    {
        //// ���콺 ��ǥ�� ���� ��ǥ�� ��ȯ
        //Vector3 mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //// ���콺 ��ǥ�� ���� ������Ʈ �̵�
        //transform.position = new Vector3(mPos.x * speed, mPos.y * speed, 5);

        //// ���콺 ���� Ŭ�� �� (����)
        if (transform.position.x < -(GameManager.instance.CameraSize * 2))
        {
            transform.position = new Vector2(-(GameManager.instance.CameraSize * 2), transform.position.y);
        }
        else if (transform.position.x > (GameManager.instance.CameraSize * 2))
        {
            transform.position = new Vector2((GameManager.instance.CameraSize * 2), transform.position.y);
        }

        if (transform.position.y < -GameManager.instance.CameraSize)
        {
            transform.position = new Vector2(transform.position.x, -GameManager.instance.CameraSize);
        }
        else if (transform.position.y > GameManager.instance.CameraSize)
        {
            transform.position = new Vector2(transform.position.x, GameManager.instance.CameraSize);
        }

        transform.position += new Vector3(movePosition.x * speed * Time.deltaTime,
            movePosition.y * speed * Time.deltaTime);
    }

    public void HasPlayer()
    {
        hasPlayer = true;
    }

    public void Hit()
    {
        if (!canHit) return;
        //Instantiate(HitObj, transform.position, transform.rotation).GetComponent<HitObjScript>().changeRadius(radius);
        //Instantiate(HitCheckObj, transform.position, transform.rotation).GetComponent<HitCheckScript>().changeRadius(radius);

        // ���� ��� ǥ�� ������Ʈ ����
        GameObject showHitObj = GameManager.instance.prefabManager.GetHit(HIT_OBJ_TYPE.SHOW_HIT);
        // ���� ��Ҵ��� Ȯ���ϴ� ������Ʈ ����
        GameObject checkHitObj = GameManager.instance.prefabManager.GetHit(HIT_OBJ_TYPE.CHECK_HIT);

        // ������Ʈ ũ�� ����
        showHitObj.GetComponent<HitObjScript>().changeRadius(radius);
        checkHitObj.GetComponent<HitCheckScript>().changeRadius(radius);

        // ���� ��ġ�� ������Ʈ �̵�
        showHitObj.transform.position = transform.position;
        checkHitObj.transform.position = transform.position;

        checkHitObj.GetComponent<HitCheckScript>().changeRadius(radius);

        // ������ ��Ÿ��
        canHit = false;
        StartCoroutine(HitDelay());

        // ������ �Ҹ� ���
        GameManager.instance.soundManager.EffectPlay(tool);
    }

    protected IEnumerator HitDelay()
    {
        yield return new WaitForSeconds(hitDelay);
        canHit = !canHit;
    }
}