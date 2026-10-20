using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    [Header("Sockets")]
    [Tooltip("ใส่แท่นวางทั้ง 3 แท่นที่นี่")]
    public ItemSocket[] sockets = new ItemSocket[3];

    [Header("Door Target")]
    public Transform doorTransform;
    public Vector3 openOffset = new Vector3(0, 4, 0); // ระยะการเลื่อนของประตู
    public float doorSpeed = 2f;

    private bool isSolved = false;
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
        // เลื่อนประตูไปยังตำแหน่งเป้าหมายแบบเรียบเนียน (Smooth Interpolation)
        if (doorTransform != null)
        {
            doorTransform.position = Vector3.Lerp(doorTransform.position, targetPosition, Time.deltaTime * doorSpeed);
        }
    }

    public void CheckPuzzleState()
    {
        bool allCorrect = true;

        // วนลูปเช็กว่าทุกแท่นถูกต้องหรือไม่
        foreach (ItemSocket socket in sockets)
        {
            if (socket == null || !socket.isCorrect)
            {
                allCorrect = false;
                break;
            }
        }

        if (allCorrect && !isSolved)
        {
            isSolved = true;
            OnPuzzleSolved();
        }
        else if (!allCorrect && isSolved)
        {
            isSolved = false;
            OnPuzzleReset();
        }
    }

    private void OnPuzzleSolved()
    {
        Debug.Log("แก้ปริศนาสำเร็จ! กำลังเปิดประตู...");
        targetPosition = closedPosition + openOffset;
    }

    private void OnPuzzleReset()
    {
        Debug.Log("เงื่อนไขไม่ครบ ประตูปิดลง");
        targetPosition = closedPosition;
    }
}