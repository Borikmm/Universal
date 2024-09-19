using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;


#region MainEvents

public readonly struct SwitchCameraLocation : IEvent
{
    public readonly Vector3 Position;

    public SwitchCameraLocation(Vector3 position)
    { 
        this.Position = position; 
    }
}

public readonly struct RightMouseButtonDown : IEvent
{
}

public readonly struct RightMouseButtonUp : IEvent
{
}

#endregion




#region testsEvents


// Тут приведены примеры событий. В данном случае события содержать в себе один параметр, но это необязательно.
public readonly struct RedEvent : IEvent
{
    public readonly Vector3 MoveDelta;

    public RedEvent(Vector3 moveDelta)
    {
        MoveDelta = moveDelta;
    }
}


public readonly struct BlueEvent : IEvent
{
    public readonly Color Color;

    public BlueEvent(Color color)
    {
        Color = color;
    }
}
#endregion
