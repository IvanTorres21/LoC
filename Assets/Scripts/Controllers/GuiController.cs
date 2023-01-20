using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [SerializeField] private GameObject contextPanel;

    public void ChangePanelBuildings(GameObject panel)
    {
        hobbyPanel.SetActive(false);
        housesPanel.SetActive(false);
        trainingPanel.SetActive(false);
        servicesPanel.SetActive(false);
        decorPanel.SetActive(false);
        panel.SetActive(true);
    }

    public void OpenMenu(GameObject menu)
    {
        optionPanel.SetActive(false);
        kpPanel.SetActive(false);
        mgPanel.SetActive(false);
        menu.SetActive(true);
    }

    public void CloseMenus()
    {
        optionPanel.SetActive(false);
        kpPanel.SetActive(false);
        mgPanel.SetActive(false);
    }
}
