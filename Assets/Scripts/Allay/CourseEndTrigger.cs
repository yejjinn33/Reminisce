using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourseEndTrigger : MonoBehaviour
{
    public bool hasCompletedCourse = false;  // 코스 완료 여부를 확인하는 변수

    private void OnTriggerEnter(Collider other)
    {

        // 플레이어가 코스를 처음 밟을 때만 실행
        if (!hasCompletedCourse && other.CompareTag("Player"))
        {
            hasCompletedCourse = true;  // 코스 완료 처리
            GameUIManager.Instance.CompleteCurrentCourse(); // 다음 코스로 진행
        }
    }
}
