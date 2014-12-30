using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotExample
{
    public static class MasterMan
    {
        #region Consts
        // Directions
        public const string Up = "up";
        public const string Down = "down";
        public const string Left = "left";
        public const string Right = "right";
        public const string UpLeft = "upleft";
        public const string UpRight = "upright";
        public const string DownLeft = "downleft";
        public const string DownRight = "downright";
        public const string None = "none";

        // Commands
        private const string SeeCommand = "see";
        private const string MoveCommand = "move";
        private const string StatusCommand = "status";

        // Entities
        public const string Dot = "dot";
        public const string Tree = "tree";


        // Status
        public const string Run = "run";
        public const string Win = "win";
        public const string GameOver = "gameover";

        #endregion

        #region Public Methods
        public static string See(string direction)
        {
            Console.WriteLine(makeCommand(SeeCommand, direction));
            var result = Console.ReadLine();
            return result;
        }

        public static bool IsType(string direction, string entity)
        {
            var result = See(direction);
            return result.Contains(entity);
        }

        public static bool IsRun()
        {
            Console.WriteLine(makeCommand(StatusCommand));
            var result = Console.ReadLine();
            return result.Contains(Run);
        }

        public static void Move(string direction)
        {
            Console.WriteLine(makeCommand(MoveCommand, direction));
        }
        #endregion

        #region Private Methods
        private static string makeCommand(string command, string direction)
        {
            return string.Format("{0} {1}", command, direction);
        }

        private static string makeCommand(string command)
        {
            return string.Format("{0}", command);
        }
        #endregion
    }
}
