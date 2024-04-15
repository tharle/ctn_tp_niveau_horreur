using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterractItemController : MonoBehaviour
{
    [SerializeField] private InterractItemScriptObject m_InterractItem;
    public bool IsInterracted = false;
    public EInterractItemType TypeId { get => m_InterractItem.Item.TypeId; }
    public EKeyType KeyId { get => m_InterractItem.Item.KeyId; }
    public string Description { get 
        {
            if (IsInterracted) return m_InterractItem.Item.ResultMessage;
            return m_InterractItem.Item.Description;
        }  
    }

    InterractManager m_Manager;

    private void Start()
    {
        m_Manager = InterractManager.Instance;
    }

    
    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log($"OBJECT {name} is IN {collider.tag}");
        if (collider.CompareTag(GameParameters.TagName.PLAYER)) m_Manager.InterractWithPlayerEnter(this);
    }

    private void OnTriggerExit(Collider collider)
    {
        Debug.Log($"OBJECT {name} is OUT {collider.tag}");
        if (collider.CompareTag(GameParameters.TagName.PLAYER)) m_Manager.InterractWithPlayerExit();

    }
}
