using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Collections.Generic;

public class MemoryFragmentUI : MonoBehaviour
{
    public Image[] images; // �̹��� UI �迭 (3�� �̹���)
    public VideoPlayer videoPlayer; // VideoPlayer ������Ʈ
    public VideoClip[] videoClips; // �� �̹����� �Ҵ�� ����

    public GameObject[] uiElements; // ������ UI ��ҵ�

    private Dictionary<GameObject, bool> uiStates; // UI Ȱ��ȭ ���� ����
    private VideoClip originalClip; // VideoPlayer�� �ʱ� ����
    private float originalTimeScale; // ������ ���� �ð� �帧 ����

    private void Start()
    {
        uiStates = new Dictionary<GameObject, bool>();
        originalClip = videoPlayer.clip;
        originalTimeScale = Time.timeScale; // ���� ���� �ӵ� ����

        // �� �̹����� Ŭ������ �� �̺�Ʈ ���
        for (int i = 0; i < images.Length; i++)
        {
            int index = i; // Ŭ���� ������ ���ϱ� ���� �ε��� ����
            images[i].GetComponent<Button>().onClick.AddListener(() => OnImageClick(index));
        }

        // ������ ������ �� ȣ��� �̺�Ʈ ���
        videoPlayer.loopPointReached += OnVideoEnd;
        videoPlayer.gameObject.SetActive(false); // ���� �� VideoPlayer ��Ȱ��ȭ
    }

    void OnImageClick(int index)
    {
        if (videoPlayer == null)
        {
            Debug.LogError("VideoPlayer�� �Ҵ���� �ʾҽ��ϴ�.");
            return;
        }

        // Ŭ���� �̹����� �Ͼ������ Ȯ��
        if (images[index].color == Color.white)
        {
            // ���� UI ���� ����
            SaveUIState();
            // ���� �ð� ����
            PauseGame();
            // ��� UI ��� �����
            SetUIElementsActive(false);
            // VideoPlayer Ȱ��ȭ �� ���
            videoPlayer.gameObject.SetActive(true);
            // �ش� �̹����� �´� ���� ���
            videoPlayer.clip = videoClips[index];
            videoPlayer.Play();
        }
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        // VideoPlayer ��Ȱ��ȭ
        videoPlayer.Stop();
        videoPlayer.clip = originalClip;
        videoPlayer.gameObject.SetActive(false);
        // ������ ���� �� UI ���� ����
        RestoreUIState();
        // ���� �ð� ����
        ResumeGame();
    }

    void SaveUIState()
    {
        Debug.Log("Saving UI state...");
        uiStates.Clear();
        foreach (var element in uiElements)
        {
            if (element != null)
            {
                bool isActive = element.activeSelf;
                uiStates[element] = isActive;
                Debug.Log($"Saved: {element.name} - Active: {isActive}");
            }
            else
            {
                Debug.LogWarning("UI Element is null. Please check the uiElements array.");
            }
        }
    }

    void RestoreUIState()
    {
        Debug.Log("Restoring UI state...");
        foreach (var element in uiElements)
        {
            if (element != null && uiStates.TryGetValue(element, out bool isActive))
            {
                element.SetActive(isActive);
                Debug.Log($"Restored: {element.name} - Active: {isActive}");
            }
            else
            {
                Debug.LogWarning($"Failed to restore: {element?.name ?? "null"}. Make sure it is included in uiElements.");
            }
        }
    }

    void SetUIElementsActive(bool isActive)
    {
        foreach (var element in uiElements)
        {
            if (element != null)
            {
                element.SetActive(isActive);
                Debug.Log($"{(isActive ? "Showing" : "Hiding")}: {element.name}");
            }
        }
    }
    void PauseGame()
    {
        originalTimeScale = Time.timeScale; // ���� ���� �ӵ� ����
        Time.timeScale = 0; // ���� ����
        Debug.Log("Game Paused.");
    }

    void ResumeGame()
    {
        Time.timeScale = originalTimeScale; // ���� �ӵ� ����
        Debug.Log("Game Resumed.");
    }
}