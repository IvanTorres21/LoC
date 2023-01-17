using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LoCManager : MonoBehaviour
{
    public static LoCManager instance;

    private int maintenanceTotal;

    public int hope { get; private set; }
    private int karmicPower;
    private int avgHappiness;
    private int genHope;

    private Building mainHall;

    private List<MagicalGirl> magicalGirls;
    private List<Building> buildings;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        } else
        {
            Destroy(this);
        }
    }

    public void EndDay()
    {
        genHope = 0;
        avgHappiness = 0;
        foreach(MagicalGirl mg in magicalGirls)
        {
           CalculateMagicalGirlDay(mg);
        }

        foreach(Building bd in buildings)
        {
            if(bd.preset.passiveBuilding)
            {
                genHope += bd.preset.hope;
            }
        }

        // New amount of hope
        genHope -= maintenanceTotal;
        hope += genHope;

        // Karmic Power
        avgHappiness = avgHappiness / magicalGirls.Count();
        if(avgHappiness >= 40)
        {
            int aux = avgHappiness;

            aux = aux / 2;
            aux = aux / 10;

            karmicPower += aux;
        }

        //TODO: Possible special events?

    }

    private void CalculateMagicalGirlDay(MagicalGirl mg)
    {
        if (mg.currentLocation != null)
        {
            Building currentLoc = mg.currentLocation;
            // Earnings
            if (currentLoc.preset.hope != 0 && ( (float) mg.tiredness / (float) mg.preset.MAXTIREDNESS) <= .7f)
            {
                genHope += mg.currentLocation.preset.hope;
            }

            // Tiredness

            if (currentLoc.preset.type == BuildingType.HOBBY || currentLoc.preset.type == BuildingType.FOOD) // If is a relaxing activity
            {
                mg.DecreaseTiredness(currentLoc.preset.relaxIndex, currentLoc.preset);
            }
            else
            {
                mg.IncreaseTiredness(currentLoc.preset.relaxIndex, currentLoc.preset);
            }

            // Happiness

            if (mg.preset.likes.Contains(currentLoc.preset.type))
                mg.IncreaseHappiness(currentLoc.preset.happyIndex, currentLoc.preset);
            else if (mg.preset.dislikes.Contains(currentLoc.preset.type))
                mg.DecreaseHappiness(currentLoc.preset.happyIndex, currentLoc.preset);

            // XP

            if (currentLoc.preset.type == BuildingType.TRAINING)
            {
                mg.IncreaseXP(currentLoc.preset.xpIndex);
            }
        }
        else if (mg.home != null) // Is resting
        {
            if (mg.home.preset.luxuryValue >= mg.preset.luxuryLevel)
            {
                mg.DecreaseTiredness(mg.home.preset.relaxIndex, mg.home.preset);
                mg.IncreaseHappiness(mg.home.preset.happyIndex, mg.home.preset);
            }
            else
            {
                mg.DecreaseHappiness(mg.home.preset.happyIndex, mg.home.preset);
            }

        }

        if (mg.home == null) // Is homeless
        {
            mg.DecreaseHappiness(mainHall.preset.happyIndex, mainHall.preset);
            mg.IncreaseTiredness(mainHall.preset.relaxIndex, mainHall.preset);
        }

        avgHappiness += mg.happiness;
    }

    public void OnCreatedBuilding(Building building)
    {

    }

    public void OnDestroyedBuilding(Building building)
    {

    }

    public void OnAddedMagicalGirl(MagicalGirl mg)
    {

    }

    public void OnMagicalGirlDied(MagicalGirl mg)
    {

    }
}
