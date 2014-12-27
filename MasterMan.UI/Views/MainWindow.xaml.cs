using MasterMan.Common.Enums;
using MasterMan.Core.Entities;
using MasterMan.Core.Services;
using MasterMan.UI.Services;
using MasterMan.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MasterMan.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Fields
        private bool initialized = false;
        private MasterManRenderService render;
        #endregion

        public MainWindow()
        {
            InitializeComponent();
            InitializeRender();
        }
        private void InitializeRender()
        {
            render = new MasterManRenderService(SceneWorld, "pack://application:,,,/MasterMan.UI;component/Assets/Textures/texture-map.png", 32, 256);
            render.SetBackgroundColor(Colors.LimeGreen);
            render.Render();
            MainViewModel viewModel = DataContext as MainViewModel;
            if (viewModel != null)
            {
                viewModel.NeedUpdate += Render;
            }
        }

        private void Render(object sender, EventArgs e)
        {
            render.Render();
        }

        private void OnWindowKeyUp(object sender, KeyEventArgs e)
        {
            if (!EntityManager.Instance.World.EndedGame)
            {
                bool action = true;

                Player player = EntityManager.Instance.Player;
                switch (e.Key)
                {
                    case Key.Up:
                        player.Move(Direction.Up);
                        break;
                    case Key.Down:
                        player.Move(Direction.Down);
                        break;
                    case Key.Left:
                        player.Move(Direction.Left);
                        break;
                    case Key.Right:
                        player.Move(Direction.Right);
                        break;
                    default:
                        action = false;
                        break;
                }

                if (action)
                {
                    EntityManager.Instance.Update();
                    render.Render();
                }
            }
        }
    }
}
