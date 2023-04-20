using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneSpawner : MonoBehaviour
{
    [SerializeField] Drone drone;
    [SerializeField] Amongus amongus;
    [SerializeField] float initialTimer = 10;

    float timer;

    void Start()
    {
        timer = initialTimer;
        drone.gameObject.SetActive(false);
    }

    void Update()
    {
        if(timer <= 0 && drone.gameObject.activeInHierarchy == false)
        {
            drone.gameObject.SetActive(true);
            drone.transform.position = amongus.transform.position + new Vector3(0, 0, 13);
            amongus.SetMoveable(false);
        }
        timer -= Time.deltaTime;
    }

    public void ResetTimer()
    {
        timer = initialTimer;
    }
}
