using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float _timeLeft;
    public void StartTimer (float timeToWait)
    {
        _timeLeft = timeToWait;
        StartCoroutine(CoroutineTimer());
    }

    public void StopTimer ()
    {
        StopCoroutine(CoroutineTimer());
    }

    IEnumerator CoroutineTimer ()
    {
        yield return new WaitForSeconds(0.1f);
        _timeLeft -= 0.1f;
        GetComponent<Animator>().SetFloat("TimeLeft", _timeLeft);
        if(_timeLeft > 0)
        {
            StartCoroutine(CoroutineTimer());
        }
    }

}
