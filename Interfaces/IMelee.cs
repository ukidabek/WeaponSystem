using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMelee
{
    float Range { get; }
    Transform HitOrigin { get; set; }
}
