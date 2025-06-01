using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager1 : MonoBehaviour
{
    
    private StoryUIManager storyUIManager;
    private RhythmGameUI rhythmGameUI;
    private Camera_switch camera_switch;

    void Start()
    {
        // StoryGameUI 객체를 찾습니다.
        storyUIManager = FindObjectOfType<StoryUIManager>();
        rhythmGameUI = FindObjectOfType<RhythmGameUI>();
        camera_switch = FindObjectOfType<Camera_switch>();
    }

    void Update()
    {
        if (rhythmGameUI != null && rhythmGameUI.endRhythmUI.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Debug.Log("R 누름");

                // ScoreManager의 점수를 확인
                ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
                if (scoreManager != null && scoreManager.Score >= 1500)
                {
                    EndRhythmGameAndStartStory();
                }
                else
                {
                    RestartGame();
                }
            }
        }
    }



    public void EndRhythmGameAndStartStory()
    {
        // 리듬게임 종료 후 스토리게임 UI 활성화
        rhythmGameUI.DeactivateRhythmGameUI(); // 리듬게임 UI 비활성화
        storyUIManager.ActivateStoryUI(); // 스토리게임 UI 활성화
        camera_switch.EndRhythmGame();
        Debug.Log("리듬 게임 종료 후 스토리 게임 시작");
        GameUIManager2.Instance.Afterrhythmgame();
    }


    public void RestartGame()
    {
        Debug.Log("게임 재시작");
        rhythmGameUI.DeactendRhythmUI();
        ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
        if (scoreManager != null)
        {
            scoreManager.ResetScores(); // 게임 상태 리셋
        }
        TimeUI.ResetTimer();
        GameUIManager2.Instance.StartCountdownExternally();
        
    }
}
