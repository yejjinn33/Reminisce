using UnityEngine;
using TMPro; // TextMeshPro 네임스페이스 추가

public class DialogueManager2 : MonoBehaviour
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

    public event System.Action OnDialogueFinished; // 대화가 끝났을 때 호출될 이벤트
    public bool isDialogueFinished = false;
    private bool isDialogueActive = false;  // 대화 활성화 상태
    public GameObject enterwall;


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
        // Enter 키를 눌렀을 때 대화가 끝났고 게임이 진행 중일 때만
        if (isDialogueActive && Input.GetKeyDown(KeyCode.Return))
        {
            // 대화가 끝난 상태이고 게임을 시작할 수 있을 때
            if (isDialogueFinished)
            {
                EndDialogue(); // 대화 종료 후, 게임 시작
            }
            else
            {
                NextDialogue();
            }
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

        // 마지막 대화일 때만 이벤트 호출(isDialogueFinished가 false일 경우만)
        if (currentIndex == dialogues.Length - 1 && !isDialogueFinished)  // 마지막 대사에서만 이벤트 발생
        {
            isDialogueFinished = true; // 대화가 끝났음을 표시
            Time.timeScale = 1;
            enterwall.SetActive(false);
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
