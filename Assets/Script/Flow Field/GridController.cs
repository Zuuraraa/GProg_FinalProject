using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GridController : MonoBehaviour
{
    public Vector2Int gridSize;
    public float cellRadius = 0.5f;
    public FlowField curFlowField;

    protected void InitializeFlowField()
    {
        curFlowField = new FlowField(cellRadius, gridSize);
        curFlowField.CreateGrid();
    }

    public void RecalculateFLowField()
    {
        InitializeFlowField();

        curFlowField.CreateCostField();
        curFlowField.CreateIntegrationField(GetDestinationCells());

        curFlowField.CreateFlowField();
    }

    protected abstract List<Cell> GetDestinationCells();
}
