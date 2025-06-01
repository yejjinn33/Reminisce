using UnityEngine;

public class GameExit : MonoBehaviour
{
    public GameObject ExitConfirmPanel; // ExitPanel ����
    private bool isPaused = false; // ���� ���� ���� Ȯ��

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
            ExitConfirmPanel.SetActive(isPaused); // �г��� ���� ������ ���� Ȱ��ȭ
        }
    }

    void TogglePauseMenu()
    {
        isPaused = !isPaused; // ���� ���� ���
        Time.timeScale = isPaused ? 0 : 1; // ���� ����/�簳
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // ������ ���� ����
#else
        Application.Quit(); // ���� ����
#endif
    }

    public void CancelExit()
    {
        ExitConfirmPanel.SetActive(false); // �г� ��Ȱ��ȭ
        Time.timeScale = 1; // ���� �簳
        isPaused = false;
    }
}
