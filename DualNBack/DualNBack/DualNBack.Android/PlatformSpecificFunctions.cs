using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DualNBack.Droid;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(PlatformSpecificFunctions))]
namespace DualNBack.Droid
{
    public class PlatformSpecificFunctions: IPlatformSpecificFunctions
    {
        public PlatformSpecificFunctions()
        {

        }
        public void CloseApplication()
        {
            var activity = (Activity)Forms.Context;
            activity.FinishAffinity();
        }
    }
}