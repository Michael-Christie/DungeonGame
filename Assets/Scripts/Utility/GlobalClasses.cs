using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class StatData
{
    public GameConstants.Stats statID;

    [XmlIgnoreAttribute] public GameConstants.StatCategory category;

    [XmlIgnoreAttribute] public string displayName;

    public float value;
}
