using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoSceneTransition : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Video Player 컴포넌트
    public string nextSceneName;    // 다음 씬 이름
    public GameObject normalUI;     // 기존 UI
    //public GameObject videoUI;      // 동영상 전용 UI

    private bool isPlaying = false; // 동영상 재생 여부 체크

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isPlaying) // 플레이어 태그 확인
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
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        // 동영상 종료 후 이벤트
        SceneManager.LoadScene(nextSceneName); // 다음 씬 로드
    }
}
