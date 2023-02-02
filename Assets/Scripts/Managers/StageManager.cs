using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CESCO;
using UnityEngine.SceneManagement;

/*
 * 스테이지를 진행하는 방법
 * 1. 씬을 만들어서 관리한다
    -> GameUI 남겨놓고 매니저들 남겨놔야 됨
    -> 장점: 원하는 씬 관리 가능
    -> 단점: 무한으로 하기는 힘듬
 * 2. 배경이나 벌레 스포너 등 오브젝트를 관리해야 함
    -> 씬 변환을 신경쓰지 않아도 됨
    -> 장점: 무한으로 여러개를 섞어서 만들 수 있다.
    -> 단점: 정해진대로 진행하기는 힘들다.

    결론: 2번으로 하는게 이 게임의 취지와 맞다.
*/

public class StageManager : MonoBehaviour
{
    public void NextStage()
    {
        /*
         * 다음 스테이지로 넘어가게 되면 스코어가 초기화 되어야 하고 시간도 초기화 되어야 한다.
         * 또한 생성되었던 벌레를 모두 삭제 시켜야 하며 뒷 배경과 타겟의 이미지(임시),
         * 다음 스테이지에 생성 될 벌레가 정해져야 한다.
        */

        // 현재 스코어 저장 및 초기화
        //GameManager.instance.scoreManager.SaveScore();
        GameManager.instance.scoreManager.Init();

        // time 초기화 및 타이머 진행
        GameManager.instance.timeManager.TimeInit();
        GameManager.instance.timeManager.ProgressTimer();

        // 일시정지 해제(시간 지나도록)
        Time.timeScale = 1;

        // 이전 벌레 삭제
        //foreach (GameObject bug in GameObject.FindGameObjectsWithTag("Bug"))
        //{
        //    Destroy(bug);
        //}
        GameManager.instance.spawnManager.RemoveBug();

        // 다음 벌레 생성
        // SpawnManager 이용
        GameManager.instance.spawnManager.Next();

        // 배경 변경
        // ImageManager(임시) 이용
    }
}
