using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ZSerializer;

public class MagicalGirl : PersistentMonoBehaviour
{
    public MGPreset preset;

    public bool isAlive = true;

    public int happiness = 80;
    public int level = 1;
    public int xp = 0;
    public int tiredness;
    public int daysTillBreaking = 0;

    public Building currentLocation;
    public Building home;

    [Header("Magical Girl Panel UI")]
    public Image cardImg;
    public TextMeshProUGUI txtName;
    public TextMeshProUGUI txtXp;
    public TextMeshProUGUI txtLevel;
    public TextMeshProUGUI txtHappy;
    public TextMeshProUGUI txtTiredness;
    public TextMeshProUGUI txtLikes;
    public TextMeshProUGUI txtDislikes;
    public TextMeshProUGUI txtAtk;
    public TextMeshProUGUI txtDef;
    public TextMeshProUGUI txtLuxury;

    private void Start()
    {
        LoadReferencesToGui();
    }

    public void ChangeLocation(Building newLocation)
    {
       
        currentLocation = newLocation;
    }

    public void ChangeHome(Building newHome)
    {
       
        home = newHome;
    }

    public void IncreaseXP(int xpIncrease)
    {
       if(xp != 6500)
        {
            xp += xpIncrease;
            if (xp > 6500)
                xp = 6500;

            if(xp >= 6500 && level != 10)
            {
                level = 10;
                LoCManager.instance.AddNotification(preset.MG_name + " has leveled up!");
            } else if (xp >= 5000 && level != 9) {
                level = 9;
                LoCManager.instance.AddNotification(preset.MG_name + " has leveled up!");
            } else if (xp >= 4000 && level != 8)
            {
                level = 8;
                LoCManager.instance.AddNotification(preset.MG_name + " has leveled up!");
            }
            else if (xp >= 3300 && level != 7)
            {
                level = 7;
                LoCManager.instance.AddNotification(preset.MG_name + " has leveled up!");
            }
            else if(xp >= 2500 && level != 6)
            {
                level = 6;
                LoCManager.instance.AddNotification(preset.MG_name + " has leveled up!");
            } else if (xp >= 1700 && level != 5)
            {
                level = 5;
                LoCManager.instance.AddNotification(preset.MG_name + " has leveled up!");
            }
            else if (xp >= 1000 && level != 4)
            {
                level = 4;
                LoCManager.instance.AddNotification(preset.MG_name + " has leveled up!");
            }
            else if (xp >= 500 && level != 3)
            {
                level = 3;
                LoCManager.instance.AddNotification(preset.MG_name + " has leveled up!");
            }
            else if (xp >= 200 && level != 2)
            {
                level = 2;
                LoCManager.instance.AddNotification(preset.MG_name + " has leveled up!");
            }

        }
    }

    public void IncreaseTiredness(int amount, BuildingPreset building)
    {

        float modifier = NegativeModifier(building.type, building.activityType, building.luxuryValue);

        tiredness += (int) (amount * modifier);

        if (tiredness > preset.MAXTIREDNESS)
            tiredness = preset.MAXTIREDNESS;
    }

    public void IncreaseHappiness(int amount, BuildingPreset building)
    {
        float modifier = PositiveModifier(building.type, building.activityType, building.luxuryValue);
        modifier -= preset.depression;

        happiness += (int)(amount * modifier);

        if (happiness > 100)
            happiness = 100;
    }

    public void DecreaseTiredness(int amount, BuildingPreset building)
    {
        float modifier = PositiveModifier(building.type, building.activityType, building.luxuryValue);

        tiredness -= (int)(amount * modifier);

        if (tiredness < 0)
            tiredness = 0;
    }

    public void DecreaseHappiness(int amount, BuildingPreset building)
    {
        float modifier = NegativeModifier(building.type, building.activityType, building.luxuryValue);
        modifier += preset.depression;

        happiness -= (int)(amount * modifier);

        if (happiness < 0)
            happiness = 0;

        if(happiness <= 10)
        {
            daysTillBreaking++;
            if (daysTillBreaking >= preset.breakingPoint)
                ReachBreakingPoint();
        } else
        {
            daysTillBreaking--;
            if (daysTillBreaking < 0)
                daysTillBreaking = 0;
        }
    }


    private float PositiveModifier(BuildingType activity, SocialType activityType, Luxury luxuryLevel)
    {
        float modifier = 1f;
        if (preset.likes.Contains(activity))
        {
            modifier = 1.2f;
        }
        else if (preset.dislikes.Contains(activity))
        {
            modifier = .8f;
        }

        if (preset.socialType == activityType)
        {
            modifier += .1f;
        }

        if (preset.luxuryLevel <= luxuryLevel)
        {
            modifier += .1f;
        }
        return modifier;
    }

