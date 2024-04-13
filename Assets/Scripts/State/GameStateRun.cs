using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateRun : AGameState
{
    private PlayerMove m_PlayerMove;
    private FlashLightManager m_FlashLightManager;

    public GameStateRun(GameStateManager attachedBehavior) : base(attachedBehavior, EGameState.Run)
    {
        m_PlayerMove = PlayerMove.Instance;
        m_FlashLightManager = FlashLightManager.Instance;
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

        m_PlayerMove.Execute();
        m_FlashLightManager.Execute();

    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("EXIT DOWN");
    }
}
