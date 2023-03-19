using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "CustomTileData", menuName = "ScriptableObject/CustomTileData")]
public class CustomTileData : ScriptableObject
{
    [SerializeField]
    private Tile _tile;
    [SerializeField]
    private bool _isValideTile = true;
    [SerializeField]
    private int _cost;

    public int GetCost { get { return CalculateCostToReturn(); } }
    public bool GetIsValideTile { get { return _isValideTile; } }
    public Tile GetTile { get { return _tile; } }

    private int CalculateCostToReturn ()
    {
        if(!_isValideTile)
        {
            return int.MaxValue;
        }
        return _cost;
    }
}
