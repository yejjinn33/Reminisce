using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameUIManager : MonoBehaviour
{
    public static GameUIManager Instance; // 싱글톤 패턴

    public GameObject[] courseUIs; // 코스별 UI 배열
    private int currentCourse = 0; // 현재 코스 번호
    public CountdownUI countdownUI;

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

    public void RestartGameFromFirstCourse()
    {
        currentCourse = 0; // 코스 초기화
        ShowCurrentCourseUI(); // 1코스 UI 활성화
        Debug.Log("게임이 처음부터 다시 시작됩니다.");
    }



    // 현재 코스 UI를 표시
    public void ShowCurrentCourseUI()
    {
        HideAllCourseUIs(); // 이전 UI 비활성화
        if (currentCourse < courseUIs.Length)
        {
            courseUIs[currentCourse].SetActive(true); // 현재 코스 UI 활성화
        }
    }

    // 모든 UI 비활성화
    public void HideAllCourseUIs()
    {
        foreach (GameObject ui in courseUIs)
        {
            ui.SetActive(false);
        }
    }

    // 다음 코스로 진행
    public void CompleteCurrentCourse()
    {
        if (currentCourse < courseUIs.Length - 1)
        {
            currentCourse++;
            ShowCurrentCourseUI();
        }
        else
        {
            Debug.Log("모든 코스를 완료했습니다!");
        }
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

        for (int i = 3; i > 0; i--)
        {
            if (countdownUI != null)
            {
                countdownUI.UpdateCountdownText(i.ToString());  // 숫자 텍스트 업데이트
            }
            yield return new WaitForSecondsRealtime(1); // 정지 상태에서 카운트다운 진행
        }

        // 게임 재개
        Time.timeScale = 1f;

        // 코스 초기화
        RestartGameFromFirstCourse();

        // 카운트다운 UI 숨기기
        if (countdownUI != null)
        {
            countdownUI.HideCountdownUI();
        }

        Debug.Log("게임 시작!");
    }


}

