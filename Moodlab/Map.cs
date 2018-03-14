using System;
using System.Collections.Generic;
using System.Linq;

namespace Moodlab
{
    public class Map
    {
        public Tile[,] Data { get; private set; }
        public Vector Size {
            get {
                return new Vector(Data.GetLength(0), Data.GetLength(1));
            }
        }

        public Map(Vector size){
            Data = new Tile[size.X, size.Y];
        }

        public void Set(int x, int y, Tile tile){
            Data[x, y] = tile;
        }
        public void Set(Vector postion, Tile tile){
            Set(postion.X, postion.Y, tile);
        }

        public Vector? GetPosition(Tile tile){
            for (int x = 0; x < Size.X; x++)
            {
                for (int y = 0; y < Size.Y; y++)
                {
                    if (Data[x, y] == tile)
                        return new Vector(x, y);
                }
            }
            return null;
        }

        public void Generate(){
            // TODO: make that possible
            //const int MIN_ROOM_SIZE = 4, MAX_ROOM_SIZE = 16;

            //var random = new Random();

            //int roomSizeX = 1;//random.Next(MIN_ROOM_SIZE, MAX_ROOM_SIZE);
            //int roomSizeY = 8;//random.Next(MIN_ROOM_SIZE, MAX_ROOM_SIZE);

            List<Door> doors = new List<Door>(){
                new Door(OrientedTile.Orientations.Down)
            };
            Set(Size.X / 2, Size.Y / 2, doors[0]);

        }
    }
}