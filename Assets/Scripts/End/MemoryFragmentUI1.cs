using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class MemoryFragmentUI1 : MonoBehaviour
{
    public Image[] images; // �̹��� UI �迭 (3�� �̹���)
    public VideoPlayer videoPlayer; // VideoPlayer ������Ʈ
    public VideoClip[] videoClips; // �� �̹����� �Ҵ�� ����
    public GameObject closeButtonPrefab; // ���� ���� ��ư ������
    public Canvas canvas; // ĵ���� ��ü
    public Camera uiCamera; // UI ī�޶� (ĵ�������� ����ϴ� ī�޶�)

    private GameObject currentCloseButton; // ���� Ȱ��ȭ�� ���� ��ư

    private void Start()
    {
        // �� �̹����� Ŭ������ �� �̺�Ʈ ���
        for (int i = 0; i < images.Length; i++)
        {
            int index = i; // Ŭ���� ������ ���ϱ� ���� �ε��� ����
            images[i].GetComponent<Button>().onClick.AddListener(() => OnImageClick(index));
        }
    }

    void OnImageClick(int index)
    {
        // Ŭ���� �̹����� �Ͼ������ Ȯ��
        if (images[index].color == Color.white)
        {
            // �ش� �̹����� �´� ���� ���
            videoPlayer.clip = videoClips[index];
            videoPlayer.Play();

            // ���� ���� ��ư ����
            ShowCloseButton();
        }
    }

    void ShowCloseButton()
    {
        if (currentCloseButton != null)
        {
            // �̹� ��ư�� �����Ǿ� ������ ���� �������� ����
            return;
        }

        // ��ư ����
        currentCloseButton = Instantiate(closeButtonPrefab);
        // ������ ��ư�� RectTransform�� �����ͼ� ��ġ ����
        RectTransform buttonRect = currentCloseButton.GetComponent<RectTransform>();

        // ��ư�� Canvas�� �ڽ����� ����
        buttonRect.SetParent(canvas.transform, false); // false�� �����Ͽ� ���� ��ġ���� ��ġ��

        // ȭ���� �߾� ��ġ�� ���� (ī�޶� ��������)
        Vector3 worldPosition = new Vector3(0, 0, 0); // Z���� 0���� �����Ͽ� ȭ�鿡 ����� ��ġ�� ����
        Vector3 screenPosition = uiCamera.WorldToScreenPoint(worldPosition); // ���� ��ǥ�� ��ũ�� ��ǥ�� ��ȯ

        buttonRect.position = screenPosition;

        // ��ư ũ�� ����
        buttonRect.sizeDelta = new Vector2(200, 60); // ũ�� ���� (������ ����)
        buttonRect.localScale = Vector3.one; // ������ �ʱ�ȭ

        // ��ư�� �̹��� Ȯ�� (���� �� 1�� ����)
        Image buttonImage = currentCloseButton.GetComponent<Image>();
        buttonImage.color = new Color(1f, 1f, 1f, 1f); // ���� ���� 1�� ���

        Button closeButton = currentCloseButton.GetComponent<Button>();
        closeButton.onClick.AddListener(CloseVideo);
        Debug.Log("Close button created!");
    }

    void CloseVideo()
    {
        // ���� ���߱�
        videoPlayer.Stop();

        // ���� ���� ��ư ��Ȱ��ȭ
        Destroy(currentCloseButton);
        currentCloseButton = null;
    }
}
