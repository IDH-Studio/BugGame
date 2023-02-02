using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CESCO;

public class FlySwatter : Tool
{
    new void Awake()
    {
        base.Awake(TOOL_SPEED.SLOW, TOOL_RADIUS.LARGE, "ÆÄ¸®Ã¤");
    }
}
