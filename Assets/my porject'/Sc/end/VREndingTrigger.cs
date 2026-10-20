using UnityEngine;
using System.Collections;

public class VREndingTrigger : MonoBehaviour
{
    [Header("UI References")]
    [Tooltip("ลาก Canvas ที่มี Canvas Group มาใส่ช่องนี้")]
    public CanvasGroup fadeCanvasGroup;

    [Header("Settings")]
    [Tooltip("ระยะเวลาที่ใช้เฟดจอดำ (วินาที)")]
    public float fadeDuration = 2.0f;

    [Tooltip("Tag ของผู้เล่น (XR Origin)")]
    public string playerTag = "Player";

    private bool isFading = false;

    private void OnTriggerEnter(Collider other)
    {
        // ตรวจสอบว่าสิ่งที่มาชนคือผู้เล่น และยังไม่ได้เริ่มเฟด
        if (other.CompareTag(playerTag) && !isFading)
        {
            isFading = true;
            StartCoroutine(FadeToBlack());
        }
    }

    private IEnumerator FadeToBlack()
    {
        float elapsedTime = 0f;

        // ค่อยๆ เพิ่มค่า Alpha จาก 0 ไป 1 ตามระยะเวลาที่กำหนด
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            if (fadeCanvasGroup != null)
            {
                fadeCanvasGroup.alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            }
            yield return null; // รอเฟรมถัดไป
        }

        if (fadeCanvasGroup != null)
        {
            fadeCanvasGroup.alpha = 1f;
        }

        Debug.Log("ฉากจบทำงานสมบูรณ์!");

        // (ตัวเลือกเสริม) หยุดเวลาในเกมเมื่อเฟดเสร็จ
        // Time.timeScale = 0; 
    }
}