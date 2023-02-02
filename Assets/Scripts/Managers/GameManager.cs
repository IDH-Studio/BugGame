using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using ObjType = CESCO.OBJ_TYPE;
using CESCO;

public class GameManager : MonoBehaviour
{
    // 싱글톤
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

    // 게임 관련 변수
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
            Debug.Log("게임 매니저 존재");
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
                // 게임 일시정지 후 정산 ui 출력
                gameState = GAME_STATE.PAY;
                GamePause();
            }
        }
    }

    public void GameInit()
    {
        // 게임 상태 초기화
        // 시간, 스코어 초기화
        timeManager.Init();
        scoreManager.Init();
        gameState = GAME_STATE.RUNNING;
        Time.timeScale = 1;
    }

    public void GameStart()
    {
        GameInit();

        // 마우스 숨기기
        //mouseManager.InVisible();

        // 화면 전환
        screenManager.ChangeScreen(SCREEN.INGAME);

        // 타겟 스폰
        //target = prefabManager.RequestInstantiate(ObjType.TARGET).GetComponent<Target>();
        target.SetActive(true);

        // 벌레(적) 스폰
        //BugSpawn();
        //spawnManager.SpawnBugs();
        spawnManager.StartSpawnBug();

        // 타이머 시작
        timeManager.TimerStart();

        // 플레이어 스폰
        //player = prefabManager.RequestInstantiate(ObjType.PLAYER).GetComponent<Player>();
        player.SetActive(true);
    }

    public void GameOver()
    {
        // 게임 오버 조건
        GameEnd();
        Debug.Log("게임 오버!");
        uiManager.GoMain();
    }

    public void GameEnd()
    {
        // 플레이어, 타겟, 벌레 모두 삭제
        //Destroy(GameObject.FindGameObjectWithTag("Player"));
        player.SetActive(false);
        //Destroy(GameObject.FindGameObjectWithTag("Target"));
        target.SetActive(false);
        //foreach (GameObject bug in GameObject.FindGameObjectsWithTag("Bug"))
        //{
        //    Destroy(bug);
        //}
        spawnManager.RemoveBug();

        // 게임 상태 NONE으로 변경
        gameState = GAME_STATE.NONE;
        Time.timeScale = 0;
        mouseManager.Visible();
        CancelInvoke("BugSpawn");

        // 강화, 상점도 다 초기화 해야 함
        reinforceManager.Init();
        shopManager.Init();
        spawnManager.Init();

        // 최종 스코어 저장 및 초기화
        int finishScore = scoreManager.ScoreInit();

        // 스코어 저장 기능(임시)
    }

    public void GameSetting()
    {
        // 배경음 바꾸기

        // 게임 진행 시 게임 멈춤
        Time.timeScale = 0;

        // uiManager를 이용하여 화면 전환
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
        // 화면 전환(Pause->InGame) 돌아가기
        screenManager.PrevScreen();

        // 마우스 커서 숨기기
        //mouseManager.InVisible();

        // 시간 조정
        Time.timeScale = 1;

        // 게임 일시정지
        gameState = GAME_STATE.RUNNING;

        spawnManager.StartSpawnBug();
    }

    public void Restart()
    {
        // 게임 재시작
        // 게임 종료 후 다시 시작
        GameEnd();
        GameStart();
    }

    public void Pause()
    {
        // 일시정지
        // 화면 전환
        screenManager.ChangeScreen(SCREEN.PAUSE);

        // 마우스 커서 보이기
        mouseManager.Visible();

        // 시간 조정
        Time.timeScale = 0;

        // 게임 일시정지
        gameState = GAME_STATE.PAUSE;

        // 벌레 스폰 중단
        spawnManager.StopSpawnBug();
    }

    public void Pay()
    {
        // 정산 후 강화 및 구매 진행
        // 게임 일시정지
        Time.timeScale = 0;

        // 마우스 보이도록
        //mouseManager.Visible();

        // 정산 화면 출력
        screenManager.ChangeScreen(SCREEN.PAY);

        // 벌레 스폰 중단
        spawnManager.StopSpawnBug();
    }

    public void NextGame()
    {
        // 정산 및 강화, 구매 진행 후 다음 게임 진행
        stageManager.NextStage();
        gameState = GAME_STATE.RUNNING;
        spawnManager.StartSpawnBug();
        //mouseManager.InVisible();
        screenManager.PrevScreen();
    }

    public void GameMain()
    {
        // 게임을 종료시키고 메인 화면으로 이동한다.
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
