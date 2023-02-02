using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using ObjType = CESCO.OBJ_TYPE;
using CESCO;

public class GameManager : MonoBehaviour
{
    // �̱���
    public static GameManager instance;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject target;

    public Player CurrentPlayer
    {
        get { return player.GetComponent<Player>(); }
    }

    public Target CurrentTarget
    {
        get { return target.GetComponent<Target>(); }
    }

    public float CameraSize;

    // Managers
    public ToolManager toolManager;
    public TimeManager timeManager;
    public UIManager uiManager;
    public PrefabManager prefabManager;
    public SoundManager soundManager;
    public SettingManager settingManager;
    public MouseManager mouseManager;
    public KeyboardManager keyboardManager;
    public ScoreManager scoreManager;
    public ScreenManager screenManager;
    public ShopManager shopManager;
    public ReinforceManager reinforceManager;
    public StageManager stageManager;
    public SpawnManager spawnManager;
    public InputManager inputManager;

    // ���� ���� ����
    private GAME_STATE gameState = GAME_STATE.NONE;
    public GAME_STATE GameState { get { return gameState; } }

    //private float minDelay = 0.6f;
    //private float maxDelay = 1.9f;
    [SerializeField] private uint payTime = 90;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("���� �Ŵ��� ����");
            Destroy(gameObject);
        }

        mouseManager.Visible();
        player.SetActive(false);
        target.SetActive(false);
        CameraSize = Camera.main.orthographicSize;
    }

    private void Update()
    {
        if (gameState == GAME_STATE.RUNNING)
        {
            if (target.GetComponent<Target>().HP <= 0)
            {
                GameOver();
            }

            if (timeManager.RunningTime >= payTime)
            {
                // ���� �Ͻ����� �� ���� ui ���
                gameState = GAME_STATE.PAY;
                GamePause();
            }
        }
    }

    public void GameInit()
    {
        // ���� ���� �ʱ�ȭ
        // �ð�, ���ھ� �ʱ�ȭ
        timeManager.Init();
        scoreManager.Init();
        gameState = GAME_STATE.RUNNING;
        Time.timeScale = 1;
    }

    public void GameStart()
    {
        GameInit();

        // ���콺 �����
        //mouseManager.InVisible();

        // ȭ�� ��ȯ
        screenManager.ChangeScreen(SCREEN.INGAME);

        // Ÿ�� ����
        //target = prefabManager.RequestInstantiate(ObjType.TARGET).GetComponent<Target>();
        target.SetActive(true);

        // ����(��) ����
        //BugSpawn();
        //spawnManager.SpawnBugs();
        spawnManager.StartSpawnBug();

        // Ÿ�̸� ����
        timeManager.TimerStart();

        // �÷��̾� ����
        //player = prefabManager.RequestInstantiate(ObjType.PLAYER).GetComponent<Player>();
        player.SetActive(true);
    }

    public void GameOver()
    {
        // ���� ���� ����
        GameEnd();
        Debug.Log("���� ����!");
        uiManager.GoMain();
    }

    public void GameEnd()
    {
        // �÷��̾�, Ÿ��, ���� ��� ����
        //Destroy(GameObject.FindGameObjectWithTag("Player"));
        player.SetActive(false);
        //Destroy(GameObject.FindGameObjectWithTag("Target"));
        target.SetActive(false);
        //foreach (GameObject bug in GameObject.FindGameObjectsWithTag("Bug"))
        //{
        //    Destroy(bug);
        //}
        spawnManager.RemoveBug();

        // ���� ���� NONE���� ����
        gameState = GAME_STATE.NONE;
        Time.timeScale = 0;
        mouseManager.Visible();
        CancelInvoke("BugSpawn");

        // ��ȭ, ������ �� �ʱ�ȭ �ؾ� ��
        reinforceManager.Init();
        shopManager.Init();
        spawnManager.Init();

        // ���� ���ھ� ���� �� �ʱ�ȭ
        int finishScore = scoreManager.ScoreInit();

        // ���ھ� ���� ���(�ӽ�)
    }

    public void GameSetting()
    {
        // ����� �ٲٱ�

        // ���� ���� �� ���� ����
        Time.timeScale = 0;

        // uiManager�� �̿��Ͽ� ȭ�� ��ȯ
        screenManager.ChangeScreen(SCREEN.SETTING);
        settingManager.Enable();
    }

    public void GamePause()
    {
        if (gameState == GAME_STATE.RUNNING)
        {
            Pause();
        }
        else if (gameState == GAME_STATE.PAUSE)
        {
            Resume();
        }
        else if (gameState == GAME_STATE.PAY)
        {
            Pay();
        }
    }

    public void Resume()
    {
        // ȭ�� ��ȯ(Pause->InGame) ���ư���
        screenManager.PrevScreen();

        // ���콺 Ŀ�� �����
        //mouseManager.InVisible();

        // �ð� ����
        Time.timeScale = 1;

        // ���� �Ͻ�����
        gameState = GAME_STATE.RUNNING;

        spawnManager.StartSpawnBug();
    }

    public void Restart()
    {
        // ���� �����
        // ���� ���� �� �ٽ� ����
        GameEnd();
        GameStart();
    }

    public void Pause()
    {
        // �Ͻ�����
        // ȭ�� ��ȯ
        screenManager.ChangeScreen(SCREEN.PAUSE);

        // ���콺 Ŀ�� ���̱�
        mouseManager.Visible();

        // �ð� ����
        Time.timeScale = 0;

        // ���� �Ͻ�����
        gameState = GAME_STATE.PAUSE;

        // ���� ���� �ߴ�
        spawnManager.StopSpawnBug();
    }

    public void Pay()
    {
        // ���� �� ��ȭ �� ���� ����
        // ���� �Ͻ�����
        Time.timeScale = 0;

        // ���콺 ���̵���
        //mouseManager.Visible();

        // ���� ȭ�� ���
        screenManager.ChangeScreen(SCREEN.PAY);

        // ���� ���� �ߴ�
        spawnManager.StopSpawnBug();
    }

    public void NextGame()
    {
        // ���� �� ��ȭ, ���� ���� �� ���� ���� ����
        stageManager.NextStage();
        gameState = GAME_STATE.RUNNING;
        spawnManager.StartSpawnBug();
        //mouseManager.InVisible();
        screenManager.PrevScreen();
    }

    public void GameMain()
    {
        // ������ �����Ű�� ���� ȭ������ �̵��Ѵ�.
        GameEnd();
        uiManager.GoMain();
    }

    public void GameExit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();  
        #endif
    }

    public void ChangeGameState(GAME_STATE gameState)
    {
        this.gameState = gameState;
    }
}
