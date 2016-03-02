/**
     Because i love chocolat...                                      
                                    88 88  
                                    "" 88  
                                       88  
8b       d8 88       88 8b,dPPYba,  88 88  
`8b     d8' 88       88 88P'    "8a 88 88  
 `8b   d8'  88       88 88       d8 88 ""  
  `8b,d8'   "8a,   ,a88 88b,   ,a8" 88 aa  
    Y88'     `"YbbdP'Y8 88`YbbdP"'  88 88  
    d8'                 88                 
   d8'                  88     
   
   Private Habbo Hotel Emulating System
   @author Claudio A. Santoro W.
   @author Kessiler R.
   @version dev-beta
   @license MIT
   @copyright Sulake Corporation Oy
   @observation All Rights of Habbo, Habbo Hotel, and all Habbo contents and it's names, is copyright from Sulake
   Corporation Oy. Yupi! has nothing linked with Sulake. 
   This Emulator is Only for DEVELOPMENT uses. If you're selling this you're violating Sulakes Copyright.
*/

using System;
using System.Text;
using Yupi.Emulator.Game.Rooms.Chat.Enums;
using Yupi.Emulator.Messages;
using Yupi.Emulator.Messages.Buffers;

namespace Yupi.Emulator.Game.Rooms.Data
{
    /// <summary>
    ///     Class DynamicRoomModel.
    /// </summary>
    internal class DynamicRoomModel
    {
        /// <summary>
        ///     The _m room
        /// </summary>
        private readonly Room _mRoom;

        /// <summary>
        ///     The _serialized heightmap
        /// </summary>
        private SimpleServerMessageBuffer _serializedHeightmap;

        /// <summary>
        ///     The _static model
        /// </summary>
        private RoomModel _staticModel;

        /// <summary>
        ///     The club only
        /// </summary>
        internal bool ClubOnly;

        /// <summary>
        ///     The door orientation
        /// </summary>
        internal int DoorOrientation;

        /// <summary>
        ///     The door x
        /// </summary>
        internal int DoorX;

        /// <summary>
        ///     The door y
        /// </summary>
        internal int DoorY;

        /// <summary>
        ///     The door z
        /// </summary>
        internal double DoorZ;

        /// <summary>
        ///     The heightmap
        /// </summary>
        internal string Heightmap;

        /// <summary>
        ///     The heightmap serialized
        /// </summary>
        internal bool HeightmapSerialized;

        /// <summary>
        ///     The map size x
        /// </summary>
        internal int MapSizeX;

        /// <summary>
        ///     The map size y
        /// </summary>
        internal int MapSizeY;

        /// <summary>
        ///     The sq character
        /// </summary>
        internal char[][] SqChar;

        /// <summary>
        ///     The sq floor height
        /// </summary>
        internal short[][] SqFloorHeight;

        /// <summary>
        ///     The sq seat rot
        /// </summary>
        internal byte[][] SqSeatRot;

        /// <summary>
        ///     The sq state
        /// </summary>
        internal SquareState[][] SqState;

        /// <summary>
        ///     Initializes a new instance of the <see cref="DynamicRoomModel" /> class.
        /// </summary>
        /// <param name="pModel">The p model.</param>
        /// <param name="room">The room.</param>
        internal DynamicRoomModel(RoomModel pModel, Room room)
        {
            _staticModel = pModel;
            DoorX = _staticModel.DoorX;
            DoorY = _staticModel.DoorY;
            DoorZ = _staticModel.DoorZ;
            DoorOrientation = _staticModel.DoorOrientation;
            Heightmap = _staticModel.Heightmap;
            MapSizeX = _staticModel.MapSizeX;
            MapSizeY = _staticModel.MapSizeY;
            ClubOnly = _staticModel.ClubOnly;
            _mRoom = room;
            Generate();
        }

