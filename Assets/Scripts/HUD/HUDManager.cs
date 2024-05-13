using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI m_TextInfoValue;
    [SerializeField] GameObject m_TextInfo;
    [SerializeField] GameObject m_ToInterractToolTip;

    [SerializeField] Slider m_HPBar;
    [SerializeField] Slider m_SPBar;
    [SerializeField] Slider m_LightBar;

    InterractManager m_InterractManager;

    PlayerController m_PlayerController;

    private void Start()
    {
        m_InterractManager = InterractManager.Instance;
        m_PlayerController = PlayerController.Instance;
        SubscribeAllEvent();
    }

    private void SubscribeAllEvent()
    {
        m_InterractManager.OnInterractItemShow += OnInterractObjectShow;
        m_InterractManager.OnInterractObjectClose += OnInterractObjectClose;
        m_InterractManager.OnInterractItemToolTip += OnToggleInterractToolTip;

        m_PlayerController.OnChangeHP += OnChangeHP;
        m_PlayerController.OnChangeSP += OnChangeSP;
        m_PlayerController.OnChangeLightDuration += OnChangeLightDuration;
    }

    private void OnInterractObjectShow(string texte)
    {
        m_TextInfoValue.text = texte;
        m_TextInfo.SetActive(true);
    }

    private void OnInterractObjectClose()
    {
        m_TextInfo.SetActive(false);
    }

    private void OnToggleInterractToolTip(bool show)
    {
        m_ToInterractToolTip.SetActive(show);
    }

    private void OnChangeHP(float ratio)
    {
        m_HPBar.value = ratio;
    }

    private void OnChangeSP(float ratio)
    {
        m_SPBar.value = ratio;
    }

    private void OnChangeLightDuration(float ratio)
    {
        m_LightBar.value = ratio;
    }


    public void OnClickBtnInterractObjectClose()
    {
        PlayerController.Instance.CloseTextInfo();
    }
}
