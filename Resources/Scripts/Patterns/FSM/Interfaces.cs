namespace Patterns.FSM
{
    public interface IState 
    {
        void Enter();
        void Exit();
        void Update();
    }
}
