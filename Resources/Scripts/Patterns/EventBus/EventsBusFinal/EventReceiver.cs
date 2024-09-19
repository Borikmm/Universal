using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;

namespace Assets.Scripts.EventBus.EventsBusFinal
{

    // Этот класс является получателем, обработчиком событий. Он реагирует на собития и выполняет свои действия при их срабатывании
    [RequireComponent(typeof(MeshRenderer))] // Просто рекомендуем иметь компонент на обьекте
    public class EventReceiver : MonoBehaviour, IEventReceiver<RedEvent>, IEventReceiver<BlueEvent> // Тут нужно наследоваться от тех событий, которые должны обрабатываться
    {

        [FormerlySerializedAs("_eventBus")] [SerializeField] private EventBusHolder _eventBusHolder;


        private MeshRenderer _meshRenderer;


        private void Start()
        {
            // Подписываемся на события. Говорим Event Bus что этот класс должен быть оповещен при срабатывании событий
            _eventBusHolder.EventBus.Register(this as IEventReceiver<RedEvent>);
            _eventBusHolder.EventBus.Register(this as IEventReceiver<BlueEvent>);

            _meshRenderer = GetComponent<MeshRenderer>();

        }

        // Тут реализуются методы, которые вызываются при срабатывании определенных событий.
        public void OnEvent(RedEvent @event)
        {
            transform.position += @event.MoveDelta;
        }

        public void OnEvent(BlueEvent @event)
        {
            _meshRenderer.sharedMaterial.color = @event.Color;
        }
    }
}
