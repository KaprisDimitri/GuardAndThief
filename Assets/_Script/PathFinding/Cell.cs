using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Cell
{
    public int indexX, indexY;
    public Vector3Int position;
    public int cost = 1;
    public float heuristicCostFromStart = 0f;

    public float GetHeuristicCoastFromStart (Vector3Int end)
    {
        return Vector3Int.Distance(position, end) + cost;
    }

    public float GetHeuristicCoastFromStartAStar(Vector3Int end)
    {
        return Vector3Int.Distance(position, end) + cost + heuristicCostFromStart;
    }
}
