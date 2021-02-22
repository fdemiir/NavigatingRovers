using NavigatingRovers.ConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace NavigatingRovers.ConsoleApp.Helper
{
    public class MoveHelper
    {
        public RoverPosition RoverMove(string command, RoverPosition firstPosition, string maxLocation)
        {
            var roverLocation = new RoverPosition();
            var rgx = new Regex("^[LRM]+$");

            if (rgx.IsMatch(command))
            {
                foreach (var item in command)
                {
                    switch (item)
                    {
                        case 'L':
                            roverLocation = LeftMove(firstPosition);
                            break;
                        case 'R':
                            roverLocation = RightMove(firstPosition);
                            break;
                        case 'M':
                            roverLocation = MidMove(firstPosition, maxLocation);
                            break;
                    }
                }
            }
            else
            {
                throw new ArgumentException($"Invalid character for move value");
            }
            return roverLocation;
        }

        public RoverPosition LeftMove (RoverPosition roverPosition)
        {
            switch (roverPosition.Direction)
            {
                case DirectionType.North:
                    roverPosition.Direction = DirectionType.West;
                    break;
                case DirectionType.South:
                    roverPosition.Direction = DirectionType.East;
                    break;
                case DirectionType.East:
                    roverPosition.Direction = DirectionType.North;
                    break;
                case DirectionType.West:
                    roverPosition.Direction = DirectionType.South;
                    break;
            }
            return roverPosition;
        }
        public RoverPosition RightMove(RoverPosition roverPosition)
        {
            switch (roverPosition.Direction)
            {
                case DirectionType.North:
                    roverPosition.Direction = DirectionType.East;
                    break;
                case DirectionType.South:
                    roverPosition.Direction = DirectionType.West;
                    break;
                case DirectionType.East:
                    roverPosition.Direction = DirectionType.South;
                    break;
                case DirectionType.West:
                    roverPosition.Direction = DirectionType.North;
                    break;
            }
            return roverPosition;
        }
        public RoverPosition MidMove(RoverPosition roverPosition, string maxLocation)
        {
            var maxCoordinate = maxLocation.Split(' ');
            var maxCoordinateX = Convert.ToInt32(maxCoordinate[0]);
            var maxCoordinateY = Convert.ToInt32(maxCoordinate[1]);

            if (roverPosition.Coordinate.X <= maxCoordinateX && roverPosition.Coordinate.Y <= maxCoordinateY)
            {
                switch (roverPosition.Direction)
                {
                    case DirectionType.North:
                        roverPosition.Coordinate.Y++;
                        break;
                    case DirectionType.South:
                        roverPosition.Coordinate.Y--;
                        break;
                    case DirectionType.East:
                        roverPosition.Coordinate.X++;
                        break;
                    case DirectionType.West:
                        roverPosition.Coordinate.X--;
                        break;
                }
            }
            else
            {
                throw new ArgumentException("Your move command is out of area.");
            }
            return roverPosition;
        }
    }
}
