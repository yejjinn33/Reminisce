using UnityEngine;

public class ItemBehavior : MonoBehaviour
{
    private Rigidbody rb;

    void Start()
    {
        // Rigidbody ��������
        rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            // ó������ ���� ���� ��Ȱ��ȭ
            rb.isKinematic = true;
        }
    }
}
