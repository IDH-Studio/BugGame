using CESCO;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/*
 * ������: �Ѹ� ���� ���� �ð����� ���⸦ �ӹ��� �ϰ�
 * �װ��� �������� ������ �������� �԰Եȴ�.
 * HitObj: ���� �� �ߴ� ���� �̹���
 * HitCheckObj: ������ �����ϱ� ���� ������Ʈ
 * GameManager.instance.CurrentPlayer.CurrentHitPos.GetComponent<ShowHitPos>()
 *  �� �÷��̾� ���� �ߴ� ���� �̹���
*/

public class Insecticide : Tool
{
    [SerializeField] private Sprite HitObjImage; // ���� �̹���
    [SerializeField] private Sprite ShowHitObjImage; // ���Ⱑ ��ġ�Ǵ� ���� �˷��ִ� �̹���
    [SerializeField] private float attackDuration;
    [SerializeField] private float attackDelay;

    private new void Awake()
    {
        base.Awake();

        float[] radiusValue = { 1.4f, 1.8f, 2.2f };
        SetRadiusValue(radiusValue);
    }

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = ShowHitObjImage;
    }

    public override void Hit()
    {
        if (!canHit) return;

        // ���� ��� ǥ�� ������Ʈ ����
        GameObject showHitObj = GameManager.instance.prefabManager.GetHit(HIT_OBJ_TYPE.SHOW_HIT);
        // ���� ��Ҵ��� Ȯ���ϴ� ������Ʈ ����
        GameObject checkHitObj = GameManager.instance.prefabManager.GetHit(HIT_OBJ_TYPE.CHECK_HIT);

        // ������Ʈ ���� ����
        showHitObj.GetComponent<HitObjScript>().ChangeInfo(radius, attackDuration);
        checkHitObj.GetComponent<HitCheckScript>().ChangeInfo(radius, damage, tool, attackDuration, attackDelay, false);

        showHitObj.GetComponent<HitObjScript>().ChangeImage(HitObjImage);
        checkHitObj.GetComponent<HitCheckScript>().ChangeImage(HitObjImage);

        // ���� ��ġ�� ������Ʈ �̵�
        showHitObj.GetComponent<HitObjScript>().Show(transform.position);
        checkHitObj.GetComponent<HitCheckScript>().Show(transform.position);


        // ������ ��Ÿ��
        canHit = false;
        StartCoroutine(HitDelay());

        // ������ �ִϸ��̼� ���
        GameManager.instance.CurrentPlayer.CurrentHitPos.GetComponent<ShowHitPos>().PlayHitAnimation(tool, hitDelay);

        // ������ �Ҹ� ���
        //GameManager.instance.soundManager.EffectPlay(tool);
    }
}