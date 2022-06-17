using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttack
{
    Transform transform { get; }
    int Damage { get; }
}
