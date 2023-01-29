using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    public GameEvents lastEvent;
    public List<GameEvents> avaliableEvents = new List<GameEvents>();

    public GameEvents currentEvent;

    public static EventManager instance;

    private GuiController guiController;

    // Particular event variables
    [Header("Petition Event")]
    public BuildingPreset petitionPreset;
    public MagicalGirl petitioner;

    [Header("Killer Event")]
    public MagicalGirl killer;
    public MagicalGirl currentVictim;

    [Header("Dispute Event")]
    public MagicalGirl mgFirst;
    public MagicalGirl mgSecond;

    [Header("Help Group && Tournament Event")]
    public Problems problem;
    public List<MagicalGirl> participants = new List<MagicalGirl>();


    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        guiController = GetComponent<GuiController>();
    }

    public void GetRandomEvent()
    {
        GameEvents randomEvent;
        do
        {
            randomEvent = avaliableEvents[(int)Mathf.Floor(Random.Range(0f, avaliableEvents.Count))];
        } while (randomEvent == lastEvent);

        switch (randomEvent)
        {
            case GameEvents.HELPGROUP:
                break;
            case GameEvents.DISPUTE:
                break;
            case GameEvents.FESTIVAL:
                break;
            case GameEvents.PETITION:
                break;
            case GameEvents.TOURNAMENT:
                break;
            default:
                break;
        }
    }

    #region LaunchEvents

    public void SuicideEvent(MagicalGirl mg)
    {
        int hopeLost = 0;

        if(mg.preset.socialType == SocialType.LONE)
        {
            hopeLost -= 500;
        } else if(mg.preset.socialType == SocialType.PROBLEMATIC)
        {
            hopeLost -= 200;    
        } else
        {
            hopeLost -= 1000;
        }

        hopeLost *= mg.level;

        string text = "News have come that one of the girls has reached her breaking point.\n";
        if (mg.preset.socialType == SocialType.LONE)
            text += mg.preset.MG_name + " was found dead this morning by one of our familiars as it did its morning routine.\n";
        else
            text += mg.preset.MG_name + " was found dead this morning by one of her friends.\n";
        text += "A funeral for the girl will be held this afternoon for those that were close to her.\n";

        text += "\nFuneral price: " + hopeLost;

        guiController.ShowEventText(text);

        foreach (Transform tan in guiController.options.transform)
        {
            Destroy(tan.gameObject);
        }

        if(mg.preset.socialType == SocialType.SOCIAL)
        {
            foreach(MagicalGirl mgAux in LoCManager.instance.magicalGirls)
            {
                if(mgAux.isAlive)
                {
                    if (mgAux.preset.socialType == SocialType.SOCIAL)
                    {
                        mgAux.happiness -= 35;

                    }
                    else if (mg.preset.socialType == SocialType.LONE)
                    {
                        mgAux.happiness -= 10;
                    }

                    if (mgAux.happiness < 0)
                        mgAux.happiness = 0;
                }
            }
        }

        LoCManager.instance.hope += hopeLost;
        LoCManager.instance.OnMagicalGirlDied(mg);

        GameObject option = Instantiate(guiController.eventOption, guiController.options.transform);

        option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "I could have prevented this...";
        option.GetComponent<Button>().onClick.AddListener(() => guiController.CloseEventPanel());
    }

    public void KillerEvent(MagicalGirl mg)
    {
        //TODO: Set up the killer event until solved;
    }

    #endregion

    #region EndEvents

    public void EndPetitionEvent(BuildingPreset preset)
    {
        if(preset == petitionPreset)
        {
            petitioner.IncreaseHappiness(35, preset);
            currentEvent = GameEvents.NONE;
        }
    }

    #endregion

}
