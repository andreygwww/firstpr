using System;

public interface IDamageable
{
    void TakeDamage(int damage);
}

public abstract class Character : IDamageable
{
    public string Name;
    public int Health;

    public abstract void Attack();

    public void Move()
    {
        Console.WriteLine($"{Name} двигается вперед");
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        Console.WriteLine($"{Name} получил {damage} урона. Осталось здоровья: {Health}");
    }
}

public class Warrior : Character
{
    public Warrior(string name, int health)
    {
        Name = name;
        Health = health;
    }

    public override void Attack()
    {
        Console.WriteLine($"{Name} наносит удар мечом");
    }
}

public class Mage : Character
{
    public Mage(string name, int health)
    {
        Name = name;
        Health = health;
    }

    public override void Attack()
    {
        Console.WriteLine($"{Name} запускает фаербол");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Character[] team = new Character[]
        {
            new Warrior("Гном", 100),
            new Mage("Эльф", 60)
        };

        foreach (var hero in team)
        {
            hero.Attack();
            hero.Move();
            hero.TakeDamage(10);
        }
    }
}
