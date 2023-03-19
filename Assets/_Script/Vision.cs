using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : MonoBehaviour
{
    [SerializeField]
    private LayerMask _layerToDetect;
    [SerializeField]
    private float _angle;
    [SerializeField]
    private float distance;
    [SerializeField]
    private float precision;
    private Transform detected;
    public Transform GetDetected { get { return detected; } }



    // Update is called once per frame
    void Update()
    {
        detected = Detection();
        if(detected!= null)
        {
            GetComponent<Animator>().SetFloat("DistToTarget", Vector3.Distance(transform.position, detected.position));
            GetComponent<Animator>().SetBool("SeeTarget", true);
        }
        else
        {
            GetComponent<Animator>().SetFloat("DistToTarget", 1000);
            GetComponent<Animator>().SetBool("SeeTarget", false);
        }
    }

    private Transform Detection()
    {
        for (int i = 0; i < precision; i++)
        {
            float angle = i * (_angle / precision) - _angle / 2 - transform.rotation.eulerAngles.z;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector3(distance * Mathf.Sin(Mathf.Deg2Rad * angle), distance * Mathf.Cos(Mathf.Deg2Rad * angle)), distance, _layerToDetect);
            if (hit.collider != null)
            {

                Debug.Log("Toucher" + hit.transform.gameObject.name);
                return hit.transform;
            }
        }

        return null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        for (int i = 0; i < precision; i++)
        {
            float angle = i * (_angle / precision) - _angle / 2 - transform.rotation.eulerAngles.z;
            Gizmos.DrawLine(transform.position, (new Vector3(distance * Mathf.Sin(Mathf.Deg2Rad * angle), distance * Mathf.Cos(Mathf.Deg2Rad * angle)) + transform.position));// Quaternion.Euler(0, 0, i) * transform.position );
        }
    }
}