        /// <summary>
        ///     Generates this instance.
        /// </summary>
        internal void Generate()
        {
            SqState = new SquareState[MapSizeX][];
            for (int i = 0; i < MapSizeX; i++) SqState[i] = new SquareState[MapSizeY];
            SqFloorHeight = new short[MapSizeX][];
            for (int i = 0; i < MapSizeX; i++) SqFloorHeight[i] = new short[MapSizeY];
            SqSeatRot = new byte[MapSizeX][];
            for (int i = 0; i < MapSizeX; i++) SqSeatRot[i] = new byte[MapSizeY];
            SqChar = new char[MapSizeX][];
            for (int i = 0; i < MapSizeX; i++) SqChar[i] = new char[MapSizeY];
            for (int i = 0; i < MapSizeY; i++)
            {
                for (int j = 0; j < MapSizeX; j++)
                {
                    if (j > _staticModel.MapSizeX - 1 || i > _staticModel.MapSizeY - 1)
                        SqState[j][i] = SquareState.Blocked;
                    else
                    {
                        SqState[j][i] = _staticModel.SqState[j][i];
                        SqFloorHeight[j][i] = _staticModel.SqFloorHeight[j][i];
                        SqSeatRot[j][i] = _staticModel.SqSeatRot[j][i];
                        SqChar[j][i] = _staticModel.SqChar[j][i];
                    }
                }
            }

            HeightmapSerialized = false;
        }

        /// <summary>
        ///     Refreshes the arrays.
        /// </summary>
        internal void RefreshArrays() => Generate();

        /// <summary>
        ///     Sets the state of the update.
        /// </summary>
        internal void SetUpdateState() => HeightmapSerialized = false;

        /// <summary>
        ///     Gets the heightmap.
        /// </summary>
        /// <returns>SimpleServerMessageBuffer.</returns>
        internal SimpleServerMessageBuffer GetHeightmap()
        {
            if (HeightmapSerialized)
                return _serializedHeightmap;

            _serializedHeightmap = SerializeHeightmap();

            HeightmapSerialized = true;

            return _serializedHeightmap;
        }

        /// <summary>
        ///     Adds the x.
        /// </summary>
        internal void AddX()
        {
            {
                MapSizeX++;
                RefreshArrays();
            }
        }

        /// <summary>
        ///     Opens the square.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="z">The z.</param>
        internal void OpenSquare(int x, int y, double z)
        {
            if (z > 9.0) z = 9.0;
            if (z < 0.0) z = 0.0;
            SqFloorHeight[x][y] = (short) z;

            SqState[x][y] = SquareState.Open;
        }

        /// <summary>
        ///     Adds the y.
        /// </summary>
        internal void AddY()
        {
            {
                MapSizeY++;
                RefreshArrays();
            }
        }

        /// <summary>
        ///     Sets the mapsize.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        internal void SetMapsize(int x, int y)
        {
            MapSizeX = x;
            MapSizeY = y;
            RefreshArrays();
        }

        /// <summary>
        ///     Destroys this instance.
        /// </summary>
        internal void Destroy()
        {
            Array.Clear(SqState, 0, SqState.Length);
            Array.Clear(SqFloorHeight, 0, SqFloorHeight.Length);
            Array.Clear(SqSeatRot, 0, SqSeatRot.Length);
            _staticModel = null;
            Heightmap = null;
            SqState = null;
            SqFloorHeight = null;
            SqSeatRot = null;
        }

        /// <summary>
        ///     Serializes the heightmap.
        /// </summary>
        /// <returns>SimpleServerMessageBuffer.</returns>
        private SimpleServerMessageBuffer SerializeHeightmap()
        {
            SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.SendRequest("FloorMapMessageComposer"));
            simpleServerMessageBuffer.AppendBool(true);
            simpleServerMessageBuffer.AppendInteger(_mRoom.RoomData.WallHeight);
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < MapSizeY; i++)
            {
                for (int j = 0; j < MapSizeX; j++)
                {
                    try
                    {
                        stringBuilder.Append(SqChar[j][i].ToString());
                    }
                    catch (Exception)
                    {
                        stringBuilder.Append("0");
                    }
                }
                stringBuilder.Append(Convert.ToChar(13));
            }
            string s = stringBuilder.ToString();
            simpleServerMessageBuffer.AppendString(s);
            return simpleServerMessageBuffer;
        }
    }
}