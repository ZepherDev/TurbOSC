using System;
using MelonLoader;
using OscCore;
using UnityEngine;
using Main = TurbOSC.Main;

[assembly: MelonGame("VRChat", "VRChat")]
[assembly: MelonInfo(typeof(Main), "TurbOSC", "1.0", "ZepherDev")]

namespace TurbOSC
{
    public class Main : MelonMod
    {
        
        private MonoBehaviourPublicInStObInOs_dOsObBoAcUnique CachedOscMidiController = null;

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            if (CachedOscMidiController != null && CachedOscMidiController.field_Private_OscServer_0 != null) return;
            
            var result =
                GameObject.FindObjectOfType<MonoBehaviourPublicInStObInOs_dOsObBoAcUnique>();
            
            if (result == null || result.field_Private_OscServer_0 == null)
                return;

            CachedOscMidiController = result;
            var server = CachedOscMidiController.field_Private_OscServer_0;
            
            server.TryAddMethod("/turbosc/iksync", 
                new Action<OscMessageValues>(IKSync.ReadValues));
            
            MelonLogger.Msg("TurbOSC Initialized!");

        }
    }
}