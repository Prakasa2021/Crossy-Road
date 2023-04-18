using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : Terrain
{
    [SerializeField] Suspect suspectPrefab;
    [SerializeField] float minSuspectSpawnInterval;
    [SerializeField] float maxSuspectSpawnInterval;

    float timer;
    Vector3 suspectSpawnPosition;
    Quaternion suspectRotation;

    private void Start() 
    {
        if(Random.value > 0.5f)
        {
            suspectSpawnPosition = new Vector3(horizontalSize / 2 + 3, 0, this.transform.position.z);
            suspectRotation = Quaternion.Euler(0, -90, 0);
        }
        else 
        {
            suspectSpawnPosition = new Vector3(-(horizontalSize / 2 + 3), 0, this.transform.position.z);
            suspectRotation = Quaternion.Euler(0, 90, 0);
        }
    }

    private void Update() 
    {
        if(timer <= 0)
        {
            timer = Random.Range(minSuspectSpawnInterval, maxSuspectSpawnInterval);
            
            var suspect = Instantiate(suspectPrefab, suspectSpawnPosition, suspectRotation);
            
            suspect.SetUpDistanceLimit(horizontalSize + 6);
            return;
        }

        timer -= Time.deltaTime;
    }
}
