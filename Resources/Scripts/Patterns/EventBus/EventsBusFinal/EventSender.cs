using UnityEngine;

namespace Assets.Scripts.EventBus.EventsBusFinal
{

    // Class for send events
    public class EventSender : MonoBehaviour
    {
        public enum TypeOfEvents
        {
            Red,
            Blue
        }



        [SerializeField] private TypeOfEvents _eventType;
        [SerializeField] private EventBusHolder _busHolder;

        private void OnMouseDown()
        {
            switch (_eventType)
            {
                case TypeOfEvents.Red:
                    // Тут мы говорим Event Bus что выбранное событие сработало, и нужно оповестить всех подписчиков
                    _busHolder.EventBus.Raise(new RedEvent(Vector3.one * 0.15f));
                    break;
                case TypeOfEvents.Blue:
                    _busHolder.EventBus.Raise(new BlueEvent(Color.blue * Random.Range(0f, 1f)));
                    break;
                default:
                    break;
            }
        }
    }
}
