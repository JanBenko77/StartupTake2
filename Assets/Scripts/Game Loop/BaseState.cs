using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState : MonoBehaviour
{
    public abstract void Enter();
    public abstract void Exit();
    public abstract void Update();
    public virtual IEnumerator EnterCoroutine() { yield break; }
    public virtual bool stateIsOver { get; set; }
}
