using UnityEngine;
using System.Collections;

public class PlatformScaler_2 : MonoBehaviour
{
    public float scaleSpeed = 0.5f;    // 크기 변화 속도
    public float waitTime = 1.0f;      // 원래 크기에서 대기 시간

    private Vector3 initialScale;      // 초기 스케일 저장
    private bool isWaiting = false;    // 대기 상태 확인

    private void Start()
    {
        // 초기 스케일 저장
        initialScale = transform.localScale;

        // Coroutine 시작
        StartCoroutine(ScalePlatform());
    }

    private IEnumerator ScalePlatform()
    {
        while (true)
        {
            // 스케일을 줄임
            float elapsed = 0f;
            while (elapsed < 1f / scaleSpeed)
            {
                float scaleFactor = Mathf.Lerp(0f, 1f, elapsed * scaleSpeed);
                transform.localScale = initialScale * scaleFactor;
                elapsed += Time.deltaTime;
                yield return null;
            }

            // 스케일이 원래 크기로 됨
            transform.localScale = initialScale;

            // 1초 대기
            isWaiting = true;
            yield return new WaitForSeconds(waitTime);
            isWaiting = false;

            // 스케일을 줄임
            elapsed = 0f;
            while (elapsed < 1f / scaleSpeed)
            {
                float scaleFactor = Mathf.Lerp(1f, 0f, elapsed * scaleSpeed);
                transform.localScale = initialScale * scaleFactor;
                elapsed += Time.deltaTime;
                yield return null;
            }

            // 스케일이 0으로 됨
            transform.localScale = Vector3.zero;
        }
    }
}
