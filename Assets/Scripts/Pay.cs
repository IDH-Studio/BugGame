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
        // 스코어 매니저에서 스코어를 불러와 100을 곱한 값이 모은 돈이 된다.
        // 플레이어의 현재 돈에 모은 돈을 추가한다.
        money = GameManager.instance.scoreManager.Score * 100;
        GameManager.instance.CurrentPlayer.ChangeMoney(money);
        PlayerMoney = GameManager.instance.CurrentPlayer.Money;

        // 모은 돈을 출력한다.
        // 플레이어가 가지고 있는 돈을 불러와 현재 잔액을 출력한다.
        showPayMoney.text = money + "원";
        showPlayerMoney.text = PlayerMoney + "원";
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
