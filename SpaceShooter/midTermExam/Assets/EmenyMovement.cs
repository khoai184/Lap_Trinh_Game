using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("=== SPAWNER SETTINGS ===")]
    public bool isSpawner = false;
    public GameObject enemyPrefab;

    public float spawnInterval = 2f;
    public float minSpawnInterval = 0.8f;
    public float difficultyIncreaseTime = 15f;

    [Header("=== WORLD LIMITS ===")]
    public float xLimit = 3f;
    public float yLimit = 6f;
    public float spawnOffset = 2f;

    [Header("=== ENEMY MOVEMENT ===")]
    public float speed = 3f;
    public float changeDirTime = 2f;

    [Header("=== AVOID PLAYER ===")]
    public float minDistanceFromPlayer = 3f;

    [Header("=== ENEMY SHOOTING ===")]
    public GameObject enemyBulletPrefab;
    public float shootInterval = 2f;

    [Header("=== BURST SHOOTING ===")]
    public int burstCount = 5;
    public float burstDelay = 0.24f;

    private Vector2 moveDir;
    private float dirTimer;
    private float shootTimer;
    private Transform player;

    void Start()
    {
        if (isSpawner)
        {
            StartSpawner();
        }
        else
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
            PickRandomDirection();
        }
    }

    void Update()
    {
        if (isSpawner) return;

        MoveEnemy();
        ChangeDirectionOverTime();
        HandleShooting();
        DestroyIfOutOfBounds();
    }

    void StartSpawner()
    {
        InvokeRepeating(nameof(SpawnEnemy), 1f, spawnInterval);
        InvokeRepeating(nameof(IncreaseDifficulty),
                        difficultyIncreaseTime,
                        difficultyIncreaseTime);
    }

    void SpawnEnemy()
    {
        Vector3 spawnPos = GetSpawnPositionOutsideScreen();
        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }

    void IncreaseDifficulty()
    {
        spawnInterval = Mathf.Max(minSpawnInterval, spawnInterval - 0.2f);

        CancelInvoke(nameof(SpawnEnemy));
        InvokeRepeating(nameof(SpawnEnemy), 0f, spawnInterval);
    }

    Vector3 GetSpawnPositionOutsideScreen()
    {
        int side = Random.Range(0, 3);

        if (side == 0)
            return new Vector3(Random.Range(-xLimit, xLimit),
                               yLimit + spawnOffset, 0);

        if (side == 1)
            return new Vector3(-xLimit - spawnOffset,
                               Random.Range(-yLimit, yLimit), 0);

        return new Vector3(xLimit + spawnOffset,
                           Random.Range(-yLimit, yLimit), 0);
    }

    void MoveEnemy()
    {
        if (player != null)
        {
            float dist = Vector2.Distance(transform.position, player.position);

            if (dist < minDistanceFromPlayer)
            {
                moveDir = (transform.position - player.position).normalized;
            }
        }

        transform.Translate(moveDir * speed * Time.deltaTime);
    }

    void ChangeDirectionOverTime()
    {
        dirTimer += Time.deltaTime;

        if (dirTimer >= changeDirTime)
        {
            PickRandomDirection();
            dirTimer = 0f;
        }
    }

    void PickRandomDirection()
    {
        int x = Random.Range(-1, 2);
        int y = Random.Range(-1, 2);

        if (x == 0 && y == 0)
            y = -1;

        moveDir = new Vector2(x, y).normalized;
    }

    void HandleShooting()
    {
        if (enemyBulletPrefab == null || player == null) return;

        shootTimer += Time.deltaTime;

        if (shootTimer >= shootInterval)
        {
            shootTimer = 0f;
            StartCoroutine(BurstShoot());
        }
    }


    void ShootAtPlayer()
    {
        if (enemyBulletPrefab == null || player == null) return;

        Vector2 dir = (player.position - transform.position).normalized;

        GameObject bullet = Instantiate(
            enemyBulletPrefab,
            transform.position,
            Quaternion.identity
        );

        EnemyBullet eb = bullet.GetComponent<EnemyBullet>();
        if (eb != null)
        {
            eb.SetDirection(dir);
        }
    }

    void DestroyIfOutOfBounds()
    {
        if (transform.position.y > yLimit + spawnOffset ||
            transform.position.y < -yLimit - spawnOffset ||
            transform.position.x > xLimit + spawnOffset ||
            transform.position.x < -xLimit - spawnOffset)
        {
            Destroy(gameObject);
        }
    }
    System.Collections.IEnumerator BurstShoot()
    {
        for (int i = 0; i < burstCount; i++)
        {
            ShootAtPlayer();
            yield return new WaitForSeconds(burstDelay);
        }
    }

}