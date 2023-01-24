using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class LoCManager : MonoBehaviour
{
    public static LoCManager instance;

    private int maintenanceTotal;

    public int hope { get; private set; } = 6000;
    public int karmicPower { get; private set; } = 20;
    private int avgHappiness;
    private int genHope;

    private Building mainHall;

    public List<MagicalGirl> magicalGirls { get; private set; }
    private List<Building> buildings;

    [SerializeField] private TextMeshProUGUI txtHope;
    [SerializeField] private TextMeshProUGUI txtKP;
    [SerializeField] private TextMeshProUGUI txtHappy;

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

    private void Start()
    {
        magicalGirls = new List<MagicalGirl>();
        buildings = new List<Building>();
        UpdateGui();
    }

    public void EndDay()
    {
        genHope = 0;
        avgHappiness = 0;
        foreach(MagicalGirl mg in magicalGirls)
        {
           if(mg.isAlive)
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
        if(magicalGirls.Count > 0)
        {
            avgHappiness = avgHappiness / magicalGirls.Count();
        }


        if(avgHappiness >= 40)
        {
            int aux = avgHappiness;

            aux = aux / 2;
            aux = aux / 10;

            karmicPower += aux;
        }

        //TODO: Possible special events?
        UpdateGui();
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

    private void UpdateGui()
    {
        txtKP.text = karmicPower.ToString();
        txtHappy.text = avgHappiness.ToString() + "%";
        txtHope.text = hope.ToString();
    }

    public void OnCreatedBuilding(Building building)
    {
        hope -= building.preset.cost;
        maintenanceTotal += building.preset.maintenance;
        buildings.Add(building);
        UpdateGui();
    }

    public void OnDestroyedBuilding(Building building)
    {
        hope += building.preset.cost / 5;
        maintenanceTotal -= building.preset.maintenance;
        buildings.Remove(building);
        UpdateGui();
    }

    public void OnAddedMagicalGirl(MagicalGirl mg)
    {
        karmicPower -= 5;
        magicalGirls.Add(mg);
        RecalculateHappiness();
        UpdateGui();
    }

    public void OnMagicalGirlDied(MagicalGirl mg)
    {
        mg.isAlive = false;
        UpdateGui();
    }

    private void RecalculateHappiness()
    {
        avgHappiness = 0;
        foreach(MagicalGirl mg in magicalGirls)
        {
            if(mg.isAlive)
            {
                avgHappiness += mg.happiness;
            }
        }

        avgHappiness = avgHappiness / magicalGirls.Count();
    }
}
