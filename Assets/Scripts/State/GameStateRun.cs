using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateRun : AGameState
{
    private PlayerController m_Controller;

    public GameStateRun(GameStateManager attachedBehavior) : base(attachedBehavior, EGameState.Run)
    {
        m_Controller = PlayerController.Instance;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("ENTER GAME");
        Time.timeScale = 1.0f;
        Cursor.visible = false;
    }

    public override void Execute()
    {
        base.Execute();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            m_AttachedBehavior.ChangeState(EGameState.PauseMenu);
        }

        m_Controller.Execute();
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("EXIT DOWN");
    }
}
