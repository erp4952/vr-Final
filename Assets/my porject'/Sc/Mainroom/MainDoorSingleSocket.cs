using UnityEngine;
using System.Collections.Generic;

public class MainDoorSingleSocket : MonoBehaviour
{
    [Header("Required Keys")]
    [Tooltip("พิมพ์ ID ของกุญแจทั้ง 3 ดอกที่ต้องการ (เช่น Key_Room1, Key_Room2, Key_Room3)")]
    public List<string> requiredKeyIDs = new List<string> { "Key_Room1", "Key_Room2", "Key_Room3" };

    [Header("Door Settings")]
    [Tooltip("ลากประตูใหญ่ที่ต้องการให้เปิดมาใส่")]
    public Transform doorTransform;
    [Tooltip("ระยะการเปิดของประตู")]
    public Vector3 openOffset = new Vector3(0, 5f, 0);
    public float doorSpeed = 2f;

    // ตัวแปรเก็บรายชื่อกุญแจที่ถูกวางลงมาแล้ว
    private List<string> currentKeysInSocket = new List<string>();
    private bool isDoorOpen = false;
    private Vector3 closedPosition;
    private Vector3 targetPosition;

    private void Start()
    {
        if (doorTransform != null)
        {
            closedPosition = doorTransform.position;
            targetPosition = closedPosition;
        }
    }

    private void Update()
    {
        if (doorTransform != null)
        {
            doorTransform.position = Vector3.Lerp(doorTransform.position, targetPosition, Time.deltaTime * doorSpeed);
        }
    }

    // เมื่อมีวัตถุเข้ามาในแท่น
    private void OnTriggerEnter(Collider other)
    {
        ItemInfo item = other.GetComponent<ItemInfo>();
        if (item != null)
        {
            // ถ้าวัตถุนั้นเป็น 1 ในกุญแจที่ต้องการ และยังไม่เคยถูกนับ
            if (requiredKeyIDs.Contains(item.itemID) && !currentKeysInSocket.Contains(item.itemID))
            {
                currentKeysInSocket.Add(item.itemID);
                Debug.Log($"วาง {item.itemID} แล้ว! (รวม {currentKeysInSocket.Count}/{requiredKeyIDs.Count})");

                CheckDoorState();
            }
        }
    }

    // เมื่อวัตถุถูกหยิบออกไปจากแท่น
    private void OnTriggerExit(Collider other)
    {
        ItemInfo item = other.GetComponent<ItemInfo>();
        if (item != null)
        {
            // ถ้าวัตถุที่หยิบออก เป็นกุญแจที่เคยวางไว้
            if (currentKeysInSocket.Contains(item.itemID))
            {
                currentKeysInSocket.Remove(item.itemID);
                Debug.Log($"หยิบ {item.itemID} ออก! (เหลือ {currentKeysInSocket.Count}/{requiredKeyIDs.Count})");

                CheckDoorState();
            }
        }
    }

    private void CheckDoorState()
    {
        // ถ้าจำนวนกุญแจบนแท่น เท่ากับ จำนวนกุญแจที่ต้องการ (ครบ 3 ดอก)
        if (currentKeysInSocket.Count >= requiredKeyIDs.Count && !isDoorOpen)
        {
            isDoorOpen = true;
            Debug.Log("วางกุญแจครบ 3 ดอกแล้ว! ประตูใหญ่เปิด");
            targetPosition = closedPosition + openOffset;
        }
        else if (currentKeysInSocket.Count < requiredKeyIDs.Count && isDoorOpen)
        {
            isDoorOpen = false;
            Debug.Log("กุญแจไม่ครบ ประตูใหญ่ปิดลง");
            targetPosition = closedPosition;
        }
    }
}