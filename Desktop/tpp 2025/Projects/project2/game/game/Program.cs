public interface ICondition
{
    bool Check(GameState state);
}
public interface IEffect
{
    void Apply(GameState state);
}
public interface IInteractable
{
    string Id { get; }
    string Interact(GameState state);
}
public interface ICommand
{
    string Name { get; }
    string Description { get; }
    string Execute(string[] args, GameState state);
}
public interface IQuest
{
    string Name { get; }
    string Description { get; }
    bool IsCompleted { get; }
    void Update(GameState state);
}
public interface IMonster
{
    string Name { get; }
    int Health { get; }
    int Damage { get; }
    int GoldReward { get; }
    bool IsAlive();
    void TakeDamage(int amount);
}
public interface IPotionSystem
{
    int PotionCount { get; }
    void UsePotion(GameState state, Player player);
    void OnLocationEnter(GameState state, Player player);
}

public abstract class ConditionBase : ICondition
{
    public string Description { get; set; }
    public abstract bool Check(GameState state);
}
public abstract class EffectBase : IEffect
{
    public string Description { get; set; }
    public abstract void Apply(GameState state);
}
public abstract class GameEventBase
{
    public ICondition Condition { get; set; }
    public IEffect[] Effects { get; set; } = new IEffect[0];
    public bool IsOneTime { get; set; }
    public bool WasTriggered { get; set; }

    public void TryTrigger(GameState state)
    {
        if (!ShouldCheck(state)) return;
        if (IsOneTime && WasTriggered) return;
        if (!Condition.Check(state)) return;

        foreach (var effect in Effects)
            effect.Apply(state);

        if (IsOneTime)
            WasTriggered = true;
    }

    public abstract bool ShouldCheck(GameState state);
}
public abstract class CommandBase : ICommand
{
    public string Name { get; set; }
    public string Description { get; set; }
    public abstract string Execute(string[] args, GameState state);
}
public abstract class InteractableBase : IInteractable
{
    public string Id { get; set; }
    public abstract string Interact(GameState state);
}
public abstract class MonsterBase : IMonster
{
    public string Name { get; set; }
    public int Health { get; set; }
    public int Damage { get; set; }
    public int GoldReward { get; set; } = 20;

    public bool IsAlive() => Health > 0;

    public void TakeDamage(int amount)
    {
        Health -= amount;
        if (Health < 0) Health = 0;
    }
}