using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MasterMan.Core.Entities;
using MasterMan.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterMan.UI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        #region Properties
        private int points;

        public int Points
        {
            get { return points; }
            set { points = value; RaisePropertyChanged(); }
        }

        private event EventHandler needUpdate;

        public EventHandler NeedUpdate
        {
            get { return needUpdate; }
            set { needUpdate = value; RaisePropertyChanged(); }
        }

        private event EventHandler endGame;

        public EventHandler EndGame
        {
            get { return endGame; }
            set { endGame = value; RaisePropertyChanged(); }
        }
        #endregion

        #region Fields
        private World world;
        #endregion

        public MainViewModel()
        {
        }


        #region Mock Methods
        private void MockWorld()
        {
            world = new World(20, 10);
            Player player = EntityFactory.Player;
            world.SetPlayer(player, new Common.Models.Position(1, 1));

            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (i != 1 || j != 1)
                    {
                        Entity entity = null;
                        if (i == 0 || j == 0 || i == 19 || j == 9)
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
            world.AddEntity(EntityFactory.Skeleton, new Common.Models.Position(3, 3));
        }
        #endregion

        #region Commands
        public RelayCommand CreateWorld
        {
            get
            {
                return new RelayCommand(() => 
                { 
                    MockWorld();  
                    if(NeedUpdate != null)
                    {
                        NeedUpdate(this, null);
                    }
                });
            }
        }
        #endregion
    }
}
