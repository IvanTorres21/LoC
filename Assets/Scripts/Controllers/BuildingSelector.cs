using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingSelector : MonoBehaviour
{
    private const float UPDATE_TIMER = 0.05f;

    private bool isPlacing = false;
    private BuildingPreset currentPreset;
    private Vector3 currentPos;
    private bool canPlace = true;

    
    private GameObject building; // Model of the building to place
    private GameObject selectedBuilding; // Gameobject that's been selected

    [Header("Building placer")]
    [SerializeField] private GameObject indicator;
    [SerializeField] private Material placeableMaterial;
    [SerializeField] private Material unplaceableMaterial;

    [Header("Building info")]
    [SerializeField] private GameObject infoPanel;
    [SerializeField] private TextMeshProUGUI buildingName;
    [SerializeField] private TextMeshProUGUI buildingInfo;
    

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            EndPlacemet();
        }

        if(isPlacing)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                building.transform.Rotate(new Vector3(0f, 90f, 0f));
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                building.transform.Rotate(new Vector3(0f, -90f, 0f));
            }
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                if(!TileSelector.instance.CheckIsOccupied())
                {
                    GameObject bd = Instantiate(currentPreset.prefab, currentPos, building.transform.rotation);
                    LoCManager.instance.OnCreatedBuilding(bd.GetComponent<Building>());
                }
                

            }
        } else
        {
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
        } else
        {
            infoPanel.SetActive(false);
        }
    }

    private IEnumerator ChooseTile()
    {
        while(isPlacing)
        {
            currentPos = TileSelector.instance.GetCurrentTile();
            indicator.transform.position = currentPos;

            if(LoCManager.instance.hope >= currentPreset.cost)
            {
                if (TileSelector.instance.CheckIsOccupied())
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
}
