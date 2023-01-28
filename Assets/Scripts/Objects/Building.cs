using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZSerializer;

public class Building : PersistentMonoBehaviour
{
    public BuildingPreset preset;

    public List<MagicalGirl> currentTenants = new List<MagicalGirl>();

    public void AssignMagicalGirl(MagicalGirl mg)
    {

        if (currentTenants.Contains(mg))
            return;



        if(preset.maxPeople > currentTenants.Count)
        {
            if (mg.currentLocation != null)
                mg.currentLocation.DeassignMagicalGirl(mg);

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

    public void Demolish()
    {
        foreach (MagicalGirl mg in currentTenants)
        {
            DeassignMagicalGirl(mg);
        }
        LoCManager.instance.OnDestroyedBuilding(this);
        Destroy(this.gameObject);
    }
}
