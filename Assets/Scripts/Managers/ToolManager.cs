using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CESCO;

public class ToolManager : MonoBehaviour
{
    public GameObject[] toolObjs;

    private List<GameObject> tools;

    private void Awake()
    {
        tools = new List<GameObject>();

        foreach (GameObject toolObj in toolObjs)
        {
            GameObject tool = Instantiate(toolObj, transform);
            tool.SetActive(false);
            tools.Insert((int)tool.GetComponent<Tool>().ToolType, tool);
        }
    }

    public GameObject GetTool(TOOL tool)
    {
        return tools[(int)tool];
    }

    public Tool GetToolInfo(TOOL tool)
    {
        return tools[(int)tool].GetComponent<Tool>();
    }

    public GameObject GivePlayer(TOOL tool)
    {
        return GameManager.instance.CurrentPlayer.GetTool(tool, tools[(int)tool]);
    }

    public GameObject GiveTool(TOOL tool)
    {
        return tools[(int)tool];
    }

    public void ReinforceRate(GameObject tool)
    {
        if (tool.GetComponent<Tool>().ToolRate == TOOL_SPEED.SUPER_FAST) return;

        tool.GetComponent<Tool>().ToolRate += 1;
        tool.GetComponent<Tool>().SetRate();
    }

    public void ReinforceRadius(GameObject tool)
    {
        if (tool.GetComponent<Tool>().ToolRadius == TOOL_RADIUS.LARGE) return;

        tool.GetComponent<Tool>().ToolRadius += 1;
        tool.GetComponent<Tool>().SetRadius();
    }

    public void ReinforceSpeed(GameObject tool)
    {
        if (tool.GetComponent<Tool>().ToolSpeed == TOOL_SPEED.SUPER_FAST) return;

        tool.GetComponent<Tool>().ToolSpeed += 1;
        tool.GetComponent<Tool>().SetSpeed();
    }
}
