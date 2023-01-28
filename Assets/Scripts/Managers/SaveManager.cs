using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using ZSerializer;

public class SaveManager : MonoBehaviour
{
    [SerializeField] private GameObject loadScreen;

    private void Start()
    {
        Debug.Log("Load level: " + !MainMenuController.instance.newGame);
        if (!MainMenuController.instance.newGame)
        {
            StartCoroutine(LoadLevelOnStart());
        }
    }

   
    private IEnumerator LoadLevelOnStart()
    {
        loadScreen.SetActive(true);
        yield return new WaitForSeconds(2f);
        loadScreen.SetActive(false);
        LoadCurrentAsync();
    }

    public async void SaveCurrentAsync ()
    {
        await ZSerialize.SaveScene();
    }

    public async void LoadCurrentAsync()
    {
       await ZSerialize.LoadScene();
       
    }

}
