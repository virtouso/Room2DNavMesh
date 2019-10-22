using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public static class Astar
{
    private static List<Cell> RetracePath(Cell startNode, Cell endNode)
    {
        //Retrace the path, is basically going from the endNode to the startNode
        List<Cell> path = new List<Cell>();
        Cell currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            //by taking the parentNodes we assigned
            currentNode = currentNode.parentNode;
        }

        //then we simply reverse the list
        path.Reverse();

        return path;
    }





    public static List<Cell> FindPathActual(Cell start, Cell target)
    {
        //Typical A* algorythm from here and on
        if (!start || !target) { ColorizeLog.MoeenLog("start or end cant be null", ColorizeLog.Colors.DarkRed); return new List<Cell>(); }
        List<Cell> foundPath = new List<Cell>();

        //We need two lists, one for the nodes we need to check and one for the nodes we've already checked
        List<Cell> openSet = new List<Cell>();
        HashSet<Cell> closedSet = new HashSet<Cell>();

        //We start adding to the open set
        openSet.Add(start);

        while (openSet.Count > 0)
        {
            Cell currentNode = openSet[0];
          
            for (int i = 0; i < openSet.Count; i++)
            {
                //We check the costs for the current node
                //You can have more opt. here but that's not important now
                if (openSet[i].F < currentNode.F ||
                    (openSet[i].F == currentNode.F &&
                    openSet[i].H < currentNode.H))
                {
                    //and then we assign a new current node
                    if (!currentNode.Equals(openSet[i]))
                    {
                        currentNode = openSet[i];
                    }
                }
            }

            //we remove the current node from the open set and add to the closed set
            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            //if the current node is the target node
            if (currentNode.Equals(target))
            {
                //that means we reached our destination, so we are ready to retrace our path
                foundPath = RetracePath(start, currentNode);
                break;
            }

            //if we haven't reached our target, then we need to start looking the neighbours
            foreach (Cell neighbour in currentNode.neighbors)
            {
                if (!neighbour) continue;
                if (!closedSet.Contains(neighbour))
                {
                    //we create a new movement cost for our neighbours
                    float newMovementCostToNeighbour = currentNode.G + Vector3.Distance(currentNode.Position, neighbour.Position);

                    //and if it's lower than the neighbour's cost
                    if (newMovementCostToNeighbour < neighbour.G || !openSet.Contains(neighbour))
                    {
                     
                        //we calculate the new costs
                        neighbour.G = newMovementCostToNeighbour;
                        neighbour.H = Vector3.Distance(neighbour.Position, target.Position);
                        //Assign the parent node
                        neighbour.parentNode = currentNode;
                        //And add the neighbour node to the open set
                        if (!openSet.Contains(neighbour))
                        {
                            openSet.Add(neighbour);
                        }
                    }
                }
            }
        }

        //we return the path at the end
        return foundPath;
    }





















}
