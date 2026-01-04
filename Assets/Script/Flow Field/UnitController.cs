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
    // List titik spawn aman. Drag object kosong dari scene ke sini.
    // Ini revisi biar musuh gak spawn di sungai/luar map.
    public List<Transform> spawnPoints; 
    
    // Radius random biar musuh gak numpuk di satu titik persis
    public float spawnRadius = 3f; 

    // Public biar bisa diakses WaveManager buat ngecek jumlah musuh
    public List<GameObject> unitsInGame;

    private void Awake()
    {
        unitsInGame = new List<GameObject>();
    }

    private void FixedUpdate()
    {
        // Cleanup list: buang data musuh yang udah mati/null biar gak error
        unitsInGame.RemoveAll(item => item == null);

        if (homeBaseTrackingGridController.curFlowField == null || playerTrackingGridController.curFlowField == null) return;
        
        foreach (GameObject unit in unitsInGame)
        {
            if (unit == null) continue;

            Enemy enemyScript = unit.GetComponent<Enemy>();
            if (enemyScript == null) continue;

            // Tentukan target: ngejar Player atau HomeBase?
            GridController gridController = enemyScript.aggro.hasAggro ? playerTrackingGridController : homeBaseTrackingGridController;
            Cell cellBelow = gridController.curFlowField.GetCellFromWorldPos(unit.transform.position);
            
            if (cellBelow != null)
            {
                // Gerakin unit sesuai flow field
                Vector3 moveDirection = new Vector3(cellBelow.bestDirection.Vector.x, cellBelow.bestDirection.Vector.y, 0);
                Rigidbody2D unitRB = unit.GetComponent<Rigidbody2D>();
                if(unitRB != null) unitRB.linearVelocity = moveDirection * moveSpeed;
            }
        }
    }

    // Fungsi ini dipanggil sama WaveManager pas mau spawn musuh
    public void SpawnSingleUnit()
    {
        // Safety check: pastiin spawn point udah dipasang di inspector
        if (spawnPoints == null || spawnPoints.Count == 0)
        {
            Debug.LogWarning("!!! Spawn Points belom dipasang di UnitController! Musuh gak bakal muncul.");
            return;
        }

        Vector3 spawnPos = Vector3.zero;
        
        // 1. Pilih region/titik spawn secara acak dari list
        Transform randomRegion = spawnPoints[Random.Range(0, spawnPoints.Count)];

        // 2. Tambahin random offset biar posisinya variatif (gak numpuk)
        Vector2 randomOffset = Random.insideUnitCircle * spawnRadius;
        spawnPos = randomRegion.position + new Vector3(randomOffset.x, randomOffset.y, 0);

        // 3. Spawn unitnya
        GameObject newUnit = Instantiate(unitPrefab, spawnPos, Quaternion.identity);
        Enemy enemyScript = newUnit.GetComponent<Enemy>();
        
        if(enemyScript != null) enemyScript.stats = stats;
        
        // Masukin ke container transform ini biar rapi di hierarchy
        newUnit.transform.parent = transform;
        
        // Catet di list buat tracking
        unitsInGame.Add(newUnit);
    }

    // Fungsi helper buat debugging aja
    private void DestroyUnits()
    {
        foreach (GameObject go in unitsInGame) if (go != null) Destroy(go);
        unitsInGame.Clear();
    }
}