using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newWeapon", menuName = "Items/New Weapon")]
public class WeaponData : ItemData
{
    public AnimationCurve stat;

    public int baseDamage;

    //
    public override void OnLeftClick()
    {
        Ray _ray = new Ray(PlayerController.Instance.PlayerCamera.transform.position, PlayerController.Instance.PlayerCamera.transform.forward);

        if (Physics.Raycast(_ray, out RaycastHit _hit, 10f, ~0)) 
        {
            _hit.collider.GetComponent<IDamageable>()?.OnDamageRecieved(Mathf.FloorToInt(baseDamage * PlayerController.Instance.playerClass.MeleeDamage));
        }
    }
}
