using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemy_cat : MonoBehaviour
{
    public Transform player; // �÷��̾�(�� �����)�� Transform
    private NavMeshAgent agent; // NavMeshAgent ������Ʈ

    private Vector3 lastPlayerPosition; // ���� �÷��̾� ��ġ ����

    public float normalSpeed = 20.0f; // �⺻ �ӵ�

    public float maxSpeed = 40.0f;    // �ִ� �ӵ�
    public float distanceThreshold = 20.0f; // �ӵ� ���� ���� �Ÿ�

    void Start()
    {
        // NavMeshAgent ��������
        agent = GetComponent<NavMeshAgent>();

        // NavMeshAgent �Ӽ� ����
        agent.speed = normalSpeed;   // �ʱ� �ӵ�

        agent.angularSpeed = 600.0f; // ȸ�� �ӵ�
        agent.stoppingDistance = 0.1f; // �÷��̾���� �ּ� �Ÿ�

        agent.radius = 0.5f; // �������� �浹 �ݰ�
        agent.avoidancePriority = 50; // ȸ�� �켱 ����
        agent.obstacleAvoidanceType = ObstacleAvoidanceType.HighQualityObstacleAvoidance; // ���� ǰ���� ȸ�� ����
    }

    void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            // ����̰� �������� �������� �ӵ��� �����ϰ� ����
            float targetSpeed = normalSpeed; // �⺻ �ӵ� ����

            // �Ÿ��� �ָ� �ӵ� ����
            if (distanceToPlayer > distanceThreshold)
            {
                targetSpeed = Mathf.Lerp(normalSpeed, maxSpeed, (distanceToPlayer - distanceThreshold) / distanceThreshold);
            }
            
            // �������� �ӵ� ������Ʈ (����̰� �������� �����̴� �������)
            agent.speed = targetSpeed;

            Debug.Log(agent.speed);

            // �÷��̾� ��ġ�� ũ�� ������ ���� SetDestination ȣ��
            if (Vector3.Distance(player.position, lastPlayerPosition) > 0.1f)
            {
                agent.SetDestination(player.position);
                lastPlayerPosition = player.position;
            }
        }
    }
}

