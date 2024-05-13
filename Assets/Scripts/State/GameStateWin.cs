using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateWin : AGameState
{
    public GameStateWin(GameStateManager attachedBehavior) : base(attachedBehavior, EGameState.Win)
    {
    }

    public override void Enter()
    {
        Time.timeScale = 0f;
        Cursor.visible = true;
    }
}
