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
            Debug.LogError("TimingManager�� �����ϴ�!");
        }
    }

    void Update()
    {
        // J ��ư�� ������ ���� ���� üũ�� ����
        if (Input.GetKeyDown(KeyCode.J))
        {
            Debug.Log("J key pressed");
            // ���� üũ
            theTimingManager.CheckTiming();
        }
    }
}
