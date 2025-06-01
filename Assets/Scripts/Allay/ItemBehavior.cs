using UnityEngine;

public class ItemBehavior : MonoBehaviour
{
    private Rigidbody rb;

    void Start()
    {
        // Rigidbody 가져오기
        rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            // 처음에는 물리 동작 비활성화
            rb.isKinematic = true;
        }
    }
}
