using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EGameState
{
    PauseMenu,
    Run,
    Interract,
    PauseMenuContinue
}
public abstract class AGameState
{
    protected GameStateManager m_AttachedBehavior;
    private EGameState m_GameStateId;
    public EGameState StateId { get => m_GameStateId; }

    public AGameState(GameStateManager attachedBehavior, EGameState stateId)
    {
        m_AttachedBehavior = attachedBehavior;
        m_GameStateId = stateId;
    }

    public virtual void Enter()
    {
        // Trigger le event de "enter state" dans le event system
        GameStateEvent.Instance.Call(m_GameStateId, true);
    }

    public virtual void Execute(){}

    public virtual void Exit()
    {
        // Trigger le event de "exit state" dans le event system
        GameStateEvent.Instance.Call(m_GameStateId, false);
    }


}
