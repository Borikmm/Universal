using Patterns.EventBus;
using UnityEngine;



// Просто содержит в себе EventBus
public class EventBusHolder : MonoBehaviour
{
    public EventBus EventBus {  get; private set; }


    private void Awake()
    {
        EventBus = new EventBus();
    }
}
