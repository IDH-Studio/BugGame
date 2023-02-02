using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CESCO;

public class ShopItem : MonoBehaviour
{
    [SerializeField] private int itemPrice;
    [SerializeField] private TOOL toolType;
    private bool isLock = true;

    private Tool toolInfo;

    #region Getter
    public int ItemPrice
    {
        get { return itemPrice; }
    }

    public TOOL ToolType
    {
        get { return toolType; }
    }

    public bool IsLock
    {
        get { return isLock; }
    }

    public Tool ToolInfo
    {
        get { return toolInfo; }
    }
    #endregion


    private void Awake()
    {
        toolInfo = GameManager.instance.toolManager.GetToolInfo(toolType);
    }

    public void UnLock()
    {
        isLock = false;
    }
}
