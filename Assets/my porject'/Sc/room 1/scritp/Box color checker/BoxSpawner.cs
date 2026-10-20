using System.Collections;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    [Header("Box Prefabs")]
    [Tooltip("ใส่ Prefab ของกล่องสีต่างๆ ลงในนี้")]
    public GameObject[] boxPrefabs;

    [Header("Spawn Settings")]
    [Tooltip("ระยะเวลารอในการเกิดของกล่องถัดไป (วินาที)")]
    public float spawnInterval = 1.5f;

    [Tooltip("สั่งให้เริ่มเกิดอัตโนมัติเมื่อเริ่มเกม")]
    public bool autoStartSpawn = true;

    [Header("Spawn Area")]
    [Tooltip("ขอบเขตพื้นที่ที่จะให้สุ่มเกิด (กว้าง X, ลึก Z)")]
    public Vector3 spawnAreaSize = new Vector3(10f, 0f, 10f);

    [Tooltip("ความสูง Y ที่ต้องการให้กล่องเกิด")]
    public float spawnYHeight = 1f;

    private Coroutine spawnCoroutine;

    private void Start()
    {
        if (autoStartSpawn)
        {
            StartSpawning();
        }
    }

    /// <summary>
    /// สั่งเริ่มเกิดกล่องเรื่อยๆ
    /// </summary>
    public void StartSpawning()
    {
        if (spawnCoroutine == null)
        {
            spawnCoroutine = StartCoroutine(SpawnBoxesForever());
        }
    }

    /// <summary>
    /// สั่งหยุดการเกิดกล่อง
    /// </summary>
    public void StopSpawning()
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }
    }

    /// <summary>
    /// Coroutine สุ่มสร้างกล่องวนลูปไปเรื่อยๆ ไม่จำกัด
    /// </summary>
    private IEnumerator SpawnBoxesForever()
    {
        if (boxPrefabs == null || boxPrefabs.Length == 0)
        {
            Debug.LogWarning("ยังไม่ได้ใส่ Box Prefabs ใน Inspector!");
            yield break;
        }

        // ลูปตลอดไปจนกว่าจะปิดเกมหรือสั่ง StopSpawning()
        while (true)
        {
            // 1. สุ่มเลือกว่าจะใช้ Prefab กล่องตัวไหน
            int randomIndex = Random.Range(0, boxPrefabs.Length);
            GameObject selectedPrefab = boxPrefabs[randomIndex];

            // 2. สุ่มตำแหน่ง X และ Z ตามขอบเขต
            float randomX = Random.Range(-spawnAreaSize.x / 2f, spawnAreaSize.x / 2f);
            float randomZ = Random.Range(-spawnAreaSize.z / 2f, spawnAreaSize.z / 2f);

            Vector3 spawnPosition = transform.position + new Vector3(randomX, spawnYHeight, randomZ);

            // 3. สร้างกล่องขึ้นมา
            Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);

            // 4. รอตามเวลาที่กำหนด ก่อนจะวนกลับไปสร้างกล่องถัดไป
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    // วาดขอบเขตพื้นที่ในหน้า Scene
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0f, 1f, 1f, 0.3f);
        Gizmos.DrawCube(transform.position + new Vector3(0, spawnYHeight, 0), spawnAreaSize);
    }
}