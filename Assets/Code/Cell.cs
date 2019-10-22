using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]




public class Cell : MonoBehaviour
{
    public string Name;
    public List<Cell> neighbors;
    public Vector3 Position { get { return transform.position; } }
    public Cell parentNode;


  public  float F, G, H;
}
