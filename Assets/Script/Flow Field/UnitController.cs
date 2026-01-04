using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    [Header("Unit Settings")]
    public GameObject unitPrefab;
    public float moveSpeed;
    public EnemyStatistics stats;

    [Header("Grid References")]
    [SerializeField] GridController homeBaseTrackingGridController;
    [SerializeField] GridController playerTrackingGridController;

    [Header("Spawn Configuration")]
    // Titik-titik spawn area (drag dari hierarchy).
    // Pastikan titik ini ada di area aman (bukan sungai/tembok).
    public List<Transform> spawnPoints; 
    
    // Radius random biar spawn-nya nyebar natural
    public float spawnRadius = 3f; 

    // List tracking unit aktif untuk kebutuhan WaveManager
    public List<GameObject> unitsInGame;

    private void Awake()
    {
        unitsInGame = new List<GameObject>();
    }

    private void FixedUpdate()
    {
        // Bersihin list kalau ada unit yang udah hancur/null biar gak error logic
        unitsInGame.RemoveAll(item => item == null);

        if (homeBaseTrackingGridController.curFlowField == null || playerTrackingGridController.curFlowField == null) return;
        
        foreach (GameObject unit in unitsInGame)
        {
            if (unit == null) continue;

            Enemy enemyScript = unit.GetComponent<Enemy>();
            if (enemyScript == null) continue;

            // Logika pathfinding: Prioritas kejar Player kalau aggro, kalau enggak ya serang Base
            GridController gridController = enemyScript.aggro.hasAggro ? playerTrackingGridController : homeBaseTrackingGridController;
            Cell cellBelow = gridController.curFlowField.GetCellFromWorldPos(unit.transform.position);
            
            if (cellBelow != null)
            {
                // Gerakkan unit via Rigidbody
                Vector3 moveDirection = new Vector3(cellBelow.bestDirection.Vector.x, cellBelow.bestDirection.Vector.y, 0);
                Rigidbody2D unitRB = unit.GetComponent<Rigidbody2D>();
                if(unitRB != null) unitRB.linearVelocity = moveDirection * moveSpeed;
            }
        }
    }

    // Dipanggil WaveManager saat jam spawn tiba
    public void SpawnSingleUnit()
    {
        // Validasi: Jangan sampe lupa pasang spawn point di inspector
        if (spawnPoints == null || spawnPoints.Count == 0)
        {
            Debug.LogWarning("Spawn Points kosong! Cek inspector UnitController.");
            return;
        }

        Vector3 spawnPos = Vector3.zero;
        
        // Pilih area spawn secara acak biar variatif
        Transform randomRegion = spawnPoints[Random.Range(0, spawnPoints.Count)];

        // Tambah offset random dalam lingkaran radius biar gak numpuk di satu titik
        Vector2 randomOffset = Random.insideUnitCircle * spawnRadius;
        spawnPos = randomRegion.position + new Vector3(randomOffset.x, randomOffset.y, 0);

        // Instantiate musuh
        GameObject newUnit = Instantiate(unitPrefab, spawnPos, Quaternion.identity);
        Enemy enemyScript = newUnit.GetComponent<Enemy>();
        
        // Set stat awal musuh (HP, Damage, dll)
        if(enemyScript != null) enemyScript.stats = stats;
        
        newUnit.transform.parent = transform;
        unitsInGame.Add(newUnit);
    }

    // Fungsi helper untuk reset game state (dipanggil saat game over/restart)
    private void DestroyUnits()
    {
        foreach (GameObject go in unitsInGame) if (go != null) Destroy(go);
        unitsInGame.Clear();
    }
}