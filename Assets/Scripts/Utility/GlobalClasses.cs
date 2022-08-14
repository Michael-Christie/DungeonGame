using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using MC.Core;

public class StatData : BaseValueData
{
    [XmlIgnoreAttribute] public override int ValueIndex { get { return (int)statID; } }

    public GameConstants.Stats statID;

    [XmlIgnoreAttribute] public GameConstants.StatCategory category;
}

public class AdvancementData: BaseValueData
{
    [XmlIgnoreAttribute] public override int ValueIndex { get { return (int)advancementID; } }

    public GameConstants.Advancement advancementID;

    [XmlIgnoreAttribute] public string description;

    public bool hasClaimed;

    [XmlIgnoreAttribute] public Reward reward = null;
}

public class Reward
{
    public GameConstants.RewardType type;

    public int amount;
}
