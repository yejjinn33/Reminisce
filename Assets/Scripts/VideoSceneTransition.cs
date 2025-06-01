using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoSceneTransition : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Video Player ������Ʈ
    public string nextSceneName;    // ���� �� �̸�
    public GameObject normalUI;     // ���� UI
    //public GameObject videoUI;      // ������ ���� UI

    private bool isPlaying = false; // ������ ��� ���� üũ

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isPlaying) // �÷��̾� �±� Ȯ��
        {
            Debug.Log("video");
            isPlaying = true; // ��� ���·� ����
            videoPlayer.gameObject.SetActive(true); // Video Player Ȱ��ȭ
            videoPlayer.Play(); // ������ ���
            if (normalUI != null) normalUI.SetActive(false);
            //if (videoUI != null) videoUI.SetActive(true);


            // ������ ��� �Ϸ� �� �̺�Ʈ ���
            videoPlayer.loopPointReached += OnVideoEnd;
        }
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        // ������ ���� �� �̺�Ʈ
        SceneManager.LoadScene(nextSceneName); // ���� �� �ε�
    }
}
