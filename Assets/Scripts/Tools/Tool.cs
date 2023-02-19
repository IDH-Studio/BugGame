using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CESCO;
using UnityEditor;
using UnityEditor.Tilemaps;

public class Tool : MonoBehaviour
{
    #region ���� ����
    // ���� �ӵ�
    // �ſ� ����, ����, ����, ����, �ſ� ����
    //     1,     0.7,  0.5,  0.3,     0.1
    [SerializeField] protected float hitDelay = .5f;
    public float Delay { get { return hitDelay; } }
    [SerializeField] private TOOL_SPEED toolRate;
    public TOOL_SPEED ToolRate
    {
        get { return toolRate; }
        set { toolRate = value; }
    }

    // ����
    // ����, ����, ŭ
    //  0.5,  1,   1.5
    [SerializeField] protected float radius = 1f;
    public float Radius { get { return radius; } }
    [SerializeField] private TOOL_RADIUS toolRadius;
    public TOOL_RADIUS ToolRadius
    {
        get { return toolRadius; }
        set { toolRadius = value; }
    }

    // �̵� �ӵ�
    // �ſ� ����, ����, ����, ����, �ſ� ����
    //     0.5,    0.7,   1,   1.3,    1.5
    [SerializeField] protected float speed = 10f;
    public float Speed { get { return speed; } }
    [SerializeField] private TOOL_SPEED toolSpeed;
    public TOOL_SPEED ToolSpeed
    {
        get { return toolSpeed; }
        set { toolSpeed = value; }
    }

    // �ſ� ����, ����, ����, ����, �ſ� ����
    private float[] rates = { 1f, 0.7f, 0.5f, 0.3f, 0.1f };
    // ����, ����, ŭ
    private float[] radiuses = { 0.8f, 1.2f, 1.6f };
    // �ſ� ����, ����, ����, ����, �ſ� ����
    private float[] speeds = { 3f, 5f, 8f, 10f, 12f };
    #endregion

    [SerializeField] protected TOOL tool;
    public TOOL ToolType
    {
        get { return tool; }
    }

    public bool hasPlayer = false;

    protected bool isPressed = false;
    protected bool canHit;
    public bool CanHit { get { return canHit; } }

    [SerializeField] private string toolName;
    public string ToolName
    {
        get { return toolName; }
    }

    [SerializeField] protected float damage;
    public float Damage { get { return damage; } }

    [SerializeField] protected Sprite toolImage;
    public Sprite ToolImage { get { return toolImage; } }

    #region �Ӽ� getter, setter
    public string GetRateText()
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

    public string GetRadiusText()
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
    
    public string GetSpeedText()
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
        hitDelay = rates[(int)toolRate];
    }

    private void SetRate(TOOL_SPEED attr)
    {
        hitDelay = rates[(int)attr];
    }

    public void SetRadius()
    {
        radius = radiuses[(int)toolRadius];
        transform.localScale = new Vector3(radius, radius, radius);
    }

    private void SetRadius(TOOL_RADIUS attr)
    {
        radius = radiuses[(int)attr];
        transform.localScale = new Vector3(radius, radius, radius);
    }

    public void SetSpeed()
    {
        speed = speeds[(int)toolSpeed];
    }

    private void SetSpeed(TOOL_SPEED attr)
    {
        speed = speeds[(int)attr];
    }
    #endregion

    protected void SetRateValue(float[] values)
    {
        if (values.Length != rates.Length) { print("�Ӽ� ���� " + rates.Length + "���� �Ǿ�� �մϴ�."); return; }

        rates = values;
    }

    protected void SetRadiusValue(float[] values)
    {
        if (values.Length != radiuses.Length) { print("�Ӽ� ���� " + radiuses.Length + "���� �Ǿ�� �մϴ�."); return; }

        radiuses = values;
    }

    protected void SetSpeedValue(float[] values)
    {
        if (values.Length != speeds.Length) { print("�Ӽ� ���� " + speeds.Length + "���� �Ǿ�� �մϴ�."); return; }

        speeds = values;
    }

    private void Init()
    {
        transform.localScale = new Vector3(radius, radius, radius);
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

    protected void FixedUpdate()
    {
        // ������ (��� ������ ����� �κ�)
        if (GameManager.instance.screenManager.CurrentScreen() != SCREEN.INGAME) return;

        if (GameManager.instance.CurrentPlayer.JoyStick.IsTouch)
        {
            Move(GameManager.instance.CurrentPlayer.JoyStick.MovePosition);
        }

        // PC �׽�Ʈ ����
        Vector2 movePos = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Move(movePos.normalized);
    }

    private void Update()
    {
        // �������� ����� �޶�����.
        if (Input.GetKeyDown(KeyCode.Space)) Hit();
    }

    public virtual void HitButtonDown()
    {
        // ��� ������ �������� ����
        Hit();
        print("Hit Button Down");
    }
    
    public virtual void HitButtonUp()
    {
        // ��� ������ �������� ����
    }

    public void SetTool()
    {
        GameManager.instance.CurrentPlayer.CurrentHitPos.GetComponent<SpriteRenderer>().sprite = toolImage;
    }

    protected void OnEnable()
    {
        Init();
    }

    public virtual void Move(Vector3 movePosition)
    {
        //// ���콺 ��ǥ�� ���� ��ǥ�� ��ȯ
        //Vector3 mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //// ���콺 ��ǥ�� ���� ������Ʈ �̵�
        //transform.position = new Vector3(mPos.x * speed, mPos.y * speed, 5);

        // ��ũ�� ����� ���ϰ� ����
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

    public virtual void Hit()
    {
        if (!canHit) return;

        // ���� ��� ǥ�� ������Ʈ ����
        GameObject showHitObj = GameManager.instance.prefabManager.GetHit(HIT_OBJ_TYPE.SHOW_HIT);
        // ���� ��Ҵ��� Ȯ���ϴ� ������Ʈ ����
        GameObject checkHitObj = GameManager.instance.prefabManager.GetHit(HIT_OBJ_TYPE.CHECK_HIT);

        // ������Ʈ ũ�� ����
        showHitObj.GetComponent<HitObjScript>().ChangeInfo(radius, hitDelay);
        checkHitObj.GetComponent<HitCheckScript>().ChangeInfo(radius, damage, tool);

        showHitObj.GetComponent<HitObjScript>().ChangeImage();

        // ���� ��ġ�� ������Ʈ �̵�
        showHitObj.GetComponent<HitObjScript>().Show(transform.position);
        checkHitObj.GetComponent<HitCheckScript>().Show(transform.position);

        // ������ ��Ÿ��
        canHit = false;
        StartCoroutine(HitDelay());

        // ������ �ִϸ��̼� ���
        GameManager.instance.CurrentPlayer.CurrentHitPos.GetComponent<ShowHitPos>().PlayHitAnimation();

        // ������ �Ҹ� ���
        GameManager.instance.soundManager.EffectPlay(tool);
    }
    
    protected IEnumerator HitDelay()
    {
        yield return new WaitForSeconds(hitDelay);
        canHit = !canHit;
    }

    public void ChangePos(Vector3 pos)
    {
        transform.position = pos;
    }
}