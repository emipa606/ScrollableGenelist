using Mlie;
using UnityEngine;
using Verse;

namespace ScrollableGenelist;

[StaticConstructorOnStartup]
internal class ScrollableGenelistMod : Mod
{
    /// <summary>
    ///     The instance of the settings to be read by the mod
    /// </summary>
    public static ScrollableGenelistMod instance;

    private static string currentVersion;

    /// <summary>
    ///     Constructor
    /// </summary>
    /// <param name="content"></param>
    public ScrollableGenelistMod(ModContentPack content) : base(content)
    {
        instance = this;
        Settings = GetSettings<ScrollableGenelistSettings>();
        currentVersion = VersionFromManifest.GetVersionFromModMetaData(content.ModMetaData);
    }

    /// <summary>
    ///     The instance-settings for the mod
    /// </summary>
    internal ScrollableGenelistSettings Settings { get; }

    /// <summary>
    ///     The title for the mod-settings
    /// </summary>
    /// <returns></returns>
    public override string SettingsCategory()
    {
        return "Scrollable Genelist";
    }

    /// <summary>
    ///     The settings-window
    ///     For more info: https://rimworldwiki.com/wiki/Modding_Tutorials/ModSettings
    /// </summary>
    /// <param name="rect"></param>
    public override void DoSettingsWindowContents(Rect rect)
    {
        var listing_Standard = new Listing_Standard();
        listing_Standard.Begin(rect);
        Settings.MaxHeight =
            listing_Standard.SliderLabeled(
                "ScrollableGenelist.MaxHeight".Translate(Settings.MaxHeight.ToStringPercent()), Settings.MaxHeight,
                0.01f, 1f);
        if (currentVersion != null)
        {
            listing_Standard.Gap();
            GUI.contentColor = Color.gray;
            listing_Standard.Label("ScrollableGenelist.CurrentModVersion".Translate(currentVersion));
            GUI.contentColor = Color.white;
        }

        listing_Standard.End();
    }
}