using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TimePeriod
{
    Morning,
    Afternoon,
    Night
}

public class NPCSpawner : MonoBehaviour
{
    [SerializeField] private TimeManager timeManager; // Reference to TimeManager
    [SerializeField] private GameObject[] morningNPCs;
    [SerializeField] private GameObject[] afternoonNPCs;
    [SerializeField] private GameObject[] nightNPCs;
    [SerializeField] private Transform[] spawnPoints;

    private TimePeriod currentTimePeriod;

    void Start()
    {
        DetermineTimePeriod();
        SpawnNPCs();
        StartCoroutine(CheckTimeChange());
    }

    // This function determines the given time period and assigns the enum as needed
    private void DetermineTimePeriod()
    {
        int hours = timeManager.TimeOfDay.Hours;

        // If the time is more than 6 and less than 12 - Morning
        if (hours >= 6 && hours < 12)
            currentTimePeriod = TimePeriod.Morning;
        // If the hours are more than 12 and less than 18 - Afternoon
        else if (hours >= 12 && hours < 18)
            currentTimePeriod = TimePeriod.Afternoon;
        // Otherwise - Night
        else
            currentTimePeriod = TimePeriod.Night;
    }

    // This function spawns NPCs based on their given time period and puts them at a spawn point
    private void SpawnNPCs()
    {
        GameObject[] npcPool = GetNPCPoolForTimePeriod();
        if (npcPool.Length == 0 || spawnPoints.Length == 0) return;

        List<Transform> availableSpawnPoints = new List<Transform>(spawnPoints);

        foreach (GameObject npcPrefab in npcPool)
        {
            if (availableSpawnPoints.Count == 0) break; // Stop if no more spawn points are available

            // Select a random spawn point and remove it from the list
            int spawnIndex = Random.Range(0, availableSpawnPoints.Count);
            Transform spawnPoint = availableSpawnPoints[spawnIndex];
            availableSpawnPoints.RemoveAt(spawnIndex);

            // Spawn the NPC
            Instantiate(npcPrefab, spawnPoint.position, Quaternion.identity);
        }
    }

    // This function gets the pool of NPCs for a given time period
    private GameObject[] GetNPCPoolForTimePeriod()
    {
        switch (currentTimePeriod)
        {
            case TimePeriod.Morning:
                return morningNPCs;
            case TimePeriod.Afternoon:
                return afternoonNPCs;
            case TimePeriod.Night:
                return nightNPCs;
            default:
                return new GameObject[0];
        }
    }

    // This function checks the time change and adjusts the time period accordingly
    private IEnumerator CheckTimeChange()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f); // Check every 10 seconds
            TimePeriod newTimePeriod = DetermineTimePeriodFromHours(timeManager.TimeOfDay.Hours);

            if (newTimePeriod != currentTimePeriod)
            {
                currentTimePeriod = newTimePeriod;
                DespawnNPCs();
                SpawnNPCs();
            }
        }
    }

    // This function determines the time period from the given hours
    private TimePeriod DetermineTimePeriodFromHours(int hours)
    {
        if (hours >= 6 && hours < 12)
            return TimePeriod.Morning;
        else if (hours >= 12 && hours < 18)
            return TimePeriod.Afternoon;
        else
            return TimePeriod.Night;
    }

    // This function despawns the NPCs
    private void DespawnNPCs()
    {
        foreach (GameObject npc in GameObject.FindGameObjectsWithTag("NPC"))
        {
            Destroy(npc);
        }
    }
}
