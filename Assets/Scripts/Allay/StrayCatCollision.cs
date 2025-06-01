using UnityEngine;

// 고양이가 길고양이와 부딪혔을 때 OnCollisionWithStrayCat()을 호출하는 코드 예시
public class StrayCatCollision : MonoBehaviour
{
    public GameManager gameManager;

    private void OnTriggerEnter(Collider other)
    {
        // 길고양이와 부딪혔을 때
        if (other.CompareTag("StrayCat"))
        {
            // GameManager의 OnCatCaught 호출
            gameManager.OnCatCaught();

        }
    }
}
