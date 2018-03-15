using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Moodlab
{
    public class Map
    {
        public Tiles.Tile[,] Tiles { get; private set; }
        public List<Entities.Entity> Entities { get; private set; }
        public Vector Size {
            get {
                return new Vector(Tiles.GetLength(0), Tiles.GetLength(1));
            }
        }

        public Map(Vector size){
            Tiles = new Tiles.Tile[size.X, size.Y];
            Entities = new List<Entities.Entity>();
        }

        public void Update(GameTime gameTime){
            foreach (Entities.Entity entity in Entities)
            {
                entity.Update(gameTime, this);
            }
        }

        public void AddEntity(Entities.Entity entity){
            Entities.Add(entity);
        }

        public Tiles.Tile GetTile(int x, int y)
        {
            if(x > 0 && y > 0 && x < Size.X && y < Size.Y)
                return Tiles[x, y];

            return null;
        }
        public Tiles.Tile GetTile(Vector postion)
        {
            return GetTile(postion.X, postion.Y);
        }

        public void SetTile(int x, int y, Tiles.Tile tile){
            Tiles[x, y] = tile;
        }
        public void SetTile(Vector postion, Tiles.Tile tile){
            SetTile(postion.X, postion.Y, tile);
        }

        public void Generate(){
            // TODO: make that possible
            //const int MIN_ROOM_SIZE = 4, MAX_ROOM_SIZE = 16;

            //var random = new Random();

            //int roomSizeX = 1;//random.Next(MIN_ROOM_SIZE, MAX_ROOM_SIZE);
            //int roomSizeY = 8;//random.Next(MIN_ROOM_SIZE, MAX_ROOM_SIZE);

            for (int x = 0; x < Size.X; x++)
            {
                for (int y = 0; y < Size.Y; y++)
                {
                    SetTile(
                        x,
                        y,
                        (x == 0 || y == 0 || x == Size.X-1 || y == Size.Y-1) ? (Tiles.Tile)new Tiles.Wall() : (Tiles.Tile)new Tiles.Ground()
                    );
                }
            }

            /*
            List<Door> doors = new List<Door>(){
                new Door(OrientedTile.Orientations.Down)
            };
            Set(Size.X / 2, Size.Y / 2, doors[0]);
            */
        }
    }
}