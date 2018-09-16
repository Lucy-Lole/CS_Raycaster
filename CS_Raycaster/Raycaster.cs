// Project: CS_Raycaster
// Filename; Raycaster.cs
// Created; 16/09/2018
// Edited: 16/09/2018

namespace CS_Raycaster
{
    using System;
    using System.Diagnostics;
    using System.Drawing;

    public class Raycaster
    {
        // Creating the world map.
        private readonly int[,] worldMap = new int[,]
        {
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,2,2,2,2,2,0,0,0,0,3,0,3,0,3,0,0,0,1},
            {1,0,0,0,0,0,2,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,2,0,5,0,2,0,0,0,0,3,0,0,0,3,0,0,0,1},
            {1,0,0,0,0,0,2,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,2,2,0,2,2,0,0,0,0,3,0,3,0,3,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,4,4,4,4,4,4,4,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,4,0,4,0,0,0,0,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,4,0,0,0,0,5,0,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,4,0,4,0,0,0,0,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,4,0,4,4,4,4,4,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,4,4,4,4,4,4,4,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}
        };

        // Vector for the width of the camera plane.
        private Vector cameraPlane = new Vector(0, 1);
        // These will be used to calculate frame lengths.

        private double moveSpeed;

        // This pen will be used to draw the pixels for each frame.
        private Pen pen = new Pen(Color.White,1);

        // Player's direction vector.
        private Vector playerDirection = new Vector(-1, 0);

        // Player's position in the map.
        private Vector playerPosition = new Vector(12, 12);

        private double rotSpeed;

        /// <summary>
        /// The Players position
        /// </summary>
        public Vector PlayerPosition
        {
            get => this.playerPosition;
            set => this.playerPosition = value;
        }

        /// <summary>
        /// Moves the player in the direction based off movement speed
        /// </summary>
        /// <param name="forwards">True for forward, False for backwards</param>
        public void Move(bool forwards)
        {
            Debug.WriteLine("Moving");
            Debug.WriteLine(this.moveSpeed);
            if (forwards)
            {
                if (this.worldMap[(int)(this.PlayerPosition.x+ this.playerDirection.x* this.moveSpeed),(int)(this.PlayerPosition.y)] == 0)
                {
                    this.PlayerPosition.x += this.playerDirection.x * this.moveSpeed;
                }
                if (this.worldMap[(int)(this.PlayerPosition.x),(int)(this.PlayerPosition.y + this.playerDirection.y * this.moveSpeed)] == 0 )
                {
                    this.PlayerPosition.y += this.playerDirection.y * this.moveSpeed;
                }
            }
            else
            {
                if (this.worldMap[(int)(this.PlayerPosition.x - this.playerDirection.x * this.moveSpeed), (int)(this.PlayerPosition.y)] == 0)
                {
                    this.PlayerPosition.x -= this.playerDirection.x * this.moveSpeed;
                }
                if (this.worldMap[(int)(this.PlayerPosition.x), (int)(this.PlayerPosition.y - this.playerDirection.y * this.moveSpeed)] == 0)
                {
                    this.PlayerPosition.y -= this.playerDirection.y * this.moveSpeed;
                }
            }
        }


