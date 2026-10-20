using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Quota Settings")]
    public int targetQuota = 10;
    private int currentScore = 0;

    [Header("Events")]
    public UnityEvent<bool> OnBoxProcessed; // true = ถูก, false = ผิด
    public UnityEvent OnQuotaReached;        // ครบ 10 ชิ้น -> ประตูเปิด

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // ให้เพื่อนเรียกใช้ฟังก์ชันนี้เมื่อตรวจเช็คกล่องสำเร็จ
    public void RegisterBoxResult(bool isCorrect)
    {
        OnBoxProcessed?.Invoke(isCorrect);

        if (isCorrect)
        {
            currentScore++;
            Debug.Log($"Score: {currentScore}/{targetQuota}");

            if (currentScore >= targetQuota)
            {
                CompleteMission();
            }
        }
    }

    private void CompleteMission()
    {
        Debug.Log("Quota Reached! Opening Door...");
        OnQuotaReached?.Invoke();
    }
}