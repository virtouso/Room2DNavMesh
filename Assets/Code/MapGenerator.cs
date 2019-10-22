using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Threading;
using System;
using System.Threading.Tasks;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private Vector2 GridSize;
    [SerializeField] public Transform StartPoint;
    [SerializeField] public Transform EndPoint;
    [SerializeField] private float GridMinSize = 0.1f;
    [SerializeField] public GameObject GridPrefab;
    /*  [HideInInspector]*/
    public List<GameObject> grid;
    public void GenerateButton()
    {
        if (GridPrefab.Equals(null))
        {
            ColorizeLog.MoeenLog("prefab can not be null", ColorizeLog.Colors.DarkRed);
            return;
        }
        if (StartPoint.Equals(null) || EndPoint.Equals(null))
        {
            ColorizeLog.MoeenLog("end point and start point cant be null", ColorizeLog.Colors.DarkRed);
            return;
        }
        if (StartPoint.position.x - GridSize.x > EndPoint.position.x ||
                StartPoint.position.y - GridSize.y > EndPoint.position.y
                )
        {
            ColorizeLog.MoeenLog("bad positions for start and end point", ColorizeLog.Colors.DarkRed);
            return;
        }
        if (GridSize.x < GridMinSize || GridSize.y < GridMinSize)
        {
            ColorizeLog.MoeenLog("grid size can not be so little. please update GridSize in inspector", ColorizeLog.Colors.DarkRed);
        }

        ClearNodes();




        GenerateNodes();

    }

    private void GenerateNodes()
    {
        grid = new List<GameObject>();
        int GridX = (int)((EndPoint.position.x - StartPoint.position.x) / GridSize.x);
        int GridY = (int)((EndPoint.position.y - StartPoint.position.y) / GridSize.y);
        GridPrefab.GetComponent<BoxCollider2D>().size = GridSize;
        for (int i = 0; i < GridX; i++)
        {
            for (int j = 0; j < GridY; j++)
            {
                GameObject g = Instantiate(GridPrefab, new Vector3(
                    StartPoint.position.x + (i * GridSize.x),
                    StartPoint.position.y + (j * GridSize.y),
                    0
                    ), Quaternion.identity);
                g.GetComponent<Cell>().Name = "cell" + "|" + i + "|" + j;
                g.name = "cell";
                g.transform.SetParent(transform);
                grid.Add(g);
            }


        }


        foreach (var item in grid)
        {
            Collider2D col = Physics2D.OverlapBox(item.transform.position, GridSize, 0);
            if (col == null) continue;
            if (col.gameObject.name == "cell") continue;
            DestroyImmediate(item);
        }
        grid.RemoveAll(item => item == null);


        foreach (var item in grid)
        {
            Collider2D[] col = Physics2D.OverlapCircleAll(new Vector2(item.transform.position.x, item.transform.position.y), GridSize.sqrMagnitude);

            item.GetComponent<Cell>().neighbors = new List<Cell>();
            foreach (var subitem in col)
            {
                item.GetComponent<Cell>().neighbors.Add(subitem.gameObject.GetComponent<Cell>());
            }
        }

    }


    public void ClearNodes()
    {
        foreach (var item in grid)
        {
            DestroyImmediate(item);
        }
        grid.Clear();
    }


    [SerializeField] private Cell TestStart;
    [SerializeField] private Cell TestEnd;
    [SerializeField] private List<Cell> TestResult;
    [SerializeField] private Sprite testSprite;
    public void TestPath()
    {
       
    TestResult=    Astar.FindPathActual(TestStart,TestEnd);
        foreach (var item in TestResult)
        {
            
            item.gameObject.AddComponent<SpriteRenderer>().sprite = testSprite;
        }
    }

}



[CustomEditor(typeof(MapGenerator))]
public class customButton : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        MapGenerator myScript = (MapGenerator)target;
        if (GUILayout.Button("Generate  NavMesh2D"))
        {
            myScript.GenerateButton();
        }
        if (GUILayout.Button("Clear Grid"))
        {
            myScript.ClearNodes();
        }
        if (GUILayout.Button("Test Generated Path"))
        {
            myScript.TestPath();
        }
    }

}