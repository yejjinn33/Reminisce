using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallenter : MonoBehaviour
{
    public DialogueManager dialogueManager; // DialogueManager 참조
    public GameObject gameui;
    public GameObject storyui;
    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        // 이미 트리거가 발생했다면 다시 실행되지 않도록
        if (hasTriggered) return;

        if (other.CompareTag("Player"))
        {
            hasTriggered = true;
            Debug.Log("발판을 처음 밟았습니다.");
            gameui.SetActive(true);
            storyui.SetActive(false);

            if (dialogueManager != null)
            {
                Debug.Log("다이올로그 호출");
                dialogueManager.StartDialogue();
            }
            else
            {
                Debug.LogWarning("DialogueManager가 설정되지 않았습니다!");
            }
        }
    }
}

