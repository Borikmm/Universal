// Base Event
public interface IEvent { }


// Base event receiver
public interface IBaseEventReceiver { }


// Event receiver
public interface IEventReceiver<T> : IBaseEventReceiver where T : struct, IEvent 
{
    void OnEvent(T @event);
}

