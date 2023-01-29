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
        
        if (!MainMenuController.instance.newGame)
        {
            StartCoroutine(LoadLevelOnStart());
        }
        TimeController.instance.ChangeSpeed(0);
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
