using UnityEngine;

public class Camera_switch : MonoBehaviour
{
    public Camera mainCamera; // 메인 카메라 참조
    public Smooth_follow2 smoothFollowScript; // Smooth_follow 스크립트 참조
    private bool isCameraLocked = false; // 카메라 고정 상태
    private bool hasTriggered = false; // 발판을 처음 밟았을 때만 실행되도록 설정
    private bool hasCameraBeenLocked = false; // 카메라가 이미 고정되었는지 여부


    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main; // 메인 카메라 자동 참조
        }

        if (smoothFollowScript == null && mainCamera != null)
        {
            smoothFollowScript = mainCamera.GetComponent<Smooth_follow2>();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 플레이어와 충돌 감지
        if (collision.gameObject.CompareTag("Player") && !isCameraLocked && !hasTriggered)
        {
            Debug.Log("Player stepped on the Mission_plane.");
            LockCamera(); // 카메라 고정
            hasTriggered = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // 플레이어가 Mission_plane을 떠날 때
        if (collision.gameObject.CompareTag("Player") && isCameraLocked && !hasCameraBeenLocked)
        {
            Debug.Log("Player left the Mission_plane.");
            UnlockCamera(); // 카메라 복구
        }
    }

    private void LockCamera()
    {
        if (!isCameraLocked && !hasCameraBeenLocked)
        {
            isCameraLocked = true;
            hasCameraBeenLocked = true;

            // Smooth_follow 스크립트 비활성화
            if (smoothFollowScript != null)
            {
                smoothFollowScript.enabled = false;
            }

            // 고정된 카메라 위치와 각도 설정
            mainCamera.transform.position = new Vector3(55, 7, 22); // 고정된 위치
            mainCamera.transform.rotation = Quaternion.Euler(7, 180, 0); // 고정된 각도

            Debug.Log("Camera is now locked.");
        }
    }

    private void UnlockCamera()
    {
        if (isCameraLocked)
        {
            isCameraLocked = false;

            // Smooth_follow 스크립트 활성화
            if (smoothFollowScript != null)
            {
                smoothFollowScript.enabled = true;
            }

            Debug.Log("Camera is now unlocked.");
        }
    }

    // 리듬 게임 종료 시 카메라 고정 해제 (필요시)
    public void EndRhythmGame()
    {
        if (hasCameraBeenLocked)
        {
            UnlockCamera(); // 리듬 게임 끝난 후 카메라 고정 해제
            hasCameraBeenLocked = false;
        }
    }
}
