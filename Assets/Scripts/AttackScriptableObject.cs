using Platformer.Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/AttackForm", order = 1)]
public class AttackScriptableObject : ScriptableObject
{
    public AttackObject[] attackObjects;
}
