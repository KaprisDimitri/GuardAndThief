using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRandom : MonoBehaviour
{
    [SerializeField]
    private EventSO _eventArriveToDestination;

    private Animator _animator;

    private Vector3 _wayPoint;
    private int _actualWayPoint;

    private Agent _agent;

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

    public void StartMovement()
    {
        _eventArriveToDestination.doAction += ArriveToWayPoint;

        ArriveToWayPoint();
    }

    private void ArriveToWayPoint()
    {
        
            GoToWayPoint();
        
    }

    private void GoToWayPoint()
    {
        _agent.SetDestionation(DataGrid.Instance.GetRandomDestionation());
    }

    public void FinishMovement()
    {
        _eventArriveToDestination.doAction -= ArriveToWayPoint;
    }
}
