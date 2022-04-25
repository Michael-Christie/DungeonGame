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

    public enum Scenes
    {
        BootLoader = 0,
        MainMenu,
        PlayerController,
        GameHub,
        UI,
        Settings,
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
        Coins = 2
    }

    public enum ItemID
    {
        Artefact = 1,
        Coins = 2,
    }

    public enum Menus
    {
        GameHud = 0,
        GameOver = 1,
    }
}
