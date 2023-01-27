[System.Serializable]
public sealed class LoCManagerZSerializer : ZSerializer.Internal.ZSerializer
{
    public LoCManager instance;
    public System.Int32 hope;
    public System.Int32 karmicPower;
    public System.Collections.Generic.List<MagicalGirl> magicalGirls;
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
         magicalGirls = (System.Collections.Generic.List<MagicalGirl>)typeof(LoCManager).GetField("magicalGirls").GetValue(instance);
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
         typeof(LoCManager).GetField("magicalGirls").SetValue(component, magicalGirls);
         typeof(LoCManager).GetField("txtHope", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(component, txtHope);
         typeof(LoCManager).GetField("txtKP", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(component, txtKP);
         typeof(LoCManager).GetField("txtHappy", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(component, txtHappy);
         typeof(ZSerializer.PersistentMonoBehaviour).GetField("groupID", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(component, groupID);
         typeof(ZSerializer.PersistentMonoBehaviour).GetField("autoSync", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(component, autoSync);
    }
}