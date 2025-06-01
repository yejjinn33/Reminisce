using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallenter : MonoBehaviour
{
    public DialogueManager dialogueManager; // DialogueManager ����
    public GameObject gameui;
    public GameObject storyui;
    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        // �̹� Ʈ���Ű� �߻��ߴٸ� �ٽ� ������� �ʵ���
        if (hasTriggered) return;

        if (other.CompareTag("Player"))
        {
            hasTriggered = true;
            Debug.Log("������ ó�� ��ҽ��ϴ�.");
            gameui.SetActive(true);
            storyui.SetActive(false);

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

