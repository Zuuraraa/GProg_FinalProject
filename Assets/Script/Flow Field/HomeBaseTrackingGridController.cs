using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class HomeBaseTrackingGridController : GridController
{

    private void Start()
    {
        Debug.Log("start home base");
        RecalculateFLowField();
    }
    protected override List<Cell> GetDestinationCells()
    {
        List<Cell> destinationCells = new List<Cell>();
        Vector3 cellHalfExtents = Vector3.one * cellRadius;
        int terrainMask = LayerMask.GetMask("HomeBase");
        foreach (Cell curCell in curFlowField.grid)
        {
            Collider2D[] obstacles = Physics2D.OverlapBoxAll(curCell.worldPos, new Vector2(cellRadius * 2, cellRadius * 2), 0, terrainMask);
            foreach (Collider2D col in obstacles)
            {
                if (col.gameObject.layer == LayerMask.NameToLayer("HomeBase"))
                {
                    destinationCells.Add(curCell);
                    continue;
                }
            }
        }
        return destinationCells;
    }
}
