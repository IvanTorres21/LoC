using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using ZSerializer;

public class TimeController : PersistentMonoBehaviour
{
    public static TimeController instance;

    public int day = 0;
    public float time = 0;
    [SerializeField] private float DAYTIME = 30;
    private float modifier = 1;

    [SerializeField] private TextMeshProUGUI timeTxt;
    [SerializeField] private TextMeshProUGUI dayTxt;

    private void Start()
    {
        instance = this;
    }

    private void Update()
    {
        time += Time.deltaTime * modifier;
        if (time > DAYTIME)
        {
            LoCManager.instance.EndDay();
            day++;
            time = 0;
            dayTxt.text = "Day: " + day;
        }

        ChangeGUI();
    }

    private void ChangeGUI()
    {
        timeTxt.text = Mathf.FloorToInt(time / DAYTIME * 24).ToString("00") + ":00";
    }

    public void ChangeSpeed(float newMod)
    {
        modifier = newMod;
    }

    public override void OnPostLoad()
    {
        instance = this;
        dayTxt = ReferenceHolder.instance.dayTxt;
        timeTxt = ReferenceHolder.instance.timeTxt;
        dayTxt.text = "Day: " + day;
        ChangeGUI();
        base.OnPostLoad();
    }
}
