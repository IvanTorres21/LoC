using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicalGirlManager : MonoBehaviour
{
    [SerializeField] private List<MGPreset> allGirls;

    [SerializeField] private GameObject playerStopper;
    [SerializeField] private GameObject winningGirl;
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private GameObject particles;

    public void GachaStart()
    {
        playerStopper.SetActive(true);
        GetComponent<BuildingSelector>().EndPlacemet();
        //List<MGPreset> mgs = GetAvaliableMGs();

        StartCoroutine(ChooseRandomGirl(allGirls));
    }

    private List<MGPreset> GetAvaliableMGs()
    {
        List<MGPreset> aux = new List<MGPreset>();

        foreach(MGPreset mGPreset in allGirls)
        {
            if(!LoCManager.instance.magicalGirls.Contains(mGPreset.prefab.GetComponent<MagicalGirl>()))
            {
                aux.Add(mGPreset);
            }
        }

        //TODO: do something if there is no more MGs or whatever

        return aux;
    }

    private IEnumerator ChooseRandomGirl(List<MGPreset> mgs)
    {
        winningGirl.GetComponent<Image>().sprite = defaultSprite;
        winningGirl.SetActive(true);
        particles.SetActive(true);
        MGPreset chosenMg = mgs[(int)Random.Range(0f, mgs.Count)];

       

        winningGirl.GetComponent<Animator>().Play("Spin");
        yield return new WaitForSeconds(4f);
        winningGirl.GetComponent<Image>().sprite = chosenMg.card;
        yield return new WaitForSeconds(4f);
        winningGirl.GetComponent<Animator>().StopPlayback();
        yield return null;
    }
}
