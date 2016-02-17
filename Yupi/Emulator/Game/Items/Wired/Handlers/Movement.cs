using System.Drawing;

namespace Yupi.Game.Items.Wired.Handlers
{
    internal enum MovementState
    {
        None = 0,
        Random = 1,
        LeftRight = 2,
        UpDown = 3,
        Up = 4,
        Right = 5,
        Down = 6,
        Left = 7
    }

    internal enum MovementDirection
    {
        Up = 0,
        UpRight = 1,
        Right = 2,
        DownRight = 3,
        Down = 4,
        DownLeft = 5,
        Left = 6,
        UpLeft = 7,
        Random = 8,
        None = 9
    }

    internal enum WhenMovementBlock
    {
        None = 0,
        Right45 = 1,
        Right90 = 2,
        Left45 = 3,
        Left90 = 4,
        TurnBack = 5,
        TurnRandom = 6
    }

    internal enum RotationState
    {
        None = 0,
        ClocWise = 1,
        CounterClockWise = 2,
        Random = 3
    }

    internal class Movement
    {
        private static void HandleMovement(ref Point coordinate, MovementState state)
        {
            switch (state)
            {
                case MovementState.Down:
                {
                    coordinate.Y++;
                    break;
                }

                case MovementState.Up:
                {
                    coordinate.Y--;
                    break;
                }

                case MovementState.Left:
                {
                    coordinate.X--;
                    break;
                }

                case MovementState.Right:
                {
                    coordinate.X++;
                    break;
                }
            }
        }

        private static void HandleMovementDir(ref Point coordinate, MovementDirection state)
        {
            switch (state)
            {
                case MovementDirection.Down:
                {
                    coordinate.Y++;
                    break;
                }

                case MovementDirection.Up:
                {
                    coordinate.Y--;
                    break;
                }

                case MovementDirection.Left:
                {
                    coordinate.X--;
                    break;
                }

                case MovementDirection.Right:
                {
                    coordinate.X++;
                    break;
                }

                case MovementDirection.DownRight:
                {
                    coordinate.X++;
                    coordinate.Y++;
                    break;
                }

                case MovementDirection.DownLeft:
                {
                    coordinate.X--;
                    coordinate.Y++;
                    break;
                }

                case MovementDirection.UpRight:
                {
                    coordinate.X++;
                    coordinate.Y--;
                    break;
                }

                case MovementDirection.UpLeft:
                {
                    coordinate.X--;
                    coordinate.Y--;
                    break;
                }
            }
        }

        public static Point HandleMovement(Point newCoordinate, MovementState state, int newRotation)
        {
            Point newPoint = new Point(newCoordinate.X, newCoordinate.Y);

            switch (state)
            {
                case MovementState.Up:
                case MovementState.Down:
                case MovementState.Left:
                case MovementState.Right:
                {
                    HandleMovement(ref newPoint, state);
                    break;
                }

                case MovementState.LeftRight:
                {
                    if (Yupi.GetRandomNumber(0, 2) == 1)
                        HandleMovement(ref newPoint, MovementState.Left);
                    else
                        HandleMovement(ref newPoint, MovementState.Right);
                    break;
                }

                case MovementState.UpDown:
                {
                    if (Yupi.GetRandomNumber(0, 2) == 1)
                        HandleMovement(ref newPoint, MovementState.Up);
                    else
                        HandleMovement(ref newPoint, MovementState.Down);
                    break;
                }

                case MovementState.Random:
                {
                    switch (Yupi.GetRandomNumber(1, 5))
                    {
                        case 1:
                        {
                            HandleMovement(ref newPoint, MovementState.Up);
                            break;
                        }
                        case 2:
                        {
                            HandleMovement(ref newPoint, MovementState.Down);
                            break;
                        }

                        case 3:
                        {
                            HandleMovement(ref newPoint, MovementState.Left);
                            break;
                        }
                        case 4:
                        {
                            HandleMovement(ref newPoint, MovementState.Right);
                            break;
                        }
                    }
                    break;
                }
            }

            return newPoint;
        }

        public static Point HandleMovementDir(Point newCoordinate, MovementDirection state, int newRotation)
        {
            Point newPoint = new Point(newCoordinate.X, newCoordinate.Y);

            switch (state)
            {
                case MovementDirection.Up:
                case MovementDirection.Down:
                case MovementDirection.Left:
                case MovementDirection.Right:
                case MovementDirection.DownRight:
                case MovementDirection.DownLeft:
                case MovementDirection.UpRight:
                case MovementDirection.UpLeft:
                {
                    HandleMovementDir(ref newPoint, state);
                    break;
                }

                case MovementDirection.Random:
                {
                    switch (Yupi.GetRandomNumber(1, 5))
                    {
                        case 1:
                        {
                            HandleMovementDir(ref newPoint, MovementDirection.Up);
                            break;
                        }
                        case 2:
                        {
                            HandleMovementDir(ref newPoint, MovementDirection.Down);
                            break;
                        }

                        case 3:
                        {
                            HandleMovementDir(ref newPoint, MovementDirection.Left);
                            break;
                        }
                        case 4:
                        {
                            HandleMovementDir(ref newPoint, MovementDirection.Right);
                            break;
                        }
                    }
                    break;
                }
            }

            return newPoint;
        }

        public static int HandleRotation(int oldRotation, RotationState state)
        {
            int rotation = oldRotation;
            switch (state)
            {
                case RotationState.ClocWise:
                {
                    HandleClockwiseRotation(ref rotation);
                    return rotation;
                }

                case RotationState.CounterClockWise:
                {
                    HandleCounterClockwiseRotation(ref rotation);
                    return rotation;
                }

                case RotationState.Random:
                {
                    if (Yupi.GetRandomNumber(0, 3) == 1)
                        HandleClockwiseRotation(ref rotation);
                    else
                        HandleCounterClockwiseRotation(ref rotation);
                    return rotation;
                }
            }

            return rotation;
        }

        private static void HandleClockwiseRotation(ref int rotation)
        {
            rotation = rotation + 2;
            if (rotation > 6)
                rotation = 0;
        }

        private static void HandleCounterClockwiseRotation(ref int rotation)
        {
            rotation = rotation - 2;
            if (rotation < 0)
                rotation = 6;
        }
    }
}