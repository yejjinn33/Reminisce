using UnityEngine;

public class PlatformScaler_1 : MonoBehaviour
{
    public float minScaleZ = 5.0f;     // Z축의 최소 크기
    public float maxScaleZ = 30.0f;    // Z축의 최대 크기
    public float scaleSpeed = 5.0f;    // 크기 변화 속도

    private Vector3 initialScale;      // 초기 스케일 저장

    private void Start()
    {
        // 초기 스케일 저장
        initialScale = transform.localScale;
    }

    private void Update()
    {
        // Z 크기 변경 (Mathf.PingPong 사용)
        float newScaleZ = Mathf.PingPong(Time.time * scaleSpeed, maxScaleZ - minScaleZ) + minScaleZ;

        // 스케일 업데이트
        transform.localScale = new Vector3(initialScale.x, initialScale.y, newScaleZ);
    }
}
