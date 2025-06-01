using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameUIManager2 : MonoBehaviour
{
    public static GameUIManager2 Instance; // �̱��� ����

    public CountdownUI countdownUI;
    public RhythmGameUI rhythmGameUI; // ������� UI ����
    public NoteManager noteManager;
    public GameObject end;
    public GameObject pointLight;

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
    public void Afterrhythmgame()
    {
        end.SetActive(true);
        pointLight.SetActive(true);
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
        Time.timeScale = 0f;

        for (int i = 3; i > 0; i--)
        {
            if (countdownUI != null)
            {
                countdownUI.UpdateCountdownText(i.ToString());  // ���� �ؽ�Ʈ ������Ʈ
            }
            yield return new WaitForSecondsRealtime(1); // ���� ���¿��� ī��Ʈ�ٿ� ����
        }



        // ī��Ʈ�ٿ� UI �����
        if (countdownUI != null)
        {
            countdownUI.HideCountdownUI();
        }

        Debug.Log("���� ����!");

        ryhthmGameStart();


    }

    private void ryhthmGameStart()
    {
        // ī��Ʈ�ٿ� ���� �� ������� UI Ȱ��ȭ
        if (rhythmGameUI != null)
        {
            rhythmGameUI.ActivateRhythmGameUI();
        }

        // ���� �簳
        Time.timeScale = 1f;


        // ���� ���� ���� (NoteManager�� �˸�)
        if (noteManager != null)
        {
            Debug.Log("������ӽ���");
            noteManager.StartRhythmGame();
        }
        else
        {
            Debug.LogError("NoteManager�� �ʱ�ȭ���� �ʾҽ��ϴ�! ������ ���۵��� �ʽ��ϴ�.");
        }
    }


}

