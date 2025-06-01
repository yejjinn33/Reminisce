using UnityEngine;

public class PlatformMover_z : MonoBehaviour
{
    public float moveDistance = 5.0f;  // 좌우 이동 거리
    public float moveSpeed = 2.0f;     // 이동 속도

    private Vector3 startPosition;     // 시작 위치

    private void Start()
    {
        // 시작 위치 저장
        startPosition = transform.position;
    }

    private void Update()
    {
        // 좌우로 움직임 계산
        float offset = Mathf.Sin(Time.time * moveSpeed) * moveDistance;

        // 새로운 위치 설정
        transform.position = new Vector3(startPosition.x, startPosition.y, startPosition.z + offset);
    }
}
