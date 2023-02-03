using System.Collections;
using TMPro;
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
    bool oneDegreeMode = false;

    [Header("Building info")]
    [SerializeField] private GameObject infoPanel;
    [SerializeField] private TextMeshProUGUI buildingName;
    [SerializeField] private TextMeshProUGUI buildingInfo;

    [Header("Sounds")]
    [SerializeField] private AudioSource audioPlayer;
    [SerializeField] private AudioClip placeSound;
    [SerializeField] private AudioClip demolishSound;

    public bool canEscape = true;


    private void FixedUpdate()
    {
        if(isPlacing) {

            if (LoCManager.instance.hope >= currentPreset.cost)
            {
                if (currentPreset.type == BuildingType.PROP)
                {
                    indicator.transform.GetChild(0).GetComponent<MeshRenderer>().material = placeableMaterial;
                    canPlace = true;
                }
                else if (TileSelector.instance.CheckIsOccupied(currentPreset, building.transform.rotation))
                {
                    indicator.transform.GetChild(0).GetComponent<MeshRenderer>().material = unplaceableMaterial;
                    canPlace = false;
                }
                else
                {
                    indicator.transform.GetChild(0).GetComponent<MeshRenderer>().material = placeableMaterial;
                    canPlace = true;
                }
            }
            else
            {
                indicator.transform.GetChild(0).GetComponent<MeshRenderer>().material = unplaceableMaterial;
                canPlace = false;
            }
        }
    }

    private void Update()
    {
        if(canEscape && Input.GetKeyDown(KeyCode.Escape))
        {
            EndPlacemet();
        }

        if(isPlacing)
        {
            // If we are placing a prop we use GetKey and 1f for the rotation, if not GetKeyDown and 90f;
            float rotValue = currentPreset.type == BuildingType.PROP ? 1f : 90f;

            if (((!oneDegreeMode && Input.GetKey(KeyCode.Q) && (currentPreset.type == BuildingType.PROP)) || (oneDegreeMode && Input.GetKeyDown(KeyCode.Q) && (currentPreset.type == BuildingType.PROP))) || Input.GetKeyDown(KeyCode.Q))
            {

                building.transform.rotation = Quaternion.Euler(building.transform.rotation.eulerAngles.x, building.transform.rotation.eulerAngles.y + rotValue, 0f);
                indicator.transform.GetChild(0).rotation = Quaternion.Euler(0, building.transform.rotation.eulerAngles.y, 0);
            }
            else if (((!oneDegreeMode && Input.GetKey(KeyCode.E) && (currentPreset.type == BuildingType.PROP)) || (oneDegreeMode && Input.GetKeyDown(KeyCode.E) && (currentPreset.type == BuildingType.PROP))) || Input.GetKeyDown(KeyCode.E))
            {
                building.transform.rotation = Quaternion.Euler(building.transform.rotation.eulerAngles.x, building.transform.rotation.eulerAngles.y - rotValue, 0f);
                indicator.transform.GetChild(0).rotation = Quaternion.Euler(0, building.transform.rotation.eulerAngles.y, 0);
            }


            if (canPlace && Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                if(canPlace || currentPreset.type == BuildingType.PROP)
                {
                    audioPlayer.volume = 1f;
                    audioPlayer.PlayOneShot(placeSound);
                    GameObject bd = Instantiate(currentPreset.prefab, currentPos, Quaternion.Euler(0f, building.transform.rotation.eulerAngles.y, 0f));
                    LoCManager.instance.OnCreatedBuilding(bd.GetComponent<Building>());
                    if(!canEscape) // We are in the tutorial end placement
                    {
                        EndPlacemet();
                    }
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

        indicator.transform.GetChild(0).localPosition = building.transform.localPosition;
        indicator.transform.GetChild(0).localScale = new Vector3(preset.hitboxSize.x, 3f, preset.hitboxSize.z);

       

        if(chooseTileRoutine != null)
            StopCoroutine(chooseTileRoutine);
        chooseTileRoutine = StartCoroutine(ChooseTile());
    }

    public void EndPlacemet()
    {
        if(isPlacing)
        {
           
            Destroy(indicator.transform.GetChild(1).gameObject);
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
            currentPos = TileSelector.instance.GetFreeForm();


            indicator.transform.position = currentPos;

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

    public void ChangeDegreeMode()
    {
        oneDegreeMode = !oneDegreeMode;
    }
}