        /// <summary>
        /// Calculates new frame and draws it to an image
        /// </summary>
        /// <param name="width">Width of the image to be made</param>
        /// <param name="height">Height of the image to be made</param>
        /// <returns>The new frame</returns>
        public Image NewFrame(int width,int height)
        {
            // First we create the frame we'll be drawing to.
            Bitmap bmp = new Bitmap(width, height);
            this.ClearFrame(bmp);

            for (int i = 0; i < width;i++)
            {
                #region Variable Declerations
                // This var tracks the relative position of the ray on the camera plane, from -1 to 1, with 0 being screen centre
                // so that we can use it to muliply the half-length of the camera plane to get the right direction of the ray.
                double cameraX = 2 * (i / Convert.ToDouble(width)) - 1;

                // This vector holds the direction the current ray is pointing.
                Vector rayDir = new Vector(
                    this.playerDirection.x + this.cameraPlane.x * cameraX,
                    this.playerDirection.y + this.cameraPlane.y * cameraX);

                // This holds the absolute SQUARE of the map the ray is in, regardless of position
                // within that square.
                int mapX = (int)this.PlayerPosition.x;
                int mapY = (int)this.PlayerPosition.y;

                // These two variables track the distance to the next side of a map square from the player, 
                // e.g where the ray touches the horizontal side of a square, the distance is sideDistX and vertical square sideDistY.
                double sideDistX;
                double sideDistY;

                // These two variables are the distance between map square side intersections
                double deltaDistX = Math.Abs(1 / rayDir.x);
                double deltaDistY = Math.Abs(1 / rayDir.y);

                // This var is for the overall length of the ray calculations
                double perpWallDist;

                // Each time we check the next square we step either 1 in the x or 1 in the y, they will be 1 or -1 depending on whether 
                // the character is facing towards the origin or away.
                int stepX;
                int stepY;

                // Finally, these two track whether a wall was hit, and the side tracks which side, horizontal or vertical was hit.
                // A horizontal side givess 0 and a vertical side is 1.
                bool hit = false;
                int side = 0;
                #endregion


                // Now we calculate the way we will step based upon the direction the character is facing
                // And the initial sideDist based upon this direction, and the deltaDist
                if (rayDir.x < 0)
                {
                    stepX = -1;
                    sideDistX = (this.PlayerPosition.x - mapX) * deltaDistX;
                }
                else
                {
                    stepX = 1;
                    sideDistX = (mapX + 1.0 - this.PlayerPosition.x) * deltaDistX;
                }
                if (rayDir.y < 0)
                {
                    stepY = -1;
                    sideDistY = (this.PlayerPosition.y - mapY) * deltaDistY;
                }
                else
                {
                    stepY = 1;
                    sideDistY = (mapY + 1.0 - this.PlayerPosition.y) * deltaDistY;
                }

                // Now we loop stepping until we hit a wall
                while (!hit)
                {
                    // Here we check which distance is closer to us, x or y, and increment the lesser
                    if (sideDistX < sideDistY)
                    {
                        // Increase the distance we've traveled.
                        sideDistX += deltaDistX;

                        // Set the ray's mapX to the new square we've reached.
                        mapX += stepX;

                        // Set it so the side we're currently on is an X side.
                        side = 0;
                    }
                    else
                    {
                        sideDistY += deltaDistY;
                        mapY += stepY;
                        side = 1;
                    }

                    // Check if the ray is not on the side of a square that is a wall.
                    if (this.worldMap[mapX,mapY] > 0)
                    {
                        hit = true;
                    }
                }

                // Now we've found where the next wall is, we have to find the actual distance.
                if (side == 0)
                {
                    perpWallDist = ((mapX - this.PlayerPosition.x + ((1 - stepX) / 2)) / rayDir.x);
                }
                else
                {
                    perpWallDist = ((mapY - this.PlayerPosition.y + ((1 - stepY)) / 2)) / rayDir.y;
                }

                // Here we'll start drawing the column of pixels, now we know what, and how far away.
                // First we find the height of the wall, e.g how much of the screen it should take up
                int columnHeight = (int)(height / perpWallDist);

                // Next we need to find where to start drawing the column and where to stop, since the walls
                // will be in the centre of the screen, finding the start and end is quite simple.
                int drawStart = ((height / 2) + (columnHeight / 2));

                // If we are going to be drawing off-screen, then draw just on screen.
                if (drawStart >= height)
                {
                    drawStart = height - 1;
                }
                int drawEnd = ((height / 2) - (columnHeight / 2));
                if (drawEnd < 0)
                {
                    drawEnd = 0;
                }

                // Now we pick the colour to draw the line in, this is based upon the colour of the wall
                // and is then made darker the further it is from the player.
                switch(this.worldMap[mapX, mapY])
                {
                    case 1:
                        if (side == 1)
                        {
                            this.pen.Color = Color.FromArgb(255, 0, 102, 102);
                            break;
                        }
                        else
                        {
                            this.pen.Color = Color.FromArgb(255,0,204,204);
                            break;
                        }
                        
                    case 2:
                        if (side == 1)
                        {
                            this.pen.Color = Color.FromArgb(255, 0, 127, 0);
                            break;
                        }
                        else
                        {
                            this.pen.Color = Color.FromArgb(255, 0, 255, 0);
                            break;
                        }

                    case 3:
                        if (side == 1)
                        {
                            this.pen.Color = Color.FromArgb(255, 0, 0, 127);
                            break;
                        }
                        else
                        {
                            this.pen.Color = Color.FromArgb(255, 0, 0, 255);
                            break;
                        }

                    case 4:
                        if (side == 1)
                        {
                            this.pen.Color = Color.FromArgb(255, 175, 175, 0);
                            break;
                        }
                        else
                        {
                            this.pen.Color = Color.FromArgb(255, 255, 255, 0);
                            break;
                        }
                    case 5:
                        if (side == 1)
                        {
                            this.pen.Color = Color.FromArgb(255, 175, 0, 0);
                            break;
                        }
                        else
                        {
                            this.pen.Color = Color.FromArgb(255, 255, 0, 0);
                            break;
                        }
                    default:
                        if (side == 1)
                        {
                            this.pen.Color = Color.FromArgb(255, 0, 102, 102);
                            break;
                        }
                        else
                        {
                            this.pen.Color = Color.FromArgb(255, 0, 204, 204);
                            break;
                        }

                }

                // Now we draw to the frame.
                this.DrawLine(i, drawStart, drawEnd, this.pen, bmp);
            }

            return bmp;
        }


