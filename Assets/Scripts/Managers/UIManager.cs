using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CESCO;
using UnityEngine.Device;

public class UIManager : MonoBehaviour
{
    public GameObject start;
    public GameObject inGame;
    public GameObject pause;
    public GameObject setting;
    public GameObject pay;
    public GameObject reinforce;
    public GameObject shop;
    public GameObject gameOver;
    public GameObject gameRule;

    private List<GameObject> GameUI;
    private Stack<GameObject> GameUIStack;
    [SerializeField] private GameObject GamePauseButton;
        
    void Awake()
    {
        GameUI = new List<GameObject>();
        GameUIStack = new Stack<GameObject>();

        GameUI.Insert((int)SCREEN.START, start);
        GameUI.Insert((int)SCREEN.INGAME, inGame);
        GameUI.Insert((int)SCREEN.PAUSE, pause);
        GameUI.Insert((int)SCREEN.SETTING, setting);
        GameUI.Insert((int)SCREEN.PAY, pay);
        GameUI.Insert((int)SCREEN.REINFORCE, reinforce);
        GameUI.Insert((int)SCREEN.SHOP, shop);
        GameUI.Insert((int)SCREEN.GAMEOVER, gameOver);
        GameUI.Insert((int)SCREEN.GAMERULE, gameRule);

        // 모든 UI를 비활성화 한 뒤
        foreach(GameObject gameUI in GameUI)
        {
            gameUI.SetActive(false);
        }

        // 시작 메뉴 UI만 활성화
        Init();
    }

    private void Init()
    {
        GameUIStack.Clear();
        GameUIStack.Push(GameUI[(int)SCREEN.START]);
        GameUIStack.Peek().SetActive(true);
        GamePauseButton.SetActive(false);
    }

    public void ActiveUI(SCREEN screen)
    {
        // 가장 최근에 보여진 UI 비활성화
        GameUIStack.Peek().SetActive(false);

        // 선택한 UI 활성화
        GameUIStack.Push(GameUI[(int)screen]);
        GameUIStack.Peek().SetActive(true);
        if (GameManager.instance.GameState != GAME_STATE.START) GamePauseButton.SetActive(true);
        else GamePauseButton.SetActive(false);
    }

    public void InActiveUI()
    {
        // 가장 최근에 보여진 UI 비활성화 및 스택 삭제
        GameUIStack.Pop().SetActive(false);
        print(GameUIStack.Peek());
        GameUIStack.Peek().SetActive(true);
        if (GameManager.instance.GameState != GAME_STATE.START) GamePauseButton.SetActive(true);
        else GamePauseButton.SetActive(false);
    }

    public void GoMain()
    {
        // 메인 화면으로 돌아갈 시 게임 종료 후
        // 최근에 출력된 UI 비활성화 후
        // UI가 담겨진 스택 초기화
        GameUIStack.Peek().SetActive(false);

        // 화면, UI 초기화
        Init();
    }
}
