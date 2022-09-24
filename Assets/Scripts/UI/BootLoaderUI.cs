using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using DG.Tweening;
using MC.Core;

public class BootLoaderUI : MonoBehaviour
{
    [SerializeField] private GameObject video;

    [SerializeField] private Slider loadingBar;

    [SerializeField] private TextMeshProUGUI txtLoadingProgress;

    [SerializeField] private CanvasGroup fadeComponent;

    //
    private void Start()
    {
        CoreCallback.Instance.showLoadingScene += ShowLoadingScreen;
        CoreCallback.Instance.updateLoadPercentage += UpdateLoadingPercentage;
    }

    private void ShowLoadingScreen(bool _shouldShow, int _sceneID)
    {
        if (_shouldShow)
        {
            video.SetActive(true);
            fadeComponent.DOFade(1, GameConstants.Animations.fadeTimeShort);
        }
        else
        {
            fadeComponent.DOFade(0, GameConstants.Animations.fadeTimeShort)
                .OnComplete(
                    delegate 
                    {
                        video.SetActive(false);
                    });
        }
    }

    private void UpdateLoadingPercentage(float _value)
    {
        loadingBar.value = _value;
        txtLoadingProgress.text = $"{(_value * 100).ToString("N0")}%";
    }
}
