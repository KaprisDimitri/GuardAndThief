using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    [SerializeField]
    private Transform _hand;
    private GameObject _gameObjectGrab;

    public void GrabObject (GameObject gameObjectToGrab)
    {
        _gameObjectGrab = gameObjectToGrab;
        _gameObjectGrab.transform.parent = _hand;
        _gameObjectGrab.transform.position = _hand.position;
        if(_gameObjectGrab.TryGetComponent( out Block block))
        {
            block.BlockObject(true);
        }
    }

    public void Release ()
    {
        
        if (_gameObjectGrab != null)
        {
            if (_gameObjectGrab.TryGetComponent(out Block block))
            {
                block.BlockObject(false);
            }
            Debug.Log("je reales");
            _gameObjectGrab.transform.parent = null;
            _gameObjectGrab = null;
        }
        
    }
}
