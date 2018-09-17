using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace CS_Raycaster
{
    using System.Diagnostics;

    public class Raycaster
    {
        // Creating the world map.
       readonly int[,] worldMap = new int[,]
        {
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,2,2,2,2,2,0,0,0,0,3,0,3,0,3,0,0,0,1},
            {1,0,0,0,0,0,2,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,0,0,0,0,2,0,0,0,2,0,0,0,0,3,0,0,0,3,0,0,0,1},
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

        // Player's position in the map.
        Vector playerPosition = new Vector(12, 12);
        // Player's direction vector.
        Vector playerDirection = new Vector(-1, 0);
        // Vector for the width of the camera plane.
        Vector cameraPlane = new Vector(0, 1);
        // These are for the moving and turning of the player
        private double moveSpeed;
        private double rotSpeed;

        // This is used to keep track of the current frames per second.
        private double framesPerSecond = 0;
        // Keeps track of how many frames have been drawn since last fps check.
        private int framesRendered;
        // Keeps track of the last time fps was checked.
        private DateTime lastTime;

        // This pen will be used to draw the pixels for each frame.
        Pen pen = new Pen(Color.White,1);


        /// <summary>
        /// Makes the given frame black.
        /// </summary>
        /// <param name="frame">Frame to clear</param>
        private void ClearFrame(Bitmap frame)
        {
            using (Graphics g = Graphics.FromImage(frame))
            {
                SolidBrush b = new SolidBrush(Color.Black);
                g.FillRectangle(b, 0, 0, frame.Width, frame.Height);
            }
        }

        /// <summary>
        /// Draws a vertical line to the next frame to be drawn.
        /// </summary>
        /// <param name="x">Line X position</param>
        /// <param name="startY">Line starting Y position</param>
        /// <param name="endY">Line ending Y position</param>
        /// <param name="pen">Pen to be used to draw</param>
        /// <param name="frame">Frame to draw on</param>
        private void DrawLine(int x, int startY, int endY, Pen pen, Bitmap frame)
        {
            using (Graphics g = Graphics.FromImage(frame))
            {
                g.DrawLine(pen, x, startY, x, endY);
            }
        }

        /// <summary>
        /// Adds text to the given frame.
        /// </summary>
        /// <param name="x">Text X position</param>
        /// <param name="y">Text Y position</param>
        /// <param name="size">Text size</param>
        /// <param name="text">Text to be drawn</param>
        /// <param name="color">Text color</param>
        /// <param name="frame">Frame to add to</param>
        private void AddTextToFrame(int x, int y, int size, string text, Color color, Bitmap frame)
        {
            using (Graphics g = Graphics.FromImage(frame))
            {
                SolidBrush b = new SolidBrush(color);
                Font f = new Font("Consolas", size);
                g.DrawString(text, f, b, x, y);
            }
        }

        #region FrameDrawing
        public Image NewFrame(int width,int height)
        {
            // First we create the frame we'll be drawing to.
            Bitmap bmp = new Bitmap(width, height);
            ClearFrame(bmp);

            for (int i = 0; i < width;i++)
            {
                // This var tracks the relative position of the ray on the camera plane, from -1 to 1, with 0 being screen centre
                // so that we can use it to muliply the half-length of the camera plane to get the right direction of the ray.
                double cameraX = 2 * (i / Convert.ToDouble(width)) - 1;
                // This vector holds the direction the current ray is pointing.
                Vector rayDir = new Vector(
                    playerDirection.x + cameraPlane.x * cameraX,
                    playerDirection.y + cameraPlane.y * cameraX);

                // This holds the absolute SQUARE of the map the ray is in, regardless of position
                // within that square.
                int mapX = (int)playerPosition.x;
                int mapY = (int)playerPosition.y;
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

                // Now we calculate the way we will step based upon the direction the character is facing
                // And the initial sideDist based upon this direction, and the deltaDist
                if (rayDir.x < 0)
                {
                    stepX = -1;
                    sideDistX = (playerPosition.x - mapX) * deltaDistX;
                }
                else
                {
                    stepX = 1;
                    sideDistX = (mapX + 1.0 - playerPosition.x) * deltaDistX;
                }
                if (rayDir.y < 0)
                {
                    stepY = -1;
                    sideDistY = (playerPosition.y - mapY) * deltaDistY;
                }
                else
                {
                    stepY = 1;
                    sideDistY = (mapY + 1.0 - playerPosition.y) * deltaDistY;
                }

                // Now we loop steping until we hit a wall
                while (!hit)
                {
                    // Here we check which distance is closer to us, x or y, and increment the lesser
                    if (sideDistX < sideDistY)
                    {
                        // Increase the distance we've travelled.
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
                    if (worldMap[mapX,mapY] > 0)
                    {
                        hit = true;
                    }
                }

                // Now we've found where the next wall is, we have to find the actual distance.
                if (side == 0)
                {
                    perpWallDist = ((mapX - playerPosition.x + ((1 - stepX) / 2)) / rayDir.x);
                }
                else
                {
                    perpWallDist = ((mapY - playerPosition.y + ((1 - stepY)) / 2)) / rayDir.y;
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
                // and is then made darker if the wall is x aligned or y aligned.
                switch(worldMap[mapX, mapY])
                {
                    case 1:
                        if (side == 1)
                        {
                            pen.Color = Color.FromArgb(255, 0, 102, 102);
                            break;
                        }
                        else
                        {
                            pen.Color = Color.FromArgb(255,0,204,204);
                            break;
                        }
                        
                    case 2:
                        if (side == 1)
                        {
                            pen.Color = Color.FromArgb(255, 0, 127, 0);
                            break;
                        }
                        else
                        {
                            pen.Color = Color.FromArgb(255, 0, 255, 0);
                            break;
                        }

                    case 3:
                        if (side == 1)
                        {
                            pen.Color = Color.FromArgb(255, 0, 0, 127);
                            break;
                        }
                        else
                        {
                            pen.Color = Color.FromArgb(255, 0, 0, 255);
                            break;
                        }

                    case 4:
                        if (side == 1)
                        {
                            pen.Color = Color.FromArgb(255, 175, 175, 0);
                            break;
                        }
                        else
                        {
                            pen.Color = Color.FromArgb(255, 255, 255, 0);
                            break;
                        }
                    case 5:
                        if (side == 1)
                        {
                            pen.Color = Color.FromArgb(255, 175, 175, 0);
                            break;
                        }
                        else
                        {
                            pen.Color = Color.FromArgb(255, 255, 255, 0);
                            break;
                        }

                }

                // Now we draw the line to the frame.
                DrawLine(i, drawStart, drawEnd, pen, bmp);
            }
            // Finally we add any text we want to the frame now its all drawn.
            AddTextToFrame(10, 10, 12, framesPerSecond.ToString(), Color.White, bmp);

            return bmp;
        }
#endregion

        public void Turn(bool turnRight)
        {
            if (!turnRight)
            {
                // We use a rotation matrix to rotate the plane and direction vectors.
                // First we keep track of the old direction, so that the transformation on X first
                // doesn't affect the Y transformation.
                Vector oldDirection = new Vector(playerDirection.x, playerDirection.y);
                playerDirection.x = (playerDirection.x * Math.Cos(rotSpeed) - playerDirection.y * Math.Sin(rotSpeed));
                playerDirection.y = (oldDirection.x * Math.Sin(rotSpeed) + playerDirection.y * Math.Cos(rotSpeed));
                Vector oldPlane = new Vector(cameraPlane.x, cameraPlane.y);
                cameraPlane.x = (cameraPlane.x * Math.Cos(rotSpeed) - cameraPlane.y * Math.Sin(rotSpeed));
                cameraPlane.y = (oldPlane.x * Math.Sin(rotSpeed) + cameraPlane.y * Math.Cos(rotSpeed));
            }
            else
            {
                Vector oldDirection = new Vector(playerDirection.x, playerDirection.y);
                playerDirection.x = (playerDirection.x * Math.Cos(-rotSpeed) - playerDirection.y * Math.Sin(-rotSpeed));
                playerDirection.y = (oldDirection.x * Math.Sin(-rotSpeed) + playerDirection.y * Math.Cos(-rotSpeed));
                Vector oldPlane = new Vector(cameraPlane.x, cameraPlane.y);
                cameraPlane.x = (cameraPlane.x * Math.Cos(-rotSpeed) - cameraPlane.y * Math.Sin(-rotSpeed));
                cameraPlane.y = (oldPlane.x * Math.Sin(-rotSpeed) + cameraPlane.y * Math.Cos(-rotSpeed));
            }
        }

        public void Move(bool forwards)
        {
            Debug.WriteLine("Moving");
            Debug.WriteLine(moveSpeed);
            if (forwards)
            {
                // First we check that moving wont put us in a wall
                if (worldMap[(int)(playerPosition.x+playerDirection.x*moveSpeed),(int)(playerPosition.y)] == 0)
                {
                    // If it doesnt put us in a wall, we can move forwards (or backwards).
                    playerPosition.x += playerDirection.x * moveSpeed;
                }
                if (worldMap[(int)(playerPosition.x),(int)(playerPosition.y + playerDirection.y * moveSpeed)] == 0 )
                {
                    playerPosition.y += playerDirection.y * moveSpeed;
                }
            }
            else
            {
                if (worldMap[(int)(playerPosition.x - playerDirection.x * moveSpeed), (int)(playerPosition.y)] == 0)
                {
                    playerPosition.x -= playerDirection.x * moveSpeed;
                }
                if (worldMap[(int)(playerPosition.x), (int)(playerPosition.y - playerDirection.y * moveSpeed)] == 0)
                {
                    playerPosition.y -= playerDirection.y * moveSpeed;
                }
            }
        }


        public void UpdateFramerate(double frameTime)
        {
            // Add one to the count of frames rendered.
            framesRendered++;
            // If a second has elapsed, we can update the fps.
            if ((DateTime.Now - lastTime).TotalSeconds >= 1)
            {
                lastTime = DateTime.Now;
                framesPerSecond = framesRendered;
                framesRendered = 0;
            }

            // The frameTime is the time between 2 frames, it is used to keep speed constant regardless of the 
            // framerate that the application is running at.
            frameTime = frameTime/1000;
            // Each frame we update the speed based on how long the frames are taking.
            moveSpeed = frameTime * 5.0;
            rotSpeed = frameTime * 3.0;
        }
        





    }
}
