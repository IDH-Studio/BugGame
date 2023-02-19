using CESCO;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public struct Price {
    public Price(int ratePrice, int radiusPrice, int speedPrice)
    {
        RatePrice = ratePrice;
        RadiusPrice = radiusPrice;
        SpeedPrice = speedPrice;
    }

    public int RatePrice { get; set; }
    public int RadiusPrice { get; set; }
    public int SpeedPrice { get; set; }
}

public class ShowToolDetail : MonoBehaviour
{
    private Sprite sprite;
    private Tool selectToolScript;

    public Image toolImage;
    public TextMeshProUGUI toolName;
    public TextMeshProUGUI toolRateOfHit;
    public TextMeshProUGUI toolRadius;
    public TextMeshProUGUI toolSpeed;
    public Button RateButton;
    public Button RadiusButton;
    public Button SpeedButton;
    public TextMeshProUGUI ratePriceText;
    public TextMeshProUGUI radiusPriceText;
    public TextMeshProUGUI speedPriceText;

    private Price price;

    private void Awake()
    {
        price = new Price(0, 0, 0);
    }

    public void SelectTool(GameObject selectTool)
    {
        /*
         * �������� ���� ���� �� ��ȭ �� �ٷ� ���� ���� �ܰ�� ��ȭ�Ǵ� ���� �߻�
        */
        sprite = selectTool.GetComponent<SpriteRenderer>().sprite;
        selectToolScript = selectTool.GetComponent<Tool>();

        // transform.GetChild(0) : Selected Tool
        // transform.GetChild(1) : Tool Info
        // �̹����� �ٲٷ��� transform.GetChild(0).GetChild(0)���� �����ؾ� �Ѵ�.
        toolImage.sprite = sprite;

        // ���� ����
        toolName.text = selectToolScript.ToolName;
        ShowAttributes();
        ShowPrices();

        GameManager.instance.reinforceManager.SelectTool(ref selectTool, price);
    }

    public void ShowAttributes()
    {
        toolRateOfHit.text = selectToolScript.GetRateText();
        toolRadius.text = selectToolScript.GetRadiusText();
        toolSpeed.text = selectToolScript.GetSpeedText();
    }

    public Price ShowPrices()
    {
        price.RatePrice = GetPrice(selectToolScript.ToolRate);
        price.RadiusPrice = GetPrice(selectToolScript.ToolRadius);
        price.SpeedPrice = GetPrice(selectToolScript.ToolSpeed);

        ratePriceText.text = price.RatePrice == -1 ? "��ȭ �Ұ�" : price.RatePrice + "��";
        radiusPriceText.text = price.RadiusPrice == -1 ? "��ȭ �Ұ�" : price.RadiusPrice + "��";
        speedPriceText.text = price.SpeedPrice == -1 ? "��ȭ �Ұ�" : price.SpeedPrice + "��";

        return price;
    }

    private int GetPrice(TOOL_SPEED toolAttr)
    {
        switch (toolAttr)
        {
            case TOOL_SPEED.SUPER_SLOW:
                return 10000;
            case TOOL_SPEED.SLOW:
                return 15000;
            case TOOL_SPEED.NORMAL:
                return 20000;
            case TOOL_SPEED.FAST:
                return 25000;
            default:
                return -1;
        }
    }

    private int GetPrice(TOOL_RADIUS toolAttr)
    {
        switch (toolAttr)
        {
            case TOOL_RADIUS.SMALL:
                return 10000;
            case TOOL_RADIUS.MEDIUM:
                return 20000;
            default:
                return -1;
        }
    }
}
