using UnityEngine;
using System.Collections;  // IEnumerator�� ����ϱ� ���� ���ӽ����̽� �߰�
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    public GameObject cat;  // ����� ������Ʈ
    public GameObject initialCatPosition;  // ����̰� ó�� ��ġ�� ��� (���� ��ġ)
    public GameObject strayCat; // ������ ������Ʈ
    public StoryUIManager storyUIManager; // ���丮 UI ����
    public Camera mainCamera;  // ���� ī�޶� ������Ʈ 

    public CourseEndTrigger[] courseEndTriggers; // ��� CourseEndTrigger�� �迭�� ����

    private Cat_moves1 catMoves; // Cat_moves1 ��ũ��Ʈ�� ����

    private void Start()
    {
        // Cat_moves1 ��ũ��Ʈ ������Ʈ�� ������
        catMoves = cat.GetComponent<Cat_moves1>();
    }

    public void OnCatCaught()  
    {

        if (strayCat != null)
        {
            NavMeshAgent agent = strayCat.GetComponent<NavMeshAgent>();
            if (agent != null)
            {
                agent.ResetPath(); // ���� ��� �ʱ�ȭ
                agent.enabled = false;
            }

            strayCat.transform.position = new Vector3(67.42757f, 0.157974f, 234.8498f);
            strayCat.transform.rotation = Quaternion.Euler(0f, 164.862f, 0f);

            if (agent != null)
            {
                agent.enabled = true;
            }
        }

        

        // ����� �ӵ� ���� (�ӵ� ���� ��)
        if (catMoves != null)
        {
            catMoves.RestoreSpeed(); // �ӵ� ����
        }

        // ����̸� ó�� ��ġ�� �ǵ�����
        cat.transform.position = initialCatPosition.transform.position;
        if (cat != null)
        {
            cat.transform.rotation = Quaternion.Euler(cat.transform.rotation.eulerAngles.x, -177.975f, cat.transform.rotation.eulerAngles.z);
        }

        

        // ī�޶� ��ġ�� ȸ�� �ʱ�ȭ
        if (mainCamera != null)
        {
            // ī�޶��� ��ġ�� ����
            mainCamera.transform.position = new Vector3(73.52478f, 2.738049f, 196.8075f);
            // ī�޶��� ȸ���� ����
            mainCamera.transform.rotation = Quaternion.Euler(-8.456f, 1.816f, 0f);
        }

        // �����̿� �浹 �� ��� CourseEndTrigger�� ���� �ʱ�ȭ
        foreach (var trigger in courseEndTriggers)
        {
            trigger.hasCompletedCourse = false;
        }


        // ���丮 UI ��Ȱ��ȭ
        if (storyUIManager != null)
        {
            storyUIManager.HideStoryUI();
        }

        // ���� �Ͻ� ����
        Time.timeScale = 0f;

        // ���� ����
        Time.timeScale = 0f;

        if (GameUIManager.Instance != null)
        {
            GameUIManager.Instance.HideAllCourseUIs();
            GameUIManager.Instance.StartCountdownExternally();
        }



    }

}
