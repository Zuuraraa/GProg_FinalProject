using UnityEngine;
using UnityEngine.Rendering;

public class HomeBaseManager : MonoBehaviour
{

    [SerializeField] HomeBase[] homeBaseList;
    
    static HomeBaseManager instance;

    private void Awake()
    {
        instance = this;
    }

    public static HomeBase GetClosest(Vector3 position)
    {
        int length = instance.homeBaseList.Length;
        int minIndex = -1;
        float minDistance = -1;

        for (int i = 0; i < length; i++)
        {
            HomeBase homeBase = instance.homeBaseList[i];
            float currentDistance = (homeBase.transform.position - position).sqrMagnitude;
            if (minDistance < 0 || (currentDistance < minDistance))
            {
                minDistance = currentDistance;
                minIndex = i;
            }
        }

        return instance.homeBaseList[minIndex];
    }
}
