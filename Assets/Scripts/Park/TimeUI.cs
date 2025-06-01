using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeUI : MonoBehaviour
{
    [SerializeField] private TMP_Text secondText;

    public static float timer = 60f; // 남은 시간 (공유)

    void Update()
    {
        // 타이머 감소
        timer -= Time.deltaTime;

        // 타이머가 0보다 작아지지 않도록 제한
        if (timer < 0)
        {
            timer = 0;
        }

        // second 텍스트를 현재 남은 시간으로 업데이트 (소수점 없이 정수로 표시)
        secondText.text = Mathf.CeilToInt(timer).ToString();
    }

    public static void ResetTimer()
    {
        timer = 60f; // 타이머 초기화
    }
}
