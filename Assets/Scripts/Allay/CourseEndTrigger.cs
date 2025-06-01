using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourseEndTrigger : MonoBehaviour
{
    public bool hasCompletedCourse = false;  // �ڽ� �Ϸ� ���θ� Ȯ���ϴ� ����

    private void OnTriggerEnter(Collider other)
    {

        // �÷��̾ �ڽ��� ó�� ���� ���� ����
        if (!hasCompletedCourse && other.CompareTag("Player"))
        {
            hasCompletedCourse = true;  // �ڽ� �Ϸ� ó��
            GameUIManager.Instance.CompleteCurrentCourse(); // ���� �ڽ��� ����
        }
    }
}
