using System;
using StegomaticProject.StegoSystemController;
using StegomaticProject.StegoSystemModel;
using StegomaticProject.StegoSystemUI;

namespace StegomaticProject
{
    public class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            
            IStegoSystemModel stegoModel = new StegoSystemModelClass();
            IStegoSystemUI stegoUI = new StegoSystemWinForm();
            IStegoSystemControl stegoController = new StegoSystemControl(stegoModel, stegoUI);

            stegoUI.Start();
        }
    }
}