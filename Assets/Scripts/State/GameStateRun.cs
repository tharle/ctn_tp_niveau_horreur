using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateRun : AGameState
{
    private PlayerMove m_PlayerMove;

    public GameStateRun(GameStateManager attachedBehavior) : base(attachedBehavior, EGameState.Run)
    {
        m_PlayerMove = PlayerMove.Instance;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("ENTER GAME");
        Time.timeScale = 1.0f;
    }

    public override void Execute()
    {
        base.Execute();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            m_AttachedBehavior.ChangeState(EGameState.PauseMenu);
        }

        m_PlayerMove.Move();

    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("EXIT DOWN");
    }
}
