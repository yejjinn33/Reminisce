using UnityEngine;
using System.Collections;  // IEnumerator를 사용하기 위한 네임스페이스 추가
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    public GameObject cat;  // 고양이 오브젝트
    public GameObject initialCatPosition;  // 고양이가 처음 위치할 장소 (리셋 위치)
    public GameObject strayCat; // 길고양이 오브젝트
    public StoryUIManager storyUIManager; // 스토리 UI 관리
    public Camera mainCamera;  // 메인 카메라 오브젝트 

    public CourseEndTrigger[] courseEndTriggers; // 모든 CourseEndTrigger를 배열로 관리

    private Cat_moves1 catMoves; // Cat_moves1 스크립트를 참조

    private void Start()
    {
        // Cat_moves1 스크립트 컴포넌트를 가져옴
        catMoves = cat.GetComponent<Cat_moves1>();
    }

    public void OnCatCaught()  
    {

        if (strayCat != null)
        {
            NavMeshAgent agent = strayCat.GetComponent<NavMeshAgent>();
            if (agent != null)
            {
                agent.ResetPath(); // 현재 경로 초기화
                agent.enabled = false;
            }

            strayCat.transform.position = new Vector3(67.42757f, 0.157974f, 234.8498f);
            strayCat.transform.rotation = Quaternion.Euler(0f, 164.862f, 0f);

            if (agent != null)
            {
                agent.enabled = true;
            }
        }

        

        // 고양이 속도 복원 (속도 감소 후)
        if (catMoves != null)
        {
            catMoves.RestoreSpeed(); // 속도 복원
        }

        // 고양이를 처음 위치로 되돌리기
        cat.transform.position = initialCatPosition.transform.position;
        if (cat != null)
        {
            cat.transform.rotation = Quaternion.Euler(cat.transform.rotation.eulerAngles.x, -177.975f, cat.transform.rotation.eulerAngles.z);
        }

        

        // 카메라 위치와 회전 초기화
        if (mainCamera != null)
        {
            // 카메라의 위치를 설정
            mainCamera.transform.position = new Vector3(73.52478f, 2.738049f, 196.8075f);
            // 카메라의 회전을 설정
            mainCamera.transform.rotation = Quaternion.Euler(-8.456f, 1.816f, 0f);
        }

        // 길고양이와 충돌 시 모든 CourseEndTrigger의 상태 초기화
        foreach (var trigger in courseEndTriggers)
        {
            trigger.hasCompletedCourse = false;
        }


        // 스토리 UI 비활성화
        if (storyUIManager != null)
        {
            storyUIManager.HideStoryUI();
        }

        // 게임 일시 정지
        Time.timeScale = 0f;

        // 게임 정지
        Time.timeScale = 0f;

        if (GameUIManager.Instance != null)
        {
            GameUIManager.Instance.HideAllCourseUIs();
            GameUIManager.Instance.StartCountdownExternally();
        }



    }

}
