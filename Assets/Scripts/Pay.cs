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
        // 모은 돈을 출력한다.
        // 플레이어가 가지고 있는 돈을 불러와 현재 잔액을 출력한다.
        showPayMoney.text = money + "원";
        showPlayerMoney.text = GameManager.instance.CurrentPlayer.Money + "원";
    }
}
