using UnityEngine;

public class PlatformScaler_1 : MonoBehaviour
{
    public float minScaleZ = 5.0f;     // Z���� �ּ� ũ��
    public float maxScaleZ = 30.0f;    // Z���� �ִ� ũ��
    public float scaleSpeed = 5.0f;    // ũ�� ��ȭ �ӵ�

    private Vector3 initialScale;      // �ʱ� ������ ����

    private void Start()
    {
        // �ʱ� ������ ����
        initialScale = transform.localScale;
    }

    private void Update()
    {
        // Z ũ�� ���� (Mathf.PingPong ���)
        float newScaleZ = Mathf.PingPong(Time.time * scaleSpeed, maxScaleZ - minScaleZ) + minScaleZ;

        // ������ ������Ʈ
        transform.localScale = new Vector3(initialScale.x, initialScale.y, newScaleZ);
    }
}
