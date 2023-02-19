using CESCO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * 전기 파리채 : Hit 버튼을 누른 시간동안 공격을 이어간다.
 * 게이지를 통해 남은 공격 시간을 보여준다.
 * 공격하지 않을 시 공격 시간이 채워진다.
*/

public class ElectricFlySwatter : Tool
{
    float attackTime = 0;
    float attackDurationTime = 0;
    [SerializeField] float maxAttackTime; // 최대로 누르고 있을 수 있는 시간
    [SerializeField] float attackDuration; // 누르는 동안의 공격 주기

    // TODO 전기 이펙트 만들기
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
            // 공격 중지
            StopAttack();
        }

        if (attackDurationTime >= attackDuration)
        {
            // 전기 공격
            ElectricAttack();
        }
    }

    public override void HitButtonDown()
    {
        isPressed = true;
        GameManager.instance.CurrentPlayer.CurrentHitPos.GetComponent<ShowHitPos>().StartFollowHitAnimation();
        print("전기 파리채 Hit Button Down");
    }

    public override void HitButtonUp()
    {
        StopAttack();

        print("전기 파리채 Hit Button Up");
    }

    void StopAttack()
    {
        // 때리기 쿨타임
        canHit = false;
        StartCoroutine(HitDelay());
        GameManager.instance.CurrentPlayer.CurrentHitPos.GetComponent<ShowHitPos>().StopHitAnimation();

        isPressed = false;
        attackTime = 0;
        InitGauge();
    }

    void ElectricAttack()
    {
        // 벌레 잡았는지 확인하는 오브젝트
        GameObject checkHitObj = GameManager.instance.prefabManager.GetHit(HIT_OBJ_TYPE.CHECK_HIT);
        checkHitObj.GetComponent<HitCheckScript>().ChangeInfo(radius, damage, tool, attackDuration);
        checkHitObj.GetComponent<HitCheckScript>().Show(transform.position);

        // 때리는 애니메이션 재생
        GameManager.instance.CurrentPlayer.CurrentHitPos.GetComponent<ShowHitPos>().PlayHitAnimation(tool, 0.1f);

        // 전기 공격
        // 전기 애니메이션이 보이며 범위 내에 있는 벌레들이 데미지를 입는다.
        attackDurationTime = 0;
    }

}
