using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CESCO;

public class Hand : Tool
{
    new void Awake()
    {
        base.Awake(TOOL_SPEED.SUPER_SLOW, TOOL_RADIUS.SMALL, TOOL_SPEED.SUPER_SLOW, "¸Ç¼Õ");
    }
}
