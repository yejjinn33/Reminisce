using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class enemy_cat1 : MonoBehaviour
{
    void Start()
    {
        // 3~5초 간격으로 Y축 회전을 변경하는 코루틴 실행
        StartCoroutine(ChangeYRotation());
    }

    private IEnumerator ChangeYRotation()
    {
        while (true)
        {
            // 3~5초 대기
            float waitTime = Random.Range(3f, 5f);
            yield return new WaitForSeconds(waitTime);

            // Y축 회전을 0도로 설정
            SetYRotation(0f);

            // 0.5초 대기 후 다시 90도로 회전
            yield return new WaitForSeconds(0.5f);
            SetYRotation(90f);
        }
    }

    private void SetYRotation(float angle)
    {
        Vector3 currentRotation = transform.eulerAngles;
        transform.eulerAngles = new Vector3(currentRotation.x, angle, currentRotation.z);
        Debug.Log($"Y rotation set to: {angle}");
    }
}
