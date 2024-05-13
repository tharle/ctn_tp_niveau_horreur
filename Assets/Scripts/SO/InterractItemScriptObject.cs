using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public enum EInterractItemType
{
    None,
    Key,
    Door,
    RecharcheFlashLight
}

public enum EKeyType
{
    None,
    Fuel,
    B,
    Gerator,
    Win
}

[Serializable]
public struct InterractItem
{
    public string Name;
    public string Description;
    public string ResultMessage;
    public EInterractItemType TypeId;
    public EKeyType KeyId;
}

[CreateAssetMenu]
public class InterractItemScriptObject : ScriptableObject
{
    public InterractItem Item;
}
