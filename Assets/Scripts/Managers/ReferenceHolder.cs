using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReferenceHolder : MonoBehaviour
{
    public static ReferenceHolder instance;

    [Header("Magical Girl Panel UI")]
    public Image cardImg;
    public TextMeshProUGUI txtName;
    public TextMeshProUGUI txtXp;
    public TextMeshProUGUI txtLevel;
    public TextMeshProUGUI txtHappy;
    public TextMeshProUGUI txtTiredness;
    public TextMeshProUGUI txtLikes;
    public TextMeshProUGUI txtDislikes;
    public TextMeshProUGUI txtAtk;
    public TextMeshProUGUI txtDef;
    public TextMeshProUGUI txtLuxury;

    [Header("LoC Manager")]
    public TextMeshProUGUI txtHopeLoC;
    public TextMeshProUGUI txtKPLoC;
    public TextMeshProUGUI txtHappyLoC;

    [Header("Time Manager")]
    public TextMeshProUGUI timeTxt;
    public TextMeshProUGUI dayTxt;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
}
