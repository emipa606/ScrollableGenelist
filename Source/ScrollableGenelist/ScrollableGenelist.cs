using System.Reflection;
using HarmonyLib;
using Verse;

namespace ScrollableGenelist;

[StaticConstructorOnStartup]
public static class ScrollableGenelist
{
    static ScrollableGenelist()
    {
        new Harmony("Mlie.ScrollableGenelist").PatchAll(Assembly.GetExecutingAssembly());
    }
}