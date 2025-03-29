using Verse;

namespace ScrollableGenelist;

/// <summary>
///     Definition of the settings for the mod
/// </summary>
internal class ScrollableGenelistSettings : ModSettings
{
    public float MaxHeight = 0.5f;

    /// <summary>
    ///     Saving and loading the values
    /// </summary>
    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_Values.Look(ref MaxHeight, "MaxHeight", 0.5f);
    }
}