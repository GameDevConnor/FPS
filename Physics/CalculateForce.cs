using UnityEngine;

public static class CalculateForce
{
    public static float calculateForceHard(float mass)
    {
        return mass * 1000f;
    }

    public static float calculateForceSoft(float mass)
    {
        return mass * 500f;
    }

    public static int calculateDamage(float speed, int mass)
    {

        int damage = (int)(mass * Mathf.Log10(speed + 1));
        //Debug.Log("Damage: " + damage);
        return damage;
    }
}
