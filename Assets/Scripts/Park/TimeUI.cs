using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeUI : MonoBehaviour
{
    [SerializeField] private TMP_Text secondText;

    public static float timer = 60f; // ���� �ð� (����)

    void Update()
    {
        // Ÿ�̸� ����
        timer -= Time.deltaTime;

        // Ÿ�̸Ӱ� 0���� �۾����� �ʵ��� ����
        if (timer < 0)
        {
            timer = 0;
        }

        // second �ؽ�Ʈ�� ���� ���� �ð����� ������Ʈ (�Ҽ��� ���� ������ ǥ��)
        secondText.text = Mathf.CeilToInt(timer).ToString();
    }

    public static void ResetTimer()
    {
        timer = 60f; // Ÿ�̸� �ʱ�ȭ
    }
}
