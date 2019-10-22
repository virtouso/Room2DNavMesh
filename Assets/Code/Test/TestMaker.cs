using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMaker : MonoBehaviour
{
    //AstarAgent myTestSingleAgent;
    //public void MakePathForAgent()
    //{
    //    if (!Input.GetMouseButtonDown(0)) return;

    //    Cell start =  Physics2D.Raycast(myTestSingleAgent.transform.position,Vector3.forward,100,LayerMask.GetMask("cell") ).transform.gameObject.GetComponent<Cell>();
    //    Cell end = MouseRayCast();
    //    print(end.name + "|||||" + start.name);
    //    myTestSingleAgent.Move(start,end);
    //}

    AstarAgent[] myTestSingleAgent;
    public void MakePathForAgent()
    {
        if (!Input.GetMouseButtonDown(0)) return;

        foreach (var item in myTestSingleAgent)
        {
            Cell start = Physics2D.Raycast(item.transform.position, Vector3.forward, 100, LayerMask.GetMask("cell")).transform.gameObject.GetComponent<Cell>();
            Cell end = MouseRayCast();
            print(end.name + "|||||" + start.name);
            item.Move(start, end);// this is the main function you have to call on agent
        }
    }






    private Cell MouseRayCast()
    {

        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward, 100, LayerMask.GetMask("cell"));

        if (hit.collider != null)
        {
            Debug.Log("Target Position: " + hit.collider.gameObject.transform.position);
            return hit.collider.gameObject.GetComponent<Cell>();
        }
        return null;
    }

    private void Awake()
    {
        myTestSingleAgent = GameObject.FindObjectsOfType<AstarAgent>();
    }

    private void FixedUpdate()
    {
        MakePathForAgent();
    }






}
