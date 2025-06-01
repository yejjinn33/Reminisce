using UnityEngine;

public class EmissionControl : MonoBehaviour
{
    private Material material;
    private Color baseColor; // 기본 색상
    private float emissionIntensity = 1.0f; // 초기 Emission 강도
    private bool isIncreasing = true; // 강도 증가 여부
    private float blinkSpeed = 4.0f; // 반짝이는 속도

    void Start()
    {
        // Renderer에서 Material 가져오기
        material = GetComponent<Renderer>().material;

        // 기본 색상 설정 (현재 Material의 Emission 색상)
        baseColor = material.GetColor("_EmissionColor");
    }

    void Update()
    {
        // Emission 강도 조정
        if (isIncreasing)
        {
            emissionIntensity += Time.deltaTime * blinkSpeed;
            if (emissionIntensity >= 3.0f) isIncreasing = false;
        }
        else
        {
            emissionIntensity -= Time.deltaTime * blinkSpeed;
            if (emissionIntensity <= 1.0f) isIncreasing = true;
        }

        // Emission 색상 업데이트 (기본 색상에 강도를 곱함)
        Color emissionColor = baseColor * Mathf.LinearToGammaSpace(emissionIntensity);
        material.SetColor("_EmissionColor", emissionColor);
    }
}
