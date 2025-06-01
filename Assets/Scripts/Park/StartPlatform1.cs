using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPlatform1 : MonoBehaviour
{
    public DialogueManager2 dialogueManager; // DialogueManager ����
    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        // �̹� Ʈ���Ű� �߻��ߴٸ� �ٽ� ������� �ʵ���
        if (hasTriggered) return;

        if (other.CompareTag("Player"))
        {
            hasTriggered = true;
            Debug.Log("������ ó�� ��ҽ��ϴ�.");

            if (dialogueManager != null)
            {
                Debug.Log("���̿÷α� ȣ��");
                dialogueManager.StartDialogue();
            }
            else
            {
                Debug.LogWarning("DialogueManager�� �������� �ʾҽ��ϴ�!");
            }
        }
    }
}

