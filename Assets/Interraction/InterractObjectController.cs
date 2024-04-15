using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct InterractObject
{
    public string Name;
    public string Description;
}

public class InterractObjectController : MonoBehaviour
{
    [SerializeField] private InterractObject m_ObjectInterraction;
    public InterractObject InterractObjectValue { get => m_ObjectInterraction; }

    InterractManager m_Manager;

    private void Start()
    {
        m_Manager = InterractManager.Instance;
    }

    
    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log($"ON COLLIDE ENTER {collider.tag}");
        if (collider.CompareTag(GameParameters.TagName.PLAYER)) m_Manager.InterractWithPlayerEnter(m_ObjectInterraction);
    }

    private void OnTriggerExit(Collider collider)
    {
        Debug.Log($"ON COLLIDE EXIT {collider.tag}");
        if (collider.CompareTag(GameParameters.TagName.PLAYER)) m_Manager.InterractWithPlayerExit();

    }

    public void Interract()
    {
        Debug.Log(m_ObjectInterraction.Description);
    }

}
