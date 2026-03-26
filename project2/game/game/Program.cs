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

public enum PlayerClass
{
    Warrior,
    Mage,
    Thief,
    Cleric
}

public class GameState
{
    public int Health { get; set; }
    public int MaxHealth { get; set; }
    public int Gold { get; set; }
    public int Turn { get; set; }
    public int PotionCount { get; set; } = 3;
    public string CurrentLocationId { get; set; }
    public bool GameOver { get; set; }
    public List<string> Inventory { get; set; } = new List<string>();
    public Dictionary<string, bool> Flags { get; set; } = new Dictionary<string, bool>();
    public List<string> EventLog { get; set; } = new List<string>();
    public List<IQuest> Quests { get; set; } = new List<IQuest>();

    public void AddItem(string itemId) => Inventory.Add(itemId);
    public void RemoveItem(string itemId) => Inventory.Remove(itemId);
    public bool HasItem(string itemId) => Inventory.Contains(itemId);

    public void SetFlag(string flag, bool value) => Flags[flag] = value;
    public bool GetFlag(string flag) => Flags.ContainsKey(flag) && Flags[flag];

    public void Damage(int amount)
    {
        Health -= amount;
        if (Health < 0) Health = 0;
    }

    public void Heal(int amount)
    {
        Health += amount;
        if (Health > MaxHealth) Health = MaxHealth;
    }

    public void AddGold(int amount) => Gold += amount;
    public void RemoveGold(int amount) => Gold -= amount;
    public void Log(string message) => EventLog.Add(message);
    public void NextTurn()
    {
        Turn++;
        foreach (var quest in Quests)
            quest.Update(this);
    }
}

public class Player
{
    public PlayerClass Class { get; set; }
    public int Strength { get; set; }
    public int Intelligence { get; set; }
    public int Faith { get; set; }
    public int Agility { get; set; }
    public int WeaponDamage { get; set; }
    public int HealAmount { get; set; }
    public int HealCooldown { get; set; }

    public void ApplyClass(GameState state)
    {
        switch (Class)
        {
            case PlayerClass.Warrior:
                state.MaxHealth = 130;
                state.Gold = 100;
                Strength = 8; Intelligence = 2; Faith = 3; Agility = 3;
                state.SetFlag("isWarrior", true);
                state.SetFlag("canBreakBarricade", true);
                break;
            case PlayerClass.Mage:
                state.MaxHealth = 70;
                state.Gold = 75;
                Strength = 2; Intelligence = 9; Faith = 4; Agility = 3;
                state.SetFlag("isMage", true);
                state.SetFlag("canSeeDark", true);
                state.AddItem("soulGem");
                break;
            case PlayerClass.Thief:
                state.MaxHealth = 90;
                state.Gold = 125;
                Strength = 4; Intelligence = 4; Faith = 2; Agility = 9;
                state.SetFlag("isThief", true);
                state.SetFlag("canPickLock", true);
                state.SetFlag("canBypassTrap", true);
                break;
            case PlayerClass.Cleric:
                state.MaxHealth = 100;
                state.Gold = 75;
                Strength = 5; Intelligence = 3; Faith = 9; Agility = 3;
                state.SetFlag("isCleric", true);
                state.SetFlag("canHeal", true);
                break;
        }
        state.Health = state.MaxHealth;
        state.PotionCount = 3;
        RecalculateStats();
    }

    public void RecalculateStats()
    {
        switch (Class)
        {
            case PlayerClass.Warrior:
                WeaponDamage = (int)(25 * (1 + Strength * 0.05));
                break;
            case PlayerClass.Mage:
                WeaponDamage = (int)(15 * (1 + Intelligence * 0.05));
                break;
            case PlayerClass.Thief:
                WeaponDamage = (int)(20 * (1 + Agility * 0.05));
                break;
            case PlayerClass.Cleric:
                WeaponDamage = (int)(18 * (1 + Faith * 0.05));
                HealAmount = (int)(20 * (1 + Faith * 0.05));
                break;
        }
    }

    public int GetUpgradeCost(int currentLevel) => 50 + currentLevel * 25;
}

public class Location
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Dictionary<string, string> Exits { get; set; } = new Dictionary<string, string>();
    public List<InteractableBase> Objects { get; set; } = new List<InteractableBase>();
    public List<GameEventBase> Events { get; set; } = new List<GameEventBase>();
    public List<MonsterBase> Monsters { get; set; } = new List<MonsterBase>();

    public void ProcessEvents(GameState state)
    {
        foreach (var e in Events)
            e.TryTrigger(state);
    }

    public InteractableBase GetObject(string id) =>
        Objects.Find(o => o.Id == id);

    public void AddExit(string direction, string locationId) =>
        Exits[direction] = locationId;

    public List<MonsterBase> GetAliveMonsters() =>
        Monsters.FindAll(m => m.IsAlive());
}

public class PotionSystem : IPotionSystem
{
    public int PotionCount { get; private set; } = 3;

    public void UsePotion(GameState state, Player player)
    {
        if (PotionCount <= 0)
        {
            state.Log("Зелий нет.");
            return;
        }
        int heal = (int)(state.MaxHealth * 0.7);
        state.Heal(heal);
        PotionCount--;
        state.Log($"Ты выпил зелье. Восстановлено {heal} HP. Осталось зелий: {PotionCount}");
    }

    public void OnLocationEnter(GameState state, Player player)
    {
        if (PotionCount < 3)
        {
            PotionCount = 3;
            state.Log("Запас зелий пополнился до 3.");
        }
    }
}