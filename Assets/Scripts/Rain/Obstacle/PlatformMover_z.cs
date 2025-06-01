using UnityEngine;

public class PlatformMover_z : MonoBehaviour
{
    public float moveDistance = 5.0f;  // �¿� �̵� �Ÿ�
    public float moveSpeed = 2.0f;     // �̵� �ӵ�

    private Vector3 startPosition;     // ���� ��ġ

    private void Start()
    {
        // ���� ��ġ ����
        startPosition = transform.position;
    }

    private void Update()
    {
        // �¿�� ������ ���
        float offset = Mathf.Sin(Time.time * moveSpeed) * moveDistance;

        // ���ο� ��ġ ����
        transform.position = new Vector3(startPosition.x, startPosition.y, startPosition.z + offset);
    }
}
