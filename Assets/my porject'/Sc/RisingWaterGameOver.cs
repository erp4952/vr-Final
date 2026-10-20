using UnityEngine;
using UnityEngine.Events;

public class RisingWaterGameOver : MonoBehaviour
{
    [Header("Water Movement Settings")]
    [Tooltip("ความเร็วในการขึ้นของน้ำ (เมตร/วินาที)")]
    public float riseSpeed = 1.0f;

    [Tooltip("ระดับความสูงสูงสุด (Y Axis) ที่ถ้าน้ำขึ้นมาถึงแล้วจะถือว่าแพ้")]
    public float loseHeight = 15.0f;

    [Tooltip("เปิดใช้งานการเคลื่นที่ของน้ำ")]
    public bool isRising = true;

    [Header("Detection Options")]
    [Tooltip("ติ๊กถูกหากต้องการให้แพ้ทันทีเมื่อน้ำท่วมตัวละครผู้เล่น (ใช้กับ Collider/Trigger)")]
    public bool loseOnPlayerTouch = true;

    [Tooltip("Tag ของตัวละครผู้เล่น")]
    public string playerTag = "Player";

    [Header("Game Over Event")]
    [Tooltip("เหตุการณ์ที่จะให้เกิดขึ้นเมื่อแพ้ (เช่น เปิด UI Game Over, หยุดเวลา)")]
    public UnityEvent onGameOver;

    private bool isGameOver = false;

    void Update()
    {
        if (isGameOver || !isRising) return;

        // 1. ดันระดับน้ำขึ้นในแกน Y
        transform.position += Vector3.up * riseSpeed * Time.deltaTime;

        // 2. ตรวจสอบว่าน้ำขึ้นถึงระดับความสูงสูงสุดที่กำหนดไว้หรือไม่
        if (transform.position.y >= loseHeight)
        {
            TriggerGameOver("น้ำขึ้นถึงระดับความสูงสูงสุดแล้ว!");
        }
    }

    // ตรวจจับเมื่อน้ำลอยมาชน/ท่วมตัวละครผู้เล่น
    private void OnTriggerEnter(Collider other)
    {
        if (loseOnPlayerTouch && !isGameOver)
        {
            if (other.CompareTag(playerTag))
            {
                TriggerGameOver("ผู้เล่นจมน้ำ!");
            }
        }
    }

    private void TriggerGameOver(string reason)
    {
        isGameOver = true;
        isRising = false;

        Debug.Log("GAME OVER: " + reason);

        // เรียกใช้ Event แพ้เกม
        onGameOver?.Invoke();
    }
}