        /// <summary>
        /// Turn the player
        /// </summary>
        /// <param name="turnRight">True for right, false for left</param>
        public void Turn(bool turnRight)
        {
            if (!turnRight)
            {
                Vector oldDirection = new Vector(this.playerDirection.x, this.playerDirection.y);
                this.playerDirection.x = (this.playerDirection.x * Math.Cos(this.rotSpeed) - this.playerDirection.y * Math.Sin(this.rotSpeed));
                this.playerDirection.y = (oldDirection.x * Math.Sin(this.rotSpeed) + this.playerDirection.y * Math.Cos(this.rotSpeed));
                Vector oldPlane = new Vector(this.cameraPlane.x, this.cameraPlane.y);
                this.cameraPlane.x = (this.cameraPlane.x * Math.Cos(this.rotSpeed) - this.cameraPlane.y * Math.Sin(this.rotSpeed));
                this.cameraPlane.y = (oldPlane.x * Math.Sin(this.rotSpeed) + this.cameraPlane.y * Math.Cos(this.rotSpeed));
            }
            else
            {
                Vector oldDirection = new Vector(this.playerDirection.x, this.playerDirection.y);
                this.playerDirection.x = (this.playerDirection.x * Math.Cos(-this.rotSpeed) - this.playerDirection.y * Math.Sin(-this.rotSpeed));
                this.playerDirection.y = (oldDirection.x * Math.Sin(-this.rotSpeed) + this.playerDirection.y * Math.Cos(-this.rotSpeed));
                Vector oldPlane = new Vector(this.cameraPlane.x, this.cameraPlane.y);
                this.cameraPlane.x = (this.cameraPlane.x * Math.Cos(-this.rotSpeed) - this.cameraPlane.y * Math.Sin(-this.rotSpeed));
                this.cameraPlane.y = (oldPlane.x * Math.Sin(-this.rotSpeed) + this.cameraPlane.y * Math.Cos(-this.rotSpeed));
            }
        }

        /// <summary>
        /// Updates the move speed and rotation speed based on frame rate
        /// </summary>
        /// <param name="frameTime">Elapsed time from the start of the frame</param>
        public void UpdateFramerate(double frameTime)
        {
            frameTime = frameTime / 1000;
            this.moveSpeed = frameTime * 5.0;
            this.rotSpeed = frameTime * 3.0;
        }


        /// <summary>
        /// Fills the frame with black
        /// </summary>
        /// <param name="frame"></param>
        private void ClearFrame(Bitmap frame)
        {
            using (Graphics g = Graphics.FromImage(frame))
            {
                SolidBrush b = new SolidBrush(Color.Black);
                g.FillRectangle(b, 0, 0, frame.Width, frame.Height);
            }
        }


        /// <summary>
        /// Draws the line of the frame
        /// </summary>
        /// <param name="x"></param>
        /// <param name="startY"></param>
        /// <param name="endY"></param>
        /// <param name="pen"></param>
        /// <param name="frame"></param>
        private void DrawLine(int x, int startY, int endY, Pen pen, Bitmap frame)
        {
            using (Graphics g = Graphics.FromImage(frame))
            {
                g.DrawLine(pen, x, startY, x, endY);
            }
        }
    }
}
