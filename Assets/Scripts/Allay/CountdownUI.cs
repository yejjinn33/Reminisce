using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountdownUI : MonoBehaviour
{
    public TextMeshProUGUI countdownText; // ī��Ʈ�ٿ� ���ڸ� ǥ���� Text UI ���
    public TextMeshProUGUI noticeText;

    public void UpdateCountdownText(string text)
    {
        countdownText.text = text;  // �ؽ�Ʈ UI ������Ʈ
    }

    public void HideCountdownUI()
    {
        countdownText.gameObject.SetActive(false); // ī��Ʈ�ٿ� UI ��Ȱ��ȭ
        noticeText.gameObject.SetActive(false);
    }

    public void ShowCountdownUI()
    {
        countdownText.gameObject.SetActive(true); // ī��Ʈ�ٿ� UI Ȱ��ȭ
        noticeText.gameObject.SetActive(true);


    }
}
