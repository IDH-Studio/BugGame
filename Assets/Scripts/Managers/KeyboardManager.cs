using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CESCO;

public class KeyboardManager : MonoBehaviour
{
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Escape) && GameManager.instance.gameState != GAME_STATE.NONE)
        //{
        //    // ���� ���϶��� �Ͻ����� ����
        //    if (GameManager.instance.gameState == GAME_STATE.PAUSE) // �Ͻ����� ����
        //    {
        //        // ���� �簳
        //        GameManager.instance.Resume();
        //    }
        //    else if (GameManager.instance.gameState == GAME_STATE.RUNNING) // ���� �������� ����
        //    {
        //        // ���� �Ͻ�����
        //        GameManager.instance.Pause();
        //    }
        //}
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.instance.GamePause();
        }

        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    GameManager.instance.CurrentPlayer.ChangeToolHand();
        //}

        //if (Input.GetKeyDown(KeyCode.Alpha2))
        //{
        //    GameManager.instance.CurrentPlayer.ChangeToolFlySwatter();
        //}

        //if (Input.GetKeyDown(KeyCode.BackQuote))
        //{
        //    foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Bug"))
        //    {
        //        Destroy(enemy);
        //    }
        //}
    }
}
