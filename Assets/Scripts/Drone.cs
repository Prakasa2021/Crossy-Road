using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    [SerializeField, Range(0, 50)] float speed;
    [SerializeField] AudioSource droneSound;

    void Start() 
    {
        droneSound.Play();
    }

    private void Update() 
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
