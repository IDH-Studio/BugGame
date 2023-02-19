using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Reinforce : MonoBehaviour
{
    /*
     * ��ȭ�� �ص� ���� ������ �ִ� ������ �ݿ��� �ȵ� -> Call by ������ ����
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
         * ex) �Ǽո� ���� ä �ĸ�ä ���� �� �ĸ�ä�� ��ȭ�ϸ�
         * �Ǽյ� ���� ��ȭ�Ǵ� ���� �߻�
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
        showPlayerMoney.text = GameManager.instance.CurrentPlayer.Money + "��";
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
