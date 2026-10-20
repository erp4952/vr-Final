using UnityEngine;

public class PuzzlePlatform : MonoBehaviour
{
    [Header("Puzzle Settings")]
    [Tooltip("ใส่ Tag ของวัตถุที่ถูกต้อง เช่น CorrectKey")]
    [SerializeField] private string correctObjectTag = "CorrectKey";

    [Tooltip("ใส่ GameObject ของประตูที่ต้องการให้อิสระเปิด")]
    [SerializeField] private GameObject door;

    [Header("Door Movement Settings")]
    [Tooltip("ระยะที่ต้องการให้ประตูเลื่อนไปเปิด (เช่น เลื่อนขึ้น Y = 3)")]
    [SerializeField] private Vector3 doorOpenOffset = new Vector3(0, 3f, 0);

    [Tooltip("ความเร็วในการเปิดประตู")]
    [SerializeField] private float doorOpenSpeed = 2f;

    private bool isSolved = false;
    private Vector3 doorClosedPosition;
    private Vector3 doorTargetPosition;

    private void Start()
    {
        if (door != null)
        {
            doorClosedPosition = door.transform.position;
            doorTargetPosition = doorClosedPosition + doorOpenOffset;
        }
    }

    private void Update()
    {
        // เคลื่อนที่ประตูเข้าหาตำแหน่งเป้าหมายแบบ Smooth
        if (door != null)
        {
            Vector3 target = isSolved ? doorTargetPosition : doorClosedPosition;
            door.transform.position = Vector3.Lerp(door.transform.position, target, Time.deltaTime * doorOpenSpeed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // ตรวจสอบว่าวัตถุที่มาสัมผัสมี Tag ตรงกับที่กำหนดไว้หรือไม่
        if (other.CompareTag(correctObjectTag))
        {
            isSolved = true;
            Debug.Log("วางของถูกต้อง! ประตูกำลังเปิด...");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // เมื่อยกวัตถุออก ประตูจะปิดลง
        if (other.CompareTag(correctObjectTag))
        {
            isSolved = false;
            Debug.Log("ยกของออก ประตูกำลังปิด...");
        }
    }
}