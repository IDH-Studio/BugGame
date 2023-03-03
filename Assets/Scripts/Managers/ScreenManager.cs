using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CESCO;

public class ScreenManager : MonoBehaviour
{
    private Stack<CESCO.SCREEN> ScreenStack;

    public CESCO.SCREEN CurrentScreen() => ScreenStack.Peek();

    private void Awake()
    {
        ScreenStack = new Stack<CESCO.SCREEN>();
        Init();
    }

    public void Init()
    {
        ScreenStack.Clear();
        ScreenStack.Push(SCREEN.START);
        BGMDelayed(ScreenStack.Peek(), 0.8f);
    }

    void BGMDelayed(SCREEN screen, float delay)
    {
        if (screen == SCREEN.START || screen == SCREEN.INGAME)
        {
            // BGM 출력
            GameManager.instance.soundManager.BGMPlay(screen, delay);
        }
        else
        {
            // BGM 출력
            GameManager.instance.soundManager.BGMStop();
        }
    }

    void BGM(SCREEN screen)
    {
        if (screen == SCREEN.START || screen == SCREEN.INGAME)
        {
            // BGM 출력
            GameManager.instance.soundManager.BGMPlay(screen);
        }
        else
        {
            // BGM 출력
            GameManager.instance.soundManager.BGMStop();
        }
    }

    public void ChangeScreen(SCREEN screen)
    {
        // 해당 화면 저장
        ScreenStack.Push(screen);
        // UI 출력
        GameManager.instance.uiManager.ActiveUI(screen);
        BGM(ScreenStack.Peek());
    }

    public void PrevScreen()
    {
        ScreenStack.Pop();
        // UI 끄기
        GameManager.instance.uiManager.InActiveUI();
<<<<<<< Updated upstream
=======
        GameManager.instance.backgroundManager.ChangeBackground();

        if (GameManager.instance.GameState == GAME_STATE.PAUSE) { return; }
        if (ScreenStack.Peek() == SCREEN.PAY) { GameManager.instance.payManager.ShowMoney(); }
>>>>>>> Stashed changes
        BGM(ScreenStack.Peek());
    }
}

// UI에 enum은 없애고 ScreenManager에 있는 SCREEN enum으로 해결
// Screen은 현재 스크린 정보를 가지고 있고 해당 정보로
// UIManager에게 화면에 띄울 UI를 요청함