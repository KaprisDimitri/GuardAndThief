using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DataGrid : MonoBehaviour
{
    public static readonly Vector3 offset = Vector2.one * 0.5f;
    public static DataGrid Instance = null;

    [SerializeField]
    private Grid _grid;
    [SerializeField]
    private Tilemap _tileMap;
    [SerializeField]
    private CustomTileData[] _customTileData;
    [SerializeField]
    private Cell[,] _gridData;

    public Cell[,] GetGridData { get { return _gridData; } }
    public Grid GetGrid { get { return _grid; } }

    private void Awake()
    {
        Instance = this;
        GenerateDataGird();
    }

    private void GenerateDataGird ()
    {
        Vector3Int size = _tileMap.size;
        _gridData = new Cell[size.x, size.y];
        Vector3Int position = Vector3Int.zero;
        TileBase data;
        for(int y = 0; y < size.y; y++)
        {
            position.y = (-size.y / 2) + y;
            for(int x = 0; x < size.x; x++)
            {
                position.x = (-size.x / 2) + x;
                data = _tileMap.GetTile(position);

                if(data is null)
                {
                    _gridData[x, y] = new Cell()
                    {
                        indexX = x,
                        indexY = y,
                        position = position,
                        cost = int.MaxValue
                    };
                    continue;
                }
                for(int i = 0; i< _customTileData.Length; i++)
                {
                    if(data == _customTileData[i].GetTile)
                    {
                        _gridData[x, y] = new Cell()
                        {
                            indexX = x,
                            indexY = y,
                            position = position,
                            cost = _customTileData[i].GetCost,
                        };
                        break;
                    }
                }
            }
        }
    }

    public Cell[] GetPath (Vector2 startPostion, Vector2 endPosition)
    {
        Debug.Log(_gridData.GetLength(0));
        Debug.Log(_gridData.GetLength(1));
        Vector3Int startCellPosition = _grid.WorldToCell(startPostion);
        Vector3Int endCellPosition = _grid.WorldToCell(endPosition);
        Debug.Log(startCellPosition);
        Debug.Log(endCellPosition);

        Cell start = null;
        Cell end = null;

        for(int y = 0; y< _gridData.GetLength(1); y++)
        {
            for (int x = 0; x < _gridData.GetLength(0); x++)
            {
                if(startCellPosition == _gridData[x,y].position)
                {
                    start = _gridData[x, y];
                }
                if(endCellPosition == _gridData[x, y].position)
                {
                    end = _gridData[x, y];
                }
                if(start != null && end != null)
                {
                    break;
                }
            }
        }

        return PathFinder.GetPathAStart(start, end, _gridData);
    }

    public Vector3 GetRandomDestionation ()
    {
        do
        {
            Cell cell = _gridData[Random.Range(0, _gridData.GetLength(0)), Random.Range(0, _gridData.GetLength(1))];
            if(cell.cost < int.MaxValue)
            {
                return cell.position;
            }
        } while (true);
    }
}
