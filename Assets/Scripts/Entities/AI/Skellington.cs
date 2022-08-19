using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Skellington : BaseEnemyAI
{
    public override int PoolID => (int)GameConstants.EntityID.BaseAI;

    public override int Health { get; set; } = 100;
}
