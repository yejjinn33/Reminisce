using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class RhythmGameUI : MonoBehaviour
{

    public GameObject rhythmGameUI; // 리듬게임 UI 오브젝트
    public GameObject endRhythmUI;  // 종료 UI 오브젝트

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
        // 초기에는 UI가 비활성화되어 있도록 설정
        rhythmGameUI.SetActive(false);
        endRhythmUI.SetActive(false); // 초기 종료 UI 비활성화
    }

    // 카운트다운 끝난 후 리듬게임 UI를 활성화하는 함수
    public void ActivateRhythmGameUI()
    {
        rhythmGameUI.SetActive(true); // 리듬게임 UI 활성화
        endRhythmUI.SetActive(false);
    }

    public void DeactendRhythmUI()
    {
        endRhythmUI.SetActive(false);
    }

    // 리듬게임 UI 비활성화 및 종료 UI 활성화
    public void ActivateEndRhythmUI(TimingManager timingManager, ScoreManager scoreManager)
    {
        rhythmGameUI.SetActive(false);
        endRhythmUI.SetActive(true);

        //secondText.text = Mathf.CeilToInt(timer).ToString();
        // 결과 텍스트 업데이트
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
        rhythmGameUI.SetActive(false); // 이 오브젝트(리듬 게임 UI)를 비활성화
        endRhythmUI.SetActive(false);
        Debug.Log("리듬 게임 UI 비활성화");
    }
}

