using BepInEx;
using BepInEx.Logging;
using BepInEx.Configuration;
using UnityEngine;
using System;

namespace CocoonRetrievalAnywhere;

[BepInPlugin("fr.mouise.silksong.cocoonretrievalanywhere", "CocoonRetrievalAnywhere", "1.0.1")]
public class CocoonRetrievalAnywherePlugin : BaseUnityPlugin
{
    internal static new ManualLogSource Logger;
    private ConfigEntry<KeyCode> retrieveCocoonKeyEntry;

    private void Awake()
    {
        Logger = base.Logger;
        Logger.LogInfo($"{this.Info.Metadata.Name} v:{this.Info.Metadata.Version} loaded ");

        retrieveCocoonKeyEntry = Config.Bind("CocoonRetrivealAnywhere",
                                         "RetrieveCocoonKey",
                                         KeyCode.F6,
                                         "The key used to retrieve your cocoon");
    }

    private void Update()
    {
        if (Input.GetKeyDown(retrieveCocoonKeyEntry.Value))
        {
            Logger.LogInfo($"Cocoon retrieve key pressed");
            RetrieveCocoon();
        }
    }

    private void RetrieveCocoon()
    {
        HeroController instance = HeroController.instance;
        if (!instance)
        {
            Logger.LogInfo($"Hero controller is null, skipping cocoon retrieval");
            return;
        }

        PlayerData instance2 = PlayerData.instance;
        Logger.LogInfo($"Hero controler: {instance}");
        Logger.LogInfo($"Player data: {instance2}");
        Logger.LogInfo($"Hero corpse scene: {instance2.HeroCorpseScene}");
        if (!string.IsNullOrEmpty(instance2.HeroCorpseScene))
        {
            instance.CocoonBroken(true, true);
            EventRegister.SendEvent(EventRegisterEvents.BreakHeroCorpse, null);
        }    
      
    }
}
