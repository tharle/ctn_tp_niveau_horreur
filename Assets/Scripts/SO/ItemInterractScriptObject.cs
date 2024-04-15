using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public enum ETypeItemInterract
{
    NONE
}

public struct ItemInterract
{
    public string Name;
    public string Description;
    public ETypeItemInterract Type;

}

public struct ConditionToUnlock 
{ 

}

[CreateAssetMenu]
public class ItemInterractScriptObject : ScriptableObject
{
    
}
