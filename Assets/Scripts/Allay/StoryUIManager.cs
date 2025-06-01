using UnityEngine;

public class StoryUIManager : MonoBehaviour
{
    public GameObject storyUI; // ���丮 UI ������Ʈ

    public void HideStoryUI()
    {
        storyUI.SetActive(false); // ���丮 UI ��Ȱ��ȭ
    }
    public void ActivateStoryUI()
    {
        storyUI.SetActive(true); // ���丮 UI ��Ȱ��ȭ
    }
}
