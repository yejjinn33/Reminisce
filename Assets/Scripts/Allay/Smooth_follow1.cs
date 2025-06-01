using UnityEngine;
using System.Collections;

public class Smooth_follow1 : MonoBehaviour
{
   
    public StoryUIManager storyUIManager; // 스토리 UI를 관리할 StoryUIManager
    public Transform target; // 고양이 Transform
    public float distance = 15.0f; // 카메라와 고양이 간의 거리
    public float height = 3.0f; // 카메라 높이
    public float heightDamping = 2.0f; // 높이 변화 속도
    public float rotationDamping = 3.0f; // 회전 변화 속도
    public float lookAtOffsetY = 1.0f; // 고양이 머리 위를 바라보는 Y축 오프셋
    public float additionalTilt = 15.0f; // 카메라가 위를 바라보는 추가 각도
    public DialogueManager1 dialogueManager; // UI를 관리할 DialogueManager

    private bool hasActivatedUI = false; // UI가 이미 활성화되었는지 여부

    private void Start()
    {
        if (dialogueManager != null)
        {
            dialogueManager.OnDialogueFinished += OnDialogueFinished;
        }
    }

    private void OnDialogueFinished()
    {
        if (target != null)
        {
            target.rotation = Quaternion.Euler(target.rotation.eulerAngles.x, target.rotation.eulerAngles.y + 180f, target.rotation.eulerAngles.z);
        }

        // 스토리 UI 비활성화
        if (storyUIManager != null)
        {
            storyUIManager.HideStoryUI();
        }

        // 게임 정지
        Time.timeScale = 0f;


        // 카운트다운 시작
        if (GameUIManager.Instance != null)
        {
            GameUIManager.Instance.StartCountdownExternally();
        }
        else
        {
            Debug.LogError("GameUIManager.Instance가 null입니다. GameUIManager가 씬에 추가되어 있는지 확인하세요.");
        }
    }

    

    void LateUpdate()
    {
        if (!target) return;

        float wantedRotationAngle = target.eulerAngles.y;
        float wantedHeight = target.position.y + height;
        float currentRotationAngle = transform.eulerAngles.y;
        float currentHeight = transform.position.y;

        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

        Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        transform.position = target.position;
        transform.position -= currentRotation * Vector3.forward * distance;

        Vector3 temp_position = transform.position;
        temp_position.y = currentHeight;
        transform.position = temp_position;

        Vector3 lookAtPosition = target.position;
        lookAtPosition.y += lookAtOffsetY;

        Quaternion lookAtRotation = Quaternion.LookRotation(lookAtPosition - transform.position, Vector3.up);
        transform.rotation = lookAtRotation * Quaternion.Euler(-additionalTilt, 0, 0);

        if (!hasActivatedUI &&
            IsPositionWithinRange(transform.position, new Vector3(73.5f, 2.83f, 196.80f), 0.1f) &&
            IsRotationWithinRange(transform.eulerAngles, new Vector3(-8.093f, 1.915f, 0f), 0.5f))
        {
            hasActivatedUI = true;
            ShowDialogueUI();
        }
    }

    private bool IsPositionWithinRange(Vector3 current, Vector3 target, float tolerance)
    {
        return Vector3.Distance(current, target) <= tolerance;
    }

    private bool IsRotationWithinRange(Vector3 current, Vector3 target, float tolerance)
    {
        Vector3 adjustedCurrent = current;
        if (adjustedCurrent.x > 180f) adjustedCurrent.x -= 360f;
        if (adjustedCurrent.y > 180f) adjustedCurrent.y -= 360f;
        if (adjustedCurrent.z > 180f) adjustedCurrent.z -= 360f;

        return Mathf.Abs(adjustedCurrent.x - target.x) <= tolerance &&
               Mathf.Abs(adjustedCurrent.y - target.y) <= tolerance &&
               Mathf.Abs(adjustedCurrent.z - target.z) <= tolerance;
    }

    void ShowDialogueUI()
    {
        if (dialogueManager != null)
        {
            dialogueManager.StartDialogue();
        }
    }
}
