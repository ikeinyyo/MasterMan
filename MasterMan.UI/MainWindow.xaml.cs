using MasterMan.Common.Enums;
using MasterMan.Core.Entities;
using MasterMan.Core.Services;
using MasterMan.UI.Services;
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
        private bool initialized = false;
        public MainWindow()
        {
            InitializeComponent();
        }


        // Temporal Implemetation
        private MasterManRenderService render;
        private void OnStartClick(object sender, RoutedEventArgs e)
        {
            initialized = true;
            // Create render service
            render = new MasterManRenderService(SceneWorld, "pack://application:,,,/MasterMan.UI;component/Assets/Textures/texture-map.png", 32, 256);
            render.SetBackgroundColor(Colors.LimeGreen);
            World world = new World(20, 10);
            Player player = EntityFactory.Player;
            world.SetPlayer(player, new Common.Models.Position(1, 1));

            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if(i != 1 || j != 1)
                    {
                        Entity entity = null;
                        if(i == 0 || j == 0 || i == 19 || j == 9)
                        {
                            entity = EntityFactory.Tree;
                        }
                        else
                        {
                            entity = EntityFactory.Dot;
                        }
                        world.AddEntity(entity, new Common.Models.Position(i, j));


                    }
                }
            }
                render.Render();
        }

        private void OnWindowKeyUp(object sender, KeyEventArgs e)
        {
            if (initialized)
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
