using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveManager : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] UnitController unitSpawner;
    [SerializeField] TextMeshProUGUI waveTextUI;

    [Header("Wave Configuration")]
    [SerializeField] float timeBetweenWaves = 10f; 
    [SerializeField] float spawnInterval = 1f;
    [SerializeField] List<WaveData> waves;

    [System.Serializable]
    public class WaveData
    {
        public string waveName;
        public int enemyCount;
    }

    private int currentWaveIndex = 0;
    private bool isWaveActive = false;
    private bool isWaitingForNextWave = false;
    private float waveTimer = 0f;
    
    // [FIX] Variabel baru: Penjaga agar tidak curi start
    private bool finishedSpawning = false; 

    private void Start()
    {
        if (waveTextUI != null) waveTextUI.gameObject.SetActive(false);
        // Memulai wave dengan delay kecil agar UnitController siap
        Invoke(nameof(StartNextWave), 1f); 
    }

    private void Update()
    {
        // 1. Logika Grace Period (Istirahat antar Wave)
        if (isWaitingForNextWave)
        {
            HandleGracePeriod();
            return;
        }

        // 2. Logika Cek Musuh Habis 
        // [FIX] Ditambah syarat: && finishedSpawning
        // Artinya: Cuma boleh cek menang kalau SEMUA musuh wave ini sudah keluar dari kandang
        if (isWaveActive && !isWaitingForNextWave && finishedSpawning)
        {
            CheckIfWaveCleared();
        }
    }

    void HandleGracePeriod()
    {
        waveTimer -= Time.deltaTime;

        if (waveTextUI != null)
        {
            // Tampilkan timer bulat ke atas
            waveTextUI.text = $"NEXT WAVE IN: {Mathf.Ceil(waveTimer)}\n[SPACE] TO SKIP";
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            waveTimer = 0;
        }

        if (waveTimer <= 0)
        {
            isWaitingForNextWave = false;
            StartNextWave();
        }
    }

    void CheckIfWaveCleared()
    {
        // Bersihkan list dari musuh yang sudah mati
        unitSpawner.unitsInGame.RemoveAll(item => item == null || !item.activeInHierarchy);

        // Jika musuh benar-benar habis (0)
        if (unitSpawner.unitsInGame.Count == 0)
        {
            isWaveActive = false;
            
            // Cek tamat
            if (currentWaveIndex >= waves.Count)
            {
                ShowVictory();
                return;
            }

            // Masuk mode istirahat
            StartGracePeriod();
        }
    }

    void StartGracePeriod()
    {
        isWaitingForNextWave = true;
        finishedSpawning = false; // Reset status spawn untuk wave berikutnya
        waveTimer = timeBetweenWaves;
        if (waveTextUI != null) waveTextUI.gameObject.SetActive(true);
    }

    void StartNextWave()
    {
        if (currentWaveIndex < waves.Count)
        {
            StartCoroutine(ProcessWave(waves[currentWaveIndex]));
        }
    }

    IEnumerator ProcessWave(WaveData wave)
    {
        isWaveActive = true; 
        finishedSpawning = false; // [FIX] Pastikan false saat mulai

        // UI Judul Wave
        if (waveTextUI != null)
        {
            waveTextUI.text = wave.waveName;
            waveTextUI.gameObject.SetActive(true);
            yield return new WaitForSeconds(2f);
            
            // [FIX] Cek lagi, kalau sudah masuk grace period (di-skip player), jangan matikan UI timer
            if (!isWaitingForNextWave) 
            {
                waveTextUI.gameObject.SetActive(false);
            }
        }

        Debug.Log($"[Wave System] Memulai: {wave.waveName} - Target Musuh: {wave.enemyCount}");

        for (int i = 0; i < wave.enemyCount; i++)
        {
            // [FIX] Cek isWaveActive. Jika game over/reset, stop spawn
            if (!isWaveActive) yield break; 

            unitSpawner.SpawnSingleUnit();
            yield return new WaitForSeconds(spawnInterval);
        }
        
        // [FIX] Nah, baru sekarang boleh bilang "Spawn Selesai"
        finishedSpawning = true;
        currentWaveIndex++;
    }

    void ShowVictory()
    {
        if (waveTextUI != null)
        {
            waveTextUI.text = "VICTORY!";
            waveTextUI.gameObject.SetActive(true);
        }
        Debug.Log("All waves completed.");
        this.enabled = false;
    }
}