using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateLose : AGameState
{
    public GameStateLose(GameStateManager attachedBehavior) : base(attachedBehavior, EGameState.Lose)
    {
    }

    public override void Enter()
    {
        Time.timeScale = 0f;
        Cursor.visible = true;
    }
}
