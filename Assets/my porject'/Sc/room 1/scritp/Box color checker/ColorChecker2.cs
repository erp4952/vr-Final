using UnityEngine;

public class ColorTriggerZone2 : MonoBehaviour
{
    private ColorChecker colorChecker;

    private void Awake()
    {
        colorChecker = GetComponent<ColorChecker>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // ดึงคอมโพเนนต์ ColorItem จากวัตถุที่ชน
        ColorItem item = other.GetComponent<ColorItem>();

        if (item != null)
        {
            // ทำการเช็คสี
            colorChecker.CheckColor(item);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // เมื่อวัตถุออกจากโซน ให้ดับไฟ
        if (other.GetComponent<ColorItem>() != null)
        {
            colorChecker.ResetLights();
        }
    }
}