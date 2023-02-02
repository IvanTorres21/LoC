using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using ZSerializer;

public class SaveManager : MonoBehaviour
{
    [SerializeField] private GameObject loadScreen;
    [SerializeField] private TutorialController controller;

    private void Start()
    {
        if (!MainMenuController.instance.newGame)
        {
            StartCoroutine(LoadLevelOnStart());
        } else
        {
            controller.StartTutorial();
        }

        TimeController.instance.ChangeSpeed(0);
    }

   
    private IEnumerator LoadLevelOnStart()
    {
        loadScreen.SetActive(true);
        yield return new WaitForSeconds(1f);
        loadScreen.SetActive(false);
        LoadStartLevel();
    }



    public async void SaveCurrentAsync ()
    {
        await ZSerialize.SaveScene();
    }

    public void LoadCurrentAsync()
    {
        StartCoroutine(LoadLevelOnStart());
    }

    public async void LoadStartLevel()
    {
        await ZSerialize.LoadScene();
    }
}
