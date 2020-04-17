using DualNBack.Views;
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace DualNBack
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("play", typeof(PlayPage));
        }
    }
}
