using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // WAJIB ADA: Biar bisa akses Text UI

public class WaveManager : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] UnitController unitSpawner;
    [SerializeField] TextMeshProUGUI waveTextUI; // Kabel ke Text UI

    [Header("Wave Configuration")]
    [SerializeField] float timeBetweenWaves = 5f;
    [SerializeField] float spawnInterval = 1f;
    [SerializeField] List<WaveData> waves;

    [System.Serializable]
    public class WaveData
    {
        public string waveName;
        public int enemyCount;
    }

    private int currentWaveIndex = 0;
    private bool isSpawning = false;

    private void Start()
    {
        // Pastikan text mati di awal
        if (waveTextUI != null) waveTextUI.gameObject.SetActive(false);

        // Delay sebelum wave 1 mulai
        Invoke(nameof(StartNextWave), 2f);
    }

    void StartNextWave()
    {
        if (currentWaveIndex >= waves.Count)
        {
            if (waveTextUI != null)
            {
                waveTextUI.text = "VICTORY!";
                waveTextUI.gameObject.SetActive(true);
            }
            Debug.Log("All waves completed.");
            return; 
        }

        StartCoroutine(ProcessWave(waves[currentWaveIndex]));
    }

    IEnumerator ProcessWave(WaveData wave)
    {
        isSpawning = true;
        
        // --- LOGIC UI TEXT ---
        if (waveTextUI != null)
        {
            waveTextUI.text = wave.waveName; // Ubah tulisan jadi nama wave
            waveTextUI.gameObject.SetActive(true); // Munculkan text
            
            // Text muncul selama 3 detik, lalu hilang
            yield return new WaitForSeconds(3f);
            waveTextUI.gameObject.SetActive(false); // Sembunyikan text
        }
        // ---------------------

        Debug.Log($"[Wave System] Memulai: {wave.waveName} - Jumlah Musuh: {wave.enemyCount}");

        for (int i = 0; i < wave.enemyCount; i++)
        {
            unitSpawner.SpawnSingleUnit();
            yield return new WaitForSeconds(spawnInterval);
        }

        isSpawning = false;
        currentWaveIndex++;

        Invoke(nameof(StartNextWave), timeBetweenWaves);
    }
}