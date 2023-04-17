using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayManager : MonoBehaviour
{
    [SerializeField] Amongus amongus;
    [SerializeField] List<Terrain> terrainList;
    [SerializeField] int initalGrassCount;
    [SerializeField] int horizontalSize;
    [SerializeField] int backViewDistance;
    [SerializeField] int forwardViewDistance;
    [SerializeField, Range(0, 1)] float treeProbability;

    Dictionary<int, Terrain> activeTerrainDict = new Dictionary<int, Terrain>(20);

    [SerializeField] private int travelDistance;

    public UnityEvent<int, int> OnUpdateTerrainLimit;

    private void Start() 
    {
        for(int zPos = backViewDistance; zPos < initalGrassCount; zPos++)
        {
            var terrain = Instantiate(terrainList[0]);
            
            terrain.transform.position = new Vector3(0, 0, zPos);
            
            if(terrain is Grass grass)
                grass.SetTreePercentage(zPos < -1 ? treeProbability : 0);
           
            terrain.Generate(horizontalSize);
            
            activeTerrainDict[zPos] = terrain;
        }

        for(int zPos = initalGrassCount; zPos < forwardViewDistance; zPos++)
        {
            SpawnRandomTerrain(zPos);
        }
    }

    private Terrain SpawnRandomTerrain(int zPos)
    {
        Terrain terrainCheck = null;
        int randomIndex;

        for (int z = -1; z >= -3; z--)
        {
            var checkPos = zPos + z;

            if(terrainCheck == null)
            {
                terrainCheck = activeTerrainDict[checkPos];
                continue;
            }
            else if(terrainCheck.GetType() != activeTerrainDict[checkPos].GetType())
            {
                randomIndex = Random.Range(0, terrainList.Count);
                return SpawnTerrain(terrainList[randomIndex], zPos);
            }
            else
            {
                continue;
            }
        }

        var candidateTerrain = new List<Terrain>(terrainList);
        for (int i = 0; i < candidateTerrain.Count; i++)
        {
            if(terrainCheck.GetType() == candidateTerrain[i].GetType())
            {
                candidateTerrain.Remove(candidateTerrain[i]);
                break;
            }
        }

        randomIndex = Random.Range(0, candidateTerrain.Count);
        return SpawnTerrain(candidateTerrain[randomIndex], zPos);
    }

    public Terrain SpawnTerrain(Terrain terrain, int zPos)
    {
        terrain = Instantiate(terrain);
        terrain.transform.position = new Vector3(0, 0, zPos);
        terrain.Generate(horizontalSize);
        activeTerrainDict[zPos] = terrain;
        return terrain;
    }

    public void UpdateTravelDistance(Vector3 targetPosition)
    {
        if(targetPosition.z > travelDistance)
        {
            travelDistance = Mathf.CeilToInt(targetPosition.z);
            UpdateTerrain();
        }
    }

    public void UpdateTerrain()
    {
        var destroyPos = travelDistance - 1 + backViewDistance;
        Destroy(activeTerrainDict[destroyPos].gameObject);
        activeTerrainDict.Remove(destroyPos);
        
        var spawnPosition = travelDistance - 1 + forwardViewDistance;
        SpawnRandomTerrain(spawnPosition);

        OnUpdateTerrainLimit.Invoke(horizontalSize, travelDistance + backViewDistance);
    }
}
