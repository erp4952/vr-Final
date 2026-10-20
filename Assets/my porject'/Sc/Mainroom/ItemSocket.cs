using UnityEngine;
using UnityEngine.Events;

public class ItemSocket : MonoBehaviour
{
    [Header("Socket Settings")]
    [Tooltip("ID ของวัตถุที่ถูกต้องสำหรับแท่นนี้")]
    public string requiredItemID;

    [Header("Status")]
    public bool isCorrect = false;

    [Header("Event Trigger")]
    [Tooltip("ลากฟังก์ชันตรวจเช็กของ Manager ห้องหรือประตูมาใส่ที่นี่")]
    public UnityEvent onStateChanged;

    private void OnTriggerEnter(Collider other)
    {
        ItemInfo item = other.GetComponent<ItemInfo>();
        if (item != null && item.itemID == requiredItemID)
        {
            isCorrect = true;
            Debug.Log($"[Socket] วางไอเทมถูกต้อง: {item.itemID}");
            onStateChanged?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        ItemInfo item = other.GetComponent<ItemInfo>();
        if (item != null && item.itemID == requiredItemID)
        {
            isCorrect = false;
            Debug.Log($"[Socket] ยกไอเทมออก: {item.itemID}");
            onStateChanged?.Invoke();
        }
    }
}