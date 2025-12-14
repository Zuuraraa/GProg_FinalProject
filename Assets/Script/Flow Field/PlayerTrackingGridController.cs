using System.Collections.Generic;
using UnityEngine;

public class PlayerTrackingGridController : GridController
{
    [SerializeField] Player player;
    Vector2Int playerGridPos;

    private void Update()
    {
        Vector2 playerPos = (Vector2)player.transform.position; 
        Vector2Int newPlayerGridPos = new Vector2Int((int)playerPos.x, (int)playerPos.y); 
        if (newPlayerGridPos != playerGridPos)
        {
            playerGridPos = newPlayerGridPos;
            RecalculateFLowField();
        }
    }


    protected override List<Cell> GetDestinationCells()
    {
        List<Cell> destinationCells = new List<Cell>();
        destinationCells.Add(curFlowField.GetCellFromWorldPos(player.transform.position));
        return destinationCells;
    }
}