using UnityEngine;

public abstract class Hittable : MonoBehaviour

{
    public float health;
    public abstract void Hit(float damage);
    public abstract void Die();

}
