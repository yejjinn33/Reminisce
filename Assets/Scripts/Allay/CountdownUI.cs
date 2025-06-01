using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountdownUI : MonoBehaviour
{
    public TextMeshProUGUI countdownText; // 카운트다운 숫자를 표시할 Text UI 요소
    public TextMeshProUGUI noticeText;

    public void UpdateCountdownText(string text)
    {
        countdownText.text = text;  // 텍스트 UI 업데이트
    }

    public void HideCountdownUI()
    {
        countdownText.gameObject.SetActive(false); // 카운트다운 UI 비활성화
        noticeText.gameObject.SetActive(false);
    }

    public void ShowCountdownUI()
    {
        countdownText.gameObject.SetActive(true); // 카운트다운 UI 활성화
        noticeText.gameObject.SetActive(true);


    }
}
