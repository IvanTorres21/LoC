[System.Serializable]
public sealed class BuildingZSerializer : ZSerializer.Internal.ZSerializer
{
    public BuildingPreset preset;
    public System.Collections.Generic.List<MagicalGirl> currentTenants;
    public System.Int32 groupID;
    public System.Boolean autoSync;

    public BuildingZSerializer(string ZUID, string GOZUID) : base(ZUID, GOZUID)
    {       var instance = ZSerializer.ZSerialize.idMap[ZSerializer.ZSerialize.CurrentGroupID][ZUID];
         preset = (BuildingPreset)typeof(Building).GetField("preset").GetValue(instance);
         currentTenants = (System.Collections.Generic.List<MagicalGirl>)typeof(Building).GetField("currentTenants").GetValue(instance);
         groupID = (System.Int32)typeof(ZSerializer.PersistentMonoBehaviour).GetField("groupID", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(instance);
         autoSync = (System.Boolean)typeof(ZSerializer.PersistentMonoBehaviour).GetField("autoSync", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(instance);
    }

    public override void RestoreValues(UnityEngine.Component component)
    {
         typeof(Building).GetField("preset").SetValue(component, preset);
         typeof(Building).GetField("currentTenants").SetValue(component, currentTenants);
         typeof(ZSerializer.PersistentMonoBehaviour).GetField("groupID", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(component, groupID);
         typeof(ZSerializer.PersistentMonoBehaviour).GetField("autoSync", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(component, autoSync);
    }
}