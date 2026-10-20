using UnityEngine;

public class GemSocket : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("พิมพ์ Tag ของ Gem ที่ถูกต้องสำหรับแท่นนี้ เช่น RedGem, GreenGem, BlueGem")]
    public string correctGemTag;

    [Tooltip("ลาก GameObject ที่มี GemRoomManager มาใส่ช่องนี้")]
    public GemRoomManager roomManager;

    [Header("Status (ไม่ต้องติ๊ก)")]
    public bool isCorrect = false;

    private void OnTriggerEnter(Collider other)
    {
        // ตรวจสอบว่าวัตถุที่มาชนมี Tag ตรงกับที่กำหนดไว้หรือไม่
        if (other.CompareTag(correctGemTag))
        {
            isCorrect = true;
            Debug.Log($"[GemSocket] วาง Gem สีถูกต้องที่แท่น {gameObject.name}");

            if (roomManager != null) roomManager.CheckPuzzle();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // ถ้ายก Gem ที่ถูกต้องออกไป
        if (other.CompareTag(correctGemTag))
        {
            isCorrect = false;
            Debug.Log($"[GemSocket] ยก Gem ออกจากแท่น {gameObject.name}");

            if (roomManager != null) roomManager.CheckPuzzle();
        }
    }
}