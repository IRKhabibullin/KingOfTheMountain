using UnityEngine;
using UnityEngine.Events;

public class StateTransition : MonoBehaviour
{
    [GameState] [SerializeField] public string fromState;
    [GameState] [SerializeField] public string toState;
    [SerializeField] public UnityEvent stateChanged;

    public void Activate()
    {
        stateChanged?.Invoke();
    }
}
