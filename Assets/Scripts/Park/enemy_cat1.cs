using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class enemy_cat1 : MonoBehaviour
{
    void Start()
    {
        // 3~5�� �������� Y�� ȸ���� �����ϴ� �ڷ�ƾ ����
        StartCoroutine(ChangeYRotation());
    }

    private IEnumerator ChangeYRotation()
    {
        while (true)
        {
            // 3~5�� ���
            float waitTime = Random.Range(3f, 5f);
            yield return new WaitForSeconds(waitTime);

            // Y�� ȸ���� 0���� ����
            SetYRotation(0f);

            // 0.5�� ��� �� �ٽ� 90���� ȸ��
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
