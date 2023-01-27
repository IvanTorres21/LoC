[System.Serializable]
public sealed class MagicalGirlZSerializer : ZSerializer.Internal.ZSerializer
{
    public MGPreset preset;
    public System.Boolean isAlive;
    public System.Int32 happiness;
    public System.Int32 tiredness;
    public Building currentLocation;
    public Building home;
    public UnityEngine.UI.Image cardImg;
    public TMPro.TextMeshProUGUI txtName;
    public TMPro.TextMeshProUGUI txtXp;
    public TMPro.TextMeshProUGUI txtLevel;
    public TMPro.TextMeshProUGUI txtHappy;
    public TMPro.TextMeshProUGUI txtTiredness;
    public TMPro.TextMeshProUGUI txtLikes;
    public TMPro.TextMeshProUGUI txtDislikes;
    public TMPro.TextMeshProUGUI txtAtk;
    public TMPro.TextMeshProUGUI txtDef;
    public TMPro.TextMeshProUGUI txtLuxury;
    public System.Int32 groupID;
    public System.Boolean autoSync;

    public MagicalGirlZSerializer(string ZUID, string GOZUID) : base(ZUID, GOZUID)
    {       var instance = ZSerializer.ZSerialize.idMap[ZSerializer.ZSerialize.CurrentGroupID][ZUID];
         preset = (MGPreset)typeof(MagicalGirl).GetField("preset").GetValue(instance);
         isAlive = (System.Boolean)typeof(MagicalGirl).GetField("isAlive").GetValue(instance);
         happiness = (System.Int32)typeof(MagicalGirl).GetField("happiness").GetValue(instance);
         tiredness = (System.Int32)typeof(MagicalGirl).GetField("tiredness").GetValue(instance);
         currentLocation = (Building)typeof(MagicalGirl).GetField("currentLocation").GetValue(instance);
         home = (Building)typeof(MagicalGirl).GetField("home").GetValue(instance);
         cardImg = (UnityEngine.UI.Image)typeof(MagicalGirl).GetField("cardImg").GetValue(instance);
         txtName = (TMPro.TextMeshProUGUI)typeof(MagicalGirl).GetField("txtName").GetValue(instance);
         txtXp = (TMPro.TextMeshProUGUI)typeof(MagicalGirl).GetField("txtXp").GetValue(instance);
         txtLevel = (TMPro.TextMeshProUGUI)typeof(MagicalGirl).GetField("txtLevel").GetValue(instance);
         txtHappy = (TMPro.TextMeshProUGUI)typeof(MagicalGirl).GetField("txtHappy").GetValue(instance);
         txtTiredness = (TMPro.TextMeshProUGUI)typeof(MagicalGirl).GetField("txtTiredness").GetValue(instance);
         txtLikes = (TMPro.TextMeshProUGUI)typeof(MagicalGirl).GetField("txtLikes").GetValue(instance);
         txtDislikes = (TMPro.TextMeshProUGUI)typeof(MagicalGirl).GetField("txtDislikes").GetValue(instance);
         txtAtk = (TMPro.TextMeshProUGUI)typeof(MagicalGirl).GetField("txtAtk").GetValue(instance);
         txtDef = (TMPro.TextMeshProUGUI)typeof(MagicalGirl).GetField("txtDef").GetValue(instance);
         txtLuxury = (TMPro.TextMeshProUGUI)typeof(MagicalGirl).GetField("txtLuxury").GetValue(instance);
         groupID = (System.Int32)typeof(ZSerializer.PersistentMonoBehaviour).GetField("groupID", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(instance);
         autoSync = (System.Boolean)typeof(ZSerializer.PersistentMonoBehaviour).GetField("autoSync", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(instance);
    }

    public override void RestoreValues(UnityEngine.Component component)
    {
         typeof(MagicalGirl).GetField("preset").SetValue(component, preset);
         typeof(MagicalGirl).GetField("isAlive").SetValue(component, isAlive);
         typeof(MagicalGirl).GetField("happiness").SetValue(component, happiness);
         typeof(MagicalGirl).GetField("tiredness").SetValue(component, tiredness);
         typeof(MagicalGirl).GetField("currentLocation").SetValue(component, currentLocation);
         typeof(MagicalGirl).GetField("home").SetValue(component, home);
         typeof(MagicalGirl).GetField("cardImg").SetValue(component, cardImg);
         typeof(MagicalGirl).GetField("txtName").SetValue(component, txtName);
         typeof(MagicalGirl).GetField("txtXp").SetValue(component, txtXp);
         typeof(MagicalGirl).GetField("txtLevel").SetValue(component, txtLevel);
         typeof(MagicalGirl).GetField("txtHappy").SetValue(component, txtHappy);
         typeof(MagicalGirl).GetField("txtTiredness").SetValue(component, txtTiredness);
         typeof(MagicalGirl).GetField("txtLikes").SetValue(component, txtLikes);
         typeof(MagicalGirl).GetField("txtDislikes").SetValue(component, txtDislikes);
         typeof(MagicalGirl).GetField("txtAtk").SetValue(component, txtAtk);
         typeof(MagicalGirl).GetField("txtDef").SetValue(component, txtDef);
         typeof(MagicalGirl).GetField("txtLuxury").SetValue(component, txtLuxury);
         typeof(ZSerializer.PersistentMonoBehaviour).GetField("groupID", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(component, groupID);
         typeof(ZSerializer.PersistentMonoBehaviour).GetField("autoSync", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(component, autoSync);
    }
}