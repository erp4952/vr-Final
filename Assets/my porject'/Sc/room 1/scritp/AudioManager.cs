using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Clips")]
    public AudioClip correctSFX;
    public AudioClip wrongSFX;
    public AudioClip successSFX;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnBoxProcessed.AddListener(PlayFeedbackSound);
            GameManager.Instance.OnQuotaReached.AddListener(PlaySuccessSound);
        }
    }

    private void PlayFeedbackSound(bool isCorrect)
    {
        AudioClip clipToPlay = isCorrect ? correctSFX : wrongSFX;
        if (clipToPlay != null) audioSource.PlayOneShot(clipToPlay);
    }

    private void PlaySuccessSound()
    {
        if (successSFX != null) audioSource.PlayOneShot(successSFX);
    }
}