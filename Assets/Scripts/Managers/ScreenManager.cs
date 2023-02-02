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
            // BGM ���
            GameManager.instance.soundManager.BGMPlay(screen, delay);
        }
        else
        {
            // BGM ���
            GameManager.instance.soundManager.BGMStop();
        }
    }

    void BGM(SCREEN screen)
    {
        if (screen == SCREEN.START || screen == SCREEN.INGAME)
        {
            // BGM ���
            GameManager.instance.soundManager.BGMPlay(screen);
        }
        else
        {
            // BGM ���
            GameManager.instance.soundManager.BGMStop();
        }
    }

    public void ChangeScreen(SCREEN screen)
    {
        // �ش� ȭ�� ����
        ScreenStack.Push(screen);
        // UI ���
        GameManager.instance.uiManager.ActiveUI(screen);
        BGM(ScreenStack.Peek());
    }

    public void PrevScreen()
    {
        ScreenStack.Pop();
        // UI ����
        GameManager.instance.uiManager.InActiveUI();
        BGM(ScreenStack.Peek());
    }
}

// UI�� enum�� ���ְ� ScreenManager�� �ִ� SCREEN enum���� �ذ�
// Screen�� ���� ��ũ�� ������ ������ �ְ� �ش� ������
// UIManager���� ȭ�鿡 ��� UI�� ��û��