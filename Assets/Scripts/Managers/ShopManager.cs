using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CESCO;

// ShopManager에선 shop을 열고 닫는 역할을 맡고
// Shop에 실질적인 아이템을 저장한 뒤 BuyItem에서 하는 구매 및 취소를 맡으며
// 상점 나가는 역할을 맡는다.

public class ShopManager : MonoBehaviour
{
    public Shop shop;

    public void Init()
    {
        // 초기화 코드 작성
        shop.Init();
    }

    public void Open()
    {
        GameManager.instance.screenManager.ChangeScreen(SCREEN.SHOP);
        shop.ShowShopInfo();
    }

    public void Close()
    {
        GameManager.instance.screenManager.PrevScreen();
    }

    public bool BuyItem(TOOL buyTool, int price)
    {
        if (GameManager.instance.CurrentPlayer.Money < price)
        {
            return false;
        }

        // 도구를 구매하면 toolManager를 통해 플레이어에게 도구를 전달해주고
        // 해당 도구를 구매했다는 정보를 shop에게 알려준 뒤
        // 다시 상점에 있는 도구를 보여주도록 함
        GameManager.instance.CurrentPlayer.Money -= price;
        GameManager.instance.toolManager.GivePlayer(buyTool);

        shop.UnLock(buyTool);
        shop.ShowItems();

        return true;
    }

    public void UnLockTool(TOOL tool)
    {
        shop.UnLock(tool);
    }

    public void LockTool(TOOL tool)
    {
        shop.Lock(tool);
    }
}
