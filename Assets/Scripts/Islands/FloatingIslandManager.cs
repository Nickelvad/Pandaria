using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pandaria.Islands
{

    public struct IslandPath
    {
        public Vector3 start;
        public Vector3 end;
    }
    public class FloatingIslandManager : MonoBehaviour
    {
        public Transform zeroPoint;
        public LayerMask whatIsGround;
        public float checkRadius = 50f;
        public List<Transform> islandsPrefabs;
        public float timeBetweenSpawnsSeconds = 5f;
        void Start()
        {
            InvokeRepeating(nameof(SpawnIsland), 0, timeBetweenSpawnsSeconds);
        }

        void Destroy()
        {
            CancelInvoke(nameof(SpawnIsland));
        }

        private Vector3 GetCoordinateBySide(int choice)
        {
            //
            //     1
            // 4       2
            //     3
            //      (0, 200)
            //(-200, 0)   (200, 0)
            //      (0, -200)
            switch (choice)
            {
                case 1:
                    return new Vector3(Random.Range(-199, 200), zeroPoint.position.y, 200);
                case 2:
                    return new Vector3(200, zeroPoint.position.y, Random.Range(-199, 200));
                case 3:
                    return new Vector3(Random.Range(-199, 200), zeroPoint.position.y, -200);
                default:
                    return new Vector3(-200, zeroPoint.position.y, Random.Range(-199, 200));
            }
        }

        private IslandPath GetRandomCoordinates()
        {

            IslandPath result = new IslandPath();
            int startChoice = Random.Range(1, 5);
            result.start = GetCoordinateBySide(startChoice);
            int endChoice;
            while (true)
            {
                endChoice = Random.Range(1, 5);
                if (endChoice != startChoice)
                {
                    result.end = GetCoordinateBySide(endChoice);
                    break;
                }

            }
            return result; 
        }

        private void SpawnIsland()
        {
            Transform islandPrefab = SelectIsland();
            IslandPath islandPath = GetRandomCoordinates();
            Transform island = InstantiateIsland(islandPrefab, islandPath);
        }

        private Transform SelectIsland()
        {
            return islandsPrefabs[Random.Range(0, islandsPrefabs.Count - 1)];
        }

        private Vector3 SelectIslandSpawnLocation()
        {
            Vector3 location = new Vector3(zeroPoint.position.x, zeroPoint.position.y, zeroPoint.position.z);
            location = Quaternion.Euler(0, Random.Range(0, 360), 0) * location;
            float distance = Random.Range(100, 200);
            location.x = distance;
            location.z = distance;
            bool result = Physics.CheckSphere(location, checkRadius, whatIsGround);
            return result == false ? location : Vector3.zero;
        }

        private Transform InstantiateIsland(Transform islandPrefab, IslandPath islandPath)
        {
            Transform island = Instantiate(islandPrefab, islandPath.start, Quaternion.identity);
            FloatingIsland floatingIsland = island.GetComponentInChildren<FloatingIsland>();
            floatingIsland.Initialize(islandPath.end);
            return island;
        }
    }
}

