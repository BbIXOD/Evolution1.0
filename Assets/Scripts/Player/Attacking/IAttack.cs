

public interface IAttack
{
    public void Attack();

    public int Cooldown { get; }
    
    public bool Enabled { get; }
}
