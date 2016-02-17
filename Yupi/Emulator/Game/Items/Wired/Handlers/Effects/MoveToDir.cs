using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Yupi.Game.Items.Interactions.Enums;
using Yupi.Game.Items.Interfaces;
using Yupi.Game.Items.Wired.Interfaces;
using Yupi.Game.Rooms;

namespace Yupi.Game.Items.Wired.Handlers.Effects
{
    internal class MoveToDir : IWiredItem
    {
        private readonly ConcurrentQueue<RoomItem> _toRemove = new ConcurrentQueue<RoomItem>();
        private bool _needChange;
        private MovementDirection _startDirection;
        private WhenMovementBlock _whenMoveIsBlocked;

        public MoveToDir(RoomItem item, Room room)
        {
            Item = item;
            Room = room;
            Items = new List<RoomItem>();
            Delay = 0;
        }

        public int StartDirection => (int) _startDirection;

        public int WhenMoveIsBlocked => (int) _whenMoveIsBlocked;

        public Queue ToWork
        {
            get { return null; }
            set { }
        }

        public Interaction Type => Interaction.ActionMoveToDir;

        public RoomItem Item { get; set; }

        public Room Room { get; set; }

        public List<RoomItem> Items { get; set; }

        public string OtherString
        {
            get { return $"{StartDirection};{WhenMoveIsBlocked}"; }
            set
            {
                string[] array = value.Split(';');

                if (array.Length != 2)
                {
                    _startDirection = MovementDirection.None;
                    _whenMoveIsBlocked = WhenMovementBlock.None;
                    return;
                }

                _startDirection = (MovementDirection) int.Parse(array[0]);
                _whenMoveIsBlocked = (WhenMovementBlock) int.Parse(array[1]);
            }
        }

        public string OtherExtraString
        {
            get { return string.Empty; }
            set { }
        }

        public string OtherExtraString2
        {
            get { return string.Empty; }
            set { }
        }

        public bool OtherBool
        {
            get { return true; }
            set { }
        }

        public int Delay { get; set; }

        public bool Execute(params object[] stuff)
        {
            if (!Items.Any())
                return true;

            foreach (RoomItem item in Items)
            {
                if (item == null || Room.GetRoomItemHandler().GetItem(item.Id) == null)
                {
                    _toRemove.Enqueue(item);
                    continue;
                }

                HandleMovement(item);
            }

            RoomItem rI;

            while (_toRemove.TryDequeue(out rI))
                if (Items.Contains(rI))
                    Items.Remove(rI);

            return true;
        }

