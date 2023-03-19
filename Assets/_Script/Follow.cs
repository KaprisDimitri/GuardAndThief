using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField]
    private EventSO _eventArriveToDestination;
    private Transform _transformToFollow;
    public void StartFollow (Transform transformToFollow)
    {
        _eventArriveToDestination.doAction += ArriveToDestination;
        _transformToFollow = transformToFollow;
        GetComponent<Agent>().SetDestionation(_transformToFollow.position);
    }

    private void ArriveToDestination ()
    {
        if (GetComponent<Animator>().GetBool("SeeTarget"))
        {
            GetComponent<Agent>().SetDestionation(_transformToFollow.position);
            return;
        }
        GetComponent<Animator>().SetInteger("WayPointsToGo", GetComponent<Animator>().GetInteger("WayPointsToGo") - 1);
    }

    public void StopFollow ()
    {
        _eventArriveToDestination.doAction -= ArriveToDestination;
    }
}
