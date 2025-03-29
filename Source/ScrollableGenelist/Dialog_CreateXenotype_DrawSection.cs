using System.Collections.Generic;
using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

namespace ScrollableGenelist;

[HarmonyPatch(typeof(Dialog_CreateXenotype), "DrawSection")]
public static class Dialog_CreateXenotype_DrawSection
{
    private static Vector2 scrollPosition = Vector2.zero;

    private static Rect originalRect;

    public static void Prefix(ref Rect containingRect, List<GeneDef> genes, bool? collapsed,
        bool adding, out float __state)
    {
        __state = -1f;
        if (collapsed == true)
        {
            return;
        }

        if (adding)
        {
            return;
        }

        if (genes is not { Count: > 0 })
        {
            return;
        }

        var geneHeight = GeneCreationDialogBase.GeneSize.y + 8f + 1f;
        var genesPerRow =
            Mathf.FloorToInt((containingRect.width - 16f) / (34f + GeneCreationDialogBase.GeneSize.x + 8f + 1f));
        var numberOfRows = Mathf.CeilToInt((float)genes.Count / genesPerRow);
        var totalHeight = numberOfRows * geneHeight;
        var maxHeight = containingRect.height * ScrollableGenelistMod.instance.Settings.MaxHeight;

        if (totalHeight <= maxHeight)
        {
            return;
        }

        totalHeight += geneHeight;

        var innerRect = new Rect(0f, 0, containingRect.width - 16f, totalHeight);
        var outerRect = new Rect(0f, 0, containingRect.width, maxHeight);
        Widgets.BeginScrollView(outerRect, ref scrollPosition, innerRect);
        originalRect = containingRect;
        containingRect = innerRect;
        __state = outerRect.height;
    }

    public static void Postfix(float __state, ref float curY, ref Rect containingRect)
    {
        if (__state == -1)
        {
            return;
        }

        Widgets.EndScrollView();
        curY = __state;
        containingRect = originalRect;
    }
}