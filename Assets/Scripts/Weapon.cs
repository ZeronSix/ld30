﻿using UnityEngine;
using System.Collections;

public abstract class Weapon : MonoBehaviour
{
    public abstract void Shoot(Vector3 target);
}