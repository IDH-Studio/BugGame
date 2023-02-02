using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private int totalScore = 0;
    private int score;
    public int Score
    {
        get { return score; }
        set { score = value; }
    }

    [SerializeField] private TextMeshProUGUI ScoreText;
    [SerializeField] private TextMeshProUGUI TotalScoreText;

    void Start()
    {
        Init();
    }

    public void PlusScore()
    {
        ++score;
        ++totalScore;
        ShowScore();
    }

    private void ShowScore()
    {
        ScoreText.text = "���� ���� ��: " + score;
        TotalScoreText.text = "�� ���� ���� ��: " + totalScore;
    }

    public void SaveScore() { totalScore += score; }

    public int ScoreInit()
    {
        int lastScore = totalScore;
        totalScore = 0;
        score = 0;
        return lastScore;
    }

    public void Init()
    {
        score = 0;
        ShowScore();
    }
}
