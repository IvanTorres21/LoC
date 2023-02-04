using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using ZSerializer;

public class LoCManager : PersistentMonoBehaviour
{
    public static LoCManager instance;

    private int maintenanceTotal;

    public int hope = 3000;
    public int karmicPower = 30;
    public int avgHappiness;
    private int genHope;

    public Building mainHall;

    public List<MagicalGirl> magicalGirls;
    public List<Building> buildings;

    [SerializeField] private TextMeshProUGUI txtHope;
    [SerializeField] private TextMeshProUGUI txtKP;
    [SerializeField] private TextMeshProUGUI txtHappy;

    public List<string> notifications = new List<string>();


    public int daysSinceLastEvent = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }


    private void Start()
    {
        
        magicalGirls = new List<MagicalGirl>();
        buildings = new List<Building>();
        LoadReferencesToGui();
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
           RecalculateHappiness();
        }


        if(avgHappiness >= 60)
        {
            int aux = avgHappiness;

            aux = aux / 2;
            aux = aux / 10;

            karmicPower += aux;
        }

        if(EventManager.instance.currentEvent == GameEvents.KILLER && EventManager.instance.currentVictim != null)
        {
            EventManager.instance.FightKillerMg();
        }

        CheckIfRandomEvent();

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
                genHope += mg.currentLocation.preset.hope * (1 + (mg.level / 10));
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

            if(((float)mg.tiredness / (float)mg.preset.MAXTIREDNESS) <= .7f)
            {
                if (mg.preset.likes.Contains(currentLoc.preset.type))
                    mg.IncreaseHappiness(currentLoc.preset.happyIndex, currentLoc.preset);
                else if (mg.preset.dislikes.Contains(currentLoc.preset.type))
                    mg.DecreaseHappiness(currentLoc.preset.happyIndex, currentLoc.preset);
            } else
            {
                mg.DecreaseHappiness(currentLoc.preset.happyIndex, currentLoc.preset);
            }

            if(mg.home.preset.luxuryValue < mg.preset.luxuryLevel)
            {
                AddNotification(mg.preset.MG_name + " isn't in a house of her luxury level");
                mg.DecreaseHappiness(mg.home.preset.happyIndex, mg.home.preset);
            }
            

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
                AddNotification(mg.preset.MG_name + " isn't in a house of her luxury level");
                mg.DecreaseHappiness(mg.home.preset.happyIndex, mg.home.preset);
            }

        }

        if (mg.home == null) // Is homeless
        {
            AddNotification(mg.preset.MG_name + " is homeless!");
            mg.DecreaseHappiness(mainHall.preset.happyIndex, mainHall.preset);
            mg.IncreaseTiredness(mainHall.preset.relaxIndex, mainHall.preset);
        }

        if(mg.happiness == 0) {
            AddNotification(mg.preset.MG_name + " happiness is in critical levels!");
        }

        if (((float)mg.tiredness / (float)mg.preset.MAXTIREDNESS) > .7f)
        {
            AddNotification(mg.preset.MG_name + " is to tired to keep working!");
        }

        avgHappiness += mg.happiness;
    }

    
    private void CheckIfRandomEvent()
    {
        if (EventManager.instance.currentEvent != GameEvents.NONE) //If there is an event running stop
            return;

        float chance = Random.Range(0f, 100f);
        if(chance <= daysSinceLastEvent * 5)
        {
            daysSinceLastEvent = 0;
            EventManager.instance.GetRandomEvent();
        } else
        {
            daysSinceLastEvent++;
        }
    }

    public void UpdateGui()
    {
        txtKP.text = karmicPower.ToString();
        txtHappy.text = avgHappiness.ToString() + "%";
        txtHope.text = hope.ToString();
    }

    public void OnCreatedBuilding(Building building)
    {
        if(building.preset.type == BuildingType.MAINHALL)
        {
            mainHall = building;
        }
        hope -= building.preset.cost;
        maintenanceTotal += building.preset.maintenance;
        buildings.Add(building);
        if(EventManager.instance.currentEvent == GameEvents.PETITION)
        {
            EventManager.instance.EndPetitionEvent(building.preset);
        }
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
        karmicPower -= 30;
        magicalGirls.Add(mg);
        RecalculateHappiness();
        CheckIfNewEventAdded();
        UpdateGui();
    }

    public void OnMagicalGirlDied(MagicalGirl mg)
    {
        mg.isAlive = false;
        RecalculateHappiness();
        UpdateGui();
    }

    private void RecalculateHappiness()
    {
        avgHappiness = 0;
        int amount = 0;
        foreach(MagicalGirl mg in magicalGirls)
        {
            if(mg.isAlive)
            {
                avgHappiness += mg.happiness;
                amount++;
            }
        }



        if(magicalGirls.Count > 0)
            avgHappiness = avgHappiness / (amount >= 1 ? amount : 1);

        if(avgHappiness < 60)
        {
            AddNotification("Average happiness is low! We are not generating Karmic Power!");
        }
    }

    public override void OnPostLoad()
    {
        base.OnPostLoad();
        instance = this;
        
        LoadReferencesToGui();
        RecalculateHappiness();
        UpdateGui();
    }

    private void LoadReferencesToGui()
    {
        txtHappy = ReferenceHolder.instance.txtHappyLoC;
        txtHope = ReferenceHolder.instance.txtHopeLoC;
        txtKP = ReferenceHolder.instance.txtKPLoC;
    }

    private void CheckIfNewEventAdded()
    {
        if(magicalGirls.Count >= 1)
        {
            if (!EventManager.instance.avaliableEvents.Contains(GameEvents.PETITION))
                EventManager.instance.avaliableEvents.Add(GameEvents.PETITION);
        }

        if (magicalGirls.Count >= 2)
        {
            if(!EventManager.instance.avaliableEvents.Contains(GameEvents.DISPUTE))
                EventManager.instance.avaliableEvents.Add(GameEvents.DISPUTE);   
        }

        if( magicalGirls.Count >= 5)
        {
            if(!EventManager.instance.avaliableEvents.Contains(GameEvents.HELPGROUP))
                EventManager.instance.avaliableEvents.Add(GameEvents.HELPGROUP);

            if (!EventManager.instance.avaliableEvents.Contains(GameEvents.FESTIVAL))
                EventManager.instance.avaliableEvents.Add(GameEvents.FESTIVAL);
        }

        if(magicalGirls.Count >= 7)
        {
            if (!EventManager.instance.avaliableEvents.Contains(GameEvents.TOURNAMENT))
                EventManager.instance.avaliableEvents.Add(GameEvents.TOURNAMENT);
        }    
    }

    public void AddNotification(string message)
    {
        GetComponent<GuiController>().NewNotifAlert();
        string notif = "Day: " + TimeController.instance.day + " | " + message;
        notifications.Add(notif);
    }

    public void RemoveNotification(int index)
    {
        notifications.RemoveAt(index);
    }

    public void ReviveMagicalGirl()
    {
        if(karmicPower >= 400)
        {
            karmicPower -= 400;
            magicalGirls.Find(x => !x.isAlive).isAlive = true;
        }
    }

    public void RecieveHope()
    {
        if (karmicPower >= 50)
        {
            karmicPower -= 50;
            hope += 5000;
        }
    }
}
