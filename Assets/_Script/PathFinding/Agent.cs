using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    [SerializeField]
    private Cell[] _path;
    [SerializeField]
    private EventSO _eventGuardArriveToDestination;
    [SerializeField]
    private float _agentRaduis;
    [SerializeField]
    private float steerForce;
    [SerializeField]
    private float _speed;
    public float SetSpeed { set { _speed = value; } }

    private int _pathIndex;
    private bool _isMoving;

    private Vector3 GetTargetPostion
    {
        get
        {
            if(_path.Length <= 0)
            {
                return transform.position;
            }
            else
            {
                return _path[_path.Length - 1].position;
            }
        }
    }

    private Vector2 _velocity;

    private void Update()
    {
        MoveAgent();
    }

    public void SetDestionation(Vector2 destination)
    {
        _path = DataGrid.Instance.GetPath(transform.position, destination);
        if (_path.Length > 1)
        {
            _pathIndex = 1;
            _isMoving = true;
        }
        else
        {
            _isMoving = false;
        }
    }

    private void MoveAgent ()
    {
        if(_isMoving)
        {
            Vector3 prevuisPosition = _path[_pathIndex - 1].position + DataGrid.offset;
            Vector3 nextPostion = _path[_pathIndex].position + DataGrid.offset;

            if (Vector3.Distance(transform.position, GetTargetPostion) <= _agentRaduis)
            {
                StopAgent();
            }
            else
            {
                if(Vector2.Distance(transform.position, nextPostion) <= _agentRaduis)
                {
                    _pathIndex++;
                    if(_pathIndex >= _path.Length)
                    {
                        StopAgent();
                        return;
                    }
                    prevuisPosition = _path[_pathIndex - 1].position + DataGrid.offset;
                    nextPostion = _path[_pathIndex].position + DataGrid.offset;
                }

                Vector3 predictedPostion = (Vector2)transform.position + _velocity.normalized;
                Vector3 normalPoint = GetNormalPoint(predictedPostion, nextPostion, prevuisPosition);
                Vector2 dir = (nextPostion - prevuisPosition).normalized;
                Vector3 targetPosition = normalPoint;
                if (PointContainedInSegment(predictedPostion, nextPostion, targetPosition))
                    targetPosition = nextPostion;

                float distance = Vector2.Distance(predictedPostion, normalPoint);
                float scalarProduct = Vector3.Dot(_velocity.normalized, dir);
                if(distance > _agentRaduis || scalarProduct == -1 || _velocity == Vector2.zero)
                {
                    Seek(targetPosition);
                }
                
                _velocity = _velocity.normalized * _speed;
                transform.position += (Vector3)_velocity * Time.deltaTime;
                transform.rotation = Quaternion.LookRotation(transform.forward, _velocity.normalized);


            }
        }
    }

    private void Seek (Vector2 target)
    {
        Vector2 targetVelocity = (target - (Vector2)transform.position).normalized * _speed;
        Vector2 steer = (targetVelocity - _velocity) * steerForce;
        steer *= Time.deltaTime;
        _velocity += steer;
    }

    private void StopAgent()
    {
        Debug.Log("fini 2");
        _isMoving = false;
        _path = new Cell[0];
        _pathIndex = 0;
        _eventGuardArriveToDestination.doAction?.Invoke();
    }

    private Vector2 GetNormalPoint(Vector2 predictefPosition, Vector2 nexPostion, Vector2 prevuisPosition)
    {
        Vector2 ap = predictefPosition - prevuisPosition;
        Vector2 ab = (nexPostion - prevuisPosition).normalized;
        Vector2 ah = ab * Vector2.Dot(ap,ab);
        Vector2 normal = (prevuisPosition * ah);
        Vector2 min = Vector2.Min(prevuisPosition, nexPostion);
        Vector2 max = Vector2.Max(prevuisPosition, nexPostion);

        if(normal.x < min.x || normal.y < min.y || normal.x > max.x || normal.y > max.y)
        {
            return nexPostion;
        }
        return normal;
    }
    
    private bool PointContainedInSegment (Vector2 firstSegmentPoint, Vector2 secondSegmentPoint, Vector2 comparePoint)
    {
        Vector2 ab = secondSegmentPoint - firstSegmentPoint;
        Vector2 ac = comparePoint - firstSegmentPoint;

        if (IsColinear(ab, ac))
            return false;
        if (Vector2.Dot(ab, ac) < 0)
                return false;
        if (Vector2.Dot(ab, ac) > ab.magnitude)
            return false;

        return true;
    }

    private bool IsColinear(Vector2 ab, Vector2 ac)
    {
        return ab.x * ac.y - ac.x * ab.y == 0;
    }


    private void OnDrawGizmos()
    {
        if(_isMoving)
        {
            Gizmos.color = Color.green;
            for(int i = 0; i<_path.Length-2; i++)
            {
                Gizmos.DrawLine(_path[i].position + DataGrid.offset, _path[i + 1].position + DataGrid.offset);
            }
        }
    }

    IEnumerator CoroutineChangeDestination ()
    {
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(CoroutineChangeDestination());
    }


}
