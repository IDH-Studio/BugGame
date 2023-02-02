using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CESCO;


// ���� ���� â
public class BuyItem : MonoBehaviour
{
    public Image buyItemImage;
    public TextMeshProUGUI ItemName;
    public TextMeshProUGUI ItemRate;
    public TextMeshProUGUI ItemRadius;
    public TextMeshProUGUI ItemSpeed;
    public TextMeshProUGUI ItemPrice;

    private TOOL itemToolType;
    private int itemPrice;

    public void Init(GameObject buyItemObj)
    {
        itemToolType = buyItemObj.GetComponent<ShopItem>().ToolType;
        itemPrice = buyItemObj.GetComponent<ShopItem>().ItemPrice;

        // buyItem�� ù��° �ڽ��� ItemImage�̴�.
        // buyItem.transform.GetChild(0).gameObject.GetComponent<Image>()
        // �����Ϸ��� �������� �̹��� ����
        buyItemImage.sprite = buyItemObj.transform.GetChild(0).gameObject.GetComponent<Image>().sprite;

        Tool toolInfo = GameManager.instance.toolManager.GetToolInfo(itemToolType);

        // buyItem.GetComponent<Tool>()
        // �����Ϸ��� �������� ����
        ItemName.text = toolInfo.ToolName;
        ItemRate.text = toolInfo.GetRate();
        ItemRadius.text = toolInfo.GetRadius();
        ItemSpeed.text = toolInfo.GetSpeed();
        ItemPrice.text = itemPrice + "��";
    }

    public void Buy()
    {
        // ���� ��ư�� �����
        GameManager.instance.shopManager.BuyItem(itemToolType, itemPrice);
    }

    public void Cancel()
    {
        // ��� ��ư�� �����
        gameObject.SetActive(false);
    }
}
