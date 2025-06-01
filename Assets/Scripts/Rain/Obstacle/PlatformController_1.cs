using System.Collections;
using UnityEngine;

public class PlatformController_1 : MonoBehaviour
{
    public float disappearTime = 8.0f; // 사라지는 시간
    public float reappearTime = 8.0f;  // 다시 나타나는 시간

    private Renderer platformRenderer;
    private Collider platformCollider;

    private void Start()
    {
        // Renderer와 Collider 가져오기
        platformRenderer = GetComponent<Renderer>();
        platformCollider = GetComponent<Collider>();

        // Coroutine 시작
        StartCoroutine(PlatformCycle());
    }

    IEnumerator PlatformCycle()
    {
        while (true)
        {
            // 발판 비활성화 (Renderer와 Collider 끄기)
            platformRenderer.enabled = false;
            platformCollider.enabled = false;
            Debug.Log("Platform disappeared");
            yield return new WaitForSeconds(disappearTime);

            // 발판 활성화 (Renderer와 Collider 켜기)
            platformRenderer.enabled = true;
            platformCollider.enabled = true;
            Debug.Log("Platform reappeared");
            yield return new WaitForSeconds(reappearTime);
        }
    }
}
