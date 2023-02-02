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
        //    // 게임 중일때만 일시정지 가능
        //    if (GameManager.instance.gameState == GAME_STATE.PAUSE) // 일시정지 상태
        //    {
        //        // 게임 재개
        //        GameManager.instance.Resume();
        //    }
        //    else if (GameManager.instance.gameState == GAME_STATE.RUNNING) // 게임 진행중인 상태
        //    {
        //        // 게임 일시정지
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
