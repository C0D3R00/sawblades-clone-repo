using UnityEngine;

using System.Collections;
using System.Collections.Generic;

public interface IGameEventListener<T>
{
    void OnEventRaised(T item);
}
