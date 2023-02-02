using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using CESCO;

public class Pay : MonoBehaviour
{
    public TextMeshProUGUI showPayMoney;
    public TextMeshProUGUI showPlayerMoney;

    private int money;
    private int PlayerMoney;

    private void OnEnable()
    {
        // ���ھ� �Ŵ������� ���ھ �ҷ��� 100�� ���� ���� ���� ���� �ȴ�.
        // �÷��̾��� ���� ���� ���� ���� �߰��Ѵ�.
        money = GameManager.instance.scoreManager.Score * 100;
        GameManager.instance.CurrentPlayer.ChangeMoney(money);
        PlayerMoney = GameManager.instance.CurrentPlayer.Money;

        // ���� ���� ����Ѵ�.
        // �÷��̾ ������ �ִ� ���� �ҷ��� ���� �ܾ��� ����Ѵ�.
        showPayMoney.text = money + "��";
        showPlayerMoney.text = PlayerMoney + "��";
    }

    public void GoReinForce()
    {
        GameManager.instance.reinforceManager.Open();
    }
    
    public void GoShop()
    {
        GameManager.instance.shopManager.Open();
    }
}
