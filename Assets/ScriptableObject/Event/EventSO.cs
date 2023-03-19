using System;
using UnityEngine;

[CreateAssetMenu(fileName ="EventSO", menuName ="Event/EventSO")]
public class EventSO : ScriptableObject
{
    public Action doAction;
}
