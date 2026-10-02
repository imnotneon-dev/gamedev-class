using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSpawnController : MonoBehaviour
{
    [SerializeField] private List<GameObject> prefabs = new List<GameObject>();
    [SerializeField] private Transform spawnPosition;

    [SerializeField] private float spawnInterval = 5f;
    [SerializeField] private Vector2 randomOffset = new Vector2(6f, 3f);

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            SpawnLaser();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnLaser()
    {
        int randomIndex = Random.Range(0, prefabs.Count);
        GameObject selectedPrefab = prefabs[randomIndex];

        if (selectedPrefab == null) 
            return;

        Vector3 basePos = spawnPosition.position;
        Quaternion baseRot = spawnPosition.rotation;

        float offsetX = Random.Range(-randomOffset.x, randomOffset.x);
        float offsetY = Random.Range(-randomOffset.y, randomOffset.y);

        Vector3 finalPos = basePos + (transform.right * offsetX) + (transform.up * offsetY);

        Instantiate(selectedPrefab, finalPos, baseRot);
    }
}