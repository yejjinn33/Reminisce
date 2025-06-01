using UnityEngine;
using TMPro; // TextMeshPro 네임스페이스 추가

public class DialogueManager : MonoBehaviour
{
    [System.Serializable]
    public struct Dialogue
    {
        public string speaker;           // 화자 이름 (고양이, 주인)
        public string text;              // 대사 내용
        public Sprite speakerBackground; // 화자에 맞는 배경 이미지
    }

    public TextMeshProUGUI dialogueText;    // 대사를 표시할 TextMeshPro UI
    public TextMeshProUGUI speakerNameText; // 화자 이름을 표시할 TextMeshPro UI
    public GameObject dialogueBackground;   // 대사 배경 UI
    public Dialogue[] dialogues;            // 대사 배열
    private int currentIndex = 0;           // 현재 대사 인덱스
    private UnityEngine.UI.Image backgroundImage; // 배경 이미지 컴포넌트 참조

    public bool isDialogueFinished = false;
    private bool isDialogueActive = false;  // 대화 활성화 상태


    void Start()
    {
        // 자막 UI는 처음에 보이지 않음
        dialogueBackground.SetActive(false);
        dialogueText.gameObject.SetActive(false);
        speakerNameText.gameObject.SetActive(false);

        // 배경 이미지 컴포넌트 가져오기
        backgroundImage = dialogueBackground.GetComponent<UnityEngine.UI.Image>();
    }

    void Update()
    {
        // Enter 키를 눌렀을 때 다음 대사로
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
            // 모든 대사가 끝났을 때 처리
            EndDialogue();
        }
    }

    void DisplayDialogue(int index)
    {
        Dialogue dialogue = dialogues[index];

        // 대사 내용 및 스타일 설정
        speakerNameText.text = dialogue.speaker;
        dialogueText.text = dialogue.text;

        // 화자에 맞는 배경 이미지 설정
        backgroundImage.sprite = dialogue.speakerBackground;

        // 대화 UI 활성화
        dialogueBackground.SetActive(true);
        dialogueText.gameObject.SetActive(true);
        speakerNameText.gameObject.SetActive(true);

        // 게임 시간 멈춤
        Time.timeScale = 0;
    }

    void EndDialogue()
    {
        // 대화 UI 비활성화
        dialogueBackground.SetActive(false);
        dialogueText.gameObject.SetActive(false);
        speakerNameText.gameObject.SetActive(false);


        if (currentIndex == dialogues.Length - 1 && !isDialogueFinished)  // 마지막 대사에서만 이벤트 발생
        {
            isDialogueFinished = true; // 대화가 끝났음을 표시
            Time.timeScale = 1;
        }
        // 대화 비활성화
        isDialogueActive = false;
    }

    // Dialogue 시작 함수
    public void StartDialogue()
    {
        // 첫 대사 표시
        if (dialogues.Length > 0)
        {
            isDialogueActive = true; // 대화 활성화
            isDialogueFinished = false; // 대화 완료 상태 초기화
            DisplayDialogue(currentIndex);
        }
        else
        {
            EndDialogue();
        }
    }
}
