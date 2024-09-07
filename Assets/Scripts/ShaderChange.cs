using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderChange : MonoBehaviour
{
    public float radius = 4;
    private SpriteRenderer spriteRenderer;
    private Material material;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        material = spriteRenderer.material; // ��ȡ��ǰ����
        transform.localScale = new Vector3 (radius, radius, 1);
        // ��ʼ����
        StartCoroutine(ChangeProperties(0.3f)); // 2��Ĺ���ʱ��
    }

    private IEnumerator ChangeProperties(float duration)
    {
        float elapsedTime = 0f;

        // ��ʼֵ
        float startFadeRadius = 0f;
        float targetFadeRadius = 0.6f;

        Color startColor = new Color(1.0f, 0.145f, 0.0f); // #FF2500
        Color targetColor = new Color(1.0f, 0.843f, 0.0f); // #FFD700

        Vector3 startScale = transform.localScale;
        Vector3 targetScale = startScale * 1.5f; // Ŀ������1.25��

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;

            // ��ֵ����
            float currentFadeRadius = Mathf.Lerp(startFadeRadius, targetFadeRadius, t);
            Color currentColor = Color.Lerp(startColor, targetColor, t);
            Vector3 currentScale = Vector3.Lerp(startScale, targetScale, t);

            // ���²�������
            material.SetFloat("_FadeRadius", currentFadeRadius);
            material.SetColor("_Color", currentColor);

            // ����Sprite��Scale
            transform.localScale = currentScale;

            elapsedTime += Time.deltaTime;
            yield return null; // �ȴ���һ֡
        }

        // ȷ������ֵ����
        material.SetFloat("_FadeRadius", targetFadeRadius);
        material.SetColor("_Color", targetColor);
        transform.localScale = targetScale; // ȷ����������
                                            // 

        Destroy(gameObject);
    }

}




