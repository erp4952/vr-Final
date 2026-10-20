using UnityEngine;

public class ColorChecker : MonoBehaviour
{
    [Header("Color Settings")]
    [Tooltip("สีเป้าหมายที่ต้องการให้ตรง")]
    public BoxColorType requiredColor;

    [Header("Feedback Lights")]
    [Tooltip("ไฟสีเขียว (ติดเมื่อสีถูกต้อง)")]
    public GameObject greenLight;

    [Tooltip("ไฟสีแดง (ติดเมื่อสีไม่ถูกต้อง)")]
    public GameObject redLight;

    private void Start()
    {
        // ปิดไฟทั้งคู่ตอนเริ่มเกม
        ResetLights();
    }

    /// <summary>
    /// ฟังก์ชันเช็คว่า ColorItem ที่ส่งเข้ามาตรงกับสีเป้าหมายหรือไม่
    /// </summary>
    public bool CheckColor(ColorItem item)
    {
        if (item == null)
        {
            ResetLights();
            return false;
        }

        // เปรียบเทียบ Enum BoxColorType
        if (item.colorType == requiredColor)
        {
            SetLights(isCorrect: true);
            return true;
        }
        else
        {
            SetLights(isCorrect: false);
            return false;
        }
    }

    /// <summary>
    /// สลับการเปิด-ปิดไฟสีเขียวและสีแดง
    /// </summary>
    private void SetLights(bool isCorrect)
    {
        if (greenLight != null) greenLight.SetActive(isCorrect);
        if (redLight != null) redLight.SetActive(!isCorrect);
    }

    /// <summary>
    /// ปิดไฟทั้งหมด
    /// </summary>
    public void ResetLights()
    {
        if (greenLight != null) greenLight.SetActive(false);
        if (redLight != null) redLight.SetActive(false);
    }
}