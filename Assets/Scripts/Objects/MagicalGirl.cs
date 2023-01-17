using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicalGirl : MonoBehaviour
{
    public MGPreset preset;

    public int happiness { get; private set; }
    private int level;
    private int xp;
    public int tiredness { get; private set; }

    public Building currentLocation { get; private set; }
    public Building home { get; private set; }

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
                    level = 1;
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
        modifier *= preset.depression;

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
        modifier *= preset.depression;

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
}
