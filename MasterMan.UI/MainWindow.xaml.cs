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
        public MainWindow()
        {
            InitializeComponent();
        }


        // Temporal Implemetation
        private MasterManRenderService render;
        private void OnStartClick(object sender, RoutedEventArgs e)
        {
            // Create render service
            render = new MasterManRenderService(SceneWorld, "pack://application:,,,/MasterMan.UI;component/Assets/Textures/texture-map.png", 32, 256);
            World world = new World(20, 20);
            Player player = EntityFactory.Player;
            world.SetPlayer(player, new Common.Models.Position(1, 1));

            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    if(i != 1 || j != 1)
                    {
                        Entity entity = null;
                        if(i == 0 || j == 0 || i == 19 || j == 19)
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
    }
}
