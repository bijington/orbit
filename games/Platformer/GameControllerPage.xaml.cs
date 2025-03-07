using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer;

public partial class GameControllerPage : ContentPage
{
    public GameControllerPage(GameControllerPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}