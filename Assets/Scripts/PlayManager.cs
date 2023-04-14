using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
    [SerializeField] Grass grassPrefab;
    [SerializeField] Road roadPrefab;
    [SerializeField] int initalGrassCount;
    [SerializeField] int horizontalSize;
    [SerializeField] int backViewDistance;
    [SerializeField] int forwardViewDistance;
    [SerializeField, Range(0, 1)] float treeProbability;

    private void Start() 
    {
        for(int zPos = backViewDistance; zPos < initalGrassCount; zPos++)
        {
            var grass = Instantiate(grassPrefab);
            grass.transform.position = new Vector3(0, 0, zPos);
            grass.SetTreePercentage(zPos < -1 ? 1 : 0);
            grass.Generate(horizontalSize);
        }

        for(int zPos = initalGrassCount; zPos < forwardViewDistance; zPos++)
        {
            var terrain = Instantiate(roadPrefab);
            terrain.transform.position = new Vector3(0, 0, zPos);
            terrain.Generate(horizontalSize);
        }
    }
}
