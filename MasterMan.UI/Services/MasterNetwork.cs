using MasterMan.Core.Enums;
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
        private bool needUpdate;

        public MasterNetwork()
        {
            actions = new Dictionary<string, Action<Direction>>();
            actions.Add("move", Move);
            actions.Add("see", See);
            actions.Add("status", GetStatus);
        }

        public void LaunchNodeBot(string botFilename)
        {
            LaunchNodeProcess(botFilename, true, false);
        }

        public void LaunchBot(string botFilename)
        {
            LaunchProcess(botFilename, false);
        }

        public void LaunchDefaultBot()
        {
            var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase).Replace("file:\\", "");
            var botFilename = Path.Combine(outPutDirectory, @"..\..\..\BotExample\bin\Debug\BotExample.exe");
            LaunchProcess(botFilename, false);
        }

        public void LaunchConsole()
        {
            var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase).Replace("file:\\", "");
            var consolePath = Path.Combine(outPutDirectory, @"Assets\Bots\console.js");
            LaunchNodeProcess(consolePath, true, false);
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
        public bool ExecuteCommand(string command)
        {
            needUpdate = false;
            ParseCommand(command);
            return needUpdate;
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
            bool moved = false;
            var world = EntityManager.Instance.World;
            if (world != null && EntityManager.Instance.Player != null && !world.EndedGame)
            {
                moved = EntityManager.Instance.Player.Move(direction);
            }

            needUpdate = moved;
        }

        public void See(Direction direction)
        {
             var world = EntityManager.Instance.World;
             if (world != null && EntityManager.Instance.Player != null && !world.EndedGame)
             {
                 var entities = world.See(direction);
                 string entityTypes = string.Empty;

                 if (entities.Any())
                 {
                     entityTypes = entities.First().Type.ToString().ToLowerInvariant();

                     foreach (var item in entities.Skip(1))
                     {
                         entityTypes += "," + entities.First().Type.ToString().ToLowerInvariant();
                     }
                 }

                 SendMessage(entityTypes);
             }
            needUpdate = false;
        }

        public void GetStatus(Direction none)
        {
            Status status = Status.Run;
            var world = EntityManager.Instance.World;
            if (world != null)
            {
                if(world.EndedGame)
                {
                    if(EntityManager.Instance.Player.IsAlive)
                    {
                        status = Status.Win;
                    }
                    else
                    {
                        status = Status.GameOver;
                    }
                }
            }
            SendMessage(status.ToString().ToLowerInvariant());
        }
        #endregion
    }
}
