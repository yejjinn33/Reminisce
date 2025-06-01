using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smooth_follow3: MonoBehaviour
{
    public Transform target; // 고양이 Transform
    public float distance = 12.0f; // 카메라와 고양이 간의 거리
    public float height = 5.0f; // 카메라 높이
    public float heightDamping = 2.0f; // 높이 변화 속도
    public float rotationDamping = 3.0f; // 회전 변화 속도
    public float lookAtOffsetY = 10.0f; // 고양이 머리 위를 바라보는 Y축 오프셋
    public float additionalTilt = -30.0f; // 카메라가 위를 바라보는 추가 각도

    public DialogueManager dialogueManager;

    private bool hasActivatedUI = false;
    private bool hasReachedBack = false;

    void LateUpdate()
    {
        if (!target) return;

        // 목표 회전 각도와 높이 계산
        float wantedRotationAngle = target.eulerAngles.y;
        float wantedHeight = target.position.y + height;
        float currentRotationAngle = transform.eulerAngles.y;
        float currentHeight = transform.position.y;

        // 부드러운 회전과 높이 변화 적용
        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

        // 회전 적용
        Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        // 카메라 위치 계산
        transform.position = target.position;
        transform.position -= currentRotation * Vector3.forward * distance;

        // 높이 설정
        Vector3 temp_position = transform.position;
        temp_position.y = currentHeight;
        transform.position = temp_position;

        // LookAt 지점 계산 (고양이 머리 위 또는 약간 앞쪽)
        Vector3 lookAtPosition = target.position;
        lookAtPosition.y += lookAtOffsetY; // 고양이 머리 위로 약간 이동

        // LookAt 지점으로 바라보되 추가로 위쪽으로 기울이기
        Quaternion lookAtRotation = Quaternion.LookRotation(lookAtPosition - transform.position, Vector3.up);
        transform.rotation = lookAtRotation * Quaternion.Euler(-additionalTilt, 0, 0); // 위로 기울임

        // 카메라가 고양이의 후면에 가까운지 체크
        float dotProduct = Vector3.Dot(transform.forward, -target.forward); // 카메라가 고양이 후면을 향하는지 확인
        if (dotProduct < -0.98f && !hasReachedBack)  // 카메라가 고양이의 후면에 가까워졌을 때
        {
            hasReachedBack = true;
            Debug.Log("후면임");
            ShowDialogueUI(); // 자막 UI를 표시
        }
    }

    void ShowDialogueUI()
    {
        if (dialogueManager != null)
        {
            dialogueManager.StartDialogue();
        }
        else
        {
            Debug.LogError("DialogueManager가 초기화되지 않았습니다.");
        }
    }
}
