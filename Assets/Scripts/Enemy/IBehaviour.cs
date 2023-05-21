using UnityEngine;

public interface IBehaviour
{
    public void Awake();
    public void Enter();
    public void DoBeh();
    public void Exit();
    public int Condition();
}
