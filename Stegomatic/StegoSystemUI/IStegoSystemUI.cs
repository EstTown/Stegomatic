﻿using System;
using System.Drawing;
using StegomaticProject.StegoSystemUI.Config;
using StegomaticProject.StegoSystemUI.Events;

namespace StegomaticProject.StegoSystemUI
{
    public interface IStegoSystemUI
    {
        // Get info from UI
        string Message { get; }
        string PathOfCoverImage { get; }
        Bitmap DisplayImage { get; }
        IConfig Config { get; }
        Func<int, int, bool, int> ImageCapacityCalculator { get; set; }
        bool Enable { get; set; }

        // Modify UI
        void SetDisplayImage(Bitmap newImage);
        void ShowNotification(string notification, string title);
        void ShowMessageNotification(string notification, string text, string title);
        string GetEncryptionKey();
        string GetStegoSeed();
        void OpenImage();
        void SaveImage(Bitmap image);

        // Start/End
        void Start();
        void Terminate();

        // Events
        event DisplayNotificationEventHandler NotifyUser;
        event BtnEventHandler DecodeBtn;
        event BtnEventHandler EncodeBtn;
        event BtnEventHandler OpenImageBtn;
    }
}
