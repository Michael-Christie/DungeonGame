using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewClass", menuName = "ScriptableObjects/Class")]
public class PlayerClass : ScriptableObject
{
    [SerializeField] private int health;

    [SerializeField] private float speed;
    [SerializeField] private float strength;
    [SerializeField] private float defence;

    public int MaxHealth { get { return health; } }

    public float Speed { get { return speed; } }
    public float Strength { get { return strength; } }
    public float Defence { get { return defence; } }
}