        private void HandleMovement(RoomItem item)
        {
            if (item.MoveToDirMovement == MovementDirection.None || _needChange)
            {
                item.MoveToDirMovement = _startDirection;
                _needChange = false;
            }

            Point newPoint = Movement.HandleMovementDir(item.Coordinate, item.MoveToDirMovement, item.Rot);

            if (newPoint == item.Coordinate)
                return;

            if (Room.GetGameMap().SquareIsOpen(newPoint.X, newPoint.Y, false))
            {
                Room.GetRoomItemHandler()
                    .SetFloorItem(null, item, newPoint.X, newPoint.Y, item.Rot, false, false, true, false, true);
            }
            else
            {
                switch (_whenMoveIsBlocked)
                {
                    case WhenMovementBlock.None:
                    {
                        item.MoveToDirMovement = MovementDirection.None;
                        break;
                    }

                    case WhenMovementBlock.Right45:
                    {
                        if (item.MoveToDirMovement == MovementDirection.Right)
                        {
                            if (Room.GetGameMap().IsValidValueItem(item.X + 1, item.Y + 1)) // abajo derecha
                                item.MoveToDirMovement = MovementDirection.DownRight;
                            if (Room.GetGameMap().IsValidValueItem(item.X, item.Y + 1)) // abajo
                                item.MoveToDirMovement = MovementDirection.Down;
                            if (Room.GetGameMap().IsValidValueItem(item.X - 1, item.Y + 1)) // abajo izq
                                item.MoveToDirMovement = MovementDirection.DownLeft;
                            if (Room.GetGameMap().IsValidValueItem(item.X - 1, item.Y)) // izq
                                item.MoveToDirMovement = MovementDirection.Left;
                            if (Room.GetGameMap().IsValidValueItem(item.X - 1, item.Y - 1)) // arriba izq
                                item.MoveToDirMovement = MovementDirection.UpLeft;
                            if (Room.GetGameMap().IsValidValueItem(item.X, item.Y - 1)) // arriba
                                item.MoveToDirMovement = MovementDirection.Up;
                            if (Room.GetGameMap().IsValidValueItem(item.X + 1, item.Y - 1)) // arriba derecha
                                item.MoveToDirMovement = MovementDirection.UpRight;
                            if (Room.GetGameMap().IsValidValueItem(item.X + 1, item.Y)) // derecha
                                item.MoveToDirMovement = MovementDirection.Right;
                        }
                        else if (item.MoveToDirMovement == MovementDirection.Left)
                        {
                            if (Room.GetGameMap().IsValidValueItem(item.X - 1, item.Y - 1)) // arriba izq
                                item.MoveToDirMovement = MovementDirection.UpLeft;
                            if (Room.GetGameMap().IsValidValueItem(item.X, item.Y - 1)) // arriba
                                item.MoveToDirMovement = MovementDirection.Up;
                            if (Room.GetGameMap().IsValidValueItem(item.X + 1, item.Y - 1)) // arriba derecha
                                item.MoveToDirMovement = MovementDirection.UpRight;
                            if (Room.GetGameMap().IsValidValueItem(item.X + 1, item.Y)) // derecha
                                item.MoveToDirMovement = MovementDirection.Right;
                            if (Room.GetGameMap().IsValidValueItem(item.X + 1, item.Y + 1)) // abajo derecha
                                item.MoveToDirMovement = MovementDirection.DownRight;
                            if (Room.GetGameMap().IsValidValueItem(item.X, item.Y + 1)) // abajo
                                item.MoveToDirMovement = MovementDirection.Down;
                            if (Room.GetGameMap().IsValidValueItem(item.X - 1, item.Y + 1)) // abajo izq
                                item.MoveToDirMovement = MovementDirection.DownLeft;
                            if (Room.GetGameMap().IsValidValueItem(item.X - 1, item.Y)) // izq
                                item.MoveToDirMovement = MovementDirection.Left;
                        }
                        else if (item.MoveToDirMovement == MovementDirection.Up)
                        {
                            if (Room.GetGameMap().IsValidValueItem(item.X + 1, item.Y - 1)) // arriba derecha
                                item.MoveToDirMovement = MovementDirection.UpRight;
                            if (Room.GetGameMap().IsValidValueItem(item.X + 1, item.Y)) // derecha
                                item.MoveToDirMovement = MovementDirection.Right;
                            if (Room.GetGameMap().IsValidValueItem(item.X + 1, item.Y + 1)) // abajo derecha
                                item.MoveToDirMovement = MovementDirection.DownRight;
                            if (Room.GetGameMap().IsValidValueItem(item.X, item.Y + 1)) // abajo
                                item.MoveToDirMovement = MovementDirection.Down;
                            if (Room.GetGameMap().IsValidValueItem(item.X - 1, item.Y + 1)) // abajo izq
                                item.MoveToDirMovement = MovementDirection.DownLeft;
                            if (Room.GetGameMap().IsValidValueItem(item.X - 1, item.Y)) // izq
                                item.MoveToDirMovement = MovementDirection.Left;
                            if (Room.GetGameMap().IsValidValueItem(item.X - 1, item.Y - 1)) // arriba izq
                                item.MoveToDirMovement = MovementDirection.UpLeft;
                            if (Room.GetGameMap().IsValidValueItem(item.X, item.Y - 1)) // arriba
                                item.MoveToDirMovement = MovementDirection.Up;
                        }
                        else if (item.MoveToDirMovement == MovementDirection.Down)
                        {
                            if (Room.GetGameMap().IsValidValueItem(item.X - 1, item.Y + 1)) // abajo izq
                                item.MoveToDirMovement = MovementDirection.DownLeft;
                            if (Room.GetGameMap().IsValidValueItem(item.X - 1, item.Y)) // izq
                                item.MoveToDirMovement = MovementDirection.Left;
                            if (Room.GetGameMap().IsValidValueItem(item.X - 1, item.Y - 1)) // arriba izq
                                item.MoveToDirMovement = MovementDirection.UpLeft;
                            if (Room.GetGameMap().IsValidValueItem(item.X, item.Y - 1)) // arriba
                                item.MoveToDirMovement = MovementDirection.Up;
                            if (Room.GetGameMap().IsValidValueItem(item.X + 1, item.Y - 1)) // arriba derecha
                                item.MoveToDirMovement = MovementDirection.UpRight;
                            if (Room.GetGameMap().IsValidValueItem(item.X + 1, item.Y)) // derecha
                                item.MoveToDirMovement = MovementDirection.Right;
                            if (Room.GetGameMap().IsValidValueItem(item.X + 1, item.Y + 1)) // abajo derecha
                                item.MoveToDirMovement = MovementDirection.DownRight;
                            if (Room.GetGameMap().IsValidValueItem(item.X, item.Y + 1)) // abajo
                                item.MoveToDirMovement = MovementDirection.Down;
                        }
                        else if (item.MoveToDirMovement == MovementDirection.UpLeft)
                        {
                            if (Room.GetGameMap().IsValidValueItem(item.X, item.Y - 1)) // arriba
                                item.MoveToDirMovement = MovementDirection.Up;
                            if (Room.GetGameMap().IsValidValueItem(item.X + 1, item.Y - 1)) // arriba derecha
                                item.MoveToDirMovement = MovementDirection.UpRight;
                            if (Room.GetGameMap().IsValidValueItem(item.X + 1, item.Y)) // derecha
                                item.MoveToDirMovement = MovementDirection.Right;
                            if (Room.GetGameMap().IsValidValueItem(item.X + 1, item.Y + 1)) // abajo derecha
                                item.MoveToDirMovement = MovementDirection.DownRight;
                            if (Room.GetGameMap().IsValidValueItem(item.X, item.Y + 1)) // abajo
                                item.MoveToDirMovement = MovementDirection.Down;
                            if (Room.GetGameMap().IsValidValueItem(item.X - 1, item.Y + 1)) // abajo izq
                                item.MoveToDirMovement = MovementDirection.DownLeft;
                            if (Room.GetGameMap().IsValidValueItem(item.X - 1, item.Y)) // izq
                                item.MoveToDirMovement = MovementDirection.Left;
                            if (Room.GetGameMap().IsValidValueItem(item.X - 1, item.Y - 1)) // arriba izq
                                item.MoveToDirMovement = MovementDirection.UpLeft;
                        }
                        else if (item.MoveToDirMovement == MovementDirection.UpRight)
                        {
                            if (Room.GetGameMap().IsValidValueItem(item.X + 1, item.Y)) // derecha
                            {
                                item.MoveToDirMovement = MovementDirection.Right;
                                break;
                            }
                            if (Room.GetGameMap().IsValidValueItem(item.X + 1, item.Y + 1)) // abajo derecha
                            {
                                item.MoveToDirMovement = MovementDirection.DownRight;
                                break;
                            }
                            if (Room.GetGameMap().IsValidValueItem(item.X, item.Y + 1)) // abajo
                            {
                                item.MoveToDirMovement = MovementDirection.Down;
                                break;
                            }
                            if (Room.GetGameMap().IsValidValueItem(item.X - 1, item.Y + 1)) // abajo izq
                            {
                                item.MoveToDirMovement = MovementDirection.DownLeft;
                                break;
                            }
                            if (Room.GetGameMap().IsValidValueItem(item.X - 1, item.Y)) // izq
                            {
                                item.MoveToDirMovement = MovementDirection.Left;
                                break;
                            }
                            if (Room.GetGameMap().IsValidValueItem(item.X - 1, item.Y - 1)) // arriba izq
                            {
                                item.MoveToDirMovement = MovementDirection.UpLeft;
                                break;
                            }
                            if (Room.GetGameMap().IsValidValueItem(item.X, item.Y - 1)) // arriba
                            {
                                item.MoveToDirMovement = MovementDirection.Up;
                                break;
                            }
                            if (Room.GetGameMap().IsValidValueItem(item.X + 1, item.Y - 1)) // arriba derecha
                            {
                                item.MoveToDirMovement = MovementDirection.UpRight;
                                break;
                            }
                            return;
                        }
                        else if (item.MoveToDirMovement == MovementDirection.DownRight)
                        {
                            if (Room.GetGameMap().IsValidValueItem(item.X, item.Y + 1)) // abajo
                                item.MoveToDirMovement = MovementDirection.Down;
                            if (Room.GetGameMap().IsValidValueItem(item.X - 1, item.Y + 1)) // abajo izq
                                item.MoveToDirMovement = MovementDirection.DownLeft;
                            if (Room.GetGameMap().IsValidValueItem(item.X - 1, item.Y)) // izq
                                item.MoveToDirMovement = MovementDirection.Left;
                            if (Room.GetGameMap().IsValidValueItem(item.X - 1, item.Y - 1)) // arriba izq
                                item.MoveToDirMovement = MovementDirection.UpLeft;
                            if (Room.GetGameMap().IsValidValueItem(item.X, item.Y - 1)) // arriba
                                item.MoveToDirMovement = MovementDirection.Up;
                            if (Room.GetGameMap().IsValidValueItem(item.X + 1, item.Y - 1)) // arriba derecha
                                item.MoveToDirMovement = MovementDirection.UpRight;
                            if (Room.GetGameMap().IsValidValueItem(item.X + 1, item.Y)) // derecha
                                item.MoveToDirMovement = MovementDirection.Right;
                            if (Room.GetGameMap().IsValidValueItem(item.X + 1, item.Y + 1)) // abajo derecha
                                item.MoveToDirMovement = MovementDirection.DownRight;
                        }
                        else if (item.MoveToDirMovement == MovementDirection.DownLeft)
                        {
                            if (Room.GetGameMap().IsValidValueItem(item.X - 1, item.Y)) // izq
                                item.MoveToDirMovement = MovementDirection.Left;
                            if (Room.GetGameMap().IsValidValueItem(item.X - 1, item.Y - 1)) // arriba izq
                                item.MoveToDirMovement = MovementDirection.UpLeft;
                            if (Room.GetGameMap().IsValidValueItem(item.X, item.Y - 1)) // arriba
                                item.MoveToDirMovement = MovementDirection.Up;
                            if (Room.GetGameMap().IsValidValueItem(item.X + 1, item.Y - 1)) // arriba derecha
                                item.MoveToDirMovement = MovementDirection.UpRight;
                            if (Room.GetGameMap().IsValidValueItem(item.X + 1, item.Y)) // derecha
                                item.MoveToDirMovement = MovementDirection.Right;
                            if (Room.GetGameMap().IsValidValueItem(item.X + 1, item.Y + 1)) // abajo derecha
                                item.MoveToDirMovement = MovementDirection.DownRight;
                            if (Room.GetGameMap().IsValidValueItem(item.X, item.Y + 1)) // abajo
                                item.MoveToDirMovement = MovementDirection.Down;
                            if (Room.GetGameMap().IsValidValueItem(item.X - 1, item.Y + 1)) // abajo izq
                                item.MoveToDirMovement = MovementDirection.DownLeft;
                        }

                        break;
                    }

                    case WhenMovementBlock.Right90:
                    {
                        if (item.MoveToDirMovement == MovementDirection.Right)
                        {
                            if (Room.GetGameMap().IsValidValueItem(item.X, item.Y + 1)) // abajo
                                item.MoveToDirMovement = MovementDirection.Down;
                            if (Room.GetGameMap().IsValidValueItem(item.X - 1, item.Y)) // izq
                                item.MoveToDirMovement = MovementDirection.Left;
                            if (Room.GetGameMap().IsValidValueItem(item.X, item.Y - 1)) // arriba
                                item.MoveToDirMovement = MovementDirection.Up;
                            if (Room.GetGameMap().IsValidValueItem(item.X + 1, item.Y)) // derecha
                                item.MoveToDirMovement = MovementDirection.Right;
                        }
                        else if (item.MoveToDirMovement == MovementDirection.Left)
                        {
                            if (Room.GetGameMap().IsValidValueItem(item.X, item.Y - 1)) // arriba
                                item.MoveToDirMovement = MovementDirection.Up;
                            if (Room.GetGameMap().IsValidValueItem(item.X + 1, item.Y)) // derecha
                                item.MoveToDirMovement = MovementDirection.Right;
                            if (Room.GetGameMap().IsValidValueItem(item.X, item.Y + 1)) // abajo
                                item.MoveToDirMovement = MovementDirection.Down;
                            if (Room.GetGameMap().IsValidValueItem(item.X - 1, item.Y)) // izq
                                item.MoveToDirMovement = MovementDirection.Left;
                        }
                        else if (item.MoveToDirMovement == MovementDirection.Up)
                        {
                            if (Room.GetGameMap().IsValidValueItem(item.X + 1, item.Y)) // derecha
                                item.MoveToDirMovement = MovementDirection.Right;
                            if (Room.GetGameMap().IsValidValueItem(item.X, item.Y + 1)) // abajo
                                item.MoveToDirMovement = MovementDirection.Down;
                            if (Room.GetGameMap().IsValidValueItem(item.X - 1, item.Y)) // izq
                                item.MoveToDirMovement = MovementDirection.Left;
                            if (Room.GetGameMap().IsValidValueItem(item.X, item.Y - 1)) // arriba
                                item.MoveToDirMovement = MovementDirection.Up;
                        }
                        else if (item.MoveToDirMovement == MovementDirection.Down)
                        {
                            if (Room.GetGameMap().IsValidValueItem(item.X - 1, item.Y)) // izq
                                item.MoveToDirMovement = MovementDirection.Left;
                            if (Room.GetGameMap().IsValidValueItem(item.X, item.Y - 1)) // arriba
                                item.MoveToDirMovement = MovementDirection.Up;
                            if (Room.GetGameMap().IsValidValueItem(item.X + 1, item.Y)) // derecha
                                item.MoveToDirMovement = MovementDirection.Right;
                            if (Room.GetGameMap().IsValidValueItem(item.X, item.Y + 1)) // abajo
                                item.MoveToDirMovement = MovementDirection.Down;
                        }
                        else if (item.MoveToDirMovement == MovementDirection.UpLeft)
                        {
                            if (Room.GetGameMap().IsValidValueItem(item.X + 1, item.Y - 1)) // arriba derecha
                                item.MoveToDirMovement = MovementDirection.UpRight;
                            if (Room.GetGameMap().IsValidValueItem(item.X + 1, item.Y + 1)) // abajo derecha
                                item.MoveToDirMovement = MovementDirection.DownRight;
                            if (Room.GetGameMap().IsValidValueItem(item.X - 1, item.Y + 1)) // abajo izq
                                item.MoveToDirMovement = MovementDirection.DownLeft;
                            if (Room.GetGameMap().IsValidValueItem(item.X - 1, item.Y - 1)) // arriba izq
                                item.MoveToDirMovement = MovementDirection.UpLeft;
                        }
                        else if (item.MoveToDirMovement == MovementDirection.UpRight)
                        {
                            if (Room.GetGameMap().IsValidValueItem(item.X + 1, item.Y + 1)) // abajo derecha
                            {
                                item.MoveToDirMovement = MovementDirection.DownRight;
                                break;
                            }
                            if (Room.GetGameMap().IsValidValueItem(item.X - 1, item.Y + 1)) // abajo izq
                            {
                                item.MoveToDirMovement = MovementDirection.DownLeft;
                                break;
                            }
                            if (Room.GetGameMap().IsValidValueItem(item.X - 1, item.Y - 1)) // arriba izq
                            {
                                item.MoveToDirMovement = MovementDirection.UpLeft;
                                break;
                            }
                            if (Room.GetGameMap().IsValidValueItem(item.X + 1, item.Y - 1)) // arriba derecha
                            {
                                item.MoveToDirMovement = MovementDirection.UpRight;
                                break;
                            }
                            return;
                        }
                        else if (item.MoveToDirMovement == MovementDirection.DownRight)
                        {
                            if (Room.GetGameMap().IsValidValueItem(item.X - 1, item.Y + 1)) // abajo izq
                                item.MoveToDirMovement = MovementDirection.DownLeft;
                            if (Room.GetGameMap().IsValidValueItem(item.X - 1, item.Y - 1)) // arriba izq
                                item.MoveToDirMovement = MovementDirection.UpLeft;
                            if (Room.GetGameMap().IsValidValueItem(item.X + 1, item.Y - 1)) // arriba derecha
                                item.MoveToDirMovement = MovementDirection.UpRight;
                            if (Room.GetGameMap().IsValidValueItem(item.X + 1, item.Y + 1)) // abajo derecha
                                item.MoveToDirMovement = MovementDirection.DownRight;
                        }
                        else if (item.MoveToDirMovement == MovementDirection.DownLeft)
                        {
                            if (Room.GetGameMap().IsValidValueItem(item.X - 1, item.Y - 1)) // arriba izq
                                item.MoveToDirMovement = MovementDirection.UpLeft;
                            if (Room.GetGameMap().IsValidValueItem(item.X + 1, item.Y - 1)) // arriba derecha
                                item.MoveToDirMovement = MovementDirection.UpRight;
                            if (Room.GetGameMap().IsValidValueItem(item.X + 1, item.Y + 1)) // abajo derecha
                                item.MoveToDirMovement = MovementDirection.DownRight;
                            if (Room.GetGameMap().IsValidValueItem(item.X - 1, item.Y + 1)) // abajo izq
                                item.MoveToDirMovement = MovementDirection.DownLeft;
                        }

                        break;
                    }

                    case WhenMovementBlock.Left45:
                    {
                        if (item.MoveToDirMovement == MovementDirection.Right)
                        {
                            if (Room.GetGameMap().IsValidValueItem(item.X + 1, item.Y - 1)) // arriba derecha
                                item.MoveToDirMovement = MovementDirection.UpRight;
                            if (Room.GetGameMap().IsValidValueItem(item.X, item.Y - 1)) // arriba
                                item.MoveToDirMovement = MovementDirection.Up;
                            if (Room.GetGameMap().IsValidValueItem(item.X - 1, item.Y - 1)) // arriba izq
                                item.MoveToDirMovement = MovementDirection.UpLeft;
                            if (Room.GetGameMap().IsValidValueItem(item.X - 1, item.Y)) // izq
                                item.MoveToDirMovement = MovementDirection.Left;
                            if (Room.GetGameMap().IsValidValueItem(item.X - 1, item.Y + 1)) // abajo izq
                                item.MoveToDirMovement = MovementDirection.DownLeft;
                            if (Room.GetGameMap().IsValidValueItem(item.X, item.Y + 1)) // abajo
                                item.MoveToDirMovement = MovementDirection.Down;
                            if (Room.GetGameMap().IsValidValueItem(item.X + 1, item.Y + 1)) // abajo derecha
                                item.MoveToDirMovement = MovementDirection.DownRight;
                            if (Room.GetGameMap().IsValidValueItem(item.X + 1, item.Y)) // derecha
                                item.MoveToDirMovement = MovementDirection.Right;
                        }
                        else if (item.MoveToDirMovement == MovementDirection.Left)
                        {
                            if (Room.GetGameMap().IsValidValueItem(item.X - 1, item.Y + 1)) // abajo izq
                                item.MoveToDirMovement = MovementDirection.DownLeft;
                            if (Room.GetGameMap().IsValidValueItem(item.X, item.Y + 1)) // abajo
                                item.MoveToDirMovement = MovementDirection.Down;
                            if (Room.GetGameMap().IsValidValueItem(item.X + 1, item.Y + 1)) // abajo derecha
                                item.MoveToDirMovement = MovementDirection.DownRight;
                            if (Room.GetGameMap().IsValidValueItem(item.X + 1, item.Y)) // derecha
                                item.MoveToDirMovement = MovementDirection.Right;
                            if (Room.GetGameMap().IsValidValueItem(item.X + 1, item.Y - 1)) // arriba derecha
                                item.MoveToDirMovement = MovementDirection.UpRight;
                            if (Room.GetGameMap().IsValidValueItem(item.X, item.Y - 1)) // arriba
                                item.MoveToDirMovement = MovementDirection.Up;
                            if (Room.GetGameMap().IsValidValueItem(item.X - 1, item.Y - 1)) // arriba izq
                                item.MoveToDirMovement = MovementDirection.UpLeft;
                            if (Room.GetGameMap().IsValidValueItem(item.X - 1, item.Y)) // izq
                                item.MoveToDirMovement = MovementDirection.Left;
                        }
                        else if (item.MoveToDirMovement == MovementDirection.Up)
                        {
                            if (Room.GetGameMap().IsValidValueItem(item.X - 1, item.Y - 1)) // arriba izq
                                item.MoveToDirMovement = MovementDirection.UpLeft;
                            if (Room.GetGameMap().IsValidValueItem(item.X - 1, item.Y)) // izq
                                item.MoveToDirMovement = MovementDirection.Left;
                            if (Room.GetGameMap().IsValidValueItem(item.X - 1, item.Y + 1)) // abajo izq
                                item.MoveToDirMovement = MovementDirection.DownLeft;
                            if (Room.GetGameMap().IsValidValueItem(item.X, item.Y + 1)) // abajo
                                item.MoveToDirMovement = MovementDirection.Down;
                            if (Room.GetGameMap().IsValidValueItem(item.X + 1, item.Y + 1)) // abajo derecha
                                item.MoveToDirMovement = MovementDirection.DownRight;
                            if (Room.GetGameMap().IsValidValueItem(item.X + 1, item.Y)) // derecha
                                item.MoveToDirMovement = MovementDirection.Right;
                            if (Room.GetGameMap().IsValidValueItem(item.X + 1, item.Y - 1)) // arriba derecha
                                item.MoveToDirMovement = MovementDirection.UpRight;
                            if (Room.GetGameMap().IsValidValueItem(item.X, item.Y - 1)) // arriba
                                item.MoveToDirMovement = MovementDirection.Up;
                        }
                        else if (item.MoveToDirMovement == MovementDirection.Down)
                        {
                            if (Room.GetGameMap().IsValidValueItem(item.X + 1, item.Y + 1)) // abajo derecha
                                item.MoveToDirMovement = MovementDirection.DownRight;
                            if (Room.GetGameMap().IsValidValueItem(item.X + 1, item.Y)) // derecha
                                item.MoveToDirMovement = MovementDirection.Right;
                            if (Room.GetGameMap().IsValidValueItem(item.X + 1, item.Y - 1)) // arriba derecha
                                item.MoveToDirMovement = MovementDirection.UpRight;
                            if (Room.GetGameMap().IsValidValueItem(item.X, item.Y - 1)) // arriba
                                item.MoveToDirMovement = MovementDirection.Up;
                            if (Room.GetGameMap().IsValidValueItem(item.X - 1, item.Y - 1)) // arriba izq
                                item.MoveToDirMovement = MovementDirection.UpLeft;
                            if (Room.GetGameMap().IsValidValueItem(item.X - 1, item.Y)) // izq
                                item.MoveToDirMovement = MovementDirection.Left;
                            if (Room.GetGameMap().IsValidValueItem(item.X - 1, item.Y + 1)) // abajo izq
                                item.MoveToDirMovement = MovementDirection.DownLeft;
                            if (Room.GetGameMap().IsValidValueItem(item.X, item.Y + 1)) // abajo
                                item.MoveToDirMovement = MovementDirection.Down;
                        }
                        else if (item.MoveToDirMovement == MovementDirection.UpLeft)
                        {
                            if (Room.GetGameMap().IsValidValueItem(item.X - 1, item.Y)) // izq
                                item.MoveToDirMovement = MovementDirection.Left;
                            if (Room.GetGameMap().IsValidValueItem(item.X - 1, item.Y + 1)) // abajo izq
                                item.MoveToDirMovement = MovementDirection.DownLeft;
                            if (Room.GetGameMap().IsValidValueItem(item.X, item.Y + 1)) // abajo
                                item.MoveToDirMovement = MovementDirection.Down;
                            if (Room.GetGameMap().IsValidValueItem(item.X + 1, item.Y + 1)) // abajo derecha
                                item.MoveToDirMovement = MovementDirection.DownRight;
                            if (Room.GetGameMap().IsValidValueItem(item.X + 1, item.Y)) // derecha
                                item.MoveToDirMovement = MovementDirection.Right;
                            if (Room.GetGameMap().IsValidValueItem(item.X + 1, item.Y - 1)) // arriba derecha
                                item.MoveToDirMovement = MovementDirection.UpRight;
                            if (Room.GetGameMap().IsValidValueItem(item.X, item.Y - 1)) // arriba
                                item.MoveToDirMovement = MovementDirection.Up;
                            if (Room.GetGameMap().IsValidValueItem(item.X - 1, item.Y - 1)) // arriba izq
                                item.MoveToDirMovement = MovementDirection.UpLeft;
                        }
                        else if (item.MoveToDirMovement == MovementDirection.UpRight)
                        {
                            if (Room.GetGameMap().IsValidValueItem(item.X, item.Y - 1)) // arriba
                                item.MoveToDirMovement = MovementDirection.Up;
                            if (Room.GetGameMap().IsValidValueItem(item.X - 1, item.Y - 1)) // arriba izq
                                item.MoveToDirMovement = MovementDirection.UpLeft;
                            if (Room.GetGameMap().IsValidValueItem(item.X - 1, item.Y)) // izq
                                item.MoveToDirMovement = MovementDirection.Left;
                            if (Room.GetGameMap().IsValidValueItem(item.X - 1, item.Y + 1)) // abajo izq
                                item.MoveToDirMovement = MovementDirection.DownLeft;
                            if (Room.GetGameMap().IsValidValueItem(item.X, item.Y + 1)) // abajo
                                item.MoveToDirMovement = MovementDirection.Down;
                            if (Room.GetGameMap().IsValidValueItem(item.X + 1, item.Y + 1)) // abajo derecha
                                item.MoveToDirMovement = MovementDirection.DownRight;
                            if (Room.GetGameMap().IsValidValueItem(item.X + 1, item.Y)) // derecha
                                item.MoveToDirMovement = MovementDirection.Right;
                            if (Room.GetGameMap().IsValidValueItem(item.X + 1, item.Y - 1)) // arriba derecha
                                item.MoveToDirMovement = MovementDirection.UpRight;
                        }
                        else if (item.MoveToDirMovement == MovementDirection.DownRight)
                        {
                            if (Room.GetGameMap().IsValidValueItem(item.X + 1, item.Y)) // derecha
                                item.MoveToDirMovement = MovementDirection.Right;
                            if (Room.GetGameMap().IsValidValueItem(item.X + 1, item.Y - 1)) // arriba derecha
                                item.MoveToDirMovement = MovementDirection.UpRight;
                            if (Room.GetGameMap().IsValidValueItem(item.X, item.Y - 1)) // arriba
                                item.MoveToDirMovement = MovementDirection.Up;
                            if (Room.GetGameMap().IsValidValueItem(item.X - 1, item.Y - 1)) // arriba izq
                                item.MoveToDirMovement = MovementDirection.UpLeft;
                            if (Room.GetGameMap().IsValidValueItem(item.X - 1, item.Y)) // izq
                                item.MoveToDirMovement = MovementDirection.Left;
                            if (Room.GetGameMap().IsValidValueItem(item.X - 1, item.Y + 1)) // abajo izq
                                item.MoveToDirMovement = MovementDirection.DownLeft;
                            if (Room.GetGameMap().IsValidValueItem(item.X, item.Y + 1)) // abajo
                                item.MoveToDirMovement = MovementDirection.Down;
                            if (Room.GetGameMap().IsValidValueItem(item.X + 1, item.Y + 1)) // abajo derecha
                                item.MoveToDirMovement = MovementDirection.DownRight;
                        }
                        else if (item.MoveToDirMovement == MovementDirection.DownLeft)
                        {
                            if (Room.GetGameMap().IsValidValueItem(item.X, item.Y + 1)) // abajo
                                item.MoveToDirMovement = MovementDirection.Down;
                            if (Room.GetGameMap().IsValidValueItem(item.X + 1, item.Y + 1)) // abajo derecha
                                item.MoveToDirMovement = MovementDirection.DownRight;
                            if (Room.GetGameMap().IsValidValueItem(item.X + 1, item.Y)) // derecha
                                item.MoveToDirMovement = MovementDirection.Right;
                            if (Room.GetGameMap().IsValidValueItem(item.X + 1, item.Y - 1)) // arriba derecha
                                item.MoveToDirMovement = MovementDirection.UpRight;
                            if (Room.GetGameMap().IsValidValueItem(item.X, item.Y - 1)) // arriba
                                item.MoveToDirMovement = MovementDirection.Up;
                            if (Room.GetGameMap().IsValidValueItem(item.X - 1, item.Y - 1)) // arriba izq
                                item.MoveToDirMovement = MovementDirection.UpLeft;
                            if (Room.GetGameMap().IsValidValueItem(item.X - 1, item.Y)) // izq
                                item.MoveToDirMovement = MovementDirection.Left;
                            if (Room.GetGameMap().IsValidValueItem(item.X - 1, item.Y + 1)) // abajo izq
                                item.MoveToDirMovement = MovementDirection.DownLeft;
                        }

                        break;
                    }

                    case WhenMovementBlock.Left90:
                    {
                        if (item.MoveToDirMovement == MovementDirection.Right)
                        {
                            if (Room.GetGameMap().IsValidValueItem(item.X, item.Y - 1)) // arriba
                                item.MoveToDirMovement = MovementDirection.Up;
                            if (Room.GetGameMap().IsValidValueItem(item.X - 1, item.Y)) // izq
                                item.MoveToDirMovement = MovementDirection.Left;
                            if (Room.GetGameMap().IsValidValueItem(item.X, item.Y + 1)) // abajo
                                item.MoveToDirMovement = MovementDirection.Down;
                            if (Room.GetGameMap().IsValidValueItem(item.X + 1, item.Y)) // derecha
                                item.MoveToDirMovement = MovementDirection.Right;
                        }
                        else if (item.MoveToDirMovement == MovementDirection.Left)
                        {
                            if (Room.GetGameMap().IsValidValueItem(item.X, item.Y + 1)) // abajo
                                item.MoveToDirMovement = MovementDirection.Down;
                            if (Room.GetGameMap().IsValidValueItem(item.X + 1, item.Y)) // derecha
                                item.MoveToDirMovement = MovementDirection.Right;
                            if (Room.GetGameMap().IsValidValueItem(item.X, item.Y - 1)) // arriba
                                item.MoveToDirMovement = MovementDirection.Up;
                            if (Room.GetGameMap().IsValidValueItem(item.X - 1, item.Y)) // izq
                                item.MoveToDirMovement = MovementDirection.Left;
                        }
                        else if (item.MoveToDirMovement == MovementDirection.Up)
                        {
                            if (Room.GetGameMap().IsValidValueItem(item.X - 1, item.Y)) // izq
                                item.MoveToDirMovement = MovementDirection.Left;
                            if (Room.GetGameMap().IsValidValueItem(item.X, item.Y + 1)) // abajo
                                item.MoveToDirMovement = MovementDirection.Down;
                            if (Room.GetGameMap().IsValidValueItem(item.X + 1, item.Y)) // derecha
                                item.MoveToDirMovement = MovementDirection.Right;
                            if (Room.GetGameMap().IsValidValueItem(item.X, item.Y - 1)) // arriba
                                item.MoveToDirMovement = MovementDirection.Up;
                        }
                        else if (item.MoveToDirMovement == MovementDirection.Down)
                        {
                            if (Room.GetGameMap().IsValidValueItem(item.X + 1, item.Y)) // derecha
                                item.MoveToDirMovement = MovementDirection.Right;
                            if (Room.GetGameMap().IsValidValueItem(item.X, item.Y - 1)) // arriba
                                item.MoveToDirMovement = MovementDirection.Up;
                            if (Room.GetGameMap().IsValidValueItem(item.X - 1, item.Y)) // izq
                                item.MoveToDirMovement = MovementDirection.Left;
                            if (Room.GetGameMap().IsValidValueItem(item.X, item.Y + 1)) // abajo
                                item.MoveToDirMovement = MovementDirection.Down;
                        }
                        else if (item.MoveToDirMovement == MovementDirection.UpLeft)
                        {
                            if (Room.GetGameMap().IsValidValueItem(item.X - 1, item.Y + 1)) // abajo izq
                                item.MoveToDirMovement = MovementDirection.DownLeft;
                            if (Room.GetGameMap().IsValidValueItem(item.X + 1, item.Y + 1)) // abajo derecha
                                item.MoveToDirMovement = MovementDirection.DownRight;
                            if (Room.GetGameMap().IsValidValueItem(item.X + 1, item.Y - 1)) // arriba derecha
                                item.MoveToDirMovement = MovementDirection.UpRight;
                            if (Room.GetGameMap().IsValidValueItem(item.X - 1, item.Y - 1)) // arriba izq
                                item.MoveToDirMovement = MovementDirection.UpLeft;
                        }
                        else if (item.MoveToDirMovement == MovementDirection.UpRight)
                        {
                            if (Room.GetGameMap().IsValidValueItem(item.X - 1, item.Y - 1)) // arriba izq
                                item.MoveToDirMovement = MovementDirection.UpLeft;
                            if (Room.GetGameMap().IsValidValueItem(item.X - 1, item.Y + 1)) // abajo izq
                                item.MoveToDirMovement = MovementDirection.DownLeft;
                            if (Room.GetGameMap().IsValidValueItem(item.X + 1, item.Y + 1)) // abajo derecha
                                item.MoveToDirMovement = MovementDirection.DownRight;
                            if (Room.GetGameMap().IsValidValueItem(item.X + 1, item.Y - 1)) // arriba derecha
                                item.MoveToDirMovement = MovementDirection.UpRight;
                        }
                        else if (item.MoveToDirMovement == MovementDirection.DownRight)
                        {
                            if (Room.GetGameMap().IsValidValueItem(item.X + 1, item.Y - 1)) // arriba derecha
                                item.MoveToDirMovement = MovementDirection.UpRight;
                            if (Room.GetGameMap().IsValidValueItem(item.X - 1, item.Y - 1)) // arriba izq
                                item.MoveToDirMovement = MovementDirection.UpLeft;
                            if (Room.GetGameMap().IsValidValueItem(item.X - 1, item.Y + 1)) // abajo izq
                                item.MoveToDirMovement = MovementDirection.DownLeft;
                            if (Room.GetGameMap().IsValidValueItem(item.X + 1, item.Y + 1)) // abajo derecha
                                item.MoveToDirMovement = MovementDirection.DownRight;
                        }
                        else if (item.MoveToDirMovement == MovementDirection.DownLeft)
                        {
                            if (Room.GetGameMap().IsValidValueItem(item.X + 1, item.Y + 1)) // abajo derecha
                                item.MoveToDirMovement = MovementDirection.DownRight;
                            if (Room.GetGameMap().IsValidValueItem(item.X + 1, item.Y - 1)) // arriba derecha
                                item.MoveToDirMovement = MovementDirection.UpRight;
                            if (Room.GetGameMap().IsValidValueItem(item.X - 1, item.Y - 1)) // arriba izq
                                item.MoveToDirMovement = MovementDirection.UpLeft;
                            if (Room.GetGameMap().IsValidValueItem(item.X - 1, item.Y + 1)) // abajo izq
                                item.MoveToDirMovement = MovementDirection.DownLeft;
                        }

                        break;
                    }

                    case WhenMovementBlock.TurnBack:
                    {
                        if (item.MoveToDirMovement == MovementDirection.Right)
                            item.MoveToDirMovement = MovementDirection.Left;
                        else if (item.MoveToDirMovement == MovementDirection.Left)
                            item.MoveToDirMovement = MovementDirection.Right;
                        else if (item.MoveToDirMovement == MovementDirection.Up)
                            item.MoveToDirMovement = MovementDirection.Down;
                        else if (item.MoveToDirMovement == MovementDirection.Down)
                            item.MoveToDirMovement = MovementDirection.Up;
                        else if (item.MoveToDirMovement == MovementDirection.UpRight)
                            item.MoveToDirMovement = MovementDirection.DownLeft;
                        else if (item.MoveToDirMovement == MovementDirection.DownLeft)
                            item.MoveToDirMovement = MovementDirection.UpRight;
                        else if (item.MoveToDirMovement == MovementDirection.UpLeft)
                            item.MoveToDirMovement = MovementDirection.DownRight;
                        else if (item.MoveToDirMovement == MovementDirection.DownRight)
                            item.MoveToDirMovement = MovementDirection.UpLeft;
                        break;
                    }

                    case WhenMovementBlock.TurnRandom:
                    {
                        item.MoveToDirMovement = (MovementDirection) new Random().Next(1, 7);
                        break;
                    }
                }

                newPoint = Movement.HandleMovementDir(item.Coordinate, item.MoveToDirMovement, item.Rot);
                Room.GetRoomItemHandler()
                    .SetFloorItem(null, item, newPoint.X, newPoint.Y, item.Rot, false, false, true, false, true);
            }
        }
    }
}