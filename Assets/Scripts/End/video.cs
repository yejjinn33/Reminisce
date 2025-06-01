using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class video : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Video Player 컴포넌트
    public string nextSceneName;    // 다음 씬 이름
    public GameObject normalUI;     // 기존 UI
    private bool isPlaying = false; // 동영상 재생 여부 체크

    void Start()
    {
        Camera mainCamera = Camera.main;  // 메인 카메라 가져오기

        // 카메라의 near clip plane 위치를 확인
        float nearClip = mainCamera.nearClipPlane;

        // VideoPlayer가 있는 오브젝트 가져오기
        GameObject videoObject = videoPlayer.gameObject;

        // 동영상 오브젝트의 위치를 카메라의 near clip plane 범위 내로 설정
        // 카메라의 위치 + 카메라의 forward 방향 벡터에 nearClip 값을 더해서 위치 지정
        videoObject.transform.position = mainCamera.transform.position + mainCamera.transform.forward * nearClip;

        // 동영상 오브젝트가 카메라의 앞에 있도록 약간의 오프셋을 추가
        videoObject.transform.position += mainCamera.transform.forward * 0.1f; // 예시: 0.1f만큼 카메라 앞쪽으로 오프셋 추가
    }


    public void OnVideoStart()
    {
        Debug.Log("video");
        isPlaying = true; // 재생 상태로 설정
        videoPlayer.gameObject.SetActive(true); // Video Player 활성화
        videoPlayer.Play(); // 동영상 재생
        if (normalUI != null) normalUI.SetActive(false);
        //if (videoUI != null) videoUI.SetActive(true);


        // 동영상 재생 완료 시 이벤트 등록
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        // 동영상 종료 후 이벤트
        SceneManager.LoadScene(nextSceneName); // 다음 씬 로드
    }
}
