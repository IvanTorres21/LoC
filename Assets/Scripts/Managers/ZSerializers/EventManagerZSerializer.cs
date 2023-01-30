[System.Serializable]
public sealed class EventManagerZSerializer : ZSerializer.Internal.ZSerializer
{
    public GameEvents lastEvent;
    public System.Collections.Generic.List<GameEvents> avaliableEvents;
    public GameEvents currentEvent;
    public EventManager instance;
    public BuildingPreset petitionPreset;
    public MagicalGirl petitioner;
    public MagicalGirl killer;
    public MagicalGirl currentVictim;
    public MagicalGirl mgFirst;
    public MagicalGirl mgSecond;
    public System.Collections.Generic.List<MagicalGirl> participants;
    public System.Collections.Generic.List<BuildingPreset> buildings;
    public System.Collections.Generic.List<System.String> disputes;
    public System.Int32 groupID;
    public System.Boolean autoSync;

    public EventManagerZSerializer(string ZUID, string GOZUID) : base(ZUID, GOZUID)
    {       var instance = ZSerializer.ZSerialize.idMap[ZSerializer.ZSerialize.CurrentGroupID][ZUID];
         lastEvent = (GameEvents)typeof(EventManager).GetField("lastEvent").GetValue(instance);
         avaliableEvents = (System.Collections.Generic.List<GameEvents>)typeof(EventManager).GetField("avaliableEvents").GetValue(instance);
         currentEvent = (GameEvents)typeof(EventManager).GetField("currentEvent").GetValue(instance);
         instance = (EventManager)typeof(EventManager).GetField("instance").GetValue(instance);
         petitionPreset = (BuildingPreset)typeof(EventManager).GetField("petitionPreset").GetValue(instance);
         petitioner = (MagicalGirl)typeof(EventManager).GetField("petitioner").GetValue(instance);
         killer = (MagicalGirl)typeof(EventManager).GetField("killer").GetValue(instance);
         currentVictim = (MagicalGirl)typeof(EventManager).GetField("currentVictim").GetValue(instance);
         mgFirst = (MagicalGirl)typeof(EventManager).GetField("mgFirst").GetValue(instance);
         mgSecond = (MagicalGirl)typeof(EventManager).GetField("mgSecond").GetValue(instance);
         participants = (System.Collections.Generic.List<MagicalGirl>)typeof(EventManager).GetField("participants").GetValue(instance);
         buildings = (System.Collections.Generic.List<BuildingPreset>)typeof(EventManager).GetField("buildings", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(instance);
         disputes = (System.Collections.Generic.List<System.String>)typeof(EventManager).GetField("disputes", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(instance);
         groupID = (System.Int32)typeof(ZSerializer.PersistentMonoBehaviour).GetField("groupID", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(instance);
         autoSync = (System.Boolean)typeof(ZSerializer.PersistentMonoBehaviour).GetField("autoSync", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(instance);
    }

    public override void RestoreValues(UnityEngine.Component component)
    {
         typeof(EventManager).GetField("lastEvent").SetValue(component, lastEvent);
         typeof(EventManager).GetField("avaliableEvents").SetValue(component, avaliableEvents);
         typeof(EventManager).GetField("currentEvent").SetValue(component, currentEvent);
         typeof(EventManager).GetField("instance").SetValue(component, instance);
         typeof(EventManager).GetField("petitionPreset").SetValue(component, petitionPreset);
         typeof(EventManager).GetField("petitioner").SetValue(component, petitioner);
         typeof(EventManager).GetField("killer").SetValue(component, killer);
         typeof(EventManager).GetField("currentVictim").SetValue(component, currentVictim);
         typeof(EventManager).GetField("mgFirst").SetValue(component, mgFirst);
         typeof(EventManager).GetField("mgSecond").SetValue(component, mgSecond);
         typeof(EventManager).GetField("participants").SetValue(component, participants);
         typeof(EventManager).GetField("buildings", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(component, buildings);
         typeof(EventManager).GetField("disputes", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(component, disputes);
         typeof(ZSerializer.PersistentMonoBehaviour).GetField("groupID", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(component, groupID);
         typeof(ZSerializer.PersistentMonoBehaviour).GetField("autoSync", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(component, autoSync);
    }
}