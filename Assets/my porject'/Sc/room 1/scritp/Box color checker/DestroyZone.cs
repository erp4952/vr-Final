using UnityEngine;

public class DestroyZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // เช็คว่าวัตถุที่เข้ามาชนมีคอมโพเนนต์ ColorItem หรือไม่ (เพื่อลบเฉพาะกล่อง)
        if (other.GetComponent<ColorItem>() != null)
        {
            Destroy(other.gameObject);
        }
    }
}