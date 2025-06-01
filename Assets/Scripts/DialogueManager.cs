using UnityEngine;
using TMPro; // TextMeshPro ���ӽ����̽� �߰�

public class DialogueManager : MonoBehaviour
{
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


    void Start()
    {
        // �ڸ� UI�� ó���� ������ ����
        dialogueBackground.SetActive(false);
        dialogueText.gameObject.SetActive(false);
        speakerNameText.gameObject.SetActive(false);

        // ��� �̹��� ������Ʈ ��������
        backgroundImage = dialogueBackground.GetComponent<UnityEngine.UI.Image>();
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

        // ��ȭ UI Ȱ��ȭ
        dialogueBackground.SetActive(true);
        dialogueText.gameObject.SetActive(true);
        speakerNameText.gameObject.SetActive(true);

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
}
