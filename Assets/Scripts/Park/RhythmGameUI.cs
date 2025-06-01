using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class RhythmGameUI : MonoBehaviour
{

    public GameObject rhythmGameUI; // ������� UI ������Ʈ
    public GameObject endRhythmUI;  // ���� UI ������Ʈ

    [SerializeField] private TMP_Text perfectText;
    [SerializeField] private TMP_Text coolText;
    [SerializeField] private TMP_Text goodText;
    [SerializeField] private TMP_Text badText;
    [SerializeField] private TMP_Text missText;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text game;
    [SerializeField] private TMP_Text notice;

    void Start()
    {
        // �ʱ⿡�� UI�� ��Ȱ��ȭ�Ǿ� �ֵ��� ����
        rhythmGameUI.SetActive(false);
        endRhythmUI.SetActive(false); // �ʱ� ���� UI ��Ȱ��ȭ
    }

    // ī��Ʈ�ٿ� ���� �� ������� UI�� Ȱ��ȭ�ϴ� �Լ�
    public void ActivateRhythmGameUI()
    {
        rhythmGameUI.SetActive(true); // ������� UI Ȱ��ȭ
        endRhythmUI.SetActive(false);
    }

    public void DeactendRhythmUI()
    {
        endRhythmUI.SetActive(false);
    }

    // ������� UI ��Ȱ��ȭ �� ���� UI Ȱ��ȭ
    public void ActivateEndRhythmUI(TimingManager timingManager, ScoreManager scoreManager)
    {
        rhythmGameUI.SetActive(false);
        endRhythmUI.SetActive(true);

        //secondText.text = Mathf.CeilToInt(timer).ToString();
        // ��� �ؽ�Ʈ ������Ʈ
        game.text = timingManager.isgame;
        notice.text = timingManager.isnotice;
        perfectText.text = scoreManager.PerfectCount.ToString();
        coolText.text = scoreManager.CoolCount.ToString();
        goodText.text = scoreManager.GoodCount.ToString();
        badText.text = scoreManager.BadCount.ToString();
        missText.text = scoreManager.MissCount.ToString();
        scoreText.text = scoreManager.Score.ToString();

    }

    public void DeactivateRhythmGameUI()
    {
        rhythmGameUI.SetActive(false); // �� ������Ʈ(���� ���� UI)�� ��Ȱ��ȭ
        endRhythmUI.SetActive(false);
        Debug.Log("���� ���� UI ��Ȱ��ȭ");
    }
}

