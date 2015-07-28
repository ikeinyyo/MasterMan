using MasterMan.Core.Enums;
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
        private MasterRender render;
        private int displacedX = 0;
        private int displacedY = 0;
        #endregion

        public MainWindow()
        {
            InitializeComponent();
            InitializeRender();
        }
        private void InitializeRender()
        {
            render = new MasterRender(SceneWorld, SettingsService.DefaultTexture, SettingsService.TileSize, SettingsService.TileMapSize);
            render.SetBackgroundColor(Colors.LimeGreen);
            render.Render(displacedX, displacedY);
            MainViewModel viewModel = DataContext as MainViewModel;
            if (viewModel != null)
            {
                viewModel.NeedUpdate += Render;
            }
        }

        private void Render(object sender, EventArgs e)
        {
            Render();
        }
        private void Render()
        {
            UpdatePadding();
            render.Render(displacedX, displacedY);
        }
        private void UpdatePadding()
        {
            Player player = EntityManager.Instance.Player;
            if (player != null)
            {
                displacedX = player.Position.X - (int)SceneWorld.ActualWidth / SettingsService.TileSize / 2;
                displacedY = player.Position.Y - (int)SceneWorld.ActualHeight / SettingsService.TileSize / 2;
            }
        }

        private void OnWindowKeyUp(object sender, KeyEventArgs e)
        {
            if (EntityManager.Instance.World != null && !EntityManager.Instance.World.EndedGame)
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
                    Render();
                }
            }
        }

        private void OnWindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
            Render();
        }
    }
}
