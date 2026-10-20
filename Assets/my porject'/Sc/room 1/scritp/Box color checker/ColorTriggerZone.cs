using UnityEngine;

public class ColorTriggerZone : MonoBehaviour
{
    private ColorChecker colorChecker;

    private void Awake()
    {
        colorChecker = GetComponent<ColorChecker>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // ดึงคอมโพเนนต์ ColorItem จากวัตถุที่เข้ามาในโซน
        ColorItem item = other.GetComponent<ColorItem>();

        if (item != null)
        {
            // 1. เช็คสีกับ ColorChecker
            bool isCorrect = colorChecker.CheckColor(item);

            // 2. ส่งผลลัพธ์ไปให้ GameManager (เพื่อบันทึกโควตา + สั่ง AudioManager เล่นเสียง)
            if (GameManager.Instance != null)
            {
                GameManager.Instance.RegisterBoxResult(isCorrect);
            }

            // 3. ทำลายกล่องทิ้งหลังจากเช็คเสร็จ (หรือย้ายไปเก็บที่อื่น) เพื่อไม่ให้ชนซ้ำ
            Destroy(other.gameObject, 0.5f); // หน่วงเวลา 0.5 วิ ให้ไฟแสดงผลแป๊บนึง
        }
    }
}