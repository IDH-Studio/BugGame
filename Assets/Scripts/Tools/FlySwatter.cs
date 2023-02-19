using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CESCO;

/*
 * 파리채는 맨손에 비해 범위가 조금 더 크다.
*/

public class FlySwatter : Tool
{
    private new void Awake()
    {
        base.Awake();

        float[] radiusValue = { 1.2f, 1.6f, 2f };
        SetRadiusValue(radiusValue);
    }
}
