using System.Dynamic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    static private Main S;
    [Header("Inscribed")]
    public GameObject[] prefabEnemies;
    public float enemySpawnPerSecond = 0.5f;
    public float enemyInsetDefault = 1.5f;

    private BoundsCheck bndCheck;

    void Awake()
    {
        S = this;
        bndCheck = GetComponent<BoundsCheck>();
        Invoke(nameof(SpawnEnemy), 1f/enemySpawnPerSecond);
    }
    public void SpawnEnemy()
{
    if (prefabEnemies.Length == 0) return;

    int ndx = Random.Range(0, prefabEnemies.Length);

    GameObject go = Instantiate<GameObject>(prefabEnemies[ndx]);

    float enemyInset = enemyInsetDefault;

    BoundsCheck bc = go.GetComponent<BoundsCheck>();

    if (bc != null)
    {
        enemyInset = Mathf.Abs(bc.radius);
    }

    float xMin = -bndCheck.camWidth + enemyInset;
    float xMax = bndCheck.camWidth - enemyInset;

    // Make sure the spawn range is valid
    if (xMin > xMax)
    {
        xMin = -bndCheck.camWidth;
        xMax = bndCheck.camWidth;
    }

    Vector3 pos = Vector3.zero;

    pos.x = Random.Range(xMin, xMax);
    pos.y = bndCheck.camHeight + enemyInset;

    go.transform.position = pos;

    Invoke(nameof(SpawnEnemy), 1f / enemySpawnPerSecond);
}
}
