using CESCO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * ���� �ĸ�ä : Hit ��ư�� ���� �ð����� ������ �̾��.
 * �������� ���� ���� ���� �ð��� �����ش�.
 * �������� ���� �� ���� �ð��� ä������.
*/

public class ElectricFlySwatter : Tool
{
    float attackTime = 0;
    float attackDurationTime = 0;
    [SerializeField] float maxAttackTime; // �ִ�� ������ ���� �� �ִ� �ð�
    [SerializeField] float attackDuration; // ������ ������ ���� �ֱ�

    // TODO ���� ����Ʈ �����
    [SerializeField] GameObject gaugeCanvas;
    [SerializeField] float yOffset;
    GameObject gaugeBGObj;
    GameObject gaugeObj;

    private new void Awake()
    {
        base.Awake();

        gaugeCanvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
        gaugeCanvas.GetComponent<Canvas>().worldCamera = GameObject.Find("Main Camera").GetComponent<Camera>();

        gaugeBGObj = GameManager.instance.prefabManager.RequestInstantiate(OBJ_TYPE.GAUGE_BG_IMAGE, gaugeCanvas.transform);
        gaugeObj = GameManager.instance.prefabManager.RequestInstantiate(OBJ_TYPE.GAUGE_IMAGE, gaugeCanvas.transform);
    }

    private new void OnEnable()
    {
        base.OnEnable();
        InitGauge();
    }

    private new void FixedUpdate()
    {
        base.FixedUpdate();

        gaugeBGObj.transform.position = gaugeObj.transform.position =
            (new Vector2(transform.position.x, transform.position.y + radius + yOffset));
    }

    private void Update()
    {
        if (!isPressed) { return; }

        attackDurationTime = attackTime += Time.deltaTime;
        gaugeObj.GetComponent<Image>().fillAmount = attackDurationTime / maxAttackTime;
        CheckAttack();
    }

    void InitGauge()
    {
        gaugeObj.GetComponent<Image>().fillAmount = 0;
    }

    void CheckAttack()
    {
        if (attackTime >= maxAttackTime)
        {
            print("Stop Attack");
            // ���� ����
            StopAttack();
        }

        if (attackDurationTime >= attackDuration)
        {
            // ���� ����
            ElectricAttack();
        }
    }

    public override void HitButtonDown()
    {
        isPressed = true;
        GameManager.instance.CurrentPlayer.CurrentHitPos.GetComponent<ShowHitPos>().StartFollowHitAnimation();
        print("���� �ĸ�ä Hit Button Down");
    }

    public override void HitButtonUp()
    {
        StopAttack();

        print("���� �ĸ�ä Hit Button Up");
    }

    void StopAttack()
    {
        // ������ ��Ÿ��
        canHit = false;
        StartCoroutine(HitDelay());
        GameManager.instance.CurrentPlayer.CurrentHitPos.GetComponent<ShowHitPos>().StopHitAnimation();

        isPressed = false;
        attackTime = 0;
        InitGauge();
    }

    void ElectricAttack()
    {
        // ���� ��Ҵ��� Ȯ���ϴ� ������Ʈ
        GameObject checkHitObj = GameManager.instance.prefabManager.GetHit(HIT_OBJ_TYPE.CHECK_HIT);
        checkHitObj.GetComponent<HitCheckScript>().ChangeInfo(radius, damage, tool, attackDuration);
        checkHitObj.GetComponent<HitCheckScript>().Show(transform.position);

        // ������ �ִϸ��̼� ���
        GameManager.instance.CurrentPlayer.CurrentHitPos.GetComponent<ShowHitPos>().PlayHitAnimation(tool, 0.1f);

        // ���� ����
        // ���� �ִϸ��̼��� ���̸� ���� ���� �ִ� �������� �������� �Դ´�.
        attackDurationTime = 0;
    }

}
