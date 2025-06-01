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
        // StoryGameUI ��ü�� ã���ϴ�.
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
                Debug.Log("R ����");

                // ScoreManager�� ������ Ȯ��
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
        // ������� ���� �� ���丮���� UI Ȱ��ȭ
        rhythmGameUI.DeactivateRhythmGameUI(); // ������� UI ��Ȱ��ȭ
        storyUIManager.ActivateStoryUI(); // ���丮���� UI Ȱ��ȭ
        camera_switch.EndRhythmGame();
        Debug.Log("���� ���� ���� �� ���丮 ���� ����");
        GameUIManager2.Instance.Afterrhythmgame();
    }


    public void RestartGame()
    {
        Debug.Log("���� �����");
        rhythmGameUI.DeactendRhythmUI();
        ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
        if (scoreManager != null)
        {
            scoreManager.ResetScores(); // ���� ���� ����
        }
        TimeUI.ResetTimer();
        GameUIManager2.Instance.StartCountdownExternally();
        
    }
}
