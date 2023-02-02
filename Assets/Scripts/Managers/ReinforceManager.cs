using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CESCO;

public class ReinforceManager : MonoBehaviour
{
    public Reinforce reinforce;

    private GameObject currentSelectTool;
    private Price price;

    public void Init()
    {
        // �ʱ�ȭ �ڵ� �ۼ�
        /*
         * �ʱ�ȭ�� �ϰ� �ȴٸ� Can Select Tool�� �ִ� ������Ʈ�� ���� ���ش�. 
        */
        reinforce.Init();
    }

    public void Open()
    {
        GameManager.instance.screenManager.ChangeScreen(SCREEN.REINFORCE);
        reinforce.ShowWindow();
    }

    public void Close()
    {
        GameManager.instance.screenManager.PrevScreen();
    }

    public void ReinforceToolRate()
    {
        // ���� �ӵ� ��ȭ
        if (price.RatePrice == -1 || GameManager.instance.CurrentPlayer.Money < price.RatePrice) return;

        GameManager.instance.CurrentPlayer.Money -= price.RatePrice;
        GameManager.instance.toolManager.ReinforceRate(currentSelectTool);
        reinforce.ShowMoney();
        price = reinforce.ShowToolInfo();
    }

    public void ReinforceToolRadius()
    {
        // ���� ��ȭ
        if (price.RadiusPrice == -1 || GameManager.instance.CurrentPlayer.Money < price.RadiusPrice) return;

        GameManager.instance.CurrentPlayer.Money -= price.RadiusPrice;
        GameManager.instance.toolManager.ReinforceRadius(currentSelectTool);
        reinforce.ShowMoney();
        price = reinforce.ShowToolInfo();
    }
    
    public void ReinforceToolSpeed()
    {
        // �̵� �ӵ� ��ȭ
        if (price.SpeedPrice == -1 || GameManager.instance.CurrentPlayer.Money < price.SpeedPrice) return;

        GameManager.instance.CurrentPlayer.Money -= price.SpeedPrice;
        GameManager.instance.toolManager.ReinforceSpeed(currentSelectTool);
        reinforce.ShowMoney();
        price = reinforce.ShowToolInfo();
    }    

    public void AddTool(GameObject tool)
    {
        reinforce.AddTool(tool);
    }

    public void SelectTool(ref GameObject selectTool, Price price)
    {
        currentSelectTool = selectTool;
        this.price = price;
    }
}
