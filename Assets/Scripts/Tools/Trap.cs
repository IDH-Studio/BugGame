using CESCO;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

/*
 * �������� �̵��ӵ��� �������� ��ġ��
 * ����� �� �ִ� ������ ������ ������ ��� ������ �� ����ϸ�
 * �ٽ� �������� �����ؾ� �Ѵ�
*/

public class Trap : Tool
{
    [SerializeField] private float stopDuration;
    [SerializeField] private int maxCount;
    [SerializeField] private Sprite hitObjImage;
    [SerializeField] private GameObject showUseCanvas;
    [SerializeField] private TextMeshProUGUI showUseText;
    [SerializeField] private TextMeshProUGUI showMaxUseText;
    [SerializeField] private float xOffset;
    [SerializeField] private float yOffset;

    private int count = 1;

    private new void Awake()
    {
        base.Awake();

        // ���� ��뷮 �˷��ִ� �ؽ�Ʈ �ʱ�ȭ
        showUseCanvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
        showUseCanvas.GetComponent<Canvas>().worldCamera = GameObject.Find("Main Camera").GetComponent<Camera>();

        float[] radiusValue = { 1.2f, 1.6f, 2f };
        SetRadiusValue(radiusValue);
    }

    public override void Move(Vector3 movePosition)
    {
        base.Move(movePosition);

        showUseText.transform.position = new Vector2(transform.position.x - xOffset, transform.position.y + yOffset);
        showMaxUseText.transform.position = new Vector2(transform.position.x + xOffset, transform.position.y + yOffset);
    }

    public override void Hit()
    {
        if (!canHit) return;

        // ���� ��� ǥ�� ������Ʈ ����
        GameObject showHitObj = GameManager.instance.prefabManager.GetHit(HIT_OBJ_TYPE.SHOW_HIT);
        // ���� ��Ҵ��� Ȯ���ϴ� ������Ʈ ����
        GameObject checkHitObj = GameManager.instance.prefabManager.GetHit(HIT_OBJ_TYPE.CHECK_HIT);

        // ������Ʈ ���� ����
        showHitObj.GetComponent<HitObjScript>().ChangeInfo(radius, stopDuration);
        checkHitObj.GetComponent<HitCheckScript>().ChangeInfo(radius, damage, tool, stopDuration, 0, false);
        
        showHitObj.GetComponent<HitObjScript>().ChangeImage(hitObjImage);        
        checkHitObj.GetComponent<HitCheckScript>().ChangeImage(hitObjImage);

        // ���� ��ġ�� ������Ʈ �̵�
        showHitObj.GetComponent<HitObjScript>().Show(transform.position);
        checkHitObj.GetComponent<HitCheckScript>().Show(transform.position);

        // ������ ��Ÿ��
        canHit = false;
        StartCoroutine(HitDelay());

        // ������ �ִϸ��̼� ���
        GameManager.instance.CurrentPlayer.CurrentHitPos.GetComponent<ShowHitPos>().PlayHitAnimation();

        // ������ �Ҹ� ���
        //GameManager.instance.soundManager.EffectPlay(tool);

        // �÷��̾�� ���̻� ����� �� ������ �˷���
        if (count >= maxCount)
        {
            StartCoroutine(CantUse());
            count = 1;
        }
        else
        {
            showUseText.text = (maxCount - count).ToString();
            ++count;
        }
    }

    IEnumerator CantUse()
    {
        yield return new WaitForSeconds(0.2f);
        GameManager.instance.CurrentPlayer.CantUse(gameObject);
    }
}
