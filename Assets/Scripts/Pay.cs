using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using CESCO;

public class Pay : MonoBehaviour
{
    public TextMeshProUGUI showPayMoney;
    public TextMeshProUGUI showPlayerMoney;

    int money;

    public void PayMoney(int money)
    {
        this.money = money;
        GameManager.instance.CurrentPlayer.ChangeMoney(money);
        ShowPayInfo();
    }

    public void ShowPayInfo()
    {
        // ���� ���� ����Ѵ�.
        // �÷��̾ ������ �ִ� ���� �ҷ��� ���� �ܾ��� ����Ѵ�.
        showPayMoney.text = money + "��";
        showPlayerMoney.text = GameManager.instance.CurrentPlayer.Money + "��";
    }
}
