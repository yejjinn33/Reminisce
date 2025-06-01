using UnityEngine;
using System.Collections;

public class PlatformScaler_2 : MonoBehaviour
{
    public float scaleSpeed = 0.5f;    // ũ�� ��ȭ �ӵ�
    public float waitTime = 1.0f;      // ���� ũ�⿡�� ��� �ð�

    private Vector3 initialScale;      // �ʱ� ������ ����
    private bool isWaiting = false;    // ��� ���� Ȯ��

    private void Start()
    {
        // �ʱ� ������ ����
        initialScale = transform.localScale;

        // Coroutine ����
        StartCoroutine(ScalePlatform());
    }

    private IEnumerator ScalePlatform()
    {
        while (true)
        {
            // �������� ����
            float elapsed = 0f;
            while (elapsed < 1f / scaleSpeed)
            {
                float scaleFactor = Mathf.Lerp(0f, 1f, elapsed * scaleSpeed);
                transform.localScale = initialScale * scaleFactor;
                elapsed += Time.deltaTime;
                yield return null;
            }

            // �������� ���� ũ��� ��
            transform.localScale = initialScale;

            // 1�� ���
            isWaiting = true;
            yield return new WaitForSeconds(waitTime);
            isWaiting = false;

            // �������� ����
            elapsed = 0f;
            while (elapsed < 1f / scaleSpeed)
            {
                float scaleFactor = Mathf.Lerp(1f, 0f, elapsed * scaleSpeed);
                transform.localScale = initialScale * scaleFactor;
                elapsed += Time.deltaTime;
                yield return null;
            }

            // �������� 0���� ��
            transform.localScale = Vector3.zero;
        }
    }
}
