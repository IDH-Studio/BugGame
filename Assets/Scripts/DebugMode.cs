using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CESCO;

public class DebugMode : MonoBehaviour
{
    private GameObject[] DebugObjects;
    public TMP_InputField inputField;
    public Shop shop;

    private void Start()
    {
        DebugObjects = GameObject.FindGameObjectsWithTag("Debug");
    }

    public void ToggleDebug(bool isOn)
    {
        if (isOn)
        {
            // 디버그 모드 ON (FPS 보이기)
            foreach (var obj in DebugObjects)
            {
                obj.SetActive(true);
            }
        }
        else
        {
            // 디버그 모드 OFF (FPS 안 보이기)
            foreach (var obj in DebugObjects)
            {
                obj.SetActive(false);
            }
        }
    }

    public void GiveMoney()
    {
        int money = int.Parse(inputField.text);
        GameManager.instance.CurrentPlayer.Money = money;
        shop.ShowMoney();
        inputField.text = "";
    }

    public void PlayerGetTool(int tool)
    {
        GameManager.instance.toolManager.GivePlayer((TOOL)tool);
        GameManager.instance.shopManager.UnLockTool((TOOL)tool);
    }

    public void GivePlayerMoney()
    {
        GameManager.instance.CurrentPlayer.Money = 100000;
    }
}
