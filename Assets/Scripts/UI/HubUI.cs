using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using DG.Tweening;

public class HubUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtCoutdown;
    

    public void ShowCoutdown(string _text)
    {
        txtCoutdown.text = _text;

        StartCoroutine(AnimateCoutdown());
    }

    private IEnumerator AnimateCoutdown()
    {
        txtCoutdown.transform.DOScale(1, GameConstants.Animations.scaleTimeShort);

        yield return GameConstants.WaitTimers.waitForOneSecond;

        txtCoutdown.transform.DOScale(0, GameConstants.Animations.scaleTimeShort);
    }
}
