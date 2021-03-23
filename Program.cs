using System;
using System.Collections.Generic;

namespace Mars_Case
{

    class Program
    {
        static List<Rover> rovers = new List<Rover>();
        static void Main(string[] args)
        {
            var _land = new Land();

            Console.WriteLine("Land Top");
            _land.SetUpperRight(Console.ReadLine());
            Console.WriteLine("Position!");
            var rover = new Rover();
            rover.SetStartPosition(Console.ReadLine());
            rovers.Add(rover);

            Console.WriteLine("Directives");
            rover.RoverMoves(Console.ReadLine());

            Console.WriteLine("Position!");
            var _rover = new Rover();
            _rover.SetStartPosition(Console.ReadLine());

            Console.WriteLine("Directives");
            _rover.RoverMoves(Console.ReadLine());
            rovers.Add(_rover);
            Console.WriteLine("Rover Positions");
            rovers.ForEach(x => Console.WriteLine($"Rover positon x:{x.LandPosition.x} y:{x.LandPosition.y} d:{Ext.Directions[x.Direction]} p:{x.RoverInPlateau(_land)}"));

            Console.ReadKey();
        }
    }

    public class Rover
    {
        public bool RoverInPlateau(Land lnd) {

            bool x = lnd.LowerLeft.x <= LandPosition.x && lnd.UpperRigt.x >= LandPosition.x;
            bool y = lnd.LowerLeft.y <= LandPosition.y && lnd.UpperRigt.y >= LandPosition.y;
            return x && y;
        } 
        public Position LandPosition { get; set; }

        private int _direction;

        public int Direction
        {
            get { return _direction; }
            set
            {
                _direction = value > 3 ? 0 : value < 0 ? 3 : value;
            }
        }

        public void SetStartPosition(string startPos)
        {
            var splitedvalue = Ext.Parser(startPos, ' ');
            if (splitedvalue.Length == 3)
            {
                int x, y;

                if (Int32.TryParse(splitedvalue[0], out x) && Int32.TryParse(splitedvalue[1], out y))
                {
                    this.LandPosition = new Position(x, y);
                    this.Direction = Ext.Directions.FindIndex(a => a == splitedvalue[2]);
                }
                else
                {
                    Console.WriteLine("Hatalı Giriş");
                }
            }
            else
            {
                Console.WriteLine("Hatalı Giriş");
            }
        }

        public void RoverMoves(string command)
        {

            foreach (char item in command.ToCharArray())
            {
                switch (item)
                {
                    case 'L':
                        Direction--;
                        break;
                    case 'R':
                        Direction++;
                        break;
                    case 'M':
                        if (Direction % 2 == 0)
                        {
                            _ = Direction < 2 ? LandPosition.y += 1 : LandPosition.y -= 1;
                        }
                        else
                        {
                            _ = Direction < 2 ? LandPosition.x += 1 : LandPosition.x -= 1;
                        }


                        break;
                    default:
                        break;
                }
            }
        }
    }

    public class Position
    {
        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public int x { get; set; }

        public int y { get; set; }
    }

    public class Land
    {

        public Position LowerLeft { get; } = new Position(0, 0);

        public Position UpperRigt { get; set; }

        public void SetUpperRight(string upperRight)
        {

            var splitedvalue = Ext.Parser(upperRight, ' ');
            if (splitedvalue.Length == 2)
            {
                int x, y;

                if (Int32.TryParse(splitedvalue[0], out x) && Int32.TryParse(splitedvalue[0], out y))
                {
                    this.UpperRigt = new Position(x, y);
                }
                else
                {
                    Console.WriteLine("Hatalı Giriş");
                }
            }
            else
            {
                Console.WriteLine("Hatalı Giriş");
            }
        }
    }

    public class Ext
    {
        public static List<string> Directions = new List<string> { "N", "E", "S", "W" };
        public static string[] Parser(string value, char seperator)
        {
            var seperatedList = value.Split(seperator);
            return seperatedList;
        }
    }


}
