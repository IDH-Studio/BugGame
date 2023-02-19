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
         * 상점에서 도구 구매 후 강화 시 바로 제일 높은 단계로 강화되는 버그 발생
        */
        sprite = selectTool.GetComponent<SpriteRenderer>().sprite;
        selectToolScript = selectTool.GetComponent<Tool>();

        // transform.GetChild(0) : Selected Tool
        // transform.GetChild(1) : Tool Info
        // 이미지를 바꾸려면 transform.GetChild(0).GetChild(0)으로 접근해야 한다.
        toolImage.sprite = sprite;

        // 도구 정보
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

        ratePriceText.text = price.RatePrice == -1 ? "강화 불가" : price.RatePrice + "원";
        radiusPriceText.text = price.RadiusPrice == -1 ? "강화 불가" : price.RadiusPrice + "원";
        speedPriceText.text = price.SpeedPrice == -1 ? "강화 불가" : price.SpeedPrice + "원";

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
