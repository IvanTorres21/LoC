using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMG", menuName = "New MG Preset")]
public class MGPreset : ScriptableObject
{
    public GameObject prefab;

    [Header("Basic Info")]
    public string MG_name;
    public int age;
    public List<Skills> skills;
    public List<Problems> problems;
    public Sprite card;

    [Header("Preferences")]
    public List<BuildingType> likes;
    public List<BuildingType> dislikes;
    public Luxury luxuryLevel;
    public int foodConsumption;
    public SocialType socialType;

    [Header("Combat info")]
    public int baseATK;
    public int baseDEF;
    public int modATK;
    public int modDEF;

    [Header("Limits info")]
    public int MAXTIREDNESS;
    public float depression;
    public int breakingPoint; // number of days with 0 happiness
}
