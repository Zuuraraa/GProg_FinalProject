using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatistics", menuName = "Scriptable Objects/Statistics/Player")]
public class PlayerStatistics : Statistics
{
    //public int[] xpTresholds = { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100 };
    public int[] xpTresholds = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

    [Header("Starting Items")]
    public int startingItemIndex = 0;
    public bool[] weaponUnlock = {true, false, false};
    public int[] startingSeed = { 0, 0 };

    [Header("Level Up Data")]
    public int hpPerLevel = 10;
    public float speedMultPerLevel = .1f;
}
