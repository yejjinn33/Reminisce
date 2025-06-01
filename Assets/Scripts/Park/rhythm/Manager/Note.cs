using UnityEngine;
using UnityEngine.UI;

public class Note : MonoBehaviour
{
    public float noteSpeed = 400.0f;

    private Image noteImage;

    void Start()
    {
        noteImage = GetComponent<Image>();
    }

    void Update()
    {
        transform.localPosition += Vector3.right * noteSpeed * Time.deltaTime;
    }

    public void HideNote()
    {
        if (noteImage != null)
        {
            noteImage.enabled = false; // �̹����� ��Ȱ��ȭ�Ͽ� ����
        }
    }
}
