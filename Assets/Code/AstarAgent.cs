using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstarAgent : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] public float reachDistance;
   private Action CurrentState;
   private List<Cell> MyPath;



    
    private int CurrentMoveIndex;
    private void MovementState()
    {
        transform.position = Vector3.MoveTowards(transform.position, MyPath[CurrentMoveIndex].Position, speed);
        if (Vector3.Distance(transform.position,MyPath[CurrentMoveIndex].Position)<=reachDistance)
        {
            if (CurrentMoveIndex < MyPath.Count - 2) { CurrentMoveIndex++; }
            else { CurrentState = StationaryState; }
        }

    }
    private void StationaryState() { }



    public void Move(Cell start, Cell destination)
    {
        if (!start || !destination) { ColorizeLog.MoeenLog("start or end cant be null", ColorizeLog.Colors.DarkRed); return ; }
        MyPath = Astar.FindPathActual(start,destination);
        CurrentState = MovementState;
        CurrentMoveIndex = 0;
    }



    private void FixedUpdate()
    {
     if(CurrentState!=null)   CurrentState.Invoke();
    }

}
