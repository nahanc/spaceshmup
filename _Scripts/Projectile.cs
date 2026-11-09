using UnityEngine;
using UnityEngine.SceneManagement;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 1;
    public bool isEnemyProjectile = false;

    void Update()
    {
        float direction = isEnemyProjectile ? -1f : 1f;

        transform.position += Vector3.up * direction * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (isEnemyProjectile)
        {
            Hero hero = other.GetComponentInParent<Hero>();

            if (hero != null)
            {
                hero.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
        else
        {
            Enemy enemy = other.GetComponentInParent<Enemy>();

            if (enemy != null)
            {
                enemy.health -= damage;
                if (enemy.health <= 0)
                {
                    Destroy(enemy.gameObject);
                }

                Destroy(gameObject);
            }
        }
    }
}