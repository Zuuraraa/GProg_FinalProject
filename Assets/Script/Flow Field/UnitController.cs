using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    public GameObject unitPrefab;
    public int numUnitsPerSpawn;
    public float moveSpeed;
    public EnemyStatistics stats;

    [Header("Grid Controllers")]
    [SerializeField] GridController homeBaseTrackingGridController;
    [SerializeField] GridController playerTrackingGridController;

    public List<GameObject> unitsInGame;
    //GridController gridController;

    private void Awake()
    {
        unitsInGame = new List<GameObject>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SpawnUnits();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            DestroyUnits();
        }
    }

    private void FixedUpdate()
    {
        if (homeBaseTrackingGridController.curFlowField == null || playerTrackingGridController.curFlowField == null) { return; }
        foreach (GameObject unit in unitsInGame)
        {
            Enemy enemyScript = unit.GetComponent<Enemy>();
            GridController gridController = enemyScript.aggro.hasAggro ? playerTrackingGridController : homeBaseTrackingGridController;
            Cell cellBelow = gridController.curFlowField.GetCellFromWorldPos(unit.transform.position);
            Vector3 moveDirection = new Vector3(cellBelow.bestDirection.Vector.x, cellBelow.bestDirection.Vector.y, 0);
            Rigidbody2D unitRB = unit.GetComponent<Rigidbody2D>();
            unitRB.linearVelocity = moveDirection * moveSpeed;
        }
    }

    private void SpawnUnits()
    {
        Vector2Int gridSize = homeBaseTrackingGridController.gridSize;
        float nodeRadius = homeBaseTrackingGridController.cellRadius;
        Vector2 maxSpawnPos = new Vector2(gridSize.x * nodeRadius * 2 + nodeRadius, gridSize.y * nodeRadius * 2 + nodeRadius);
        int colMask = LayerMask.GetMask("Obstacle", "EnemyHitbox");
        Vector3 newPos;
        for (int i = 0; i < numUnitsPerSpawn; i++)
        {
            GameObject newUnit = Instantiate(unitPrefab);
            Enemy enemyScript = newUnit.GetComponent<Enemy>();
            enemyScript.stats = stats;
            newUnit.transform.parent = transform;
            unitsInGame.Add(newUnit);
            do
            {
                newPos = new Vector3(Random.Range(0, maxSpawnPos.x), Random.Range(0, maxSpawnPos.y), 0);
                newUnit.transform.position = newPos;
            }
            while (Physics.OverlapSphere(newPos, 0.25f, colMask).Length > 0);
        }
    }

    private void DestroyUnits()
    {
        foreach (GameObject go in unitsInGame)
        {
            Destroy(go);
        }
        unitsInGame.Clear();
    }

    // Method baru untuk spawn satu unit, dipanggil oleh WaveManager
    public void SpawnSingleUnit()
    {
        Vector2Int gridSize = homeBaseTrackingGridController.gridSize;
        float nodeRadius = homeBaseTrackingGridController.cellRadius;
        
        // Hitung batas area spawn berdasarkan ukuran grid
        Vector2 maxSpawnPos = new Vector2(
            gridSize.x * nodeRadius * 2 + nodeRadius, 
            gridSize.y * nodeRadius * 2 + nodeRadius
        );

        int colMask = LayerMask.GetMask("Obstacle", "EnemyHitbox");
        Vector3 newPos;

        GameObject newUnit = Instantiate(unitPrefab);
        Enemy enemyScript = newUnit.GetComponent<Enemy>();
        
        enemyScript.stats = stats;
        newUnit.transform.parent = transform;
        
        // Tambahkan ke list agar terhitung di FlowField
        unitsInGame.Add(newUnit);

        // Cari posisi spawn valid yang tidak menabrak obstacle
        do
        {
            newPos = new Vector3(Random.Range(0, maxSpawnPos.x), Random.Range(0, maxSpawnPos.y), 0);
            newUnit.transform.position = newPos;
        }
        while (Physics.OverlapSphere(newPos, 0.25f, colMask).Length > 0);
    }
}