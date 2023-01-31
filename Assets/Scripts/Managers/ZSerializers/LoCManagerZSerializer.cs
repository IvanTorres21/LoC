[System.Serializable]
public sealed class LoCManagerZSerializer : ZSerializer.Internal.ZSerializer
{
    public LoCManager instance;
    public System.Int32 hope;
    public System.Int32 karmicPower;
    public System.Int32 avgHappiness;
    public Building mainHall;
    public System.Collections.Generic.List<MagicalGirl> magicalGirls;
    public System.Collections.Generic.List<Building> buildings;
    public System.Collections.Generic.List<System.String> notifications;
    public System.Int32 daysSinceLastEvent;
    public TMPro.TextMeshProUGUI txtHope;
    public TMPro.TextMeshProUGUI txtKP;
    public TMPro.TextMeshProUGUI txtHappy;
    public System.Int32 groupID;
    public System.Boolean autoSync;

    public LoCManagerZSerializer(string ZUID, string GOZUID) : base(ZUID, GOZUID)
    {       var instance = ZSerializer.ZSerialize.idMap[ZSerializer.ZSerialize.CurrentGroupID][ZUID];
         instance = (LoCManager)typeof(LoCManager).GetField("instance").GetValue(instance);
         hope = (System.Int32)typeof(LoCManager).GetField("hope").GetValue(instance);
         karmicPower = (System.Int32)typeof(LoCManager).GetField("karmicPower").GetValue(instance);
         avgHappiness = (System.Int32)typeof(LoCManager).GetField("avgHappiness").GetValue(instance);
         mainHall = (Building)typeof(LoCManager).GetField("mainHall").GetValue(instance);
         magicalGirls = (System.Collections.Generic.List<MagicalGirl>)typeof(LoCManager).GetField("magicalGirls").GetValue(instance);
         buildings = (System.Collections.Generic.List<Building>)typeof(LoCManager).GetField("buildings").GetValue(instance);
         notifications = (System.Collections.Generic.List<System.String>)typeof(LoCManager).GetField("notifications").GetValue(instance);
         daysSinceLastEvent = (System.Int32)typeof(LoCManager).GetField("daysSinceLastEvent").GetValue(instance);
         txtHope = (TMPro.TextMeshProUGUI)typeof(LoCManager).GetField("txtHope", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(instance);
         txtKP = (TMPro.TextMeshProUGUI)typeof(LoCManager).GetField("txtKP", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(instance);
         txtHappy = (TMPro.TextMeshProUGUI)typeof(LoCManager).GetField("txtHappy", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(instance);
         groupID = (System.Int32)typeof(ZSerializer.PersistentMonoBehaviour).GetField("groupID", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(instance);
         autoSync = (System.Boolean)typeof(ZSerializer.PersistentMonoBehaviour).GetField("autoSync", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(instance);
    }

    public override void RestoreValues(UnityEngine.Component component)
    {
         typeof(LoCManager).GetField("instance").SetValue(component, instance);
         typeof(LoCManager).GetField("hope").SetValue(component, hope);
         typeof(LoCManager).GetField("karmicPower").SetValue(component, karmicPower);
         typeof(LoCManager).GetField("avgHappiness").SetValue(component, avgHappiness);
         typeof(LoCManager).GetField("mainHall").SetValue(component, mainHall);
         typeof(LoCManager).GetField("magicalGirls").SetValue(component, magicalGirls);
         typeof(LoCManager).GetField("buildings").SetValue(component, buildings);
         typeof(LoCManager).GetField("notifications").SetValue(component, notifications);
         typeof(LoCManager).GetField("daysSinceLastEvent").SetValue(component, daysSinceLastEvent);
         typeof(LoCManager).GetField("txtHope", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(component, txtHope);
         typeof(LoCManager).GetField("txtKP", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(component, txtKP);
         typeof(LoCManager).GetField("txtHappy", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(component, txtHappy);
         typeof(ZSerializer.PersistentMonoBehaviour).GetField("groupID", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(component, groupID);
         typeof(ZSerializer.PersistentMonoBehaviour).GetField("autoSync", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(component, autoSync);
    }
}