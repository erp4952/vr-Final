using UnityEngine;

public class GemRoomManager : MonoBehaviour
{
    [Header("Gem Sockets")]
    [Tooltip("ลากแท่นสีแดง สีเขียว สีฟ้า มาใส่ให้ครบ 3 ช่อง")]
    public GemSocket[] sockets = new GemSocket[3];

    [Header("Key Reward")]
    [Tooltip("ลาก Key สีเขียวที่อยู่บนแท่นสี่เหลี่ยมมาใส่ช่องนี้")]
    public GameObject keyOnPedestal;

    private void Start()
    {
        // ซ่อน Key ไว้ก่อนตอนเริ่มเกม
        if (keyOnPedestal != null)
        {
            keyOnPedestal.SetActive(false);
        }
    }

    public void CheckPuzzle()
    {
        bool allCorrect = true;

        // วนลูปเช็กว่าทุกแท่นเปลี่ยนเป็น isCorrect = true หรือยัง
        foreach (GemSocket socket in sockets)
        {
            if (socket == null || !socket.isCorrect)
            {
                allCorrect = false;
                break;
            }
        }

        // ถ้าวางถูกสีครบทั้ง 3 แท่น
        if (allCorrect)
        {
            Debug.Log("วาง Gem ตรงสีครบ 3 อันแล้ว! Key ปรากฏขึ้น");
            if (keyOnPedestal != null) keyOnPedestal.SetActive(true);
        }
        else
        {
            // ถ้าดึง Gem ออก ให้ซ่อน Key กลับไป
            if (keyOnPedestal != null) keyOnPedestal.SetActive(false);
        }
    }
}