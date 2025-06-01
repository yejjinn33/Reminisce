using UnityEngine;

public class Smooth_follow : MonoBehaviour
{
    public Transform target; // ����� Transform
    public float distance = 15.0f; // ī�޶�� ����� ���� �Ÿ�
    public float height = 3.0f; // ī�޶� ����
    public float heightDamping = 2.0f; // ���� ��ȭ �ӵ�
    public float rotationDamping = 3.0f; // ȸ�� ��ȭ �ӵ�
    public float lookAtOffsetY = 1.0f; // ����� �Ӹ� ���� �ٶ󺸴� Y�� ������
    public float additionalTilt = 15.0f; // ī�޶� ���� �ٶ󺸴� �߰� ����
    public DialogueManager dialogueManager; // DialogueManager ���� �߰�

    private bool hasReachedBack = false; // �ĸ鿡 �����ߴ��� ����

    void LateUpdate()
    {
        if (!target) return;

        // ��ǥ ȸ�� ������ ���� ���
        float wantedRotationAngle = target.eulerAngles.y;
        float wantedHeight = target.position.y + height;
        float currentRotationAngle = transform.eulerAngles.y;
        float currentHeight = transform.position.y;

        // �ε巯�� ȸ���� ���� ��ȭ ����
        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

        // ȸ�� ����
        Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        // ī�޶� ��ġ ���
        transform.position = target.position;
        transform.position -= currentRotation * Vector3.forward * distance;

        // ���� ����
        Vector3 temp_position = transform.position;
        temp_position.y = currentHeight;
        transform.position = temp_position;

        // LookAt ���� ��� (����� �Ӹ� �� �Ǵ� �ణ ����)
        Vector3 lookAtPosition = target.position;
        lookAtPosition.y += lookAtOffsetY; // ����� �Ӹ� ���� �ణ �̵�

        // LookAt �������� �ٶ󺸵� �߰��� �������� ����̱�
        Quaternion lookAtRotation = Quaternion.LookRotation(lookAtPosition - transform.position, Vector3.up);
        transform.rotation = lookAtRotation * Quaternion.Euler(-additionalTilt, 0, 0); // ���� �����

        // ī�޶� ������� �ĸ鿡 ������� üũ
        float dotProduct = Vector3.Dot(transform.forward, -target.forward); // ī�޶� ����� �ĸ��� ���ϴ��� Ȯ��
        if (dotProduct < -0.98f && !hasReachedBack)  // ī�޶� ������� �ĸ鿡 ��������� ��
        {
            hasReachedBack = true;
            ShowDialogueUI(); // �ڸ� UI�� ǥ��
        }
    }

    void ShowDialogueUI()
    {
        // �ڸ� UI Ȱ��ȭ
        if (dialogueManager != null)
        {
            dialogueManager.StartDialogue(); // DialogueManager�� StartDialogue �Լ� ȣ��
        }
    }
}
