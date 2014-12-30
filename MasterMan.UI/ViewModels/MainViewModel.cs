using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MasterMan.Core.Entities;
using MasterMan.Core.Models;
using MasterMan.Core.Services;
using MasterMan.UI.Services;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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

        private string botPath;

        public string BotPath
        {
            get { return botPath; }
            set { botPath = value; RaisePropertyChanged(); }
        }

        private string mapPath;

        public string MapPath
        {
            get { return mapPath; }
            set { mapPath = value; RaisePropertyChanged(); }
        }

        private int velocity;

        public int Velocity
        {
            get { return velocity; }
            set { velocity = value; RaisePropertyChanged(); }
        }

        #endregion

        #region Fields
        private World world;
        private MasterNetwork network;
        private BackgroundWorker worker; 
        #endregion

        public MainViewModel()
        {
            Velocity = 500;
            network = new MasterNetwork();
        }


        #region Mock Methods
        private void MockWorld()
        {
            world = new World(15, 11);
            Player player = EntityFactory.Player;
            world.SetPlayer(player, new Position(1, 1));

            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    if (i != 1 || j != 1)
                    {
                        Entity entity = null;
                        if (i == 0 || j == 0 || i == 14 || j == 10)
                        {
                            entity = EntityFactory.Tree;
                        }
                        else if(i == 4 && j != 9)
                        {
                            entity = EntityFactory.Tree;
                        }
                        else if (i == 8 && j != 1)
                        {
                            entity = EntityFactory.Tree;
                        }
                        else
                        {
                            entity = EntityFactory.Dot;
                        }
                        world.AddEntity(entity, new Position(i, j));
                    }
                }
            }
           // world.AddEntity(EntityFactory.Skeleton, new Common.Models.Position(3, 3));
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
                    reset();
                    MockWorld();
                    SendNeedUpdate();

                    launchBoot();
                    launchWorker();
                });
            }
        }

        public RelayCommand SelectBot
        {
            get
            {
                return new RelayCommand(selectBot);
            }
        }

        public RelayCommand SelectMap
        {
            get
            {
                return new RelayCommand(selectMap);
            }
        }

        public RelayCommand Dispose
        {
            get
            {
                return new RelayCommand(() =>
                {
                    dispose();
                });
            }
        }
        #endregion

        #region Private Methods
        private void launchBoot()
        {
            if (network != null)
            {
                network.Dispose();
            }
            network = new MasterNetwork();
            if (!string.IsNullOrEmpty(BotPath))
            {
                var extension = Path.GetExtension(BotPath); 

                switch(extension)
                {
                    case ".exe":
                    network.LaunchBot(BotPath);
                        break;
                    case ".js":
                        network.LaunchNodeBot(BotPath);
                        break;
                }
            }
            else
            {
                network.LaunchDefaultBot();
            }
        }

        private void launchWorker()
        {
            if (worker != null)
            {
                worker.DoWork -= loopGame;
                worker.Dispose();
            }

            worker = new BackgroundWorker();
            worker.WorkerSupportsCancellation = true;
            worker.DoWork += loopGame;
            worker.RunWorkerAsync();
        }


        private void reset()
        {
            dispose();
        }

        private void dispose()
        {
            if (worker != null)
            {
                worker.DoWork -= loopGame;
                worker.CancelAsync();
            }

            if (network != null)
            {
                network.Dispose();
            }
        }
        private async void loopGame(object sender, DoWorkEventArgs e)
        {
            string command = network.Step();
            while (!command.Equals("exit"))
            {
                bool needUpdate = network.ExecuteCommand(command);
                if (needUpdate)
                {
                    await Task.Delay(Velocity);
                    EntityManager.Instance.Update();
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        SendNeedUpdate();
                    });
                }

                command = network.Step();
            }
        }

        private void selectBot()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Executables|*.exe|Node Script|*.js";

            bool? select = dialog.ShowDialog();

            if (select != null && select.Value)
            {
                BotPath = dialog.FileName;
            }
        }

        private void selectMap()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Text file|*.txt";

            bool? select = dialog.ShowDialog();

            if (select != null && select.Value)
            {
                MapPath = dialog.FileName;
            }
        }
        #endregion
    }
}
