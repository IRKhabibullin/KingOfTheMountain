using UnityEngine;
using System.Collections.Generic;

public class GameStateAttribute : PropertyAttribute
{
    public List<string> states;

    public GameStateAttribute()
    {
        states = GameStateMachine.Instance.gameStates;
    }
}
