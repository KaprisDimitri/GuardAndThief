using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public void BlockObject (bool isBlock)
    {
        GetComponent<Animator>().SetBool("Block", isBlock);
        if(isBlock)
        {
            GetComponent<Agent>().SetDestionation(transform.position);
            GetComponent<Grab>().Release();
        }
    }


}
