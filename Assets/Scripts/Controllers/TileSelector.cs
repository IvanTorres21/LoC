using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileSelector : MonoBehaviour
{
    private Camera cam;

    public static TileSelector instance;


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

    public Vector3 GetCurrentTile()
    {
        Vector3 tile = new Vector3(0, -100, 0);

        if (EventSystem.current.IsPointerOverGameObject())
        {
            return tile;
        }

        Plane plane = new Plane(Vector3.up, Vector3.zero);
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        float rayOut = 0f;
        
        if(plane.Raycast(ray, out rayOut))
        {
            tile = ray.GetPoint(rayOut) - new Vector3(.5f, 0f, .5f);
            tile = new Vector3(Mathf.CeilToInt(tile.x), 0f, Mathf.CeilToInt(tile.z));
        }

        return tile;
    }

    public bool CheckIsOccupied()
    {
        bool isOccupied = false;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if(hit.collider.CompareTag("Building")) 
                isOccupied = true;
        }

        return isOccupied;
    }

    public GameObject GetClickedBuilding()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("Building"))
            {
               return hit.collider.gameObject;
            }
                
        }
        return null;
    }
}
