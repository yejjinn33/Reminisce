using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class video : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Video Player ������Ʈ
    public string nextSceneName;    // ���� �� �̸�
    public GameObject normalUI;     // ���� UI
    private bool isPlaying = false; // ������ ��� ���� üũ

    void Start()
    {
        Camera mainCamera = Camera.main;  // ���� ī�޶� ��������

        // ī�޶��� near clip plane ��ġ�� Ȯ��
        float nearClip = mainCamera.nearClipPlane;

        // VideoPlayer�� �ִ� ������Ʈ ��������
        GameObject videoObject = videoPlayer.gameObject;

        // ������ ������Ʈ�� ��ġ�� ī�޶��� near clip plane ���� ���� ����
        // ī�޶��� ��ġ + ī�޶��� forward ���� ���Ϳ� nearClip ���� ���ؼ� ��ġ ����
        videoObject.transform.position = mainCamera.transform.position + mainCamera.transform.forward * nearClip;

        // ������ ������Ʈ�� ī�޶��� �տ� �ֵ��� �ణ�� �������� �߰�
        videoObject.transform.position += mainCamera.transform.forward * 0.1f; // ����: 0.1f��ŭ ī�޶� �������� ������ �߰�
    }


    public void OnVideoStart()
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

    void OnVideoEnd(VideoPlayer vp)
    {
        // ������ ���� �� �̺�Ʈ
        SceneManager.LoadScene(nextSceneName); // ���� �� �ε�
    }
}
