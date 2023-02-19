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
    public GameObject LockObj;
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

            Instantiate(LockObj, shopItem.transform);

            shopItem.GetComponent<Button>().onClick.AddListener(() =>
            {
                BuyItemObj.GetComponent<BuyItem>().Init(shopItem);
                BuyItemObj.SetActive(true);
            });

            //shopItems.Insert((int)item.GetComponent<ShopItem>().ToolType, shopItem);
            shopItems.Add(shopItem);
        }
    }

    public void UnLock(TOOL tool)
    {
        shopItems[(int)tool].GetComponent<ShopItem>().UnLock();
        shopItems[(int)tool].transform.GetChild(1).gameObject.SetActive(false); // Lock �̹��� ����
        shopItems[(int)tool].GetComponent<Button>().onClick.RemoveAllListeners(); // ��ư �̺�Ʈ ����
    }

    public void Lock(TOOL tool)
    {
        shopItems[(int)tool].GetComponent<ShopItem>().Lock();
        shopItems[(int)tool].transform.GetChild(1).gameObject.SetActive(true); // Lock �̹��� ������
        shopItems[(int)tool].GetComponent<Button>().onClick.AddListener(() =>
        {
            BuyItemObj.GetComponent<BuyItem>().Init(shopItems[(int)tool]);
            BuyItemObj.SetActive(true);
        }); // ��ư �߰�
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
