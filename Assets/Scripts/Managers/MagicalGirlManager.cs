using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MagicalGirlManager : MonoBehaviour
{
    [SerializeField] private List<MGPreset> rate4Girls;
    [SerializeField] private List<MGPreset> rate3Girls;
    [SerializeField] private List<MGPreset> rate2Girls;

    private int rate = 0;

    [SerializeField] private GameObject playerStopper;
    [SerializeField] private GameObject winningGirl;
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private GameObject particles4star;
    [SerializeField] private GameObject particles3star;
    [SerializeField] private GameObject particles2star;
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
        float rarityValue = Random.Range(0f, 100f);
        if (rarityValue <= 60f && rate2Girls.Count > 0)
        {
            rate = 2;
            StartCoroutine(ChooseRandomGirl(rate2Girls));
        }
        else if (rarityValue <= 90f && rate3Girls.Count > 0)
        {
            rate = 3;
            StartCoroutine(ChooseRandomGirl(rate3Girls));
        }
        else if(rate4Girls.Count > 0)
        {
            rate = 4;
            StartCoroutine(ChooseRandomGirl(rate4Girls));
        }
            
    }

    private void GetAvaliableMGs()
    {
        List<MGPreset> aux = new List<MGPreset>();

        foreach(MagicalGirl mg in LoCManager.instance.magicalGirls)
        {
            if(rate2Girls.Contains(mg.preset))
            {
                rate2Girls.Remove(mg.preset);
            }
            if (rate3Girls.Contains(mg.preset))
            {
                rate3Girls.Remove(mg.preset);
            }
            if (rate4Girls.Contains(mg.preset))
            {
                rate4Girls.Remove(mg.preset);
            }
        }
    }

    private IEnumerator ChooseRandomGirl(List<MGPreset> mgs)
    {

        winningGirl.GetComponent<Image>().sprite = defaultSprite;
        winningGirl.SetActive(true);

        switch (rate)
        {
            case 2:
                particles2star.SetActive(true);
                break;
            case 3:
                particles3star.SetActive(true);
                break;
            case 4:
                particles4star.SetActive(true);
                break;
            default:
                particles2star.SetActive(true);
                break;
        }

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
        particles2star.SetActive(false);
        particles3star.SetActive(false);
        particles4star.SetActive(false);
        ClickToContinue.SetActive(false);
    }
}
