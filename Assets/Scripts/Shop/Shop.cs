using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using CESCO;
using UnityEngine.UI;
using UnityEngine.Events;

// ���� â
public class Shop : MonoBehaviour
{
    public TextMeshProUGUI showPlayerMoney;
    public GameObject ToolObjs; // ���� ���õǴ�(���� �ϴ�) �������� ��Ƴ��� ������Ʈ
    public GameObject BuyItemObj;
    public GameObject Lock;
    public GameObject[] Items; // ���̽��� �Ǵ� ������

    private List<GameObject> shopItems;

    private void Awake()
    {
        print("Shop ȣ��");
        shopItems = new List<GameObject>();
        GetItems();
    }

    public void Init()
    {
        shopItems.Clear();
        foreach (Transform child in ToolObjs.transform)
        {
            Destroy(child.gameObject);
        }
        GetItems();
    }

    public void GetItems()
    {
        foreach (GameObject item in Items)
        {
            GameObject shopItem = Instantiate(item, ToolObjs.transform);

            Instantiate(Lock, shopItem.transform);

            shopItem.GetComponent<Button>().onClick.AddListener(() =>
            {
                BuyItemObj.GetComponent<BuyItem>().Init(shopItem);
                BuyItemObj.SetActive(true);
            });

            shopItems.Insert((int)item.GetComponent<ShopItem>().ToolType, shopItem);
        }
    }

    public void UnLock(TOOL tool)
    {
        shopItems[(int)tool].GetComponent<ShopItem>().UnLock();
        Destroy(shopItems[(int)tool].transform.GetChild(1).gameObject); // Lock �̹��� ����
        shopItems[(int)tool].GetComponent<Button>().onClick.RemoveAllListeners(); // ��ư �̺�Ʈ ����
    }

    public void ShowShopInfo()
    {
        ShowItems();
    }

    public void ShowMoney()
    {
        showPlayerMoney.text = GameManager.instance.CurrentPlayer.Money + "��";
    }

    public void ShowItems()
    {
        ShowMoney();
        BuyItemObj.SetActive(false);
    }
}
