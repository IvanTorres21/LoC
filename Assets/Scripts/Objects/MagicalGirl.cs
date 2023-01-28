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
    private int level = 1;
    private int xp = 0;
    public int tiredness;

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

            switch(xp)
            {
                case 200:
                    level = 2;
                    break;
                case 500:
                    level = 3;
                    break;
                case 1000:
                    level = 4;
                    break;
                case 1700:
                    level = 5;
                    break;
                case 2500:
                    level = 6;
                    break;
                case 3300:
                    level = 7;
                    break;
                case 4000:
                    level = 8;
                    break;
                case 5000:
                    level = 9;
                    break;
                case 6500:
                    level = 10;
                    break;
                default:
                    break;
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
}
