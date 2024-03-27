using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Hittable : MonoBehaviour

{
    public float health;
    public abstract void Hit(float damage);
    public abstract void Die();

}
