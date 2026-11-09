using UnityEngine;
using UnityEngine.SceneManagement;

public class Hero : MonoBehaviour
{
    // Start is called before the first frame update
    static public Hero S {get; private set;}

    [Header("Inscibed")]
    public float speed = 30f;
    public float rollMult = -45f;
    public float pitchMult = 30f;

    [Header("Dynamic")] [Range(0, 4)]
    public float shieldLevel = 4;


    [Header("Shooting")]
    public GameObject prefabProjectile;
    public float fireRate = 0.25f;

private float nextFire = 0f;

    void Awake()
    {
        if (S == null)
        {
            S = this;
        }
        else
        {
            Debug.LogError("Hero.Awake() - Attempted to assign second Hero.S");
        }
    }
    void Update()
    {
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        Vector3 pos = transform.position;
        pos.x += hAxis * speed * Time.deltaTime;
        pos.y += vAxis * speed * Time.deltaTime;
        transform.position = pos;

        transform.rotation = Quaternion.Euler(vAxis*pitchMult, hAxis*rollMult, 0);

        if (Input.GetKey(KeyCode.Space) && Time.time >= nextFire)
        {
            Fire();
            nextFire = Time.time + fireRate;
        }
    }

    void Fire()
    {
        Instantiate(
            prefabProjectile,
            transform.position,
            Quaternion.identity
        );
    }
    public void TakeDamage(int damage)
    {
        shieldLevel -= damage;

        if (shieldLevel <= 0)
        {
            Die();
        }
    }

    void Die()  
    {
        Destroy(gameObject);
        Debug.Log("Hero destroyed! Restarting game...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }
}
