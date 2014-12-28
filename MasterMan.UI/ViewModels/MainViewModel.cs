using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MasterMan.Core.Entities;
using MasterMan.Core.Services;
using MasterMan.UI.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
        private MasterNetwork network;
        private BackgroundWorker worker; 
        #endregion

        public MainViewModel()
        {
            network = new MasterNetwork();
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
        private void SendNeedUpdate()
        {
            if (NeedUpdate != null)
            {
                NeedUpdate(this, null);
            }
        }

        public RelayCommand CreateWorld
        {
            get
            {
                return new RelayCommand(() => 
                { 
                    MockWorld();
                    network.LaunchConsole();
                    SendNeedUpdate();

                    if (worker != null)
                    {
                        worker.DoWork -= LoopGame;
                        worker.CancelAsync();
                        worker.Dispose();
                    }

                    worker = new BackgroundWorker();
                    worker.DoWork += LoopGame;
                    worker.RunWorkerAsync();
                });
            }
        }
        #endregion

        #region Private Methods
        private void LoopGame(object sender, DoWorkEventArgs e)
        {
            string command = network.Step();
            while (!command.Equals("exit"))
            {
                network.ExecuteCommand(command);
                Application.Current.Dispatcher.Invoke(() =>
                {
                    SendNeedUpdate();
                });

                command = network.Step();
            }
        }
        #endregion
    }
}
