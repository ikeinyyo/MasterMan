using MasterMan.Common.Enums;
using MasterMan.Core.Services;
using NetworkService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MasterMan.UI.Services
{
    public class MasterNetwork : BaseNetwork
    {
        private Dictionary<string, Action<Direction>> actions;

        public MasterNetwork()
        {
            actions = new Dictionary<string, Action<Direction>>();
            actions.Add("move", Move);
        }

        public void LaunchBot(string botFilename)
        {
            LaunchNodeProcess(botFilename, true);
        }

        public void LaunchConsole()
        {
            var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase).Replace("file:\\", "");
            var iconPath = Path.Combine(outPutDirectory, @"Assets\Bots\console.js");
            LaunchNodeProcess(iconPath, true, false);
        }

        public void Dispose()
        {
            StopProcess();
        }

        public string Step()
        {
            string command = string.Empty;

            if (IsProcessLaunched())
            {
                try
                {
                    SendMessage("Continue");
                }
                catch
                {
                }

                bool newStep = false;
                while (!newStep)
                {
                    command = ReceiveCommand();

                    if (!string.IsNullOrWhiteSpace(command))
                    {
                        newStep = true;
                    }
                }

            }
            return command;
        }

        #region Prepare Commands
        public void ExecuteCommand(string command)
        {
            ParseCommand(command);
        }

        private void ParseCommand(string command)
        {
            if (!string.IsNullOrWhiteSpace(command))
            {
                string[] tokens = command.Split(' ');

                if (tokens.Length >= 1 && tokens.Length <= 2)
                {
                    string actionName = tokens[0];
                    Direction direction = Direction.None;

                    if (tokens.Length == 2)
                    {
                        string directionName = tokens[1];
                        direction = DirectionHelper.ParseDirection(directionName);
                    }

                    if (actions.ContainsKey(actionName.ToLower()))
                    {
                        Action<Direction> action = null;
                        actions.TryGetValue(actionName, out action);

                        if (action != null && !direction.Equals(Direction.Unknow))
                        {
                            action.Invoke(direction);
                        }
                    }
                }
            }
        }
        #endregion

        #region Command
        public void Move(Direction direction)
        {
            var world = EntityManager.Instance.World;
            if (world != null && EntityManager.Instance.Player != null)
            {
                bool moved = EntityManager.Instance.Player.Move(direction);
                
                if(moved)
                {
                    EntityManager.Instance.Update();
                }
            }
            else
            {
                throw new NullReferenceException("World or Player are null. MasterServices no initialized");
            }
        }
        #endregion
    }
}
