using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    TimingManager theTimingManager;

    void Awake()
    {
        theTimingManager = FindObjectOfType<TimingManager>();
        if (theTimingManager == null)
        {
            Debug.LogError("TimingManager가 없습니다!");
        }
    }

    void Update()
    {
        // J 버튼을 눌렀을 때만 판정 체크를 실행
        if (Input.GetKeyDown(KeyCode.J))
        {
            Debug.Log("J key pressed");
            // 판정 체크
            theTimingManager.CheckTiming();
        }
    }
}
