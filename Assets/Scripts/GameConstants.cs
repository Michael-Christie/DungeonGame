using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameConstants
{
    public static class WaitTimers
    {
        public static WaitForEndOfFrame waitForEndFrame = new WaitForEndOfFrame();

        //Whole Seconds
        public static WaitForSeconds waitForOneSecond = new WaitForSeconds(1F);
        public static WaitForSeconds waitForTwoSeconds = new WaitForSeconds(2F);

        //Point Seconds 
        public static WaitForSeconds waitForPointOne = new WaitForSeconds(0.1F);
        public static WaitForSeconds waitForPointTwo = new WaitForSeconds(0.2F);
        public static WaitForSeconds waitForPointFive = new WaitForSeconds(0.5F);

        //Animations
        public static WaitForSeconds waitForFade = new WaitForSeconds(Animations.fadeTime);
        public static WaitForSeconds waitForScale = new WaitForSeconds(Animations.scaleTime);
        public static WaitForSeconds waitForMove = new WaitForSeconds(Animations.moveTime);
        public static WaitForSeconds waitForFlip = new WaitForSeconds(Animations.flipTime);
        public static WaitForSeconds waitForRotate = new WaitForSeconds(Animations.rotateTime);

        //Short Animations
        public static WaitForSeconds waitForFadeShort = new WaitForSeconds(Animations.fadeTimeShort);
        public static WaitForSeconds waitForScaleShort = new WaitForSeconds(Animations.scaleTimeShort);
        public static WaitForSeconds waitForMoveShort = new WaitForSeconds(Animations.moveTimeShort);
        public static WaitForSeconds waitForFlipShort = new WaitForSeconds(Animations.flipTimeShort);

        public static WaitForSecondsRealtime waitForOneRealSecond = new WaitForSecondsRealtime(1.0F);
    }

    public static class Animations
    {
        public static float scaleTime = 0.5F;
        public static float fadeTime = 1F;
        public static float moveTime = 0.5F;
        public static float flipTime = 0.5F;
        public static float shakeTime = 0.5F;
        public static float rotateTime = 1.5F;

        public static float scaleTimeShort = 0.25F;
        public static float fadeTimeShort = 0.25F;
        public static float moveTimeShort = 0.25F;
        public static float flipTimeShort = 0.25F;
        public static float shakeTimeShort = 0.25F;
    }

    public static class PlayerPrefs
    {
        public static string isSFXMuted = "issfxmute";
        public static string isMusicMuted = "ismusicmute";
    }

    public static class Tags
    {
        public static string player = "Player";
    }

    public static class SaveFiles
    {
        public static string PlayerOneSave = "/PlayerData1.dat";
        public static string PlayerTwoSave = "/PlayerData2.dat";
        public static string PlayerThreeSave = "/PlayerData3.dat";
        public static string ErrorSave = "/ErrorSave.dat";
    }

    public enum Scenes
    {
        BootLoader = 0,
        MainMenu,
        PlayerController,
        GameHub,
        UI,
        GameWorld,
        ObjectPooler
    }

    public enum SceneCollections
    {
        Bootloader = 0,
        MainMenu = 1,
        HubWorld = 2,
        Game = 3
    }

    public enum MusicClip
    {
        MainMenu
    }

    public enum SoundClip
    {
        ButtonPress
    }

    public enum EntityID
    {
        None = -1,
        Artefact = 1,
        Coins = 2,
        BaseAI = 3,
    }

    public enum ItemID
    {
        Artefact,
        Coins,
        BardDefault,
        BruteDefault,
        GunMageDefault,
        HunterDefault,
        ScoutDefault
    }

    public enum EnemyID
    {
        BaseAI = 3
    }

    public enum Menus
    {
        GameHud = 0,
        GameOver = 1,
        Inventory = 2,
        HubHud = 3,
        Pause = 4,
        Settings = 5,
        Statistics = 6,
        Advancements = 7
    }

    public enum CharacterTypes
    {
        Hunter,
        Scout, //Quick movement, Light attack, light defence
        Heavy, //Slow movement, heavy attack, heavy defence
        Mage, //Quick movement, ranged attacks, light defence
        Bard,
    }

    public enum Stats
    {
        PlayTime,
        DungeonsPlayed,
        DungeonsComplete,
        DungeonsLost,
        DungeonsAbandoned,
        EnemiesKilled,
    }

    public enum Advancement
    {
        Killer
    }

    public enum RewardType
    {
        XP,
        Coin,
    }

    public enum StatCategory
    {
        General,
        Dungeons,
        Enemies,
        Misc
    }

    //
    public static string GetSavePathFromInt(int _saveSlot)
    {
        if (_saveSlot == 0)
        {
            return SaveFiles.PlayerOneSave;
        }
        else if (_saveSlot == 1)
        {
            return SaveFiles.PlayerTwoSave;
        }
        else if (_saveSlot == 2)
        {
            return SaveFiles.PlayerThreeSave;
        }

        Debug.LogError($"Load Save System | Save slot {_saveSlot} is not in Range");
        return SaveFiles.ErrorSave;
    }
}
