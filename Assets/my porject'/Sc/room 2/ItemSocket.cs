using UnityEngine;

public class ItemSocket : MonoBehaviour
{
    [Header("Socket Settings")]
    [Tooltip("ชื่อ ID ของไอเทมที่ถูกต้องสำหรับแท่นนี้")]
    public string requiredItemID;

    [Header("Status")]
    public bool isCorrect = false;

    [Header("Manager Reference")]
    public PuzzleManager puzzleManager;

    private void OnTriggerEnter(Collider other)
    {
        // ตรวจสอบว่าวัตถุที่มาชนมีคอมโพเนนต์ ItemInfo หรือไม่
        ItemInfo item = other.GetComponent<ItemInfo>();
        if (item != null)
        {
            if (item.itemID == requiredItemID)
            {
                isCorrect = true;
                Debug.Log($"[Socket] วางไอเทม {item.itemID} ถูกต้อง!");
            }
            else
            {
                isCorrect = false;
                Debug.Log($"[Socket] วางไอเทมผิดชนิด!");
            }

            // แจ้งเตือนไปยัง PuzzleManager ให้ตรวจสอบสถานะรวม
            puzzleManager.CheckPuzzleState();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        ItemInfo item = other.GetComponent<ItemInfo>();
        if (item != null)
        {
            // เมื่อยกของออกจากแท่น ให้ยกเลิกสถานะถูกต้อง
            isCorrect = false;
            puzzleManager.CheckPuzzleState();
        }
    }
}