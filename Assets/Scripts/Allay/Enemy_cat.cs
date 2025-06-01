using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemy_cat : MonoBehaviour
{
    public Transform player; // 플레이어(내 고양이)의 Transform
    private NavMeshAgent agent; // NavMeshAgent 컴포넌트

    private Vector3 lastPlayerPosition; // 이전 플레이어 위치 저장

    public float normalSpeed = 20.0f; // 기본 속도

    public float maxSpeed = 40.0f;    // 최대 속도
    public float distanceThreshold = 20.0f; // 속도 증가 시작 거리

    void Start()
    {
        // NavMeshAgent 가져오기
        agent = GetComponent<NavMeshAgent>();

        // NavMeshAgent 속성 설정
        agent.speed = normalSpeed;   // 초기 속도

        agent.angularSpeed = 600.0f; // 회전 속도
        agent.stoppingDistance = 0.1f; // 플레이어와의 최소 거리

        agent.radius = 0.5f; // 길고양이의 충돌 반경
        agent.avoidancePriority = 50; // 회피 우선 순위
        agent.obstacleAvoidanceType = ObstacleAvoidanceType.HighQualityObstacleAvoidance; // 높은 품질의 회피 설정
    }

    void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            // 고양이가 느려져도 길고양이의 속도는 일정하게 유지
            float targetSpeed = normalSpeed; // 기본 속도 유지

            // 거리가 멀면 속도 증가
            if (distanceToPlayer > distanceThreshold)
            {
                targetSpeed = Mathf.Lerp(normalSpeed, maxSpeed, (distanceToPlayer - distanceThreshold) / distanceThreshold);
            }
            
            // 길고양이의 속도 업데이트 (고양이가 느려져도 길고양이는 상관없음)
            agent.speed = targetSpeed;

            Debug.Log(agent.speed);

            // 플레이어 위치가 크게 변했을 때만 SetDestination 호출
            if (Vector3.Distance(player.position, lastPlayerPosition) > 0.1f)
            {
                agent.SetDestination(player.position);
                lastPlayerPosition = player.position;
            }
        }
    }
}

