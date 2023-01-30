using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Presets;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingSelector : MonoBehaviour
{
    private const float UPDATE_TIMER = 0.05f;

    private bool isPlacing = false;
    private BuildingPreset currentPreset;
    private Vector3 currentPos;
    private bool canPlace = true;
    private bool isDemolishing = false;

    
    private GameObject building; // Model of the building to place
    public GameObject selectedBuilding { get; private set; } // Gameobject that's been selected

    [Header("Building placer")]
    [SerializeField] private GameObject indicator;
    [SerializeField] private Material placeableMaterial;
    [SerializeField] private Material unplaceableMaterial;
    [SerializeField] private GameObject DemolishWarning;

    [Header("Building info")]
    [SerializeField] private GameObject infoPanel;
    [SerializeField] private TextMeshProUGUI buildingName;
    [SerializeField] private TextMeshProUGUI buildingInfo;

    [Header("Sounds")]
    [SerializeField] private AudioSource audioPlayer;
    [SerializeField] private AudioClip placeSound;
    [SerializeField] private AudioClip demolishSound;
    


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            EndPlacemet();
        }

        if(isPlacing)
        {
            if(currentPreset.type == BuildingType.DECORATION)
            { 
                if (Input.GetKey(KeyCode.Q))
                {

                    building.transform.rotation = Quaternion.Euler(building.transform.rotation.eulerAngles.x, building.transform.rotation.eulerAngles.y + 1f, 0f);
                }
                else if (Input.GetKey(KeyCode.E))
                {
                    building.transform.rotation = Quaternion.Euler(building.transform.rotation.eulerAngles.x, building.transform.rotation.eulerAngles.y - 1f, 0f);
                }
            } else
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {

                    building.transform.rotation = Quaternion.Euler(building.transform.rotation.eulerAngles.x, building.transform.rotation.eulerAngles.y + 90f, 0f);
                }
                else if (Input.GetKeyDown(KeyCode.E))
                {
                    building.transform.rotation = Quaternion.Euler(building.transform.rotation.eulerAngles.x, building.transform.rotation.eulerAngles.y - 90f, 0f);
                }
            }
            


            if (canPlace && Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                if(!TileSelector.instance.CheckIsOccupied() || currentPreset.type == BuildingType.DECORATION)
                {
                    audioPlayer.volume = 1f;
                    audioPlayer.PlayOneShot(placeSound);
                    GameObject bd = Instantiate(currentPreset.prefab, currentPos, Quaternion.Euler(0f, building.transform.rotation.eulerAngles.y, 0f));
                    LoCManager.instance.OnCreatedBuilding(bd.GetComponent<Building>());
                }
                

            }
        } else if(isDemolishing)
        {
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                GameObject bd = TileSelector.instance.GetClickedBuildingDemolishing();
                if(bd != null)
                {
                    audioPlayer.volume = .2f;
                    audioPlayer.PlayOneShot(demolishSound);
                    bd.GetComponent<Building>().Demolish();
                }
                    
            }
        }
        else {
            if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                SelectABuilding();
            }
        }  
    }

    Coroutine chooseTileRoutine;
    public void BeginPlacement(BuildingPreset preset)
    {
        if(isPlacing)
        {
            EndPlacemet();
        }
        currentPreset = preset;
        isPlacing = true;
        indicator.SetActive(true);

        building = Instantiate(preset.prefab.transform.GetChild(0).gameObject, indicator.transform);
        building.GetComponentInChildren<MeshRenderer>().material = placeableMaterial;

        if(chooseTileRoutine != null)
            StopCoroutine(chooseTileRoutine);
        chooseTileRoutine = StartCoroutine(ChooseTile());
    }

    public void EndPlacemet()
    {
        if(isPlacing)
        {
           
            Destroy(indicator.transform.GetChild(0).gameObject);
            indicator.SetActive(false);
            isPlacing = false;
            currentPreset = null;
        } else if (isDemolishing)
        {
            isDemolishing = false;
            DemolishWarning.SetActive(false);
            indicator.SetActive(false);
        }
    }

    public void DestroyBuilding()
    {
        if(selectedBuilding != null)
        {
            infoPanel.SetActive(false);
            LoCManager.instance.OnDestroyedBuilding(selectedBuilding.GetComponent<Building>());
            Destroy(selectedBuilding);
        }
    }

    private void SelectABuilding()
    {
        selectedBuilding = TileSelector.instance.GetClickedBuilding();
        if(selectedBuilding != null)
        {
            infoPanel.SetActive(true);
            Building building = selectedBuilding.GetComponent<Building>();
            if(building.preset.type == BuildingType.HOUSE)
                this.gameObject.GetComponent<GuiController>().SeeHomeDetails(building);
            else
                this.gameObject.GetComponent<GuiController>().SeeBuildingDetails(building);
        } else
        {
            this.gameObject.GetComponent<GuiController>().CloseMenus();
            infoPanel.SetActive(false);
        }
    }

    public void BeginDemolition()
    {
        if (isPlacing)
        {
            EndPlacemet();
        }
        isDemolishing = true;
        indicator.SetActive(true);
        DemolishWarning.SetActive(true);

        if (chooseTileRoutine != null)
            StopCoroutine(chooseTileRoutine);
        chooseTileRoutine = StartCoroutine(chooseBuildingToDelete());
    }

    private IEnumerator ChooseTile()
    {
        while(isPlacing)
        {
            if(currentPreset.type == BuildingType.DECORATION)
               currentPos = TileSelector.instance.GetFreeForm();
            else
                currentPos = TileSelector.instance.GetCurrentTile();


            indicator.transform.position = currentPos;

            if(LoCManager.instance.hope >= currentPreset.cost)
            {
                if(currentPreset.type == BuildingType.DECORATION)
                {
                    building.GetComponentInChildren<MeshRenderer>().material = placeableMaterial;
                    canPlace = true;
                }
                else if (TileSelector.instance.CheckIsOccupied())
                {
                    building.GetComponentInChildren<MeshRenderer>().material = unplaceableMaterial;
                    canPlace = false;
                }
                else
                {
                    building.GetComponentInChildren<MeshRenderer>().material = placeableMaterial;
                    canPlace = true;
                }
            } else
            {
                building.GetComponentInChildren<MeshRenderer>().material = unplaceableMaterial;
                canPlace = false;
            }


            yield return new WaitForSeconds(UPDATE_TIMER);
        }
    }

    private IEnumerator chooseBuildingToDelete()
    {
        while (isDemolishing)
        {
            currentPos = TileSelector.instance.GetFreeForm();

            indicator.transform.position = currentPos;


            yield return new WaitForSeconds(UPDATE_TIMER);
        }
    }
}
