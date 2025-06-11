using UnityEngine;

public class RainbowLine : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public int segments = 20; // số đoạn con
    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer.positionCount = segments;
        lineRenderer.widthMultiplier = 0.1f;

        // Gradient màu cầu vồng
        Gradient gradient = new Gradient();
        GradientColorKey[] colorKeys = new GradientColorKey[7];
        GradientAlphaKey[] alphaKeys = new GradientAlphaKey[2];

        colorKeys[0].color = Color.red;
        colorKeys[0].time = 0.0f;

        colorKeys[1].color = new Color(1f, 0.5f, 0f); // cam
        colorKeys[1].time = 0.16f;

        colorKeys[2].color = Color.yellow;
        colorKeys[2].time = 0.33f;

        colorKeys[3].color = Color.green;
        colorKeys[3].time = 0.5f;

        colorKeys[4].color = Color.cyan;
        colorKeys[4].time = 0.66f;

        colorKeys[5].color = Color.blue;
        colorKeys[5].time = 0.83f;

        colorKeys[6].color = new Color(0.6f, 0f, 1f); // tím
        colorKeys[6].time = 1.0f;

        alphaKeys[0].alpha = 1.0f;
        alphaKeys[0].time = 0.0f;

        alphaKeys[1].alpha = 1.0f;
        alphaKeys[1].time = 1.0f;

        gradient.SetKeys(colorKeys, alphaKeys);
        lineRenderer.colorGradient = gradient;

        DrawRainbowLine();
    }

    void DrawRainbowLine()
    {
        for (int i = 0; i < segments; i++)
        {
            float t = i / (float)(segments - 1);
            Vector3 point = Vector3.Lerp(pointA.position, pointB.position, t);
            lineRenderer.SetPosition(i, point);
        }
    }
}