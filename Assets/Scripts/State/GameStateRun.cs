using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateRun : AGameState
{
    private PlayerMoveManager m_PlayerMove;
    private FlashLightManager m_FlashLightManager;
    private LookAtManager m_LookAtManager;

    public GameStateRun(GameStateManager attachedBehavior) : base(attachedBehavior, EGameState.Run)
    {
        m_PlayerMove = PlayerMoveManager.Instance;
        m_FlashLightManager = FlashLightManager.Instance;
        m_LookAtManager = LookAtManager.Instance;
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

        m_PlayerMove.Execute();
        m_FlashLightManager.Execute();
        m_LookAtManager.Execute();
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("EXIT DOWN");
    }
}
