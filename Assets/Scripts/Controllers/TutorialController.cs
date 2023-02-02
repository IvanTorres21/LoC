using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    [Header("GUI")]
    [SerializeField] private GameObject bottomItems;
    [SerializeField] private GameObject tutorialTextBox;
    [SerializeField] private GameObject anthonyImage;
    [SerializeField] private TextMeshProUGUI tutorialText;
    [SerializeField] private GameObject buttonScreen;
    [SerializeField] private GameObject blocker;

    [Header("Data")]
    [SerializeField] private BuildingPreset mainhall;
    [SerializeField] private BuildingPreset house;
    [SerializeField] private BuildingPreset portal;
    [SerializeField] private List<Sprite> sprites;

    [SerializeField] private int currentText = -1;

    [SerializeField] private List<string> texts = new List<string>();

    protected enum Expressions
    {
        BASE,
        HAPPY,
        SURPRISED,
        SCARED
    }

    private void Awake()
    {
        // First text 0-13
        texts.Add("Welcome to Law of Cycles Builder!");
        texts.Add("A certain Goddess is pretty busy with... things. So she decide to have you take care of this place in her stead!");
        texts.Add("My! What an honor! You should DEFINITELY spend as much time helping us as your short human life will allow you!");
        texts.Add("But of course, we are not going to just leave you here without knowing what's right or left");
        texts.Add("And that's why I'm here!");
        texts.Add("You see, I was the FIRST familiar the Goddess brought with her! So I basically know this place like the tips of my moustache!");
        texts.Add("First a few explanations of how things work here.");
        texts.Add("At the top you can see three icons and some numbers!");
        texts.Add("In order they are:\n the happiness average, you should keep this up if you want to last more than a month here!");
        texts.Add("The second thing is 'hope' which is what we will use to build things with!");
        texts.Add("And the latest one is our Karmic Power! It let us give our Goddess a call and ask for help!");
        texts.Add("But first, we need a good place for us to be in!");
        texts.Add("Let's build a mainhall first!");
        texts.Add("");

        // Second text 14-18
        texts.Add("Good job! You are already doing more than I expected you to be able to do!");
        texts.Add("Now, this wouldn't be our own Law of Cycles if we don't have any dead magical girls in it, don't you think so?");
        texts.Add("Thankfully, the Goddess is a nice person and left us with just enough Karmic Power to request one!");
        texts.Add("Click on the karmic power usage icon and select 'Get Magical Girl'");
        texts.Add("");

        // Third text 19-22
        texts.Add("Congratulation! You just pried the soul of a girl that has been manipulated into a life of fighting away from her loved ones!");
        texts.Add("I guess you must take responsability now!");
        texts.Add("Why don't you get her a home to live in? Or are you going to let her be homeless?");
        texts.Add("");

        // Fourth text 23-26
        texts.Add("Wow, Wow, Wow!");
        texts.Add("You easily do everything I tell you! Just how I like 'em!");
        texts.Add("Now click on the house and assign the girl we just 'saved'");
        texts.Add("");

        // Fifth text 27-29
        texts.Add("Now, this isn't a charity... Please, don't let the Goddess hear I said that");
        texts.Add("They have to work! Add a wraith slaying protal and we can make more hope to build more things!");
        texts.Add("");

        // Sixth text 30
        texts.Add("Make sure to not let her get too tired or depressed!");
        texts.Add("If a girl reaches 70% tiredness she won't be able to work anymore! And that ain't good!");
        texts.Add("Make sure to build places your girls would like and let them rest from time to time!");
        texts.Add("That's all I can teach you!");
        texts.Add("Now go and keep building this place up! The more girls we get around the livelier things will get");
        texts.Add("...");
        texts.Add("What happens if they reach 0 happiness?");
        texts.Add("I don't recommend letting them get there...");
        texts.Add("");
    }

    private void FixedUpdate()
    {
        if(currentText == 13 && LoCManager.instance.mainHall != null)
        {
            GetComponent<GuiController>().CloseMenus();
            tutorialTextBox.SetActive(true);
            buttonScreen.SetActive(true);
            ControlTutorial();
        }

        if(currentText == 18 && LoCManager.instance.magicalGirls.Count >= 1 && !blocker.activeSelf)
        {
            GetComponent<GuiController>().CloseMenus();
            tutorialTextBox.SetActive(true);
            buttonScreen.SetActive(true);
            ControlTutorial();
        }

        if(currentText == 22 && LoCManager.instance.buildings.Count >= 2)
        {
            GetComponent<GuiController>().CloseMenus();
            tutorialTextBox.SetActive(true);
            buttonScreen.SetActive(true);
            ControlTutorial();
        }

        if(currentText == 26 && LoCManager.instance.magicalGirls[0].home != null)
        {
            GetComponent<GuiController>().CloseMenus();
            tutorialTextBox.SetActive(true);
            buttonScreen.SetActive(true);
            ControlTutorial();
        }

        if(currentText == 29 && LoCManager.instance.magicalGirls[0].currentLocation != null)
        {
            GetComponent<GuiController>().CloseMenus();
            tutorialTextBox.SetActive(true);
            buttonScreen.SetActive(true);
            ControlTutorial();
        }
    }

    public void StartTutorial()
    {
        GetComponent<BuildingSelector>().canEscape = false;
        bottomItems.SetActive(false);
        tutorialTextBox.SetActive(true);
        ChangeSprite(Expressions.BASE);
        buttonScreen.SetActive(true);
        ControlTutorial();
    }

    public void ControlTutorial()
    {
        GoToNextText();
        if (currentText == 13)
        {
            PlaceMainHall();
        }

        if(currentText == 18)
        {
            GetMagicalGirl();
        }

        if(currentText == 22)
        {
            BuildHouse();
        }

        if (currentText == 26)
        {
            AssignGirl();
        }

        if (currentText == 29)
        {
            BuildPortal();
        }

        if (currentText == texts.Count-1)
        {
            EndTutorial();
        }

        // Sprite Management

        switch(currentText)
        {
            case 2:
                ChangeSprite(Expressions.SURPRISED);
                break;
            case 3:
                ChangeSprite(Expressions.BASE);
                break;
            case 4:
                ChangeSprite(Expressions.SURPRISED);
                break;
            case 6:
                ChangeSprite(Expressions.SURPRISED);
                break;
            case 7:
                ChangeSprite(Expressions.BASE);
                break;
            case 12:
                ChangeSprite(Expressions.SURPRISED);
                break;
            case 15:
                ChangeSprite(Expressions.BASE);
                break;
            case 18:
                ChangeSprite(Expressions.SURPRISED);
                break;
            case 19:
                ChangeSprite(Expressions.BASE);
                break;
            case 21:
                ChangeSprite(Expressions.SURPRISED);
                break;
            case 23:
                ChangeSprite(Expressions.HAPPY);
                break;
            case 25:
                ChangeSprite(Expressions.BASE);
                break;
            case 26:
                ChangeSprite(Expressions.SCARED);
                break;
            case 27:
                ChangeSprite(Expressions.BASE);
                break;
            case 31:
                ChangeSprite(Expressions.SURPRISED);
                break;
            case 32:
                ChangeSprite(Expressions.BASE);
                break;
            case 33:
                ChangeSprite(Expressions.SURPRISED);
                break;
            case 34:
                ChangeSprite(Expressions.BASE);
                break;
            case 37:
                ChangeSprite(Expressions.SCARED);
                break;
        }
    }

    private void BuildPortal()
    {
        tutorialTextBox.SetActive(false);
        buttonScreen.SetActive(false);
        GetComponent<BuildingSelector>().BeginPlacement(portal);
    }

    private void AssignGirl()
    {
        tutorialTextBox.SetActive(false);
        buttonScreen.SetActive(false);
        
    }

    private void BuildHouse()
    {
        tutorialTextBox.SetActive(false);
        buttonScreen.SetActive(false);
        GetComponent<BuildingSelector>().BeginPlacement(house);
    }

    private void GetMagicalGirl()
    {
        tutorialTextBox.SetActive(false);
        buttonScreen.SetActive(false);
        
    }

    private void PlaceMainHall()
    {
        tutorialTextBox.SetActive(false);
        buttonScreen.SetActive(false);
        GetComponent<BuildingSelector>().BeginPlacement(mainhall);
    }

    private void EndTutorial()
    {
        bottomItems.SetActive(true);
        tutorialTextBox.SetActive(false);
        buttonScreen.SetActive(false);
        GetComponent<BuildingSelector>().canEscape = true;
        TimeController.instance.ChangeSpeed(1);
    }

    private void GoToNextText()
    {
        currentText++;
        tutorialText.text = texts[currentText];
    }

    private void ChangeSprite(Expressions expression)
    {
        int index = 0;
        switch(expression)
        {
            case Expressions.HAPPY:
                index = 1;
                break;
            case Expressions.SURPRISED:
                index = 2;
                break;
            case Expressions.SCARED:
                index = 3;
                break;
            default:
                index = 0;
                break;
        }
        anthonyImage.GetComponent<Image>().sprite = sprites[index];
    }
}
