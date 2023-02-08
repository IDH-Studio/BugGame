using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using CESCO;
using UnityEngine.UI;
using UnityEngine.Events;

// 상점 창
public class Shop : MonoBehaviour
{
    public TextMeshProUGUI showPlayerMoney;
    public GameObject ToolObjs; // 실제 전시되는(구매 하는) 아이템을 모아놓은 오브젝트
    public GameObject BuyItemObj;
    public GameObject Lock;
    public GameObject[] Items; // 베이스가 되는 도구들

    private List<GameObject> shopItems;

    private void Awake()
    {
        print("Shop 호출");
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
        Destroy(shopItems[(int)tool].transform.GetChild(1).gameObject); // Lock 이미지 삭제
        shopItems[(int)tool].GetComponent<Button>().onClick.RemoveAllListeners(); // 버튼 이벤트 삭제
    }

    public void ShowShopInfo()
    {
        ShowItems();
    }

    public void ShowMoney()
    {
        showPlayerMoney.text = GameManager.instance.CurrentPlayer.Money + "원";
    }

    public void ShowItems()
    {
        ShowMoney();
        BuyItemObj.SetActive(false);
    }
}
