using UnityEngine;

public class DoorController : MonoBehaviour
{
    [Header("Door Settings")]
    public Vector3 openOffset = new Vector3(0, 3, 0); // ระยะที่ประตูจะเลื่อนขึ้น
    public float openSpeed = 2.0f;

    private bool isOpening = false;
    private Vector3 targetPosition;

    private void Start()
    {
        targetPosition = transform.position + openOffset;
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnQuotaReached.AddListener(OpenDoor);
        }
    }

    private void Update()
    {
        if (isOpening)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * openSpeed);
        }
    }

    public void OpenDoor()
    {
        isOpening = true;
    }
}