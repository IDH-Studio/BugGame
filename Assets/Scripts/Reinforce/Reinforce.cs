using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Reinforce : MonoBehaviour
{
    /*
     * 강화를 해도 실제 가지고 있는 도구에 반영이 안됨 -> Call by 문제로 예상
    */
    public TextMeshProUGUI showPlayerMoney;
    public GameObject toolDetail;
    public GameObject canSelectToolObj;
    public GameObject hasToolPrefab;

    private List<GameObject> hasPlayerTools;

    private void Awake()
    {
        hasPlayerTools = new List<GameObject>();
    }

    public void AddTool(GameObject tool)
    {
        /*
         * ex) 맨손만 가진 채 파리채 구매 후 파리채를 강화하면
         * 맨손도 같이 강화되는 버그 발생
        */
        GameObject showTool = Instantiate(hasToolPrefab, canSelectToolObj.transform);
        showTool.transform.GetChild(0).GetComponent<Image>().sprite =
            tool.GetComponent<SpriteRenderer>().sprite;

        showTool.GetComponent<Button>().onClick.AddListener(() =>
        {
            toolDetail.GetComponent<ShowToolDetail>().SelectTool(tool);
        });

        hasPlayerTools.Add(tool);
    }

    public void RemoveTool(GameObject tool)
    {
        int index = hasPlayerTools.IndexOf(tool);
        hasPlayerTools.Remove(tool);
        Destroy(canSelectToolObj.transform.GetChild(index).gameObject);
    }

    public void ShowMoney()
    {
        showPlayerMoney.text = GameManager.instance.CurrentPlayer.Money + "원";
    }

    public Price ShowToolInfo()
    {
        toolDetail.GetComponent<ShowToolDetail>().ShowAttributes();
        return toolDetail.GetComponent<ShowToolDetail>().ShowPrices();
    }

    public void ShowWindow()
    {
        ShowMoney();

        if (hasPlayerTools.Count > 0)
        {
            GameObject selectTool = hasPlayerTools[0];
            toolDetail.GetComponent<ShowToolDetail>().SelectTool(selectTool);
        }

        ShowToolInfo();
    }

    public void Init()
    {
        hasPlayerTools.Clear();
        foreach (Transform child in canSelectToolObj.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
