using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Used on Toggles, turns the base image off to stop overdrawing it once and helping draw calls
/// </summary>
public class ToggleImageSwitch : MonoBehaviour
{
    [SerializeField] private Image baseImage;

    [SerializeField] private Toggle toggle;

    //
    private void Start()
    {
        toggle.onValueChanged.AddListener(ToggleBaseState);
    }

    private void ToggleBaseState(bool _enabled)
    {
        baseImage.enabled = !_enabled;
    }
}
