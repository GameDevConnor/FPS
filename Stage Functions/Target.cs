using UnityEngine;

public class Target : Hittable
{
    public bool hit = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Die();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Pickup thrown = collision.collider.GetComponent<Pickup>();

        if (thrown != null)
        {
            Hit(0);
        }
    }

    public override void Hit(float damage)
    {
        transform.GetComponent<MeshRenderer>().material.color = new Color(0.0f, 1.0f, 0.0f, 1.0f);
        hit = true;
        health -= damage;
    }

    public override void Die()
    {

    }

}
