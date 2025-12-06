using UnityEngine;

public class State : MonoBehaviour
{
    Enemy enemy;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
    }

    public void Enter()
    {

    }

    public void ProccessUpdate()
    {

    }

    public void ProccessFixedUpdate()
    {

    }

    public void Exit()
    {

    } 
}
