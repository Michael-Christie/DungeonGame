using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EditorResources
{
    public static class Blocks
    {
        public const string DefaultPath = "Blocks/";

        public static readonly string stone = $"{DefaultPath}Stone";
        public static readonly string cobblestone = $"{DefaultPath}CobbleStone";
        public static readonly string mossyCobblestone = $"{DefaultPath}MossyCobbleStone";
        public static readonly string andisite = $"{DefaultPath}Andisite";

        public static readonly string oakLog = $"{DefaultPath}OakLog";
        public static readonly string oakLogStripped = $"{DefaultPath}StrippedOakLog";
        public static readonly string oakPlank = $"{DefaultPath}OakPlank";
        public static readonly string oakSlab = $"{DefaultPath}OakSlab";
    }
}
