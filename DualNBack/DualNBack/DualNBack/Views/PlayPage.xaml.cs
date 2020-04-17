using DualNBack.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DualNBack.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlayPage : ContentPage
    {
        PlayViewModel viewModel;
        public PlayPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new PlayViewModel();
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            if (height > width)
            {
                mainGrid.HeightRequest = width - 50;
                mainGrid.WidthRequest = width - 50;
            }
            else
            {
                mainGrid.WidthRequest = height - 50;
                mainGrid.HeightRequest = height - 50;
            }
        }
    }
}