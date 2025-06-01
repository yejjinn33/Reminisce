using UnityEngine;
using TMPro; // TextMeshPro ���ӽ����̽� �߰�
using UnityEngine.UI;
using System.Collections;


public class DialogueManager3 : MonoBehaviour
{
    public GameObject goingHomeUI; // goinghome UI ������Ʈ ����
    public video videoScript; // video ��ũ��Ʈ ���� �߰�

    [System.Serializable]
    public struct Dialogue
    {
        public string speaker;           // ȭ�� �̸� (�����, ����)
        public string text;              // ��� ����
        public Sprite speakerBackground; // ȭ�ڿ� �´� ��� �̹���
    }

    public TextMeshProUGUI dialogueText;    // ��縦 ǥ���� TextMeshPro UI
    public TextMeshProUGUI speakerNameText; // ȭ�� �̸��� ǥ���� TextMeshPro UI
    public GameObject dialogueBackground;   // ��� ��� UI
    public Dialogue[] dialogues;            // ��� �迭
    private int currentIndex = 0;           // ���� ��� �ε���
    private UnityEngine.UI.Image backgroundImage; // ��� �̹��� ������Ʈ ����
   
    public bool isDialogueFinished = false;
    private bool isDialogueActive = false;  // ��ȭ Ȱ��ȭ ����

    // ��� �̹��� ���� ����
    public Sprite[] canvasBackgroundImages; // ��� �̹�����
    private int backgroundIndex = 0;         // ���� ��� �ε���
    public Image canvasBackgroundImage;     // ĵ���� ��� �̹��� ������Ʈ ����






    void Start()
    {

        // ��� �̹��� ������Ʈ ��������
        backgroundImage = dialogueBackground.GetComponent<UnityEngine.UI.Image>();
  
        StartDialogue();
    }

    void Update()
    {
        // Enter Ű�� ������ �� ���� ����
        if (isDialogueActive && Input.GetKeyDown(KeyCode.Return))
        {
            NextDialogue();
        }
    }

    void NextDialogue()
    {
        if (currentIndex < dialogues.Length - 1)
        {
            currentIndex++;
            DisplayDialogue(currentIndex);
        }
        else
        {
            // ��� ��簡 ������ �� ó��
            EndDialogue();
        }
    }

    void DisplayDialogue(int index)
    {
        Dialogue dialogue = dialogues[index];

        // ��� ���� �� ��Ÿ�� ����
        speakerNameText.text = dialogue.speaker;
        dialogueText.text = dialogue.text;

        // ȭ�ڿ� �´� ��� �̹��� ����
        backgroundImage.sprite = dialogue.speakerBackground;

        // ���� �ð� ����
        Time.timeScale = 0;
    }

    void EndDialogue()
    {
        // ��ȭ UI ��Ȱ��ȭ
        dialogueBackground.SetActive(false);
        dialogueText.gameObject.SetActive(false);
        speakerNameText.gameObject.SetActive(false);


        if (currentIndex == dialogues.Length - 1 && !isDialogueFinished)  // ������ ��翡���� �̺�Ʈ �߻�
        {
            isDialogueFinished = true; // ��ȭ�� �������� ǥ��

            Time.timeScale = 1;
            // ĵ���� ��� ���� �ڷ�ƾ ����
            StartCoroutine(ChangeCanvasBackgroundImages());
        }
        // ��ȭ ��Ȱ��ȭ
        isDialogueActive = false;
    }

    // Dialogue ���� �Լ�
    public void StartDialogue()
    {
        // ù ��� ǥ��
        if (dialogues.Length > 0)
        {
            isDialogueActive = true; // ��ȭ Ȱ��ȭ
            isDialogueFinished = false; // ��ȭ �Ϸ� ���� �ʱ�ȭ
            DisplayDialogue(currentIndex);
        }
        else
        {
            EndDialogue();
        }
    }

    private IEnumerator ChangeCanvasBackgroundImages()
    {
        goingHomeUI.SetActive(true);
        while (backgroundIndex < canvasBackgroundImages.Length)
        {
            // ��� �̹��� ����
            canvasBackgroundImage.sprite = canvasBackgroundImages[backgroundIndex];
            backgroundIndex++;

            // 2�ʸ��� ��� ����
            yield return new WaitForSeconds(2f);
        }

        // ������ �̹��� ǥ�� �� ���߱�
        canvasBackgroundImage.sprite = canvasBackgroundImages[canvasBackgroundImages.Length - 1];

        // ������ �̹����� �ٲ� �� 2�� ��ٸ���, �� �Ŀ� OnVideoStart ȣ��
        yield return new WaitForSeconds(2f);

        // video ��ũ��Ʈ�� OnVideoStart ȣ��
        if (videoScript != null)
        {
            videoScript.OnVideoStart(); // ������ ����
        }

        // goinghome UI ��Ȱ��ȭ
        goingHomeUI.SetActive(false);
    }

 
}
