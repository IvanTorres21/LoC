using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildingPreset", menuName = "New Building")]
public class BuildingPreset : ScriptableObject
{
    public GameObject prefab;

    [Header("General Data")]
    public int cost;
    public int maintenance;
    public int hope;
    public int maxPeople; // Defines how many people can use the establishment at the same time
    public BuildingSize size;
    public BuildingType type;
    public Luxury luxuryValue;


    [Header("Food Data")]
    public int foodAmount;

    [Header("Training Data")]
    public int trainingAmount;

    [Header("Hobby Data")]
    public List<HobbyType> hobbyType;

}
