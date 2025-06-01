using UnityEngine;
using System.Collections.Generic;

public class ItemClickHandler : MonoBehaviour
{
    private Camera mainCamera;
    private HashSet<GameObject> clickedItems = new HashSet<GameObject>();

    void Start()
    {
        mainCamera = Camera.main; // ���� ī�޶� ��������
    }

    void Update()
    {

        // ���콺 ��Ŭ�� ����
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("��Ŭ����");
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition); // ���콺 ��ġ���� Ray ����
            RaycastHit hit;

            // Ray�� �����ΰ��� �浹�ߴ��� Ȯ��
            if (Physics.Raycast(ray, out hit))
            {
                GameObject clickedObject = hit.collider.gameObject;
                // �浹�� ������Ʈ�� "Item" �±׸� ������ �ִ��� Ȯ��
                if (!clickedItems.Contains(clickedObject) && clickedObject.CompareTag("Item"))
                {
                    Debug.Log($"Raycast hit object: {hit.collider.gameObject.name}");
                    clickedItems.Add(clickedObject); // Ŭ���� ���������� �߰�
                    Debug.Log("Rotating item: " + clickedObject.name);

                    // X�� �������� 90�� ȸ��
                    clickedObject.transform.Rotate(Vector3.right, 90.0f);

                    Debug.Log($"Item {clickedObject.name} rotated to {clickedObject.transform.eulerAngles}");
                }
                else
                {
                    Debug.Log("Item already clicked or not an Item.");
                }

            }
        }
    }
}

