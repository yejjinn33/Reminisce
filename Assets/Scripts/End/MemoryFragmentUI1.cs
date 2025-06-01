using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class MemoryFragmentUI1 : MonoBehaviour
{
    public Image[] images; // 이미지 UI 배열 (3개 이미지)
    public VideoPlayer videoPlayer; // VideoPlayer 컴포넌트
    public VideoClip[] videoClips; // 각 이미지에 할당될 영상
    public GameObject closeButtonPrefab; // 영상 끄는 버튼 프리팹
    public Canvas canvas; // 캔버스 객체
    public Camera uiCamera; // UI 카메라 (캔버스에서 사용하는 카메라)

    private GameObject currentCloseButton; // 현재 활성화된 끄는 버튼

    private void Start()
    {
        // 각 이미지를 클릭했을 때 이벤트 등록
        for (int i = 0; i < images.Length; i++)
        {
            int index = i; // 클로저 문제를 피하기 위한 인덱스 저장
            images[i].GetComponent<Button>().onClick.AddListener(() => OnImageClick(index));
        }
    }

    void OnImageClick(int index)
    {
        // 클릭한 이미지가 하얀색인지 확인
        if (images[index].color == Color.white)
        {
            // 해당 이미지에 맞는 영상 재생
            videoPlayer.clip = videoClips[index];
            videoPlayer.Play();

            // 영상 끄는 버튼 생성
            ShowCloseButton();
        }
    }

    void ShowCloseButton()
    {
        if (currentCloseButton != null)
        {
            // 이미 버튼이 생성되어 있으면 새로 생성하지 않음
            return;
        }

        // 버튼 생성
        currentCloseButton = Instantiate(closeButtonPrefab);
        // 생성된 버튼의 RectTransform을 가져와서 위치 설정
        RectTransform buttonRect = currentCloseButton.GetComponent<RectTransform>();

        // 버튼을 Canvas의 자식으로 설정
        buttonRect.SetParent(canvas.transform, false); // false로 설정하여 원래 위치에서 배치됨

        // 화면의 중앙 위치로 설정 (카메라를 기준으로)
        Vector3 worldPosition = new Vector3(0, 0, 0); // Z값을 0으로 설정하여 화면에 가까운 위치로 설정
        Vector3 screenPosition = uiCamera.WorldToScreenPoint(worldPosition); // 월드 좌표를 스크린 좌표로 변환

        buttonRect.position = screenPosition;

        // 버튼 크기 설정
        buttonRect.sizeDelta = new Vector2(200, 60); // 크기 설정 (적당히 설정)
        buttonRect.localScale = Vector3.one; // 스케일 초기화

        // 버튼의 이미지 확인 (알파 값 1로 설정)
        Image buttonImage = currentCloseButton.GetComponent<Image>();
        buttonImage.color = new Color(1f, 1f, 1f, 1f); // 알파 값이 1인 흰색

        Button closeButton = currentCloseButton.GetComponent<Button>();
        closeButton.onClick.AddListener(CloseVideo);
        Debug.Log("Close button created!");
    }

    void CloseVideo()
    {
        // 영상 멈추기
        videoPlayer.Stop();

        // 영상 끄는 버튼 비활성화
        Destroy(currentCloseButton);
        currentCloseButton = null;
    }
}
