using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitController : MonoBehaviour
{
    [Header("Unit Settings")]
    public GameObject enemyPrefab;

    [Header("Grid References")]
    [SerializeField] GridController homeBaseTrackingGridController;
    [SerializeField] GridController playerTrackingGridController;

    [Header("Spawn Configuration")]
    // Titik spawn di map
    public List<Transform> spawnPoints; 
    public float spawnRadius = 3f; 

    // List tracking unit aktif untuk dicek WaveManager
    public List<GameObject> unitsInGame;

    private void Awake()
    {
        unitsInGame = new List<GameObject>();
    }

    private void FixedUpdate()
    {
        // Hapus referensi unit yang udah mati biar memory aman
        unitsInGame.RemoveAll(item => item == null);

        if (homeBaseTrackingGridController.curFlowField == null || playerTrackingGridController.curFlowField == null) return;
        
        // Logic pathfinding flow field
        foreach (GameObject unit in unitsInGame)
        {
            if (unit == null) continue;

            Enemy enemyScript = unit.GetComponent<Enemy>();
            if (enemyScript == null) continue;

            // Tentukan target: Player (kalau aggro) atau Base
            GridController gridController = enemyScript.aggro.hasAggro ? playerTrackingGridController : homeBaseTrackingGridController;
            Cell cellBelow = gridController.curFlowField.GetCellFromWorldPos(unit.transform.position);
            
            if (cellBelow != null)
            {
                Vector3 moveDirection = new Vector3(cellBelow.bestDirection.Vector.x, cellBelow.bestDirection.Vector.y, 0);
                Rigidbody2D unitRB = unit.GetComponent<Rigidbody2D>();
                if(unitRB != null) unitRB.linearVelocity = moveDirection * enemyScript.stats.baseMoveSpeed;
                enemyScript.graphics.flipX = moveDirection.x < 0;
            }

        }
    }

    // Fungsi spawn dipanggil oleh WaveManager
    // Parameter 'prefabToSpawn' ditentukan dari settingan Wave di inspector
    public void SpawnSingleUnit(EnemyStatistics stats)
    {
        if (spawnPoints == null || spawnPoints.Count == 0)
        {
            Debug.LogWarning("Spawn Points belum diset di inspector!");
            return;
        }

        // Safety check kalau prefab lupa dimasukin di WaveManager
        if (stats == null)
        {
            Debug.LogError("Enemy stats kosong (null). Cek settingan WaveManager.");
            return;
        }

        Vector3 spawnPos = Vector3.zero;
        
        // Pilih lokasi spawn random + offset radius biar natural
        Transform randomRegion = spawnPoints[Random.Range(0, spawnPoints.Count)];
        Vector2 randomOffset = Random.insideUnitCircle * spawnRadius;
        spawnPos = randomRegion.position + new Vector3(randomOffset.x, randomOffset.y, 0);

        GameObject newUnit = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        
        // Add Stats
        Enemy enemyScript = newUnit.GetComponent<Enemy>();
        enemyScript.stats = stats;
        enemyScript.Reset();

        // Setup parent biar hierarchy rapi
        newUnit.transform.parent = transform;
        
        // Masukin ke list buat ditrack WaveManager
        unitsInGame.Add(newUnit);
    }

    // Bersihin unit pas restart game
    private void DestroyUnits()
    {
        foreach (GameObject go in unitsInGame) if (go != null) Destroy(go);
        unitsInGame.Clear();
    }
}