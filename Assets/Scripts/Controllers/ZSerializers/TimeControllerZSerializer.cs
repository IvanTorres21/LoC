[System.Serializable]
public sealed class TimeControllerZSerializer : ZSerializer.Internal.ZSerializer
{
    public TimeController instance;
    public System.Int32 day;
    public System.Single time;
    public System.Single DAYTIME;
    public TMPro.TextMeshProUGUI timeTxt;
    public TMPro.TextMeshProUGUI dayTxt;
    public System.Int32 groupID;
    public System.Boolean autoSync;

    public TimeControllerZSerializer(string ZUID, string GOZUID) : base(ZUID, GOZUID)
    {       var instance = ZSerializer.ZSerialize.idMap[ZSerializer.ZSerialize.CurrentGroupID][ZUID];
         instance = (TimeController)typeof(TimeController).GetField("instance").GetValue(instance);
         day = (System.Int32)typeof(TimeController).GetField("day").GetValue(instance);
         time = (System.Single)typeof(TimeController).GetField("time").GetValue(instance);
         DAYTIME = (System.Single)typeof(TimeController).GetField("DAYTIME", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(instance);
         timeTxt = (TMPro.TextMeshProUGUI)typeof(TimeController).GetField("timeTxt", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(instance);
         dayTxt = (TMPro.TextMeshProUGUI)typeof(TimeController).GetField("dayTxt", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(instance);
         groupID = (System.Int32)typeof(ZSerializer.PersistentMonoBehaviour).GetField("groupID", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(instance);
         autoSync = (System.Boolean)typeof(ZSerializer.PersistentMonoBehaviour).GetField("autoSync", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(instance);
    }

    public override void RestoreValues(UnityEngine.Component component)
    {
         typeof(TimeController).GetField("instance").SetValue(component, instance);
         typeof(TimeController).GetField("day").SetValue(component, day);
         typeof(TimeController).GetField("time").SetValue(component, time);
         typeof(TimeController).GetField("DAYTIME", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(component, DAYTIME);
         typeof(TimeController).GetField("timeTxt", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(component, timeTxt);
         typeof(TimeController).GetField("dayTxt", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(component, dayTxt);
         typeof(ZSerializer.PersistentMonoBehaviour).GetField("groupID", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(component, groupID);
         typeof(ZSerializer.PersistentMonoBehaviour).GetField("autoSync", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(component, autoSync);
    }
}