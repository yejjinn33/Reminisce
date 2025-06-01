using UnityEngine;
using TMPro; // TextMeshPro 네임스페이스 추가
using UnityEngine.UI;
using System.Collections;


public class DialogueManager3 : MonoBehaviour
{
    public GameObject goingHomeUI; // goinghome UI 오브젝트 연결
    public video videoScript; // video 스크립트 참조 추가

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

    // 배경 이미지 변경 관련
    public Sprite[] canvasBackgroundImages; // 배경 이미지들
    private int backgroundIndex = 0;         // 현재 배경 인덱스
    public Image canvasBackgroundImage;     // 캔버스 배경 이미지 컴포넌트 참조






    void Start()
    {

        // 배경 이미지 컴포넌트 가져오기
        backgroundImage = dialogueBackground.GetComponent<UnityEngine.UI.Image>();
  
        StartDialogue();
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
            // 캔버스 배경 변경 코루틴 시작
            StartCoroutine(ChangeCanvasBackgroundImages());
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

    private IEnumerator ChangeCanvasBackgroundImages()
    {
        goingHomeUI.SetActive(true);
        while (backgroundIndex < canvasBackgroundImages.Length)
        {
            // 배경 이미지 변경
            canvasBackgroundImage.sprite = canvasBackgroundImages[backgroundIndex];
            backgroundIndex++;

            // 2초마다 배경 변경
            yield return new WaitForSeconds(2f);
        }

        // 마지막 이미지 표시 후 멈추기
        canvasBackgroundImage.sprite = canvasBackgroundImages[canvasBackgroundImages.Length - 1];

        // 마지막 이미지가 바뀐 후 2초 기다리고, 그 후에 OnVideoStart 호출
        yield return new WaitForSeconds(2f);

        // video 스크립트의 OnVideoStart 호출
        if (videoScript != null)
        {
            videoScript.OnVideoStart(); // 동영상 시작
        }

        // goinghome UI 비활성화
        goingHomeUI.SetActive(false);
    }

 
}
