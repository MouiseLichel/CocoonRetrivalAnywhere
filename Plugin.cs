using BepInEx;
using BepInEx.Logging;
using BepInEx.Configuration;
using UnityEngine;
using System;

namespace CocoonRetrivalAnywhere;

[BepInPlugin("fr.mouise.silksong.cocoonretrivalanywhere", "CocoonRetrivalAnywhere", "1.0.0")]
public class CocoonRetrivalAnywherePlugin : BaseUnityPlugin
{
    internal static new ManualLogSource Logger;
    private ConfigEntry<KeyboardShortcut> retrieveCocoonKeyEntry;

    private void Awake()
    {
        Logger = base.Logger;
        Logger.LogInfo($"{this.Info.Metadata.Name} v:{this.Info.Metadata.Version} loaded ");

        retrieveCocoonKeyEntry = Config.Bind("CocoonRetrivalAnywhere",
                                         "RetrieveCocoonKey",
                                         new KeyboardShortcut(KeyCode.F6),
                                         "The key used to retrieve your cocoon");
    }

    private void Update()
    {
        if (retrieveCocoonKeyEntry.Value.IsDown())
        {
            RetrieveCocoon();
        }
    }

    private void RetrieveCocoon()
    {
        HeroController instance = HeroController.instance;
        if (!instance)
        {
            return;
        }

        PlayerData instance2 = PlayerData.instance;

        if (!string.IsNullOrEmpty(instance2.HeroCorpseScene))
        {
            instance.CocoonBroken(true, true);
            EventRegister.SendEvent(EventRegisterEvents.BreakHeroCorpse, null);
        }    
      
    }
}
