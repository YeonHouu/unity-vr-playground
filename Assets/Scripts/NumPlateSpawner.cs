using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class NumPlateSpawner : MonoBehaviour
{
    public GameObject numPlatePrefab; 
    public Transform spawnPoint;     

    public void SpawnPlate()
    {
        Instantiate(numPlatePrefab, spawnPoint.position, spawnPoint.rotation);
    }
}