using NavigatingRovers.ConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NavigatingRovers.ConsoleApp.Helper
{
    public class CommandHelper
    {
        public List<string> Command(string[] commandList)
        {
            if (commandList.Length >= 3 && commandList.Length % 2 == 1)
            {
                var moveHelper = new MoveHelper();
                var roverPositionList = new List<RoverPosition>();
                var expectedPositionList = new List<string>();
                var areaOfRover = CreateRoverArea(commandList[0]);

                RoverPosition firstPosition = null;
                for (int i = 1; i < commandList.Length; i++)
                {
                    var modeOfCommand = (i - 1) % 2;
                    if (modeOfCommand == 0)
                    {
                        firstPosition = RoverLocation(commandList[i], areaOfRover);
                    }
                    else
                    {
                        roverPositionList.Add(moveHelper.RoverMove(commandList[i], firstPosition, commandList[0]));
                    }
                }

                foreach (var roverPosition in roverPositionList)
                {
                    string roverDirection = DirectionConvertToString(roverPosition.Direction);
                    expectedPositionList.Add($"{roverPosition.Coordinate.X} {roverPosition.Coordinate.Y} {roverDirection}");
                }

                return expectedPositionList;
            }
            else
            {
                throw new ArgumentException("Invalid character for command");
            }
        }

        public List<Coordinate> CreateRoverArea(string command)
        {
            var coordinateList = new List<Coordinate>();
            var coordinate = command.Split(' ');

            if (!int.TryParse(coordinate[0], out var xCoordinate))
            {
                throw new ArgumentException("Invalid character for X value.");
            }
            if (!int.TryParse(coordinate[1], out var yCoordinate))
            {
                throw new ArgumentException("Invalid character for Y value.");
            }

            if (coordinate.Length == 2)
            {
                for (int x = 0; x <= xCoordinate; x++)
                {
                    for (int y = 0; y <= yCoordinate; y++)
                    {
                        coordinateList.Add(new Coordinate { X = x, Y = y });
                    }
                }
                return coordinateList;
            }
            return coordinateList;
        }

        public RoverPosition RoverLocation(string command, List<Coordinate> areaOfRover)
        {
            var roverLocation = new RoverPosition();
            var location = command.Split(' ');

            if (location.Length == 3)
            {
                var direction = DirectionType.None;
                if (!int.TryParse(location[0], out var xCoordinate))
                {
                    throw new ArgumentException("Invalid character for X value.");
                }
                if (!int.TryParse(location[1], out var yCoordinate))
                {
                    throw new ArgumentException("Invalid character for Y value.");
                }

                switch (location[2])
                {
                    case "N":
                        direction = DirectionType.North;
                        break;
                    case "S":
                        direction = DirectionType.South;
                        break;
                    case "E":
                        direction = DirectionType.East;
                        break;
                    case "W":
                        direction = DirectionType.West;
                        break;
                }

                if (direction.Equals(DirectionType.None))
                {
                    throw new ArgumentException("Invalid character for direction value");
                }

                var coordinate = new Coordinate { X = xCoordinate, Y = yCoordinate };
                var isCoordinateOnArea = areaOfRover.FirstOrDefault(x => x.X == xCoordinate && x.Y==yCoordinate);

                if (isCoordinateOnArea == null)
                {
                    throw new ArgumentException("Rover's first location is out of area.");
                }

                roverLocation = new RoverPosition { Direction = direction, Coordinate = coordinate };
            }

            return roverLocation;
        }

        public string DirectionConvertToString(DirectionType direction)
        {
            string directionString = string.Empty;
            switch (direction)
            {
                case DirectionType.North:
                    directionString = "N";
                    break;
                case DirectionType.South:
                    directionString = "S";
                    break;
                case DirectionType.East:
                    directionString = "E";
                    break;
                case DirectionType.West:
                    directionString = "W";
                    break;
            }
            return directionString;
        }
    }
}
