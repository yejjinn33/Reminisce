using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public int Score { get; private set; } = 0;
    public int PerfectCount { get; private set; } = 0;
    public int CoolCount { get; private set; } = 0;
    public int GoodCount { get; private set; } = 0;
    public int BadCount { get; private set; } = 0;
    public int MissCount { get; private set; } = 0;

    public int PerfectScore = 50;
    public int CoolScore = 25;
    public int GoodScore = 15;
    public int BadScore = -5;
    public int MissScore = -20;

    // UI Slider�� ������ ������Ʈ�� ���� ����
    [SerializeField] private Slider scoreSlider;
    [SerializeField] private int maxScore = 1500; // �ִ� ���� (�������� ���� ��)

    void Start()
    {
        // �ʱ� �����̴� �� ����
        scoreSlider.maxValue = maxScore;
        scoreSlider.value = Score;
    }

    public void ResetScores()
    {
        Score = 0;
        PerfectCount = 0;
        CoolCount = 0;
        GoodCount = 0;
        BadCount = 0;
        MissCount = 0;
        UpdateScoreSlider();
    }

    public void AddPerfect()
    {
        PerfectCount++;
        Score += PerfectScore;
        UpdateScoreSlider();
    }

    public void AddCool()
    {
        CoolCount++;
        Score += CoolScore;
        UpdateScoreSlider();
    }

    public void AddGood()
    {
        GoodCount++;
        Score += GoodScore;
        UpdateScoreSlider();
    }

    public void AddBad()
    {
        BadCount++;
        Score += BadScore;
        if (Score < 0) Score = 0;
        UpdateScoreSlider();
    }

    public void AddMiss()
    {
        MissCount++;
        Score += MissScore;
        if (Score < 0) Score = 0;
        UpdateScoreSlider();
    }
    // ���� ��ȭ�� ���� �����̴� �� ����
    private void UpdateScoreSlider()
    {
        scoreSlider.value = Score;
    }
}
