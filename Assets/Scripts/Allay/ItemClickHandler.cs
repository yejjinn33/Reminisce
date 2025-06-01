using UnityEngine;
using System.Collections.Generic;

public class ItemClickHandler : MonoBehaviour
{
    private Camera mainCamera;
    private HashSet<GameObject> clickedItems = new HashSet<GameObject>();

    void Start()
    {
        mainCamera = Camera.main; // 메인 카메라 가져오기
    }

    void Update()
    {

        // 마우스 좌클릭 감지
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("좌클릭됨");
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition); // 마우스 위치에서 Ray 생성
            RaycastHit hit;

            // Ray가 무엇인가와 충돌했는지 확인
            if (Physics.Raycast(ray, out hit))
            {
                GameObject clickedObject = hit.collider.gameObject;
                // 충돌한 오브젝트가 "Item" 태그를 가지고 있는지 확인
                if (!clickedItems.Contains(clickedObject) && clickedObject.CompareTag("Item"))
                {
                    Debug.Log($"Raycast hit object: {hit.collider.gameObject.name}");
                    clickedItems.Add(clickedObject); // 클릭된 아이템으로 추가
                    Debug.Log("Rotating item: " + clickedObject.name);

                    // X축 기준으로 90도 회전
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

