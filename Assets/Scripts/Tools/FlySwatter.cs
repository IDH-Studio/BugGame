using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CESCO;

/*
 * �ĸ�ä�� �Ǽտ� ���� ������ ���� �� ũ��.
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
