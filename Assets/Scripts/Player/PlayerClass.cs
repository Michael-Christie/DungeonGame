using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewClass", menuName = "ScriptableObjects/Class")]
public class PlayerClass : ScriptableObject
{
    [SerializeField, Range(1, 200)] private int health;

    [SerializeField, Range(0.1f, 2f)] private float speed;
    [SerializeField, Range(0.1f, 2f)] private float meleeDamage;
    [SerializeField, Range(0.1f, 2f)] private float rangedDamage;
    [SerializeField, Range(0.1f, 2f)] private float defence;

    [SerializeField] private WeaponData defaultWeapon;

    public int Health { get { return health; } }

    public float Speed { get { return speed; } }
    public float MeleeDamage { get { return meleeDamage; } }
    public float RangedDamage { get { return rangedDamage; } }
    public float Defence { get { return defence; } }

    public WeaponData DefaultWeapon { get { return defaultWeapon; } }
}
