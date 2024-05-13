using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatePauseMenu : AGameState
{
    public GameStatePauseMenu(GameStateManager attachedBehavior) : base(attachedBehavior, EGameState.PauseMenu)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Time.timeScale = 0f;
        Cursor.visible = true;
        GameStateEvent.Instance.SubscribeTo(EGameState.PauseMenuContinue, OnClickPauseMenuContinue);
    }

    private void OnClickPauseMenuContinue(bool enter)
    {
        m_AttachedBehavior.ChangeState(EGameState.Run);
    }

    public override void Execute()
    {
        base.Execute();

        Debug.Log("IS PauseMenu");

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnClickPauseMenuContinue(true);
        }
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("EXIT PauseMenu");
        GameStateEvent.Instance.UnsubscribeFrom(EGameState.PauseMenuContinue, OnClickPauseMenuContinue);
    }
}
