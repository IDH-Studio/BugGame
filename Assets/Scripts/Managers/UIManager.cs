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

    private List<GameObject> GameUI;
    private Stack<GameObject> GameUIStack;
    private GameObject activeUI;
    private GameObject prevUI;
        
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

        // ��� UI�� ��Ȱ��ȭ �� ��
        foreach(GameObject gameUI in GameUI)
        {
            gameUI.SetActive(false);
        }

        // ���� �޴� UI�� Ȱ��ȭ
        Init();
    }

    private void Init()
    {
        GameUIStack.Clear();
        GameUIStack.Push(GameUI[(int)SCREEN.START]);
        GameUIStack.Peek().SetActive(true);
    }

    public void ActiveUI(SCREEN screen)
    {
        // ���� �ֱٿ� ������ UI ��Ȱ��ȭ
        GameUIStack.Peek().SetActive(false);

        // ������ UI Ȱ��ȭ
        GameUIStack.Push(GameUI[(int)screen]);
        GameUIStack.Peek().SetActive(true);
    }

    public void InActiveUI()
    {
        // ���� �ֱٿ� ������ UI ��Ȱ��ȭ �� ���� ����
        GameUIStack.Pop().SetActive(false);
        print(GameUIStack.Peek());
        GameUIStack.Peek().SetActive(true);
    }

    public void GoMain()
    {
        // ���� ȭ������ ���ư� �� ���� ���� ��
        // �ֱٿ� ��µ� UI ��Ȱ��ȭ ��
        // UI�� ����� ���� �ʱ�ȭ
        GameUIStack.Peek().SetActive(false);

        // ȭ��, UI �ʱ�ȭ
        GameManager.instance.screenManager.Init();
        Init();
    }
}