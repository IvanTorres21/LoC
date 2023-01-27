using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MagicalGirlManager : MonoBehaviour
{
    [SerializeField] private List<MGPreset> allGirls;

    [SerializeField] private GameObject playerStopper;
    [SerializeField] private GameObject winningGirl;
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private GameObject particles;
    [SerializeField] private GameObject ClickToContinue;

    [Header("Magical Girl Panel UI")]
    [SerializeField] private Image cardImg;
    [SerializeField] private TextMeshProUGUI txtName;
    [SerializeField] private TextMeshProUGUI txtXp;
    [SerializeField] private TextMeshProUGUI txtLevel;
    [SerializeField] private TextMeshProUGUI txtHappy;
    [SerializeField] private TextMeshProUGUI txtTiredness;
    [SerializeField] private TextMeshProUGUI txtLikes;
    [SerializeField] private TextMeshProUGUI txtDislikes;
    [SerializeField] private TextMeshProUGUI txtAtk;
    [SerializeField] private TextMeshProUGUI txtDef;
    [SerializeField] private TextMeshProUGUI txtLuxury;

    public void GachaStart()
    {
        playerStopper.SetActive(true);
        GetComponent<BuildingSelector>().EndPlacemet();
        GetAvaliableMGs();
        if (allGirls.Count > 0)
            StartCoroutine(ChooseRandomGirl(allGirls));
    }

    private void GetAvaliableMGs()
    {
        List<MGPreset> aux = new List<MGPreset>();

        foreach(MagicalGirl mg in LoCManager.instance.magicalGirls)
        {
            if(allGirls.Contains(mg.preset))
            {
                allGirls.Remove(mg.preset);
            }
        }
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

        GameObject mgObject = Instantiate(chosenMg.prefab, LoCManager.instance.gameObject.transform);
        MagicalGirl girl = mgObject.GetComponent<MagicalGirl>();
        girl.txtAtk = txtAtk;
        girl.txtDef = txtDef;
        girl.txtXp = txtXp;
        girl.txtName = txtName;
        girl.txtTiredness = txtTiredness;
        girl.txtHappy = txtHappy;
        girl.txtLevel = txtLevel;
        girl.txtLikes = txtLikes;
        girl.txtDislikes = txtDislikes;
        girl.cardImg = cardImg;
        girl.txtLuxury = txtLuxury;
        
        LoCManager.instance.OnAddedMagicalGirl(girl);

        yield return new WaitForSeconds(1f);

        ClickToContinue.SetActive(true);

        yield return null;
    }

    public void CloseGacha()
    {
        winningGirl.SetActive(false);
        playerStopper.SetActive(false);
        particles.SetActive(false);
        ClickToContinue.SetActive(false);
    }
}
