using UnityEngine;

public class EmissionControl : MonoBehaviour
{
    private Material material;
    private Color baseColor; // �⺻ ����
    private float emissionIntensity = 1.0f; // �ʱ� Emission ����
    private bool isIncreasing = true; // ���� ���� ����
    private float blinkSpeed = 4.0f; // ��¦�̴� �ӵ�

    void Start()
    {
        // Renderer���� Material ��������
        material = GetComponent<Renderer>().material;

        // �⺻ ���� ���� (���� Material�� Emission ����)
        baseColor = material.GetColor("_EmissionColor");
    }

    void Update()
    {
        // Emission ���� ����
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

        // Emission ���� ������Ʈ (�⺻ ���� ������ ����)
        Color emissionColor = baseColor * Mathf.LinearToGammaSpace(emissionIntensity);
        material.SetColor("_EmissionColor", emissionColor);
    }
}
