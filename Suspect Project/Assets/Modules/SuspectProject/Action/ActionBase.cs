namespace SuspectProject.Data
{
    public abstract class GameActionBase
    {
        public abstract void Execute(GameStateData state);
        public abstract string Description();
    }
}

