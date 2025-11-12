namespace Sander.DroneBattle
{
    public interface IFSMState
    {
        public void OnEnter();
        public void OnUpdate();
        public void OnExit();
    }
}
