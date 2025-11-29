using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatistics", menuName = "Scriptable Objects/PlayerStatistics")]
public class PlayerStatistics : Statistics
{
    [Header("Progression")]
    public int level = 1;
    public int xp = 0;
    public int xpTreshold = 50;
}
