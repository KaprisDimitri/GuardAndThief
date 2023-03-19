using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PathFinder
{
    public static Cell[] GetPath (Cell cellStart, Cell cellEnd, Cell[,] gridData)
    {
        Dictionary<Cell, Cell> cameFrom = new Dictionary<Cell, Cell>();
        List<Cell> frontier = new List<Cell>() { cellStart };
        Cell curentCell, neightbourCell;

        while (frontier.Count > 0)
        {
            curentCell = frontier[0];
            if (curentCell == cellEnd)
            {
                return BuildPath(cellStart,cellEnd, cameFrom);
            }

                if (curentCell.indexX > 0 &&
                    (neightbourCell = gridData[curentCell.indexX - 1, curentCell.indexY]).cost < int.MaxValue &&
                    !cameFrom.ContainsKey(neightbourCell))
                {
                    frontier.Add(neightbourCell);
                    cameFrom.Add(neightbourCell, curentCell);
                }
                if (curentCell.indexX < gridData.GetLength(0) - 1 &&
                    (neightbourCell = gridData[curentCell.indexX + 1, curentCell.indexY]).cost < int.MaxValue &&
                    !cameFrom.ContainsKey(neightbourCell))
                {
                    frontier.Add(neightbourCell);
                    cameFrom.Add(neightbourCell, curentCell);
                }
                if (curentCell.indexY > 0 &&
                    (neightbourCell = gridData[curentCell.indexX, curentCell.indexY - 1]).cost < int.MaxValue &&
                    !cameFrom.ContainsKey(neightbourCell))
                {
                    frontier.Add(neightbourCell);
                    cameFrom.Add(neightbourCell, curentCell);
                }
                if (curentCell.indexY < gridData.GetLength(1) - 1 &&
                    (neightbourCell = gridData[curentCell.indexX, curentCell.indexY + 1]).cost < int.MaxValue &&
                    !cameFrom.ContainsKey(neightbourCell))
                {
                    frontier.Add(neightbourCell);
                    cameFrom.Add(neightbourCell, curentCell);
                }

            frontier.RemoveAt(0);

        }


        return new Cell[0];
    }

    private static Cell[] BuildPath(Cell cellStart, Cell cellEnd, Dictionary<Cell, Cell> cameFrom)
    {
        List<Cell> path = new List<Cell> { cellEnd };
        Cell currentCell = cellEnd;
        while(currentCell != cellStart)
        {
            currentCell = cameFrom[currentCell];
            path.Add(currentCell);
        }
        path.Reverse();
        return path.ToArray();
    }

    public static Cell[] GetPathDijkstra(Cell cellStart, Cell cellEnd, Cell[,] gridData)
    {
        Dictionary<Cell, Cell> cameFrom = new Dictionary<Cell, Cell>();
        List<Cell> frontier = new List<Cell>() { cellStart };
        Cell curentCell, neightbourCell;
        int bestIndex;
        while (frontier.Count > 0)
        {
            bestIndex = GetBestIndex(frontier.ToArray(), cellEnd);
            if (bestIndex == -1) break;
            curentCell = frontier[bestIndex];

            if (curentCell == cellEnd)
            {
                return BuildPath(cellStart, cellEnd, cameFrom);
            }

            if (curentCell.indexX > 0 &&
                (neightbourCell = gridData[curentCell.indexX - 1, curentCell.indexY]).cost < int.MaxValue &&
                !cameFrom.ContainsKey(neightbourCell))
            {
                frontier.Add(neightbourCell);
                cameFrom.Add(neightbourCell, curentCell);
            }
            if (curentCell.indexX < gridData.GetLength(0) - 1 &&
                (neightbourCell = gridData[curentCell.indexX + 1, curentCell.indexY]).cost < int.MaxValue &&
                !cameFrom.ContainsKey(neightbourCell))
            {
                frontier.Add(neightbourCell);
                cameFrom.Add(neightbourCell, curentCell);
            }
            if (curentCell.indexY > 0 &&
                (neightbourCell = gridData[curentCell.indexX, curentCell.indexY - 1]).cost < int.MaxValue &&
                !cameFrom.ContainsKey(neightbourCell))
            {
                frontier.Add(neightbourCell);
                cameFrom.Add(neightbourCell, curentCell);
            }
            if (curentCell.indexY < gridData.GetLength(1) - 1 &&
                (neightbourCell = gridData[curentCell.indexX, curentCell.indexY + 1]).cost < int.MaxValue &&
                !cameFrom.ContainsKey(neightbourCell))
            {
                frontier.Add(neightbourCell);
                cameFrom.Add(neightbourCell, curentCell);
            }

            frontier.RemoveAt(0);

        }


        return new Cell[0];
    }

    private static int GetBestIndex (Cell[] frontier, Cell endCell)
    {
        float bestHeuristic = int.MaxValue;
        float currentHeuristic;
        int bestIndex = -1;

        for(int i = 0; i< frontier.Length; i++)
        {
            currentHeuristic = frontier[i].GetHeuristicCoastFromStart(endCell.position);
            if(currentHeuristic < bestHeuristic)
            {
                bestHeuristic = currentHeuristic;
                bestIndex = i;
            }
        }

        return bestIndex;
    }

    public static Cell[] GetPathAStart(Cell cellStart, Cell cellEnd, Cell[,] gridData)
    {
        Dictionary<Cell, Cell> cameFrom = new Dictionary<Cell, Cell>();
        List<Cell> frontier = new List<Cell>() { cellStart };
        Cell curentCell, neightbourCell;
        int bestIndex;
        while (frontier.Count > 0)
        {
            bestIndex = GetBestIndexAStart(frontier.ToArray(), cellEnd);
            if (bestIndex == -1) break;
            curentCell = frontier[bestIndex];

            if (curentCell == cellEnd)
            {
                return BuildPath(cellStart, cellEnd, cameFrom);
            }

            if (curentCell.indexX > 0 &&
                (neightbourCell = gridData[curentCell.indexX - 1, curentCell.indexY]).cost < int.MaxValue &&
                !cameFrom.ContainsKey(neightbourCell))
            {
                neightbourCell.heuristicCostFromStart = curentCell.heuristicCostFromStart + 1;
                frontier.Add(neightbourCell);
                cameFrom.Add(neightbourCell, curentCell);
            }
            if (curentCell.indexX < gridData.GetLength(0) - 1 &&
                (neightbourCell = gridData[curentCell.indexX + 1, curentCell.indexY]).cost < int.MaxValue &&
                !cameFrom.ContainsKey(neightbourCell))
            {
                neightbourCell.heuristicCostFromStart = curentCell.heuristicCostFromStart + 1;
                frontier.Add(neightbourCell);
                cameFrom.Add(neightbourCell, curentCell);
            }
            if (curentCell.indexY > 0 &&
                (neightbourCell = gridData[curentCell.indexX, curentCell.indexY - 1]).cost < int.MaxValue &&
                !cameFrom.ContainsKey(neightbourCell))
            {
                neightbourCell.heuristicCostFromStart = curentCell.heuristicCostFromStart + 1;
                frontier.Add(neightbourCell);
                cameFrom.Add(neightbourCell, curentCell);
            }
            if (curentCell.indexY < gridData.GetLength(1) - 1 &&
                (neightbourCell = gridData[curentCell.indexX, curentCell.indexY + 1]).cost < int.MaxValue &&
                !cameFrom.ContainsKey(neightbourCell))
            {
                neightbourCell.heuristicCostFromStart = curentCell.heuristicCostFromStart + 1;
                frontier.Add(neightbourCell);
                cameFrom.Add(neightbourCell, curentCell);
            }

            frontier.RemoveAt(bestIndex);

        }


        return new Cell[0];
    }

    private static int GetBestIndexAStart(Cell[] frontier, Cell endCell)
    {
        float bestHeuristic = int.MaxValue;
        float currentHeuristic;
        int bestIndex = -1;

        for (int i = 0; i < frontier.Length; i++)
        {
            currentHeuristic = frontier[i].GetHeuristicCoastFromStartAStar(endCell.position);
            if (currentHeuristic < bestHeuristic)
            {
                bestHeuristic = currentHeuristic;
                bestIndex = i;
            }
        }

        return bestIndex;
    }

}
