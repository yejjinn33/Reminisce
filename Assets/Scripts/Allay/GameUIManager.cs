using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameUIManager : MonoBehaviour
{
    public static GameUIManager Instance; // �̱��� ����

    public GameObject[] courseUIs; // �ڽ��� UI �迭
    private int currentCourse = 0; // ���� �ڽ� ��ȣ
    public CountdownUI countdownUI;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RestartGameFromFirstCourse()
    {
        currentCourse = 0; // �ڽ� �ʱ�ȭ
        ShowCurrentCourseUI(); // 1�ڽ� UI Ȱ��ȭ
        Debug.Log("������ ó������ �ٽ� ���۵˴ϴ�.");
    }



    // ���� �ڽ� UI�� ǥ��
    public void ShowCurrentCourseUI()
    {
        HideAllCourseUIs(); // ���� UI ��Ȱ��ȭ
        if (currentCourse < courseUIs.Length)
        {
            courseUIs[currentCourse].SetActive(true); // ���� �ڽ� UI Ȱ��ȭ
        }
    }

    // ��� UI ��Ȱ��ȭ
    public void HideAllCourseUIs()
    {
        foreach (GameObject ui in courseUIs)
        {
            ui.SetActive(false);
        }
    }

    // ���� �ڽ��� ����
    public void CompleteCurrentCourse()
    {
        if (currentCourse < courseUIs.Length - 1)
        {
            currentCourse++;
            ShowCurrentCourseUI();
        }
        else
        {
            Debug.Log("��� �ڽ��� �Ϸ��߽��ϴ�!");
        }
    }

    // ī��Ʈ�ٿ� �ڷ�ƾ�� �ܺο��� ȣ��
    public void StartCountdownExternally()
    {
        StartCoroutine(StartCountdownWithReset());
    }

    // ī��Ʈ�ٿ� �� �ʱ�ȭ
    private IEnumerator StartCountdownWithReset()
    {
        // ī��Ʈ�ٿ� UI ǥ��
        if (countdownUI != null)
        {
            countdownUI.ShowCountdownUI();
        }

        for (int i = 3; i > 0; i--)
        {
            if (countdownUI != null)
            {
                countdownUI.UpdateCountdownText(i.ToString());  // ���� �ؽ�Ʈ ������Ʈ
            }
            yield return new WaitForSecondsRealtime(1); // ���� ���¿��� ī��Ʈ�ٿ� ����
        }

        // ���� �簳
        Time.timeScale = 1f;

        // �ڽ� �ʱ�ȭ
        RestartGameFromFirstCourse();

        // ī��Ʈ�ٿ� UI �����
        if (countdownUI != null)
        {
            countdownUI.HideCountdownUI();
        }

        Debug.Log("���� ����!");
    }


}

