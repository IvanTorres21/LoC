using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GuiController : MonoBehaviour
{
    [Header("Buildings Panels")]
    [SerializeField] private GameObject housesPanel;
    [SerializeField] private GameObject trainingPanel;
    [SerializeField] private GameObject hobbyPanel;
    [SerializeField] private GameObject servicesPanel;
    [SerializeField] private GameObject decorPanel;

    [Header("Options Panels")]
    [SerializeField] private GameObject optionPanel;
    [SerializeField] private GameObject kpPanel;
    [SerializeField] private GameObject mgPanel;

    [Header("Magical Girl Details")]
    [SerializeField] private GameObject scrollViewContent;
    [SerializeField] private GameObject mgDetailsPanel;
    [SerializeField] private GameObject prefabCardMg;


    [Header("General Details")]
    [SerializeField] private GameObject assignablePanel;
    [SerializeField] private GameObject assignableGirlPrefab;
    [SerializeField] private GameObject assignableScroller;

    [Header("Home Details")]
    [SerializeField] private GameObject houseInfoPanel;
    [SerializeField] private TextMeshProUGUI houseName;
    [SerializeField] private TextMeshProUGUI houseSizeType;
    [SerializeField] private TextMeshProUGUI houseLuxuryType;
    [SerializeField] private TextMeshProUGUI houseRelaxLevel;
    [SerializeField] private TextMeshProUGUI houseHappyLevel;
    [SerializeField] private TextMeshProUGUI houseTenantsAmount;
    [SerializeField] private GameObject tenantsScrollContent;
    [SerializeField] private GameObject prefabTenant;

    [Header("Other Building Details")]
    [SerializeField] private GameObject buildingInfoPanel;
    [SerializeField] private TextMeshProUGUI buildingName;
    [SerializeField] private TextMeshProUGUI buildingType;
    [SerializeField] private TextMeshProUGUI buildingSizeType;
    [SerializeField] private TextMeshProUGUI buildingLuxuryType;
    [SerializeField] private TextMeshProUGUI buildingRelaxLevel;
    [SerializeField] private TextMeshProUGUI buildingHappyLevel;
    [SerializeField] private TextMeshProUGUI buildingTenantsAmount;
    [SerializeField] private TextMeshProUGUI buildingHope;
    [SerializeField] private TextMeshProUGUI buildingMaintenance;
    [SerializeField] private GameObject buildingScrollContent;
    [SerializeField] private GameObject prefabBuildingTenant;

    [Header("Event Panels")]
    [SerializeField] private GameObject eventPanel;
    [SerializeField] private TextMeshProUGUI eventText;
    public GameObject options;
    [SerializeField] private GameObject blocker;
    public GameObject eventOption;

    [Header("Notification Panel")]
    [SerializeField] private GameObject notifPanel;
    [SerializeField] private TextMeshProUGUI notifText;
    [SerializeField] private GameObject notifContent;
    [SerializeField] private GameObject notifPrefab;
    [SerializeField] private GameObject newNotif;

    public void ChangePanelBuildings(GameObject panel)
    {
        hobbyPanel.SetActive(false);
        housesPanel.SetActive(false);
        trainingPanel.SetActive(false);
        servicesPanel.SetActive(false);
        decorPanel.SetActive(false);
        eventPanel.SetActive(false);
        buildingInfoPanel.SetActive(false);
        notifPanel.SetActive(false);
        panel.SetActive(true);
    }

    public void OpenMenu(GameObject menu)
    {
        optionPanel.SetActive(false);
        kpPanel.SetActive(false);
        mgPanel.SetActive(false);
        mgDetailsPanel.SetActive(false);
        menu.SetActive(true);
        if (menu.activeSelf && menu == mgPanel)
        {
            GetMagicalGirls();
        }

    }

    public void CloseMenus()
    {
        eventPanel.SetActive(false);
        buildingInfoPanel.SetActive(false);
        notifPanel.SetActive(false);
        optionPanel.SetActive(false);
        mgDetailsPanel.SetActive(false);
        kpPanel.SetActive(false);
        mgPanel.SetActive(false);
        houseInfoPanel.SetActive(false);
        assignablePanel.SetActive(false);
        buildingInfoPanel.SetActive(false);
    }

    private void GetMagicalGirls()
    {
        if(LoCManager.instance.magicalGirls.Count == scrollViewContent.transform.childCount)
        {
            return;
        }

        foreach (Transform tan in scrollViewContent.transform)
        {
            Destroy(tan.gameObject);
        }


        foreach (MagicalGirl girl in LoCManager.instance.magicalGirls)
        {
            GameObject card = Instantiate(prefabCardMg, scrollViewContent.transform);
            card.GetComponent<Image>().sprite = girl.preset.card;
            if (!girl.isAlive)
            {
                Debug.Log("O h");
                card.GetComponent<Image>().color = Color.red;
            }
                
            card.GetComponent<Button>().onClick.AddListener(() => SeeMgDetails(girl));
        }
    }

    public void SeeMgDetails(MagicalGirl girl)
    {
        mgDetailsPanel.SetActive(true);
        girl.FillGUIData();
    }

    public void SeeHomeDetails(Building building)
    {
        assignablePanel.SetActive(false);
        buildingInfoPanel.SetActive(false);
        houseInfoPanel.SetActive(true);
        houseName.text = building.preset.building_name;
        houseSizeType.text = "Size: " + building.preset.size.ToString();
        houseLuxuryType.text = "Luxury: " + building.preset.luxuryValue.ToString();
        houseRelaxLevel.text = "Relax: " + building.preset.relaxIndex.ToString();
        houseHappyLevel.text = "Happiness: " + building.preset.happyIndex.ToString();
        houseTenantsAmount.text = "Tenants (" + building.currentTenants.Count.ToString() + "/" + building.preset.maxPeople + ")";


        foreach (Transform tan in tenantsScrollContent.transform)
        {
            Destroy(tan.gameObject);
        }

        foreach (MagicalGirl mg in building.currentTenants)
        {
            GameObject mgTenant = Instantiate(prefabTenant, tenantsScrollContent.transform);
            mgTenant.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = mg.preset.MG_name;
            mgTenant.transform.GetChild(1).gameObject.GetComponent<Button>().onClick.AddListener(() => building.DeassignMagicalGirl(mg));
            mgTenant.transform.GetChild(1).gameObject.GetComponent<Button>().onClick.AddListener(() => SeeHomeDetails(building));
        }

    }

    public void SeeBuildingDetails(Building building)
    {
        assignablePanel.SetActive(false);
        houseInfoPanel.SetActive(false);
        buildingInfoPanel.SetActive(true);
        buildingName.text = building.preset.building_name;
        buildingSizeType.text = "Size: " + building.preset.size.ToString();
        buildingLuxuryType.text = "Luxury: " + building.preset.luxuryValue.ToString();
        if(building.preset.type == BuildingType.HOBBY && building.preset.type == BuildingType.FOOD)
            buildingRelaxLevel.text = "Relax: " + building.preset.relaxIndex.ToString();
        else
            buildingRelaxLevel.text = "Stress: " + building.preset.relaxIndex.ToString();
        buildingHappyLevel.text = "Happiness: " + building.preset.happyIndex.ToString();
        buildingTenantsAmount.text = "Being Used By (" + building.currentTenants.Count.ToString() + "/" + building.preset.maxPeople + ")";

        buildingHope.text = "Hope: +" + building.preset.hope;
        buildingMaintenance.text = "Cost Per Day: " + building.preset.cost;

        foreach (Transform tan in buildingScrollContent.transform)
        {
            Destroy(tan.gameObject);
        }

        foreach (MagicalGirl mg in building.currentTenants)
        {
            GameObject mgTenant = Instantiate(prefabBuildingTenant, buildingScrollContent.transform);
            mgTenant.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = mg.preset.MG_name;
            mgTenant.transform.GetChild(1).gameObject.GetComponent<Button>().onClick.AddListener(() => building.DeassignMagicalGirl(mg));
            mgTenant.transform.GetChild(1).gameObject.GetComponent<Button>().onClick.AddListener(() => SeeBuildingDetails(building));
        }

    }

    public void GetGirlsToAssign(Building building)
    {
        foreach(Transform tan in assignableScroller.transform)
        {
            Destroy(tan.gameObject);
        }

        foreach (MagicalGirl mg in LoCManager.instance.magicalGirls)
        {
            if (mg.isAlive)
            {
                GameObject assignMg = Instantiate(assignableGirlPrefab, assignableScroller.transform);
                assignMg.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = mg.preset.MG_name;
                assignMg.GetComponent<Button>().onClick.AddListener(() => building.AssignMagicalGirl(mg));
                if (building.preset.type == BuildingType.HOUSE)
                    assignMg.GetComponent<Button>().onClick.AddListener(() => SeeHomeDetails(building));
                else
                    assignMg.GetComponent<Button>().onClick.AddListener(() => SeeBuildingDetails(building));
            }
        }
    }

    public void ShowAssignablegirls()
    {
        Building currentBuilding = GetComponent<BuildingSelector>().selectedBuilding.GetComponent<Building>();
        assignablePanel.SetActive(true);
        if (currentBuilding != null)
            GetGirlsToAssign(currentBuilding);
    }

    public void ShowEventText(string text)
    {
        
        TimeController.instance.ChangeSpeed(0f);
        eventPanel.SetActive(true);
        blocker.SetActive(true);
        eventText.text = text;
    }

    public void CloseEventPanel()
    {
        TimeController.instance.ChangeSpeed(1f);
        eventPanel.SetActive(false);
        blocker.SetActive(false);
    }

    public void ShowNotifPanel()
    {
        foreach (Transform tan in notifContent.transform)
        {
            Destroy(tan.gameObject);
        }

        ChangePanelBuildings(notifPanel);
        notifText.text = EventManager.instance.GetEventNotificationText();
        ReadNotifications();

        for(int i = 0; i < LoCManager.instance.notifications.Count; i++)
        {
            GameObject notif = Instantiate(notifPrefab, notifContent.transform);
            notif.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = LoCManager.instance.notifications[i];
            notif.GetComponent<Button>().onClick.AddListener(() => LoCManager.instance.RemoveNotification(i));
            notif.GetComponent<Button>().onClick.AddListener(() => Destroy(notif));
        }
    }

    public void NewNotifAlert()
    {
        newNotif.SetActive(true);
    }

    public void ReadNotifications()
    {
        newNotif.SetActive(false);
    }
}
