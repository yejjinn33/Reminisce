using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    public GameObject ControlPanel;  
    public GameObject ExitConfirmPanel;
    public GameObject StartGameButton;
    public GameObject ControlButton;
    public GameObject ExitButton;
    public GameObject Title;

    public void StartGame()
    {
        SceneManager.LoadScene("Start"); 
    }

    public void ShowControls()
    {
        ControlPanel.SetActive(true);
    }

    public void HideControls()
    {
        ControlPanel.SetActive(false);
    }

    public void ShowExitConfirm()
    {
        ExitConfirmPanel.SetActive(true);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // 에디터 실행 종료
#else
        Application.Quit(); // 게임 종료
#endif
    }

    public void CancelExit()
    {
        ExitConfirmPanel.SetActive(false);
    }

    public void HideMenu()
    {
        StartGameButton.SetActive(false);
        ControlButton.SetActive(false);
        ExitButton.SetActive(false);
        Title.SetActive(false);
    }

    public void ShowMenu()
    {
        StartGameButton.SetActive(true);
        ControlButton.SetActive(true);
        ExitButton.SetActive(true);
        Title.SetActive(true);
    }
}
