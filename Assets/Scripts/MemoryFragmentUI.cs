using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Collections.Generic;

public class MemoryFragmentUI : MonoBehaviour
{
    public Image[] images; // 이미지 UI 배열 (3개 이미지)
    public VideoPlayer videoPlayer; // VideoPlayer 컴포넌트
    public VideoClip[] videoClips; // 각 이미지에 할당될 영상

    public GameObject[] uiElements; // 복원할 UI 요소들

    private Dictionary<GameObject, bool> uiStates; // UI 활성화 상태 저장
    private VideoClip originalClip; // VideoPlayer의 초기 상태
    private float originalTimeScale; // 게임의 원래 시간 흐름 저장

    private void Start()
    {
        uiStates = new Dictionary<GameObject, bool>();
        originalClip = videoPlayer.clip;
        originalTimeScale = Time.timeScale; // 현재 게임 속도 저장

        // 각 이미지를 클릭했을 때 이벤트 등록
        for (int i = 0; i < images.Length; i++)
        {
            int index = i; // 클로저 문제를 피하기 위한 인덱스 저장
            images[i].GetComponent<Button>().onClick.AddListener(() => OnImageClick(index));
        }

        // 영상이 끝났을 때 호출될 이벤트 등록
        videoPlayer.loopPointReached += OnVideoEnd;
        videoPlayer.gameObject.SetActive(false); // 시작 시 VideoPlayer 비활성화
    }

    void OnImageClick(int index)
    {
        if (videoPlayer == null)
        {
            Debug.LogError("VideoPlayer가 할당되지 않았습니다.");
            return;
        }

        // 클릭한 이미지가 하얀색인지 확인
        if (images[index].color == Color.white)
        {
            // 현재 UI 상태 저장
            SaveUIState();
            // 게임 시간 멈춤
            PauseGame();
            // 모든 UI 요소 숨기기
            SetUIElementsActive(false);
            // VideoPlayer 활성화 후 재생
            videoPlayer.gameObject.SetActive(true);
            // 해당 이미지에 맞는 영상 재생
            videoPlayer.clip = videoClips[index];
            videoPlayer.Play();
        }
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        // VideoPlayer 비활성화
        videoPlayer.Stop();
        videoPlayer.clip = originalClip;
        videoPlayer.gameObject.SetActive(false);
        // 영상이 끝난 후 UI 상태 복원
        RestoreUIState();
        // 게임 시간 복원
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
        originalTimeScale = Time.timeScale; // 현재 게임 속도 저장
        Time.timeScale = 0; // 게임 멈춤
        Debug.Log("Game Paused.");
    }

    void ResumeGame()
    {
        Time.timeScale = originalTimeScale; // 게임 속도 복원
        Debug.Log("Game Resumed.");
    }
}