using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pandaria.Islands
{
    public class IslandManager : MonoBehaviour
    {
        public Transform zeroPoint;
        public LayerMask whatIsGround;
        public float checkRadius = 50f;
        public List<Transform> islandsPrefabs;
        public float timeBetweenSpawnsSeconds = 5f;
        void Start()
        {
            Invoke(nameof(SpawnIsland), timeBetweenSpawnsSeconds);
        }

        private void SpawnIsland()
        {
            Transform islandPrefab = SelectIsland();
            Vector3 islandSpawnLocation = SelectIslandSpawnLocation();
            if (islandSpawnLocation == Vector3.zero)
            {
                Debug.Log("Failed to find coordinates");
                return;
            }

            Transform island = InstantiateIsland(islandPrefab, islandSpawnLocation);
            // Vector3 islandMovementDirection = GetIslandMovementDirection(islandSpawnLocation, zeroPoint);
        }

        private Transform SelectIsland()
        {
            return islandsPrefabs[Random.Range(0, islandsPrefabs.Count - 1)];
        }

        private Vector3 SelectIslandSpawnLocation()
        {
            Vector3 location = new Vector3(zeroPoint.position.x, zeroPoint.position.y - Random.Range(30, 60), zeroPoint.position.z);
            location = Quaternion.Euler(0, Random.Range(0, 360), 0) * location;
            float distance = Random.Range(100, 200);
            location.x = distance;
            location.z = distance;
            bool result = Physics.CheckSphere(location, checkRadius, whatIsGround);
            return result == false ? location : Vector3.zero;
        }

        private Transform InstantiateIsland(Transform islandPrefab, Vector3 location)
        {
            Transform island = Instantiate(islandPrefab, location, Quaternion.identity);
            FloatingIsland floatingIsland = island.GetComponent<FloatingIsland>();
            floatingIsland.Initialize(zeroPoint.transform.position);
            return island;
        }
    }
}

