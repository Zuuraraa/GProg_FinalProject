using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatistics", menuName = "Scriptable Objects/Statistics/Player")]
public class PlayerStatistics : Statistics
{
    public int[] xpTresholds = { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100 };
}
