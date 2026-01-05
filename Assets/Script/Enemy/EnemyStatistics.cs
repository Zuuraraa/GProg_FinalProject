using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStatistics", menuName = "Scriptable Objects/Statistics/Enemy Statistics")]
public class EnemyStatistics : Statistics
{
    public Sprite sprite;
    public int xpDrop = 1;
    public int damage = 10;
    public bool isBoss;
}
