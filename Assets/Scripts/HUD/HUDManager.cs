using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI m_TextInfoValue;
    [SerializeField] GameObject m_TextInfo;

    InterractManager m_InterractManager;

    private void Start()
    {
        m_InterractManager = InterractManager.Instance;
        SubscribeAllEvent();
    }

    private void SubscribeAllEvent()
    {
        m_InterractManager.OnInterractObjectShow += OnInterractObjectShow;
        m_InterractManager.OnInterractObjectClose += OnInterractObjectClose;
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

    public void OnClickBtnInterractObjectClose()
    {
        PlayerController.Instance.CloseTextInfo();
    }
}
