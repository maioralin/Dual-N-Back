using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using DualNBack.iOS;
using Foundation;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(PlatformSpecificFunctions))]
namespace DualNBack.iOS
{
    public class PlatformSpecificFunctions : IPlatformSpecificFunctions
    {
        public PlatformSpecificFunctions()
        {

        }
        public void CloseApplication()
        {
            Thread.CurrentThread.Abort();
        }
    }
}