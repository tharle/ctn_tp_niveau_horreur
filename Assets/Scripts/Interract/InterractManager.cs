using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterractManager : MonoBehaviour
{

    private InterractItemController m_InterractItemController;

    private bool m_IsCollidigWithPlayer = false;
    public bool IsCollidigWithPlayer { get => m_IsCollidigWithPlayer; }


    public event Action<string> OnInterractItemShow;
    public event Action OnInterractObjectClose;
    public event Action OnRecharcheFlashLight;
    public event Action<bool> OnInterractItemToolTip;


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
        if (m_InterractItemController == null) return;

        if (entering)
        {
            if (m_InterractItemController.TypeId == EInterractItemType.Door)
            {
                if (PlayerController.Instance.HasKeyInInvetory(m_InterractItemController.KeyId))
                {
                    m_InterractItemController.IsInterracted = true;
                }

                if(m_InterractItemController.KeyId == EKeyType.Fuel) PlayerController.Instance.AddKeyToInvetory(EKeyType.Gerator);
            }

            OnInterractItemShow?.Invoke(m_InterractItemController.Description);

            if (m_InterractItemController.TypeId == EInterractItemType.Key && !m_InterractItemController.IsInterracted)
            {
                PlayerController.Instance.AddKeyToInvetory(m_InterractItemController.KeyId);
                m_InterractItemController.IsInterracted = true;
            }
            else if (m_InterractItemController.TypeId == EInterractItemType.RecharcheFlashLight)
            {
                OnRecharcheFlashLight?.Invoke();
            }
                
        }
        else
        {
            if (m_InterractItemController.KeyId == EKeyType.Gerator) PlayerController.Instance.AddKeyToInvetory(EKeyType.Win);
            OnInterractObjectClose?.Invoke();
        }
    }

    public void InterractWithPlayerEnter(InterractItemController controller)
    {
        m_InterractItemController = controller;
        m_IsCollidigWithPlayer = true;
        OnInterractItemToolTip?.Invoke(m_IsCollidigWithPlayer);
    }

    public void InterractWithPlayerExit()
    {
        m_IsCollidigWithPlayer = false;
        m_InterractItemController = null;
        OnInterractItemToolTip?.Invoke(m_IsCollidigWithPlayer);
    }

    public void Execute()
    {
    }
}
