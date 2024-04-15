using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterractManager : MonoBehaviour
{

    private InterractObject m_InterractionObject;

    private bool m_IsCollidigWithPlayer = false;
    public bool IsCollidigWithPlayer { get => m_IsCollidigWithPlayer; }


    public event Action<string> OnInterractObjectShow;
    public event Action OnInterractObjectClose;


    private static InterractManager m_Instance;
    public static InterractManager Instance {

        get { 
            if (m_Instance == null)
            {
                GameObject go = new GameObject("InterractManager");
                m_Instance =  go.AddComponent<InterractManager>();
                DontDestroyOnLoad(go);
            }
            return m_Instance;
        }
    }
    private void Awake()
    {
        if (m_Instance != null && m_Instance != this) Destroy(gameObject);

        m_Instance = this;
    }

    private void Start()
    {
        SubscribeAllEvents();
    }

    private void SubscribeAllEvents()
    {
        GameStateEvent.Instance.SubscribeTo(EGameState.Interract, OnInterract);
    }

    private void OnInterract(bool entering)
    {
        if (entering)
        {
            OnInterractObjectShow?.Invoke(m_InterractionObject.Description);
        }
        else
        {
            OnInterractObjectClose?.Invoke();
        }
    }

    public void InterractWithPlayerEnter(InterractObject interractionObject)
    {
        m_InterractionObject = interractionObject;
        m_IsCollidigWithPlayer = true;
    }

    public void InterractWithPlayerExit()
    {
        m_IsCollidigWithPlayer = false;
    }

    public void Execute()
    {
    }
}
