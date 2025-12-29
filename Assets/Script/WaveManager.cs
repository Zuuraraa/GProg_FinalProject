using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header("Dependencies")]
    // Masukkan GameObject yang memegang script UnitController di sini
    [SerializeField] UnitController unitSpawner;

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
        // Beri jeda sedikit saat game mulai sebelum wave pertama
        Invoke(nameof(StartNextWave), 2f);
    }

    void StartNextWave()
    {
        if (currentWaveIndex >= waves.Count)
        {
            // Semua wave selesai
            return; 
        }

        StartCoroutine(ProcessWave(waves[currentWaveIndex]));
    }

    IEnumerator ProcessWave(WaveData wave)
    {
        isSpawning = true;
        
        Debug.Log($"[Wave System] Memulai: {wave.waveName} - Jumlah Musuh: {wave.enemyCount}");
        
        // Loop spawn musuh sesuai jumlah di inspector
        for (int i = 0; i < wave.enemyCount; i++)
        {
            unitSpawner.SpawnSingleUnit();
            yield return new WaitForSeconds(spawnInterval);
        }

        isSpawning = false;
        currentWaveIndex++;

        // Tunggu durasi istirahat sebelum lanjut wave berikutnya
        Invoke(nameof(StartNextWave), timeBetweenWaves);
    }
}