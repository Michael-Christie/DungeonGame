using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class AdvancementManager : BaseValueTracker
{
    public AdvancementManager Instance { get; private set; }

    // 
    private void Awake()
    {
        Instance = this;
    }
}
