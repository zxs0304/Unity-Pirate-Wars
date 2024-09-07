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
        material = spriteRenderer.material; // 获取当前材质
        transform.localScale = new Vector3 (radius, radius, 1);
        // 开始渐变
        StartCoroutine(ChangeProperties(0.3f)); // 2秒的过渡时间
    }

    private IEnumerator ChangeProperties(float duration)
    {
        float elapsedTime = 0f;

        // 初始值
        float startFadeRadius = 0f;
        float targetFadeRadius = 0.6f;

        Color startColor = new Color(1.0f, 0.145f, 0.0f); // #FF2500
        Color targetColor = new Color(1.0f, 0.843f, 0.0f); // #FFD700

        Vector3 startScale = transform.localScale;
        Vector3 targetScale = startScale * 1.5f; // 目标缩放1.25倍

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;

            // 插值计算
            float currentFadeRadius = Mathf.Lerp(startFadeRadius, targetFadeRadius, t);
            Color currentColor = Color.Lerp(startColor, targetColor, t);
            Vector3 currentScale = Vector3.Lerp(startScale, targetScale, t);

            // 更新材质属性
            material.SetFloat("_FadeRadius", currentFadeRadius);
            material.SetColor("_Color", currentColor);

            // 更新Sprite的Scale
            transform.localScale = currentScale;

            elapsedTime += Time.deltaTime;
            yield return null; // 等待下一帧
        }

        // 确保最终值设置
        material.SetFloat("_FadeRadius", targetFadeRadius);
        material.SetColor("_Color", targetColor);
        transform.localScale = targetScale; // 确保最终缩放
                                            // 

        Destroy(gameObject);
    }

}




