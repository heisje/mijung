public interface ILifeCycle
{
    public void BeforeStage();
    public void StartStage();
    public void StartTurn();
    public void EndTurn();
    public void EndStage();
}