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

    // Class buat nyimpen data per wave biar rapi di inspector
    [System.Serializable]
    public class WaveData
    {
        public string waveName;
        public int enemyCount;
        
        // Slot buat masukin prefab musuh (Rat, Cobra, Bee, dll)
        public GameObject enemyPrefab; 
    }

    private int currentWaveIndex = 0;
    private bool isWaveActive = false;
    private bool isWaitingForNextWave = false;
    private float waveTimer = 0f;
    private bool finishedSpawning = false; 

    private void Start()
    {
        if (waveTextUI != null) waveTextUI.gameObject.SetActive(false);
        
        // Kasih delay dikit pas start biar gak kaget
        Invoke(nameof(StartNextWave), 1f); 
    }

    private void Update()
    {
        // Kalau lagi fase istirahat antar wave
        if (isWaitingForNextWave)
        {
            HandleGracePeriod();
            return;
        }

        // Cek kondisi wave kelar (musuh abis & spawn beres)
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
            waveTextUI.text = $"NEXT WAVE IN: {Mathf.Ceil(waveTimer)}\n[SPACE] TO SKIP";
        }

        // Cheat skip wave buat testing cepet (atau fitur gameplay)
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
        // Cleanup list unit biar gak ngecek object null
        unitSpawner.unitsInGame.RemoveAll(item => item == null || !item.activeInHierarchy);

        if (unitSpawner.unitsInGame.Count == 0)
        {
            isWaveActive = false;
            
            // Cek apakah ini wave terakhir
            if (currentWaveIndex >= waves.Count)
            {
                ShowVictory();
                return;
            }

            StartGracePeriod();
        }
    }

    void StartGracePeriod()
    {
        isWaitingForNextWave = true;
        finishedSpawning = false; 
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
        finishedSpawning = false;

        // Show UI nama wave sebentar
        if (waveTextUI != null)
        {
            waveTextUI.text = wave.waveName;
            waveTextUI.gameObject.SetActive(true);
            yield return new WaitForSeconds(2f);
            
            // Hide UI kalau player gak lagi nunggu (cegah bug UI numpuk)
            if (!isWaitingForNextWave) 
            {
                waveTextUI.gameObject.SetActive(false);
            }
        }

        // Mulai spawning musuh satu per satu
        for (int i = 0; i < wave.enemyCount; i++)
        {
            if (!isWaveActive) yield break; 

            // Panggil spawner dengan parameter prefab musuh spesifik
            unitSpawner.SpawnSingleUnit(wave.enemyPrefab);
            
            yield return new WaitForSeconds(spawnInterval);
        }
        
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
        this.enabled = false;
    }
}