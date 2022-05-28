using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PlayerData 
{
    public GameConstants.CharacterTypes charType;

    public int coin;

    public StatData[] stats;
}
