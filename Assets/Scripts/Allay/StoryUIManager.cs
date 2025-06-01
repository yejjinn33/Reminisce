using UnityEngine;

public class StoryUIManager : MonoBehaviour
{
    public GameObject storyUI; // 스토리 UI 오브젝트

    public void HideStoryUI()
    {
        storyUI.SetActive(false); // 스토리 UI 비활성화
    }
    public void ActivateStoryUI()
    {
        storyUI.SetActive(true); // 스토리 UI 비활성화
    }
}
