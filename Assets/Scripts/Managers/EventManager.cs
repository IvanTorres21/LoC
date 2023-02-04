using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using ZSerializer;

public class EventManager : PersistentMonoBehaviour
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
    [SerializeField] private List<BuildingPreset> buildings;

    [Header("Killer Event")]
    public MagicalGirl killer;
    public MagicalGirl currentVictim;

    [Header("Dispute Event")]
    public MagicalGirl mgFirst;
    public MagicalGirl mgSecond;
    [SerializeField] private List<string> disputes;

    [Header("Help Group && Tournament Event")]
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
        if(avaliableEvents.Count >= 2)
        {
            do
            {
                randomEvent = avaliableEvents[(int)Mathf.Floor(Random.Range(0f, avaliableEvents.Count))];
            } while (randomEvent == lastEvent);
        } else
        {
            randomEvent = avaliableEvents[0];
        }

        switch (randomEvent)
        {
            case GameEvents.HELPGROUP:
                lastEvent = GameEvents.HELPGROUP;
                HelpGroupEvent();
                break;
            case GameEvents.DISPUTE:
                lastEvent = GameEvents.DISPUTE;
                DisputeEvent();
                break;
            case GameEvents.FESTIVAL:
                lastEvent = GameEvents.FESTIVAL;
                FestivalEvent();
                break;
            case GameEvents.PETITION:
                lastEvent = GameEvents.PETITION;
                PetitionEvent();
                break;
            case GameEvents.TOURNAMENT:
                lastEvent = GameEvents.TOURNAMENT;
                break;
            default:
                break;
        }

        
    }

    #region LaunchEvents

    public void FestivalEvent()
    {
        

        currentEvent = GameEvents.FESTIVAL;
        string text = "The girl have talked about making a festival, but we need to decide on a theme and create the materials needed.";

        guiController.ShowEventText(text);

        GameObject option = Instantiate(guiController.eventOption, guiController.options.transform);
        option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Summer | -6500 hope";
        option.GetComponent<Button>().onClick.AddListener(() => PayForFestival(6500, 0));

        option = Instantiate(guiController.eventOption, guiController.options.transform);
        option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Norse | -5000 hope";
        option.GetComponent<Button>().onClick.AddListener(() => PayForFestival(5000, 1));

        option = Instantiate(guiController.eventOption, guiController.options.transform);
        option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Mediterranean | -5500 hope";
        option.GetComponent<Button>().onClick.AddListener(() => PayForFestival(5500, 2));

        option = Instantiate(guiController.eventOption, guiController.options.transform);
        option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Christmas | -15000 hope";
        option.GetComponent<Button>().onClick.AddListener(() => PayForFestival(15000, 3));

        option = Instantiate(guiController.eventOption, guiController.options.transform);
        option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Sorry, we can't afford anything...";
        option.GetComponent<Button>().onClick.AddListener(() =>EndEvent());

    }

    public void DisputeEvent()
    {

        int randomGirl = Mathf.FloorToInt(Random.Range(0f, LoCManager.instance.magicalGirls.Count));
        int randomGirl2 = 0;
        do
        {
            randomGirl2 = Mathf.FloorToInt(Random.Range(0f, LoCManager.instance.magicalGirls.Count));
        } while (randomGirl2 == randomGirl);

        string dispute = disputes[Mathf.FloorToInt(Random.Range(0f, disputes.Count))];

        mgFirst = LoCManager.instance.magicalGirls[randomGirl];
        mgSecond = LoCManager.instance.magicalGirls[randomGirl2];

        string text = "There has been a problem with two magical girls: " + mgFirst.preset.MG_name + ", " + mgSecond.preset.MG_name + "\n";
        text += mgFirst.preset.MG_name + " " + dispute;
        guiController.ShowEventText(text);

        GameObject option = Instantiate(guiController.eventOption, guiController.options.transform);

        currentEvent = GameEvents.DISPUTE;

        option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = mgFirst.preset.MG_name + " is in the right";
        option.GetComponent<Button>().onClick.AddListener(() => EndDispute(1));

        option = Instantiate(guiController.eventOption, guiController.options.transform);

        option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = mgSecond.preset.MG_name + " in in the right";
        option.GetComponent<Button>().onClick.AddListener(() => EndDispute(2));

    }

    public void PetitionEvent()
    {

        int randomGirl = Mathf.FloorToInt(Random.Range(0f, LoCManager.instance.magicalGirls.Count));
        int randomBuilding = Mathf.FloorToInt(Random.Range(0f,buildings.Count));

        petitioner = LoCManager.instance.magicalGirls[randomGirl];
        petitionPreset = buildings[randomBuilding];

        string text = petitioner.preset.MG_name + " came today to the Town Hall with a petition: \nBuild a " + petitionPreset.building_name;

        guiController.ShowEventText(text);

        GameObject option = Instantiate(guiController.eventOption, guiController.options.transform);

        currentEvent = GameEvents.PETITION;

        option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "I'll see what I can do!";
        option.GetComponent<Button>().onClick.AddListener(() => ClosePetition());
    }

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
                    else if (mgAux.preset.socialType == SocialType.LONE)
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
        option.GetComponent<Button>().onClick.AddListener(() => EndEvent());
    }

    public void KillerEvent(MagicalGirl mg)
    {
        int hopeLost = -500 * mg.level;

        killer = mg;

        string text = "News have come that one of the girls has reached her breaking point. And what's even worse, she has gone crazy and is now on a killing spree!\n";
        text += "\n" + mg.preset.MG_name + " has gone mad and is now attacking Magical Girls throguhout the LoC! We have to send someone to stop her";
        text += "\nWho do we send? (Magical girls sent can die in battle, and can't be changed until either of them fall)";

        guiController.ShowEventText(text);

        currentEvent = GameEvents.KILLER;

        if(LoCManager.instance.magicalGirls.Count > 1)
        {
            foreach (MagicalGirl mgAux in LoCManager.instance.magicalGirls)
            {
                if (mgAux.isAlive && mgAux.preset != mg.preset)
                {
                    GameObject option = Instantiate(guiController.eventOption, guiController.options.transform);

                    option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = mgAux.preset.MG_name;
                    option.GetComponent<Button>().onClick.AddListener(() => currentVictim = mgAux);
                    option.GetComponent<Button>().onClick.AddListener(() => ClosePetition());
                }
            }
        } else
        {
            GameObject option = Instantiate(guiController.eventOption, guiController.options.transform);

            option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "We don't have anyone to stop her";
            option.GetComponent<Button>().onClick.AddListener(() => ClosePetition());
        }

        LoCManager.instance.hope += hopeLost;

    }

    public void HelpGroupEvent()
    {
       
        foreach (MagicalGirl mgAux in LoCManager.instance.magicalGirls)
        {
            if (mgAux.isAlive)
            {
               if(participants.Count <= 10)
                {
                    if (Random.Range(0f, 100f) <= (100 - (participants.Count * 10)))
                    {
                        participants.Add(mgAux);
                    }
                }
            }
        }

        string text = "A group of magical girls wanted to do a session of group therapy together, this would greatly benefit their mental health.\n";
        text += "The participants are: \n";
        foreach(MagicalGirl mg in participants)
        {
            text += mg.preset.MG_name + ", ";
        }

        int price = 100 + LoCManager.instance.magicalGirls.Count * 400;

        text += "\nThe event will cost " + price + " and every girl happiness will increase in 40% and decrease its tiredness in 25%";

        GameObject option = Instantiate(guiController.eventOption, guiController.options.transform);

        currentEvent = GameEvents.HELPGROUP;

        option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Of course, it's important to take care of yourself";
        option.GetComponent<Button>().onClick.AddListener(() => PayHelpGroup(price));

        option = Instantiate(guiController.eventOption, guiController.options.transform);

        option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Sorry, we can't afford it";
        option.GetComponent<Button>().onClick.AddListener(() => EndEvent());

        guiController.ShowEventText(text);
    }

    #endregion

    #region EndEvents

    public void EndPetitionEvent(BuildingPreset preset)
    {
        if(preset == petitionPreset)
        {
            petitioner.IncreaseHappiness(35, preset);
            currentEvent = GameEvents.NONE;
            petitioner = null;
        }
    }

    public void EndKillerEvent()
    {
        string text = "We have finally stopped " + killer.preset.MG_name + " and their killing spree, sadly her soul could not be saved.";
        text += "\nWe are grateful to " + currentVictim.preset.MG_name + " for their help during this time.";

        guiController.ShowEventText(text);

        GameObject option = Instantiate(guiController.eventOption, guiController.options.transform);

        option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Things could have been better...";
        option.GetComponent<Button>().onClick.AddListener(() => EndEvent());

        killer.isAlive = false;
        currentVictim = null;
        killer = null;

    }

    #endregion

    #region EventOptions
        
    public void FightKillerMg()
    {
        int atkDiff = killer.CalculateAtkModifier() - currentVictim.CalculateDefModifier();
        int defDiff = killer.CalculateDefModifier() - currentVictim.CalculateDefModifier();

        if(atkDiff > 0 || defDiff > 0) 
        {
            currentVictim.LoseBattle(atkDiff + defDiff);
            int hopeLost = -300;
            LoCManager.instance.hope += hopeLost;
        }

        if(atkDiff < 0 || defDiff < 0 )
        {
            killer.LoseBattle(atkDiff + defDiff);
        }

       
    }

    public void PayForFestival(int price, int type)
    {
        LoCManager.instance.hope -= price;

        foreach (MagicalGirl mgAux in LoCManager.instance.magicalGirls)
        {
            if(mgAux.isAlive)
            {
                switch(type)
                {
                    case 0:
                        mgAux.happiness += 20;
                        break;
                    case 1:
                        if(mgAux.preset.likes.Contains(BuildingType.TRAINING))
                            mgAux.happiness += 30;
                        else
                            mgAux.happiness += 10;
                        break;
                    case 2:
                        if(mgAux.preset.socialType == SocialType.SOCIAL)
                            mgAux.happiness += 30;
                        else
                            mgAux.happiness += 10;
                        break;
                    case 3:
                        mgAux.happiness += 50;
                        break;
                    default:
                        break;
                }
                if (mgAux.happiness > 100)
                    mgAux.happiness = 100;
            }
        }

        EndEvent();
    }

    public void KilledMagicalGirl()
    {
        currentVictim.isAlive = false;

        if (currentVictim.preset.socialType == SocialType.SOCIAL)
        {
            foreach (MagicalGirl mgAux in LoCManager.instance.magicalGirls)
            {
                if (mgAux.isAlive)
                {
                    if (mgAux.preset.socialType == SocialType.SOCIAL)
                    {
                        mgAux.happiness -= 35;

                    }
                    else if (mgAux.preset.socialType == SocialType.LONE)
                    {
                        mgAux.happiness -= 10;
                    }

                    if (mgAux.happiness < 0)
                        mgAux.happiness = 0;
                }
            }
        }

        int hopeLost = -500 * currentVictim.level;
        LoCManager.instance.hope += hopeLost;
        LoCManager.instance.OnMagicalGirlDied(currentVictim);

        GameObject option = Instantiate(guiController.eventOption, guiController.options.transform);

        option.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = killer.preset.MG_name + " must be stopped";
        option.GetComponent<Button>().onClick.AddListener(() => EndEvent());

    }

    public void EndEvent()
    {
        currentEvent = GameEvents.NONE;
        guiController.CloseEventPanel();
        foreach (Transform tran in guiController.options.transform)
        {
            Destroy(tran.gameObject);
        }
    }

    public void ClosePetition()
    {
        guiController.CloseEventPanel();
        foreach (Transform tran in guiController.options.transform)
        {
            Destroy(tran.gameObject);
        }
    }

    public void PayHelpGroup(int price)
    {
        LoCManager.instance.hope -= price;

        foreach(MagicalGirl mg in participants)
        {
            mg.happiness += 40;
            mg.tiredness -= 25;
            if (mg.happiness > 100)
                mg.happiness = 100;
            if (mg.tiredness < 0)
                mg.tiredness = 0;
        }

        EndEvent();

    }

    public void EndDispute(int number)
    {
        if(number == 1)
        {
            mgFirst.happiness += 10;
            if (mgFirst.happiness > 100)
                mgFirst.happiness = 100;
            mgSecond.happiness -= 10;
            if (mgSecond.happiness < 0)
                mgSecond.happiness = 0;
        } else
        {
            mgFirst.happiness -= 10;
            if (mgFirst.happiness < 0)
                mgFirst.happiness = 0;

            mgSecond.happiness += 10;
            if (mgSecond.happiness > 100)
                mgSecond.happiness = 100;
        }

        EndEvent();
    }

    #endregion


    public string GetEventNotificationText()
    {
        string text = "";
        switch(currentEvent)
        {
            case GameEvents.HELPGROUP:
                text = "";
                break;
            case GameEvents.DISPUTE:
                text = "";
                break;
            case GameEvents.FESTIVAL:
                text = "";
                break;
            case GameEvents.PETITION:
                text = petitioner.preset.MG_name + " has requested a " + petitionPreset.building_name;
                break;
            case GameEvents.TOURNAMENT:
                text = "";
                break;
            default:
                text = "There is no ongoing event";
                break;
        }
        return text;
    }

    public override void OnPostLoad()
    {
        instance = this;
        base.OnPostLoad();
    }

}
