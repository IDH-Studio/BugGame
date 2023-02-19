using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CESCO;

// ShopManager���� shop�� ���� �ݴ� ������ �ð�
// Shop�� �������� �������� ������ �� BuyItem���� �ϴ� ���� �� ��Ҹ� ������
// ���� ������ ������ �ô´�.

public class ShopManager : MonoBehaviour
{
    public Shop shop;

    public void Init()
    {
        // �ʱ�ȭ �ڵ� �ۼ�
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

        // ������ �����ϸ� toolManager�� ���� �÷��̾�� ������ �������ְ�
        // �ش� ������ �����ߴٴ� ������ shop���� �˷��� ��
        // �ٽ� ������ �ִ� ������ �����ֵ��� ��
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
