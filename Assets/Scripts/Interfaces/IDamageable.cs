
public interface IDamageable
{
    int Health { get; set; }

    void OnDamageRecieved(int _damageAmount);
}
