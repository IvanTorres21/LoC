using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using ZSerializer;

public class SaveManager : MonoBehaviour
{
   public async void SaveCurrentAsync ()
    {
        await ZSerialize.SaveScene();
    }

    public async void LoadCurrentAsync()
    {
       await ZSerialize.LoadScene();
       
    }

}