    private float NegativeModifier(BuildingType activity, SocialType activityType, Luxury luxuryLevel)
    {
        float modifier = 1f;
        if (preset.likes.Contains(activity))
        {
            modifier = .8f;
        }
        else if (preset.dislikes.Contains(activity))
        {
            modifier = 1.2f;
        }

        if (preset.socialType == activityType)
        {
            modifier -= .1f;
        }

        if (preset.luxuryLevel >= luxuryLevel)
        {
            modifier -= .1f;
        }

        return modifier;
    }

    public void LoseBattle(int totalDiff)
    {
        if (totalDiff <= 500)
            tiredness += 20;
        else if (totalDiff > 500 && totalDiff <= 1500)
        {
            tiredness += 30;
        } else if (totalDiff > 1500 && totalDiff <= 3000)
        {
            tiredness += 40;
        } else if (totalDiff > 0)
        {
            tiredness += 50;
        }

        if(tiredness >= 100)
        {
            tiredness = 100;
            Die();
        }
    }

    public int CalculateAtkModifier()
    {
        float mod = preset.modATK * level; // 1 * 3 = 3
        mod = mod / 10; // .3
        mod += 1; // 1.3
        return (int) (preset.baseATK * mod);
    }

    public int CalculateDefModifier()
    {
        float mod = preset.modDEF * level; // 1 * 3 = 3
        mod = mod / 10; // .3
        mod += 1; // 1.3
        return (int)(preset.baseDEF * mod);
    }

    private string UntilNextLevel()
    {
        string result = "";
        result = xp + "/";
        switch(level)
        {
            case 1:
                result += "200";
                break;
            case 2:
                result += "500";
                break;
            case 3:
                result += "1000";
                break;
            case 4:
                result += "1700";
                break;
            case 5:
                result += "2500";
                break;
            case 6:
                result += "3300";
                break;
            case 7:
                result += "4000";
                break;
            case 8:
                result += "5000";
                break;
            case 9:
                result += "6500";
                break;
            case 10:
                result += "6500";
                break;

        }
        return result;
    }

    public void FillGUIData()
    {
        cardImg.sprite = preset.card;
        txtName.text = preset.MG_name;
        txtLevel.text = "Level: " + level.ToString();
        txtXp.text = UntilNextLevel();
        txtAtk.text = CalculateAtkModifier().ToString();
        txtDef.text = CalculateDefModifier().ToString();
        txtHappy.text = "Happiness: " + happiness +"%";
        txtTiredness.text = "Tiredness: " + tiredness + "%";
        txtLikes.text = "";
        txtDislikes.text = "";
        txtLuxury.text = "Luxury: " + preset.luxuryLevel.ToString();
        foreach(BuildingType type in preset.likes)
        {
            txtLikes.text += type.ToString() + "\n";
        }
        foreach(BuildingType type in preset.dislikes)
        {
            txtDislikes.text += type.ToString() + "\n";
        }
    }

    public void moveLocation(Building newLoc)
    {
        currentLocation = newLoc;
    }

    private void LoadReferencesToGui()
    {
        cardImg = ReferenceHolder.instance.cardImg;
        txtHappy = ReferenceHolder.instance.txtHappy;
        txtAtk = ReferenceHolder.instance.txtAtk;
        txtDef = ReferenceHolder.instance.txtDef;
        txtDislikes = ReferenceHolder.instance.txtDislikes;
        txtLevel = ReferenceHolder.instance.txtLevel;
        txtLikes = ReferenceHolder.instance.txtLikes;
        txtLuxury = ReferenceHolder.instance.txtLuxury;
        txtName = ReferenceHolder.instance.txtName;
        txtTiredness = ReferenceHolder.instance.txtTiredness;
        txtXp = ReferenceHolder.instance.txtXp;
        
    }

    private void ReachBreakingPoint()
    {

        LoCManager.instance.AddNotification(preset.MG_name + " has reached her breaking point.");
      if(EventManager.instance.currentEvent != GameEvents.KILLER)
        {
            if (preset.socialType == SocialType.PROBLEMATIC)
            {
                EventManager.instance.KillerEvent(this);
            }
            else
            {
                EventManager.instance.SuicideEvent(this);
            }
        }
    }

    private void Die()
    {
        LoCManager.instance.AddNotification(preset.MG_name + " has died.");

        if (currentLocation != null)
            currentLocation.DeassignMagicalGirl(this);
        if (home != null)
            home.DeassignMagicalGirl(this);
        if (EventManager.instance.currentVictim == this)
        {
            EventManager.instance.KilledMagicalGirl();
        } else
        {
            EventManager.instance.EndKillerEvent();
        }
    }
}
