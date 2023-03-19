using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToWayPoint : MonoBehaviour
{
    [SerializeField]
    private EventSO _eventArriveToDestination;

    private Animator _animator;

    private Vector3[] _wayPoints;
    private int _actualWayPoint;

    private Agent _agent;
    private Action _finishMovement;

    private void Awake()
    {
        _agent = GetComponent<Agent>();
        _animator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        //_eventArriveToDestination.doAction += ArriveToWayPoint;
    }
    private void OnDisable()
    {
        //_eventArriveToDestination.doAction -= ArriveToWayPoint;
    }

    public void StartMovement(Vector3[] wayPoints)
    {
        _eventArriveToDestination.doAction += ArriveToWayPoint;
        _wayPoints = wayPoints;
        //_finishMovement = finishMovement;
        _actualWayPoint = 0;
        GoToWayPoint();
    }

    private void ArriveToWayPoint()
    {
        _actualWayPoint++;
        _animator.SetInteger("WayPointsToGo", _animator.GetInteger("WayPointsToGo") - 1);
        if (_actualWayPoint >= _wayPoints.Length)
        {
            Debug.Log("fini");
            return;
        }
        GoToWayPoint();
    }

    private void GoToWayPoint()
    {
        _agent.SetDestionation(_wayPoints[_actualWayPoint]);
    }

    public void FinishMovement()
    {
        _eventArriveToDestination.doAction -= ArriveToWayPoint;
    }
}
