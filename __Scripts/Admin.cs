using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Admin", menuName ="Admin")]
public class Admin : ScriptableObject
{
    [Range(0, 1000)]
    public float speed = 0;
    public GameObject prefPoint = null;
    public Material lineMaterial = null;
}
