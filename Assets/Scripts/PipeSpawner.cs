using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    [SerializeField] GameObject pipePrefab;
    [SerializeField] float YmaxSpawn = 2.75f;
    [SerializeField] float YminSpawn = -1.75f;
    [SerializeField] float Xspawn = 8f;
    [SerializeField] float interval = 3f;

    private float spawnTimer;

    private void Start()
    {
        ResetPipes();
    }

    private void Update()
    {
        if (Time.timeScale > 0)
        {
            spawnTimer -= Time.deltaTime;
            if (spawnTimer <= 0)
            {
                Spawn();
                spawnTimer = interval;
            }
        }
    }

    private void Spawn()
    {
        GameObject instance = Instantiate(pipePrefab, transform.position, Quaternion.identity);
        instance.transform.position = new Vector2(Xspawn, Random.Range(YminSpawn, YmaxSpawn));
        instance.transform.SetParent(transform);
    }

    public void ResetPipes()
    {
        // Cancel any existing invokes
        CancelInvoke("Spawn");

        // Destroy all existing pipes
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        // Reset spawn timer
        spawnTimer = interval;
    }
}
