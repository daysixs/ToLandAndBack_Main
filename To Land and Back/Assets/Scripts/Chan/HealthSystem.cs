using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    private int health = 1;
    public int maxHealth = 1;
    public GameObject deathEffect;
    public bool isPlayerOwned;
    public Color unique;
    public void TakeDamage(int damage)
    {
        health = health - damage;
        for (int i = 0; i < 5; i++)
        {
            GameObject sceneObject = Instantiate(deathEffect, gameObject.transform.position, transform.rotation) as GameObject;
            sceneObject.GetComponent<Renderer>().material.color = unique;
            Vector3 randomVelocity = new Vector3(Random.Range(-3, 4), Random.Range(-3, 4));
            sceneObject.GetComponent<Rigidbody2D>().velocity = randomVelocity;
        }
        if (health <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        Debug.Log(gameObject.name + "Died");
        for (int i = 0; i < 11; i++)
        {
            GameObject sceneObject = Instantiate(deathEffect, gameObject.transform.position, transform.rotation) as GameObject;
            sceneObject.GetComponent<Renderer>().material.color = unique;
            Vector3 randomVelocity = new Vector3(Random.Range(-3, 4), Random.Range(-3, 4));
            sceneObject.GetComponent<Rigidbody2D>().velocity = randomVelocity;
        }
        Destroy(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
