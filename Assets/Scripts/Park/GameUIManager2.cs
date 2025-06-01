using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameUIManager2 : MonoBehaviour
{
    public static GameUIManager2 Instance; // 싱글톤 패턴

    public CountdownUI countdownUI;
    public RhythmGameUI rhythmGameUI; // 리듬게임 UI 관리
    public NoteManager noteManager;
    public GameObject end;
    public GameObject pointLight;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void Afterrhythmgame()
    {
        end.SetActive(true);
        pointLight.SetActive(true);
    }

    // 카운트다운 코루틴을 외부에서 호출
    public void StartCountdownExternally()
    {
        StartCoroutine(StartCountdownWithReset());
    }

    // 카운트다운 및 초기화
    private IEnumerator StartCountdownWithReset()
    {
        // 카운트다운 UI 표시
        if (countdownUI != null)
        {
            countdownUI.ShowCountdownUI();
        }
        Time.timeScale = 0f;

        for (int i = 3; i > 0; i--)
        {
            if (countdownUI != null)
            {
                countdownUI.UpdateCountdownText(i.ToString());  // 숫자 텍스트 업데이트
            }
            yield return new WaitForSecondsRealtime(1); // 정지 상태에서 카운트다운 진행
        }



        // 카운트다운 UI 숨기기
        if (countdownUI != null)
        {
            countdownUI.HideCountdownUI();
        }

        Debug.Log("게임 시작!");

        ryhthmGameStart();


    }

    private void ryhthmGameStart()
    {
        // 카운트다운 종료 후 리듬게임 UI 활성화
        if (rhythmGameUI != null)
        {
            rhythmGameUI.ActivateRhythmGameUI();
        }

        // 게임 재개
        Time.timeScale = 1f;


        // 리듬 게임 시작 (NoteManager에 알림)
        if (noteManager != null)
        {
            Debug.Log("리듬게임시작");
            noteManager.StartRhythmGame();
        }
        else
        {
            Debug.LogError("NoteManager가 초기화되지 않았습니다! 게임이 시작되지 않습니다.");
        }
    }


}

