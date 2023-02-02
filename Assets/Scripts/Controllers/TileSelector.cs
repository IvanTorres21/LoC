using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileSelector : MonoBehaviour
{
    private Camera cam;

    public static TileSelector instance;

    [SerializeField] private float offset;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    private void Start()
    {
        
        cam = Camera.main;
    }

    public Vector3 GetFreeForm()
    {
        Vector3 tile = new Vector3(0, -100, 0);

        if (EventSystem.current.IsPointerOverGameObject())
        {
            return tile;
        }

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit rayOut;

        if (Physics.Raycast(ray, out rayOut, Mathf.Infinity, 1 << 3))
        {
            tile = rayOut.point;
          
        }

        return tile;
    }

    public bool CheckIsOccupied(BuildingPreset preset, Quaternion rotation)
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {

            Collider[] colliders = Physics.OverlapBox(hit.point, preset.hitboxSize / 2, rotation, 1 << 0);

            Debug.Log("Point: " + hit.point + "\nHit: " + colliders.Length);

            if (colliders.Length == 0)
                return false;

        }

        return true;
    }

    public GameObject GetClickedBuilding()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("Building") )
            {
               return hit.collider.gameObject;
            }
                
        }
        return null;
    }

    public GameObject GetClickedBuildingDemolishing()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("Building") || hit.collider.CompareTag("Decoration") || hit.collider.CompareTag("Road"))
            {
                return hit.collider.gameObject;
            }

        }
        return null;
    }



}
