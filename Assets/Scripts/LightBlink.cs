using UnityEngine;

public class LightBlink : MonoBehaviour
{
    private Light pointLight;
    private float intensity = 2.0f;
    private bool isIncreasing = true;

    void Start()
    {
        pointLight = GetComponent<Light>();
    }

    void Update()
    {
        if (pointLight == null) return;

        if (isIncreasing)
        {
            intensity += Time.deltaTime * 5.0f; // 증가 속도
            if (intensity >= 8.0f) isIncreasing = false;
        }
        else
        {
            intensity -= Time.deltaTime * 5.0f;
            if (intensity <= 0.5f) isIncreasing = true;
        }

        pointLight.intensity = intensity;
    }
}
