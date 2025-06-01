using UnityEngine;

public class GameExit : MonoBehaviour
{
    public GameObject ExitConfirmPanel; // ExitPanel 연결
    private bool isPaused = false; // 게임 멈춤 상태 확인

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
            ExitConfirmPanel.SetActive(isPaused); // 패널은 멈춤 상태일 때만 활성화
        }
    }

    void TogglePauseMenu()
    {
        isPaused = !isPaused; // 멈춤 상태 토글
        Time.timeScale = isPaused ? 0 : 1; // 게임 멈춤/재개
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // 에디터 실행 종료
#else
        Application.Quit(); // 게임 종료
#endif
    }

    public void CancelExit()
    {
        ExitConfirmPanel.SetActive(false); // 패널 비활성화
        Time.timeScale = 1; // 게임 재개
        isPaused = false;
    }
}
