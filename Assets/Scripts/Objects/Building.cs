using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public BuildingPreset preset;

    public List<MagicalGirl> currentTenants = new List<MagicalGirl>();

    public void AssignMagicalGirl(MagicalGirl mg)
    {
        if (currentTenants.Contains(mg))
            return;

        if(preset.maxPeople > currentTenants.Count)
        {
            currentTenants.Add(mg);
            if(preset.type == BuildingType.HOUSE)
                mg.ChangeHome(this);
            else
                mg.ChangeLocation(this);
        }
    }

    public void DeassignMagicalGirl(MagicalGirl mg)
    {
        currentTenants.Remove(mg);
        if (preset.type == BuildingType.HOUSE)
            mg.ChangeHome(null);
        else
            mg.ChangeLocation(null);
    }
}
