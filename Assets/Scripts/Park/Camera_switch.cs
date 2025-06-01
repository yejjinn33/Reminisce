using UnityEngine;

public class Camera_switch : MonoBehaviour
{
    public Camera mainCamera; // ���� ī�޶� ����
    public Smooth_follow2 smoothFollowScript; // Smooth_follow ��ũ��Ʈ ����
    private bool isCameraLocked = false; // ī�޶� ���� ����
    private bool hasTriggered = false; // ������ ó�� ����� ���� ����ǵ��� ����
    private bool hasCameraBeenLocked = false; // ī�޶� �̹� �����Ǿ����� ����


    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main; // ���� ī�޶� �ڵ� ����
        }

        if (smoothFollowScript == null && mainCamera != null)
        {
            smoothFollowScript = mainCamera.GetComponent<Smooth_follow2>();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // �÷��̾�� �浹 ����
        if (collision.gameObject.CompareTag("Player") && !isCameraLocked && !hasTriggered)
        {
            Debug.Log("Player stepped on the Mission_plane.");
            LockCamera(); // ī�޶� ����
            hasTriggered = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // �÷��̾ Mission_plane�� ���� ��
        if (collision.gameObject.CompareTag("Player") && isCameraLocked && !hasCameraBeenLocked)
        {
            Debug.Log("Player left the Mission_plane.");
            UnlockCamera(); // ī�޶� ����
        }
    }

    private void LockCamera()
    {
        if (!isCameraLocked && !hasCameraBeenLocked)
        {
            isCameraLocked = true;
            hasCameraBeenLocked = true;

            // Smooth_follow ��ũ��Ʈ ��Ȱ��ȭ
            if (smoothFollowScript != null)
            {
                smoothFollowScript.enabled = false;
            }

            // ������ ī�޶� ��ġ�� ���� ����
            mainCamera.transform.position = new Vector3(55, 7, 22); // ������ ��ġ
            mainCamera.transform.rotation = Quaternion.Euler(7, 180, 0); // ������ ����

            Debug.Log("Camera is now locked.");
        }
    }

    private void UnlockCamera()
    {
        if (isCameraLocked)
        {
            isCameraLocked = false;

            // Smooth_follow ��ũ��Ʈ Ȱ��ȭ
            if (smoothFollowScript != null)
            {
                smoothFollowScript.enabled = true;
            }

            Debug.Log("Camera is now unlocked.");
        }
    }

    // ���� ���� ���� �� ī�޶� ���� ���� (�ʿ��)
    public void EndRhythmGame()
    {
        if (hasCameraBeenLocked)
        {
            UnlockCamera(); // ���� ���� ���� �� ī�޶� ���� ����
            hasCameraBeenLocked = false;
        }
    }
}
