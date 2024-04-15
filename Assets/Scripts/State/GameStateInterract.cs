using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateInterract : AGameState
{
    InterractManager m_Manager;

    public GameStateInterract(GameStateManager attachedBehavior) : base(attachedBehavior, EGameState.Interract)
    {
        m_Manager = InterractManager.Instance;
    }

    public override void Enter()
    {
        base.Enter();
        Time.timeScale = 0f;
        Cursor.visible = true;
    }

    public override void Execute()
    {
        base.Execute();

        Debug.Log("IS INTERRACTING");

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(GameParameters.InputName.PLAYER_INTERRACT_KEY))
        {
            m_AttachedBehavior.ChangeState(EGameState.Run);
            return;
        }

        m_Manager.Execute();
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("EXIT INTERRACTING");
    }
}
