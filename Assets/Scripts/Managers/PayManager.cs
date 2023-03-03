using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PayManager : MonoBehaviour
{
    public Pay pay;

    public void PayMoney()
    {
        pay.PayMoney(GameManager.instance.scoreManager.Score * 100);
    }

    public void ShowMoney()
    {
        pay.ShowPayInfo();
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
