using System.Collections;
using UnityEngine;

public class PlatformController_1 : MonoBehaviour
{
    public float disappearTime = 8.0f; // ������� �ð�
    public float reappearTime = 8.0f;  // �ٽ� ��Ÿ���� �ð�

    private Renderer platformRenderer;
    private Collider platformCollider;

    private void Start()
    {
        // Renderer�� Collider ��������
        platformRenderer = GetComponent<Renderer>();
        platformCollider = GetComponent<Collider>();

        // Coroutine ����
        StartCoroutine(PlatformCycle());
    }

    IEnumerator PlatformCycle()
    {
        while (true)
        {
            // ���� ��Ȱ��ȭ (Renderer�� Collider ����)
            platformRenderer.enabled = false;
            platformCollider.enabled = false;
            Debug.Log("Platform disappeared");
            yield return new WaitForSeconds(disappearTime);

            // ���� Ȱ��ȭ (Renderer�� Collider �ѱ�)
            platformRenderer.enabled = true;
            platformCollider.enabled = true;
            Debug.Log("Platform reappeared");
            yield return new WaitForSeconds(reappearTime);
        }
    }
}
