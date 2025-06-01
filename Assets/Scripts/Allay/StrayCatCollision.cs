using UnityEngine;

// ����̰� �����̿� �ε����� �� OnCollisionWithStrayCat()�� ȣ���ϴ� �ڵ� ����
public class StrayCatCollision : MonoBehaviour
{
    public GameManager gameManager;

    private void OnTriggerEnter(Collider other)
    {
        // �����̿� �ε����� ��
        if (other.CompareTag("StrayCat"))
        {
            // GameManager�� OnCatCaught ȣ��
            gameManager.OnCatCaught();

        }
    }
}
