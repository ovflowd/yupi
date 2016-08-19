using System;
using System.Globalization;
using Yupi.Model.Domain.Components;
using Headspring;

namespace Yupi.Model.Domain
{
	public class RoomModel : Enumeration<RoomModel>
	{
		public static readonly RoomModel Model_a = new RoomModel (1, "model_a") {
			Door = new Vector (3, 5, 0),
			DoorOrientation = 2,
			ClubOnly = false,
			Heightmap = "xxxxxxxxxxxx\r\nxxxx00000000\r\nxxxx00000000\r\nxxxx00000000\r\nxxxx00000000\r\nxxx000000000\r\nxxxx00000000\r\nxxxx00000000\r\nxxxx00000000\r\nxxxx00000000\r\nxxxx00000000\r\nxxxx00000000\r\nxxxx00000000\r\nxxxx00000000\r\nxxxxxxxxxxxx\r\nxxxxxxxxxxxx"
		};

		public static readonly RoomModel Model_b = new RoomModel (2, "model_b") {
			Door = new Vector (0, 5, 0),
			DoorOrientation = 2,
			ClubOnly = false,
			Heightmap = "xxxxxxxxxxxx\r\nxxxxx0000000\r\nxxxxx0000000\r\nxxxxx0000000\r\nxxxxx0000000\r\nx00000000000\r\nx00000000000\r\nx00000000000\r\nx00000000000\r\nx00000000000\r\nx00000000000\r\nxxxxxxxxxxxx\r\nxxxxxxxxxxxx\r\nxxxxxxxxxxxx\r\nxxxxxxxxxxxx\r\nxxxxxxxxxxxx"
		};

		public static readonly RoomModel Model_c = new RoomModel (3, "model_c") {
			Door = new Vector (4, 7, 0),
			DoorOrientation = 2,
			ClubOnly = false,
			Heightmap = "xxxxxxxxxxxx\r\nxxxxxxxxxxxx\r\nxxxxxxxxxxxx\r\nxxxxxxxxxxxx\r\nxxxxxxxxxxxx\r\nxxxxx000000x\r\nxxxxx000000x\r\nxxxxx000000x\r\nxxxxx000000x\r\nxxxxx000000x\r\nxxxxx000000x\r\nxxxxxxxxxxxx\r\nxxxxxxxxxxxx\r\nxxxxxxxxxxxx\r\nxxxxxxxxxxxx\r\nxxxxxxxxxxxx"
		};

		public static readonly RoomModel Model_d = new RoomModel (4, "model_d") {
			Door = new Vector (4, 7, 0),
			DoorOrientation = 2,
			ClubOnly = false,
			Heightmap = "xxxxxxxxxxxx\r\nxxxxx000000x\r\nxxxxx000000x\r\nxxxxx000000x\r\nxxxxx000000x\r\nxxxxx000000x\r\nxxxxx000000x\r\nxxxxx000000x\r\nxxxxx000000x\r\nxxxxx000000x\r\nxxxxx000000x\r\nxxxxx000000x\r\nxxxxx000000x\r\nxxxxx000000x\r\nxxxxx000000x\r\nxxxxxxxxxxxx"
		};

		public static readonly RoomModel Model_e = new RoomModel (5, "model_e") {
			Door = new Vector (1, 5, 0),
			DoorOrientation = 2,
			ClubOnly = false,
			Heightmap = "xxxxxxxxxxxx\r\nxxxxxxxxxxxx\r\nxxxxxxxxxxxx\r\nxx0000000000\r\nxx0000000000\r\nxx0000000000\r\nxx0000000000\r\nxx0000000000\r\nxx0000000000\r\nxx0000000000\r\nxx0000000000\r\nxxxxxxxxxxxx\r\nxxxxxxxxxxxx\r\nxxxxxxxxxxxx\r\nxxxxxxxxxxxx\r\nxxxxxxxxxxxx"
		};

		public static readonly RoomModel Model_f = new RoomModel (6, "model_f") {
			Door = new Vector (2, 5, 0),
			DoorOrientation = 2,
			ClubOnly = false,
			Heightmap = "xxxxxxxxxxxx\r\nxxxxxxx0000x\r\nxxxxxxx0000x\r\nxxx00000000x\r\nxxx00000000x\r\nxxx00000000x\r\nxxx00000000x\r\nx0000000000x\r\nx0000000000x\r\nx0000000000x\r\nx0000000000x\r\nxxxxxxxxxxxx\r\nxxxxxxxxxxxx\r\nxxxxxxxxxxxx\r\nxxxxxxxxxxxx\r\nxxxxxxxxxxxx"
		};

		public static readonly RoomModel Model_g = new RoomModel (7, "model_g") {
			Door = new Vector (1, 7, 1),
			DoorOrientation = 2,
			ClubOnly = true,
			Heightmap = "xxxxxxxxxxxx\r\nxxxxxxxxxxxx\r\nxxxxxxx00000\r\nxxxxxxx00000\r\nxxxxxxx00000\r\nxx1111000000\r\nxx1111000000\r\nxx1111000000\r\nxx1111000000\r\nxx1111000000\r\nxxxxxxx00000\r\nxxxxxxx00000\r\nxxxxxxx00000\r\nxxxxxxxxxxxx\r\nxxxxxxxxxxxx\r\nxxxxxxxxxxxx\r\nxxxxxxxxxxxx"
		};

		public static readonly RoomModel Model_h = new RoomModel (8, "model_h") {
			Door = new Vector (4, 4, 1),
			DoorOrientation = 2,
			ClubOnly = true,
			Heightmap = "xxxxxxxxxxxx\r\nxxxxxxxxxxxx\r\nxxxxx111111x\r\nxxxxx111111x\r\nxxxxx111111x\r\nxxxxx111111x\r\nxxxxx111111x\r\nxxxxx000000x\r\nxxxxx000000x\r\nxxx00000000x\r\nxxx00000000x\r\nxxx00000000x\r\nxxx00000000x\r\nxxxxxxxxxxxx\r\nxxxxxxxxxxxx\r\nxxxxxxxxxxxx"
		};

		public static readonly RoomModel Model_i = new RoomModel (9, "model_i") {
			Door = new Vector (0, 10, 0),
			DoorOrientation = 2,
			ClubOnly = false,
			Heightmap = "xxxxxxxxxxxxxxxxx\r\nx0000000000000000\r\nx0000000000000000\r\nx0000000000000000\r\nx0000000000000000\r\nx0000000000000000\r\nx0000000000000000\r\nx0000000000000000\r\nx0000000000000000\r\nx0000000000000000\r\nx0000000000000000\r\nx0000000000000000\r\nx0000000000000000\r\nx0000000000000000\r\nx0000000000000000\r\nx0000000000000000\r\nx0000000000000000\r\nx0000000000000000\r\nx0000000000000000\r\nx0000000000000000\r\nx0000000000000000\r\nx0000000000000000\r\nx0000000000000000\r\nx0000000000000000\r\nx0000000000000000\r\nx0000000000000000\r\nx0000000000000000\r\nxxxxxxxxxxxxxxxxx"
		};

		public static readonly RoomModel Model_j = new RoomModel (10, "model_j") {
			Door = new Vector (0, 10, 0),
			DoorOrientation = 2,
			ClubOnly = false,
			Heightmap = "xxxxxxxxxxxxxxxxxxxxx\r\nxxxxxxxxxxx0000000000\r\nxxxxxxxxxxx0000000000\r\nxxxxxxxxxxx0000000000\r\nxxxxxxxxxxx0000000000\r\nxxxxxxxxxxx0000000000\r\nxxxxxxxxxxx0000000000\r\nx00000000000000000000\r\nx00000000000000000000\r\nx00000000000000000000\r\nx00000000000000000000\r\nx00000000000000000000\r\nx00000000000000000000\r\nx00000000000000000000\r\nx00000000000000000000\r\nx00000000000000000000\r\nx00000000000000000000\r\nx0000000000xxxxxxxxxx\r\nx0000000000xxxxxxxxxx\r\nx0000000000xxxxxxxxxx\r\nx0000000000xxxxxxxxxx\r\nx0000000000xxxxxxxxxx\r\nx0000000000xxxxxxxxxx\r\nxxxxxxxxxxxxxxxxxxxxx"
		};

		public static readonly RoomModel Model_k = new RoomModel (11, "model_k") {
			Door = new Vector (0, 13, 0),
			DoorOrientation = 2,
			ClubOnly = false,
			Heightmap = "xxxxxxxxxxxxxxxxxxxxxxxxx\r\nxxxxxxxxxxxxxxxxx00000000\r\nxxxxxxxxxxxxxxxxx00000000\r\nxxxxxxxxxxxxxxxxx00000000\r\nxxxxxxxxxxxxxxxxx00000000\r\nxxxxxxxxx0000000000000000\r\nxxxxxxxxx0000000000000000\r\nxxxxxxxxx0000000000000000\r\nxxxxxxxxx0000000000000000\r\nx000000000000000000000000\r\nx000000000000000000000000\r\nx000000000000000000000000\r\nx000000000000000000000000\r\nx000000000000000000000000\r\nx000000000000000000000000\r\nx000000000000000000000000\r\nx000000000000000000000000\r\nxxxxxxxxx0000000000000000\r\nxxxxxxxxx0000000000000000\r\nxxxxxxxxx0000000000000000\r\nxxxxxxxxx0000000000000000\r\nxxxxxxxxx0000000000000000\r\nxxxxxxxxx0000000000000000\r\nxxxxxxxxx0000000000000000\r\nxxxxxxxxx0000000000000000\r\nxxxxxxxxx0000000000000000\r\nxxxxxxxxx0000000000000000\r\nxxxxxxxxxxxxxxxxxxxxxxxxx"
		};

		public static readonly RoomModel Model_l = new RoomModel (12, "model_l") {
			Door = new Vector (0, 16, 0),
			DoorOrientation = 2,
			ClubOnly = false,
			Heightmap = "xxxxxxxxxxxxxxxxxxxxx\r\nx00000000000000000000\r\nx00000000000000000000\r\nx00000000000000000000\r\nx00000000000000000000\r\nx00000000000000000000\r\nx00000000000000000000\r\nx00000000000000000000\r\nx00000000000000000000\r\nx00000000xxxx00000000\r\nx00000000xxxx00000000\r\nx00000000xxxx00000000\r\nx00000000xxxx00000000\r\nx00000000xxxx00000000\r\nx00000000xxxx00000000\r\nx00000000xxxx00000000\r\nx00000000xxxx00000000\r\nx00000000xxxx00000000\r\nx00000000xxxx00000000\r\nx00000000xxxx00000000\r\nx00000000xxxx00000000\r\nxxxxxxxxxxxxxxxxxxxxx"
		};

		public static readonly RoomModel Model_m = new RoomModel (13, "model_m") {
			Door = new Vector (0, 15, 0),
			DoorOrientation = 2,
			ClubOnly = false,
			Heightmap = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxx\r\nxxxxxxxxxxx00000000xxxxxxxxxx\r\nxxxxxxxxxxx00000000xxxxxxxxxx\r\nxxxxxxxxxxx00000000xxxxxxxxxx\r\nxxxxxxxxxxx00000000xxxxxxxxxx\r\nxxxxxxxxxxx00000000xxxxxxxxxx\r\nxxxxxxxxxxx00000000xxxxxxxxxx\r\nxxxxxxxxxxx00000000xxxxxxxxxx\r\nxxxxxxxxxxx00000000xxxxxxxxxx\r\nxxxxxxxxxxx00000000xxxxxxxxxx\r\nxxxxxxxxxxx00000000xxxxxxxxxx\r\nx0000000000000000000000000000\r\nx0000000000000000000000000000\r\nx0000000000000000000000000000\r\nx0000000000000000000000000000\r\nx0000000000000000000000000000\r\nx0000000000000000000000000000\r\nx0000000000000000000000000000\r\nx0000000000000000000000000000\r\nxxxxxxxxxxx00000000xxxxxxxxxx\r\nxxxxxxxxxxx00000000xxxxxxxxxx\r\nxxxxxxxxxxx00000000xxxxxxxxxx\r\nxxxxxxxxxxx00000000xxxxxxxxxx\r\nxxxxxxxxxxx00000000xxxxxxxxxx\r\nxxxxxxxxxxx00000000xxxxxxxxxx\r\nxxxxxxxxxxx00000000xxxxxxxxxx\r\nxxxxxxxxxxx00000000xxxxxxxxxx\r\nxxxxxxxxxxx00000000xxxxxxxxxx\r\nxxxxxxxxxxx00000000xxxxxxxxxx\r\nxxxxxxxxxxxxxxxxxxxxxxxxxxxxx"
		};

		public static readonly RoomModel Model_n = new RoomModel (14, "model_n") {
			Door = new Vector (0, 16, 0),
			DoorOrientation = 2,
			ClubOnly = false,
			Heightmap = "xxxxxxxxxxxxxxxxxxxxx\r\nx00000000000000000000\r\nx00000000000000000000\r\nx00000000000000000000\r\nx00000000000000000000\r\nx00000000000000000000\r\nx00000000000000000000\r\nx000000xxxxxxxx000000\r\nx000000x000000x000000\r\nx000000x000000x000000\r\nx000000x000000x000000\r\nx000000x000000x000000\r\nx000000x000000x000000\r\nx000000x000000x000000\r\nx000000xxxxxxxx000000\r\nx00000000000000000000\r\nx00000000000000000000\r\nx00000000000000000000\r\nx00000000000000000000\r\nx00000000000000000000\r\nx00000000000000000000\r\nxxxxxxxxxxxxxxxxxxxxx"
		};

		public static readonly RoomModel Model_o = new RoomModel (15, "model_o") {
			Door = new Vector (0, 18, 1),
			DoorOrientation = 2,
			ClubOnly = true,
			Heightmap = "xxxxxxxxxxxxxxxxxxxxxxxxx\r\nxxxxxxxxxxxxx11111111xxxx\r\nxxxxxxxxxxxxx11111111xxxx\r\nxxxxxxxxxxxxx11111111xxxx\r\nxxxxxxxxxxxxx11111111xxxx\r\nxxxxxxxxxxxxx11111111xxxx\r\nxxxxxxxxxxxxx11111111xxxx\r\nxxxxxxxxxxxxx11111111xxxx\r\nxxxxxxxxxxxxx00000000xxxx\r\nxxxxxxxxx0000000000000000\r\nxxxxxxxxx0000000000000000\r\nxxxxxxxxx0000000000000000\r\nxxxxxxxxx0000000000000000\r\nxxxxxxxxx0000000000000000\r\nxxxxxxxxx0000000000000000\r\nx111111100000000000000000\r\nx111111100000000000000000\r\nx111111100000000000000000\r\nx111111100000000000000000\r\nx111111100000000000000000\r\nx111111100000000000000000\r\nx111111100000000000000000\r\nx111111100000000000000000\r\nxxxxxxxxx0000000000000000\r\nxxxxxxxxx0000000000000000\r\nxxxxxxxxx0000000000000000\r\nxxxxxxxxx0000000000000000\r\nxxxxxxxxxxxxxxxxxxxxxxxxx"
		};

		public static readonly RoomModel Model_p = new RoomModel (16, "model_p") {
			Door = new Vector (0, 23, 2),
			DoorOrientation = 2,
			ClubOnly = true,
			Heightmap = "xxxxxxxxxxxxxxxxxxx\r\nxxxxxxx222222222222\r\nxxxxxxx222222222222\r\nxxxxxxx222222222222\r\nxxxxxxx222222222222\r\nxxxxxxx222222222222\r\nxxxxxxx222222222222\r\nxxxxxxx22222222xxxx\r\nxxxxxxx11111111xxxx\r\nx222221111111111111\r\nx222221111111111111\r\nx222221111111111111\r\nx222221111111111111\r\nx222221111111111111\r\nx222221111111111111\r\nx222221111111111111\r\nx222221111111111111\r\nx2222xx11111111xxxx\r\nx2222xx00000000xxxx\r\nx2222xx000000000000\r\nx2222xx000000000000\r\nx2222xx000000000000\r\nx2222xx000000000000\r\n22222xx000000000000\r\nx2222xx000000000000\r\nxxxxxxxxxxxxxxxxxxx"
		};

		public static readonly RoomModel Model_q = new RoomModel (17, "model_q") {
			Door = new Vector (10, 4, 2),
			DoorOrientation = 2,
			ClubOnly = true,
			Heightmap = "xxxxxxxxxxxxxxxxxxx\r\nxxxxxxxxxxx22222222\r\nxxxxxxxxxxx22222222\r\nxxxxxxxxxxx22222222\r\nxxxxxxxxxxx22222222\r\nxxxxxxxxxxx22222222\r\nxxxxxxxxxxx22222222\r\nx222222222222222222\r\nx222222222222222222\r\nx222222222222222222\r\nx222222222222222222\r\nx222222222222222222\r\nx222222222222222222\r\nx2222xxxxxxxxxxxxxx\r\nx2222xxxxxxxxxxxxxx\r\nx2222211111xx000000\r\nx222221111110000000\r\nx222221111110000000\r\nx2222211111xx000000\r\nxx22xxx1111xxxxxxxx\r\nxx11xxx1111xxxxxxxx\r\nx1111xx1111xx000000\r\nx1111xx111110000000\r\nx1111xx111110000000\r\nx1111xx1111xx000000\r\nxxxxxxxxxxxxxxxxxxx"
		};

		public static readonly RoomModel Model_r = new RoomModel (18, "model_r") {
			Door = new Vector (10, 4, 3),
			DoorOrientation = 2,
			ClubOnly = true,
			Heightmap = "xxxxxxxxxxxxxxxxxxxxxxxxx\r\nxxxxxxxxxxx33333333333333\r\nxxxxxxxxxxx33333333333333\r\nxxxxxxxxxxx33333333333333\r\nxxxxxxxxxx333333333333333\r\nxxxxxxxxxxx33333333333333\r\nxxxxxxxxxxx33333333333333\r\nxxxxxxx333333333333333333\r\nxxxxxxx333333333333333333\r\nxxxxxxx333333333333333333\r\nxxxxxxx333333333333333333\r\nxxxxxxx333333333333333333\r\nxxxxxxx333333333333333333\r\nx4444433333xxxxxxxxxxxxxx\r\nx4444433333xxxxxxxxxxxxxx\r\nx44444333333222xx000000xx\r\nx44444333333222xx000000xx\r\nxxx44xxxxxxxx22xx000000xx\r\nxxx33xxxxxxxx11xx000000xx\r\nxxx33322222211110000000xx\r\nxxx33322222211110000000xx\r\nxxxxxxxxxxxxxxxxx000000xx\r\nxxxxxxxxxxxxxxxxx000000xx\r\nxxxxxxxxxxxxxxxxx000000xx\r\nxxxxxxxxxxxxxxxxx000000xx\r\nxxxxxxxxxxxxxxxxxxxxxxxxx"
		};
				
		public static readonly RoomModel Model_t = new RoomModel (19, "model_t") {
			Door = new Vector (0, 3, 2),
			DoorOrientation = 2,
			ClubOnly = true,
			Heightmap = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxx\r\nx222222222222222222222222222x\r\nx222222222222222222222222222x\r\n2222222222222222222222222222x\r\nx222222222222222222222222222x\r\nx2222xxxxxx222222xxxxxxx2222x\r\nx2222xxxxxx111111xxxxxxx2222x\r\nx2222xx111111111111111xx2222x\r\nx2222xx111111111111111xx2222x\r\nx2222xx11xxx1111xxxx11xx2222x\r\nx2222xx11xxx0000xxxx11xx2222x\r\nx22222111x00000000xx11xx2222x\r\nx22222111x00000000xx11xx2222x\r\nx22222111x00000000xx11xx2222x\r\nx22222111x00000000xx11xx2222x\r\nx22222111x00000000xx11xx2222x\r\nx22222111x00000000xx11xx2222x\r\nx2222xx11xxxxxxxxxxx11xx2222x\r\nx2222xx11xxxxxxxxxxx11xx2222x\r\nx2222xx111111111111111xx2222x\r\nx2222xx111111111111111xx2222x\r\nx2222xxxxxxxxxxxxxxxxxxx2222x\r\nx2222xxxxxxxxxxxxxxxxxxx2222x\r\nx222222222222222222222222222x\r\nx222222222222222222222222222x\r\nx222222222222222222222222222x\r\nx222222222222222222222222222x\r\nxxxxxxxxxxxxxxxxxxxxxxxxxxxxx"
		};

		public static readonly RoomModel Model_u = new RoomModel (20, "model_u") {
			Door = new Vector (0, 17, 1),
			DoorOrientation = 2,
			ClubOnly = true,
			Heightmap = "xxxxxxxxxxxxxxxxxxxxxxxx\r\nx1111100000000000000000x\r\nx1111100000000000000000x\r\nx1111100000000000000000x\r\nx1111100000000000000000x\r\nx1111100000000000000000x\r\nx1111100000000000000000x\r\nx1111100000000000000000x\r\nx1111100000000000000000x\r\nx1111100000000000000000x\r\nx1111100000000000000000x\r\nx1111100000000000000000x\r\nx1111100000000000000000x\r\nx1111100000000000000000x\r\nx1111100000000000000000x\r\nx1111100000000000000000x\r\nx1111100000000000000000x\r\n11111100000000000000000x\r\nx1111100000000000000000x\r\nx1111100000000000000000x\r\nx1111100000000000000000x\r\nx1111100000000000000000x\r\nx1111100000000000000000x\r\nx1111100000000000000000x\r\nx1111100000000000000000x\r\nx1111100000000000000000x\r\nx1111100000000000000000x\r\nx1111100000000000000000x\r\nx1111100000000000000000x\r\nx1111100000000000000000x\r\nx1111100000000000000000x\r\nx1111100000000000000000x\r\nx1111100000000000000000x\r\nx1111100000000000000000x\r\nx1111100000000000000000x\r\nxxxxxxxxxxxxxxxxxxxxxxxx"
		};

		public static readonly RoomModel Model_v = new RoomModel (21, "model_v") {
			Door = new Vector (0, 3, 2),
			DoorOrientation = 2,
			ClubOnly = true,
			Heightmap = "xxxxxxxxxxxxxxxxxxxx\r\nx222221111111111111x\r\nx222221111111111111x\r\n2222221111111111111x\r\nx222221111111111111x\r\nx222221111111111111x\r\nx222221111111111111x\r\nxxxxxxxx1111xxxxxxxx\r\nxxxxxxxx0000xxxxxxxx\r\nx000000x0000x000000x\r\nx000000x0000x000000x\r\nx00000000000x000000x\r\nx00000000000x000000x\r\nx000000000000000000x\r\nx000000000000000000x\r\nxxxxxxxx00000000000x\r\nx000000x00000000000x\r\nx000000x0000xxxxxxxx\r\nx00000000000x000000x\r\nx00000000000x000000x\r\nx00000000000x000000x\r\nx00000000000x000000x\r\nxxxxxxxx0000x000000x\r\nx000000x0000x000000x\r\nx000000x0000x000000x\r\nx000000000000000000x\r\nx000000000000000000x\r\nx000000000000000000x\r\nx000000000000000000x\r\nxxxxxxxxxxxxxxxxxxxx"
		};

		public static readonly RoomModel Model_w = new RoomModel (22, "model_w") {
			Door = new Vector (0, 3, 2),
			DoorOrientation = 2,
			ClubOnly = true,
			Heightmap = "xxxxxxxxxxxxxxxxxxxxxxxxxxx\r\nx2222xx1111111111xx11111111\r\nx2222xx1111111111xx11111111\r\n222222111111111111111111111\r\nx22222111111111111111111111\r\nx22222111111111111111111111\r\nx22222111111111111111111111\r\nx2222xx1111111111xx11111111\r\nx2222xx1111111111xx11111111\r\nx2222xx1111111111xxxx1111xx\r\nx2222xx1111111111xxxx0000xx\r\nxxxxxxx1111111111xx00000000\r\nxxxxxxx1111111111xx00000000\r\nx22222111111111111000000000\r\nx22222111111111111000000000\r\nx22222111111111111000000000\r\nx22222111111111111000000000\r\nx2222xx1111111111xx00000000\r\nx2222xx1111111111xx00000000\r\nx2222xxxx1111xxxxxxxxxxxxxx\r\nx2222xxxx0000xxxxxxxxxxxxxx\r\nx2222x0000000000xxxxxxxxxxx\r\nx2222x0000000000xxxxxxxxxxx\r\nx2222x0000000000xxxxxxxxxxx\r\nx2222x0000000000xxxxxxxxxxx\r\nx2222x0000000000xxxxxxxxxxx\r\nx2222x0000000000xxxxxxxxxxx"
		};

		public static readonly RoomModel Model_x = new RoomModel (23, "model_x") {
			Door = new Vector (0, 12, 0),
			DoorOrientation = 2,
			ClubOnly = true,
			Heightmap = "xxxxxxxxxxxxxxxxxxxx\r\nx000000000000000000x\r\nx000000000000000000x\r\nx000000000000000000x\r\nx000000000000000000x\r\nx000000000000000000x\r\nx000000000000000000x\r\nxxx00xxx0000xxx00xxx\r\nx000000x0000x000000x\r\nx000000x0000x000000x\r\nx000000x0000x000000x\r\nx000000x0000x000000x\r\n0000000x0000x000000x\r\nx000000x0000x000000x\r\nx000000x0000x000000x\r\nx000000x0000x000000x\r\nx000000x0000x000000x\r\nx000000x0000x000000x\r\nx000000xxxxxx000000x\r\nx000000000000000000x\r\nx000000000000000000x\r\nx000000000000000000x\r\nx000000000000000000x\r\nx000000000000000000x\r\nx000000000000000000x\r\nxxxxxxxxxxxxxxxxxxxx"
		};

		public static readonly RoomModel Model_y = new RoomModel (24, "model_y") {
			Door = new Vector (0, 3, 0),
			DoorOrientation = 2,
			ClubOnly = true,
			Heightmap = "xxxxxxxxxxxxxxxxxxxxxxxxxxxx\r\nx00000000xx0000000000xx0000x\r\nx00000000xx0000000000xx0000x\r\n000000000xx0000000000xx0000x\r\nx00000000xx0000000000xx0000x\r\nx00000000xx0000xx0000xx0000x\r\nx00000000xx0000xx0000xx0000x\r\nx00000000xx0000xx0000000000x\r\nx00000000xx0000xx0000000000x\r\nxxxxx0000xx0000xx0000000000x\r\nxxxxx0000xx0000xx0000000000x\r\nxxxxx0000xx0000xxxxxxxxxxxxx\r\nxxxxx0000xx0000xxxxxxxxxxxxx\r\nx00000000xx0000000000000000x\r\nx00000000xx0000000000000000x\r\nx00000000xx0000000000000000x\r\nx00000000xx0000000000000000x\r\nx0000xxxxxxxxxxxxxxxxxx0000x\r\nx0000xxxxxxxxxxxxxxxxxx0000x\r\nx00000000000000000000000000x\r\nx00000000000000000000000000x\r\nx00000000000000000000000000x\r\nx00000000000000000000000000x\r\nxxxxxxxxxxxxxxxxxxxxxxxxxxxx"
		};

		public static readonly RoomModel Model_z = new RoomModel (25, "model_z") {
			Door = new Vector (0, 9, 0),
			DoorOrientation = 2,
			ClubOnly = true,
			Heightmap = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx\r\nxxxxxxxxxxx00000000000000000000\r\nxxxxxxxxxxx00000000000000000000\r\nxxxxxxxxxxx00000000000000000000\r\nx00000000xx00000000000000000000\r\nx00000000xx00000000000000000000\r\nx00000000xx00000000000000000000\r\nx00000000xx00000000000000000000\r\nx00000000xx00000000000000000000\r\n000000000xx00000000000000000000\r\nx00000000xx00000000000000000000\r\nx00000000xx00000000000000000000\r\nx00000000xx00000000000000000000\r\nx00000000xx00000000000000000000\r\nx00000000xx00000000000000000000\r\nx00000000xx00000000000000000000\r\nxxxxxxxxxxx00000000000000000000\r\nxxxxxxxxxxx00000000000000000000\r\nxxxxxxxxxxx00000000000000000000\r\nxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx"
		};

		public static readonly RoomModel Model_0 = new RoomModel (26, "model_0") {
			Door = new Vector (0, 4, 0),
			DoorOrientation = 2,
			ClubOnly = true,
			Heightmap = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx\r\nx00000000xx00000000xx00000000xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx\r\nx00000000xx00000000xx00000000xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx\r\nx00000000xx00000000xx00000000xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx0000\r\n000000000xx00000000xx00000000xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx0000\r\nx00000000xx00000000xx00000000xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx0000\r\nx00000000xx00000000xx00000000xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx0000\r\nx00000000xx00000000xx00000000xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx\r\nx00000000xx00000000xx00000000xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx\r\nxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx"
		};

		public static readonly RoomModel Model_1 = new RoomModel (27, "model_1") {
			Door = new Vector (0, 10, 10),
			DoorOrientation = 2,
			ClubOnly = true,
			Heightmap = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx\r\nxeeeeeeeeeeeeeeeedcba9888888888888\r\nxeeeeeeeeeeeeeeeexxxxxx88888888888\r\nxeeeeeeeeeeeeeeeexxxxxx88888888888\r\nxeeeeeeeeeeeeeeeexxxxxx88888888888\r\nxeeeeeeeeeeeeeeeexxxxxx88888888888\r\nxdxxxxxxxxxxxxxxxxxxxxx88888888888\r\nxcxxxxxxxxxxxxxxxxxxxxx88888888888\r\nxbxxxxxxxxxxxxxxxxxxxxx88888888888\r\nxaxxxxxxxxxxxxxxxxxxxxx88888888888\r\naaaaaaaaaaaaaaaaaxxxxxxxxxxxxxxxxx\r\nxaaaaaaaaaaaaaaaaxxxxxxxxxxxxxxxxx\r\nxaaaaaaaaaaaaaaaaxxxxxxxxxxxxxxxxx\r\nxaaaaaaaaaaaaaaaaxxxx6666666666666\r\nxaaaaaaaaaaaaaaaaxxxx6666666666666\r\nxaaaaaaaaaaaaaaaaxxxx6666666666666\r\nxaaaaaaaaaaaaaaaaxxxx6666666666666\r\nxaaaaaaaaaaaaaaaaxxxx6666666666666\r\nxaaaaaaaaaaaaaaaa98766666666666666\r\nxaaaaaaaaaaaaaaaaxxxxxxxxxxxx5xxxx\r\nxaaaaaaaaaaaaaaaaxxxxxxxxxxxx4xxxx\r\nxaaaaaaaaaaaaaaaaxxxxxxxxxxxx3xxxx\r\nxaaaaaaaaaaaaaaaaxxx3333333333xxxx\r\nxaaaaaaaaaaaaaaaaxxx3333333333xxxx\r\nxaaaaaaaaaaaaaaaaxxx3333333333xxxx\r\nxaaaaaaaaaaaaaaaaxxx3333333333xxxx\r\nxaaaaaaaaaaaaaaaaxxx3333333333xxxx\r\nxaaaaaaaaaaaaaaaaxxx3333333333xxxx\r\nxaaaaaaaaaaaaaaaaxxx3333333333xxxx\r\nxaaaaaaaaaaaaaaaaxxx3333333333xxxx\r\nxaaaaaaaaaaaaaaaaxxx3333333333xxxx\r\nxaaaaaaaaaaaaaaaaxxx3333333333xxxx\r\nxxxxxxxxxxxxxxxx9xxx3333333333xxxx\r\nxxxxxxxxxxxxxxxx8xxx3333333333xxxx\r\nxxxxxxxxxxxxxxxx7xxx3333333333xxxx\r\nxxx777777777xxxx6xxx3333333333xxxx\r\nxxx777777777xxxx5xxxxxxxxxxxxxxxxx\r\nxxx777777777xxxx4xxxxxxxxxxxxxxxxx\r\nxxx777777777xxxx3xxxxxxxxxxxxxxxxx\r\nxxx777777777xxxx2xxxxxxxxxxxxxxxxx\r\nxfffffffffxxxxxx1xxxxxxxxxxxxxxxxx\r\nxfffffffffxxxxxx111111111111111111\r\nxfffffffffxxxxxx111111111111111111\r\nxfffffffffxxxxxx111111111111111111\r\nxfffffffffxxxxxx111111111111111111\r\nxfffffffffxxxxxx111111111111111111\r\nxfffffffffxxxxxx111111111111111111\r\nxxxxxxxxxxxxxxxx111111111111111111\r\nxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx"
		};

		public static readonly RoomModel Model_2 = new RoomModel (28, "model_2") {
			Door = new Vector (0, 15, 14),
			DoorOrientation = 2,
			ClubOnly = false,
			Heightmap = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxx\r\nxjjjjjjjjjjjjjx0000xxxxxxxxxx\r\nxjjjjjjjjjjjjjx0000xxxxxxxxxx\r\nxjjjjjjjjjjjjjx0000xxxxxxxxxx\r\nxjjjjjjjjjjjjjx0000xxxxxxxxxx\r\nxjjjjjjjjjjjjjx0000xxxxxxxxxx\r\nxjjjjjjjjjjjjjx0000xxxxxxxxxx\r\nxjjjjjjjjjjjjjx0000xxxxxxxxxx\r\nxjjjjjjjjjjjjjx0000xxxxxxxxxx\r\nxxxxxxxxxxxxiix0000xxxxxxxxxx\r\nxxxxxxxxxxxxhhx0000xxxxxxxxxx\r\nxxxxxxxxxxxxggx0000xxxxxxxxxx\r\nxxxxxxxxxxxxffx0000xxxxxxxxxx\r\nxxxxxxxxxxxxeex0000xxxxxxxxxx\r\nxeeeeeeeeeeeeex0000xxxxxxxxxx\r\neeeeeeeeeeeeeex0000xxxxxxxxxx\r\nxeeeeeeeeeeeeex0000xxxxxxxxxx\r\nxeeeeeeeeeeeeex0000xxxxxxxxxx\r\nxeeeeeeeeeeeeex0000xxxxxxxxxx\r\nxeeeeeeeeeeeeex0000xxxxxxxxxx\r\nxeeeeeeeeeeeeex0000xxxxxxxxxx\r\nxeeeeeeeeeeeeex0000xxxxxxxxxx\r\nxeeeeeeeeeeeeex0000xxxxxxxxxx\r\nxeeeeeeeeeeeeex0000xxxxxxxxxx\r\nxxxxxxxxxxxxddx00000000000000\r\nxxxxxxxxxxxxccx00000000000000\r\nxxxxxxxxxxxxbbx00000000000000\r\nxxxxxxxxxxxxaax00000000000000\r\nxaaaaaaaaaaaaax00000000000000\r\nxaaaaaaaaaaaaax00000000000000\r\nxaaaaaaaaaaaaax00000000000000\r\nxaaaaaaaaaaaaax00000000000000\r\nxaaaaaaaaaaaaax00000000000000\r\nxaaaaaaaaaaaaax00000000000000\r\nxaaaaaaaaaaaaax00000000000000\r\nxaaaaaaaaaaaaax00000000000000\r\nxaaaaaaaaaaaaax00000000000000\r\nxaaaaaaaaaaaaax00000000000000\r\nxxxxxxxxxxxx99x0000xxxxxxxxxx\r\nxxxxxxxxxxxx88x0000xxxxxxxxxx\r\nxxxxxxxxxxxx77x0000xxxxxxxxxx\r\nxxxxxxxxxxxx66x0000xxxxxxxxxx\r\nxxxxxxxxxxxx55x0000xxxxxxxxxx\r\nxxxxxxxxxxxx44x0000xxxxxxxxxx\r\nx4444444444444x0000xxxxxxxxxx\r\nx4444444444444x0000xxxxxxxxxx\r\nx4444444444444x0000xxxxxxxxxx\r\nx4444444444444x0000xxxxxxxxxx\r\nx4444444444444x0000xxxxxxxxxx\r\nx4444444444444x0000xxxxxxxxxx\r\nx4444444444444x0000xxxxxxxxxx\r\nx4444444444444x0000xxxxxxxxxx\r\nx4444444444444x0000xxxxxxxxxx\r\nx4444444444444x0000xxxxxxxxxx\r\nxxxxxxxxxxxx33x0000xxxxxxxxxx\r\nxxxxxxxxxxxx22x0000xxxxxxxxxx\r\nxxxxxxxxxxxx11x0000xxxxxxxxxx\r\nxxxxxxxxxxxx00x0000xxxxxxxxxx\r\nx000000000000000000xxxxxxxxxx\r\nx000000000000000000xxxxxxxxxx\r\nx000000000000000000xxxxxxxxxx\r\nx000000000000000000xxxxxxxxxx\r\nx000000000000000000xxxxxxxxxx\r\nx000000000000000000xxxxxxxxxx\r\nx000000000000000000xxxxxxxxxx\r\nx000000000000000000xxxxxxxxxx\r\nxxxxxxxxxxxxxxxxxxxxxxxxxxxxx\r\nxxxxxxxxxxxxxxxxxxxxxxxxxxxxx"
		};

		public static readonly RoomModel Model_3 = new RoomModel (29, "model_3") {
			Door = new Vector (0, 10, 0),
			DoorOrientation = 2,
			ClubOnly = true,
			Heightmap = "xxxxxxxxxxxxxxxxx\r\nxxx0000000000000x\r\nxxx0000000000000x\r\nxxx0000000000000x\r\nxxx0000000000000x\r\nxxx0000000000000x\r\nxxx0000000000000x\r\nx000000000000000x\r\nx000000000000000x\r\nx000000000000000x\r\n0000000000000000x\r\nx000000000000000x\r\nx000000000000000x\r\nx000000000000000x\r\nxxxxxxxxxxxxxxxxx"
		};

		public static readonly RoomModel Model_4 = new RoomModel (30, "model_4") {
			Door = new Vector (0, 10, 0),
			DoorOrientation = 2,
			ClubOnly = true,
			Heightmap = "xxxxxxxxxxxxxxxxxxxxx\r\nxxxxxxxxxaaaaaaaaaaax\r\nxxxxxxxxxaaaaaaaaaaax\r\nxxxxxxxxxaaaaaaaaaaax\r\nxxxxxxxxxaaaaaaaaaaax\r\nx00000000xxxxxaaaaaax\r\nx00000000xxxxxaaaaaax\r\nx00000000xxxxxaaaaaax\r\nx00000000xxxxxaaaaaax\r\nx0000000000000aaaaaax\r\n00000000000000aaaaaax\r\nx0000000000000aaaaaax\r\nx0000000000000aaaaaax\r\nx0000000000000xxxxxxx\r\nx0000000000000xxxxxxx\r\nx0000000000000xxxxxxx\r\nx0000000000000xxxxxxx\r\nx0000000000000xxxxxxx\r\nx0000000000000xxxxxxx\r\nxxxxxxxxxxxxxxxxxxxxx"
		};

		public static readonly RoomModel Model_5 = new RoomModel (31, "model_5") {
			Door = new Vector (0, 10, 0),
			DoorOrientation = 2,
			ClubOnly = false,
			Heightmap = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx\r\nx00000000000000000000000000000000x\r\nx00000000000000000000000000000000x\r\nx00000000000000000000000000000000x\r\nx00000000000000000000000000000000x\r\nx00000000000000000000000000000000x\r\nx00000000000000000000000000000000x\r\nx00000000000000000000000000000000x\r\nx00000000000000000000000000000000x\r\nx00000000000000000000000000000000x\r\n000000000000000000000000000000000x\r\nx00000000000000000000000000000000x\r\nx00000000000000000000000000000000x\r\nx00000000000000000000000000000000x\r\nx00000000000000000000000000000000x\r\nx00000000000000000000000000000000x\r\nx00000000000000000000000000000000x\r\nx00000000000000000000000000000000x\r\nx00000000000000000000000000000000x\r\nx00000000000000000000000000000000x\r\nx00000000000000000000000000000000x\r\nx00000000000000000000000000000000x\r\nx00000000000000000000000000000000x\r\nx00000000000000000000000000000000x\r\nx00000000000000000000000000000000x\r\nx00000000000000000000000000000000x\r\nx00000000000000000000000000000000x\r\nx00000000000000000000000000000000x\r\nx00000000000000000000000000000000x\r\nx00000000000000000000000000000000x\r\nx00000000000000000000000000000000x\r\nx00000000000000000000000000000000x\r\nx00000000000000000000000000000000x\r\nxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx"
		};

		public static readonly RoomModel Model_6 = new RoomModel (32, "model_6") {
			Door = new Vector (0, 15, 0),
			DoorOrientation = 2,
			ClubOnly = false,
			Heightmap = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx\r\nx222222222x000000000000000000000000xxxx\r\nx222222222x000000000000000000000000xxxx\r\nx222222222x000000000000000000000000xxxx\r\nx222222222x000000000000000000000000xxxx\r\nx222222222x000000000000000000000000xxxx\r\nx222222222x000000000000000000000000xxxx\r\nx222222222x000000000000000000000000xxxx\r\nx222222222x000000000000000000000000xxxx\r\nx222222222x00000000xxxxxxxx00000000xxxx\r\nx11xxxxxxxx00000000xxxxxxxx00000000xxxx\r\nx00x000000000000000xxxxxxxx00000000xxxx\r\nx00x000000000000000xxxxxxxx00000000xxxx\r\nx000000000000000000xxxxxxxx00000000xxxx\r\nx000000000000000000xxxxxxxx00000000xxxx\r\n0000000000000000000xxxxxxxx00000000xxxx\r\nx000000000000000000xxxxxxxx00000000xxxx\r\nx00x000000000000000xxxxxxxx00000000xxxx\r\nx00x000000000000000xxxxxxxx00000000xxxx\r\nx00xxxxxxxxxxxxxxxxxxxxxxxx00000000xxxx\r\nx00xxxxxxxxxxxxxxxxxxxxxxxx00000000xxxx\r\nx00x0000000000000000000000000000000xxxx\r\nx00x0000000000000000000000000000000xxxx\r\nx0000000000000000000000000000000000xxxx\r\nx0000000000000000000000000000000000xxxx\r\nx0000000000000000000000000000000000xxxx\r\nx0000000000000000000000000000000000xxxx\r\nx00x0000000000000000000000000000000xxxx\r\nx00x0000000000000000000000000000000xxxx\r\nxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx\r\nxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx\r\nxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx"
		};

		public static readonly RoomModel Model_7 = new RoomModel (33, "model_7") {
			Door = new Vector (0, 17, 0),
			DoorOrientation = 2,
			ClubOnly = false,
			Heightmap = "xxxxxxxxxxxxxxxxxxxxxxxxx\r\nx222222xx00000000xxxxxxxx\r\nx222222xx00000000xxxxxxxx\r\nx2222221000000000xxxxxxxx\r\nx2222221000000000xxxxxxxx\r\nx222222xx00000000xxxxxxxx\r\nx222222xx00000000xxxxxxxx\r\nx222222xxxxxxxxxxxxxxxxxx\r\nx222222xkkkkkkxxiiiiiiiix\r\nx222222xkkkkkkxxiiiiiiiix\r\nx222222xkkkkkkjiiiiiiiiix\r\nx222222xkkkkkkjiiiiiiiiix\r\nx222222xkkkkkkxxiiiiiiiix\r\nxxx11xxxkkkkkkxxiiiiiiiix\r\nxxx00xxxkkkkkkxxxxxxxxxxx\r\nx000000xkkkkkkxxxxxxxxxxx\r\nx000000xkkkkkkxxxxxxxxxxx\r\n0000000xkkkkkkxxxxxxxxxxx\r\nx000000xkkkkkkxxxxxxxxxxx\r\nx000000xkkkkkkxxxxxxxxxxx\r\nx000000xxxjjxxxxxxxxxxxxx\r\nx000000xxxiixxxxxxxxxxxxx\r\nx000000xiiiiiixxxxxxxxxxx\r\nxxxxxxxxiiiiiixxxxxxxxxxx\r\nxxxxxxxxiiiiiixxxxxxxxxxx\r\nxxxxxxxxiiiiiixxxxxxxxxxx\r\nxxxxxxxxiiiiiixxxxxxxxxxx\r\nxxxxxxxxiiiiiixxxxxxxxxxx\r\nxxxxxxxxiiiiiixxxxxxxxxxx\r\nxxxxxxxxiiiiiixxxxxxxxxxx"
		};

		public static readonly RoomModel Model_8 = new RoomModel (34, "model_8") {
			Door = new Vector (0, 15, 5),
			DoorOrientation = 2,
			ClubOnly = false,
			Heightmap = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx\r\nx5555555555555555555555555xxxxxxxxx\r\nx5555555555555555555555555xxxxxxxxx\r\nx5555555555555555555555555xxxxxxxxx\r\nx5555555555555555555555555xxxxxxxxx\r\nx5555555555555555555555555xxxxxxxxx\r\nx5555555555555555555555555xxxxxxxxx\r\nx5555555555xxxxxxxxxxxxxxxxxxxxxxxx\r\nx55555555554321000000000000000000xx\r\nx55555555554321000000000000000000xx\r\nx5555555555xxxxx00000000000000000xx\r\nx555555x44x0000000000000000000000xx\r\nx555555x33x0000000000000000000000xx\r\nx555555x22x0000000000000000000000xx\r\nx555555x11x0000000000000000000000xx\r\n5555555x00x0000000000000000000000xx\r\nx555555x0000000000000000000000000xx\r\nx555555x0000000000000000000000000xx\r\nx555555x0000000000000000000000000xx\r\nx555555x0000000000000000000000000xx\r\nx555555x0000000000000000000000000xx\r\nx555555x0000000000000000000000000xx\r\nx555555x0000000000000000000000000xx\r\nx555555x0000000000000000000000000xx\r\nx555555x0000000000000000000000000xx\r\nx555555x0000000000000000000000000xx\r\nxxxxxxxx0000000000000000000000000xx\r\nxxxxxxxx0000000000000000000000000xx\r\nxxxxxxxx0000000000000000000000000xx\r\nxxxxxxxx0000000000000000000000000xx\r\nxxxxxxxx0000000000000000000000000xx\r\nxxxxxxxx0000000000000000000000000xx\r\nxxxxxxxx0000000000000000000000000xx\r\nxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx"
		};

		public static readonly RoomModel Model_9 = new RoomModel (35, "model_9") {
			Door = new Vector (0, 17, 0),
			DoorOrientation = 2,
			ClubOnly = false,
			Heightmap = "xxxxxxxxxxxxxxxxxxxxxxxx\r\nx0000000000000000000000x\r\nx0000000000000000000000x\r\nx0000000000000000000000x\r\nx0000000000000000000000x\r\nx0000000000000000000000x\r\nx0000000000000000000000x\r\nx0000000000000000000000x\r\nx0000000000000000000000x\r\nx0000000000000000000000x\r\nx0000000000000000000000x\r\nx0000000000000000000000x\r\nx0000000000000000000000x\r\nx0000000000000000000000x\r\nx0000000000000000000000x\r\nx0000000000000000000000x\r\nx0000000000000000000000x\r\n00000000000000000000000x\r\nx0000000000000000000000x\r\nx0000000000000000000000x\r\nx0000000000000000000000x\r\nx0000000000000000000000x\r\nx0000000000000000000000x\r\nx0000000000000000000000x\r\nx0000000000000000000000x\r\nx0000000000000000000000x\r\nx0000000000000000000000x\r\nx0000000000000000000000x\r\nx0000000000000000000000x\r\nx0000000000000000000000x\r\nx0000000000000000000000x\r\nx0000000000000000000000x\r\nx0000000000000000000000x\r\nx0000000000000000000000x\r\nx0000000000000000000000x\r\nx0000000000000000000000x\r\nx0000000000000000000000x\r\nx0000000000000000000000x\r\nx0000000000000000000000x\r\nx0000000000000000000000x\r\nx0000000000000000000000x\r\nx0000000000000000000000x\r\nx0000000000000000000000x\r\nxxxxxxxxxxxxxxxxxxxxxxxx"
		};

		public static readonly RoomModel Model_snowwar1 = new RoomModel (36, "model_snowwar1") {
			Door = new Vector (0, 16, 0),
			DoorOrientation = 2,
			ClubOnly = false,
			Heightmap = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx\r\nxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx\r\nxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx\r\nxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx\r\nxxxxxxxxxxx0000000000000000xxxxxxxxxxxxxxxxxxx\r\nxxxxxxxxxx00000000000000000xxxxxxxxxxxxxxxxxxx\r\nxxxxxxxx00000000000000000000xxxxxxxxxxxxxxxxxx\r\nxxxxxxx0000000000000000000000xxxxxxxxxxxxxxxxx\r\nxxxxxx000000000000000000000000xxxxxxxxxxxxxxxx\r\nxxxxx0000000000000000000000000000xxxxxxxxxxxxx\r\nxxxx000000000000000000000000000000xxxxxxxxxxxx\r\nxxx00000000000000000000000000000000xxxxxxxxxxx\r\nxxx0000000000000000000000000000000000xxxxxxxxx\r\nxx000000000000000000000000000000000000xxxxxxxx\r\nxx0000000000000000000000000000000000000xxxxxxx\r\nxx0000000000000000000000000000000000000xxxxxxx\r\nx00000000000000000000000000000000000000xxxxxxx\r\nxx00000000000000000000000000000000000000xxxxxx\r\nx0000000000000000000000000000000000000000xxxxx\r\nx00000000000000000000000000000000000000000xxxx\r\nx00000000000000000000000000000000000000000xxxx\r\nx000000000000000000000000000000000000000000xxx\r\nxx000000000000000000000000000000000000000000xx\r\nxx00000000000000000000000000000000000000000000\r\nxx00000000000000000000000000000000000000000000\r\nxx00000000000000000000000000000000000000000000\r\nxx00000000000000000000000000000000000000000000\r\nxx00000000000000000000000000000000000000000000\r\nxxx0000000000000000000000000000000000000000000\r\nxxxx00000000000000000000000000000000000000xx0x\r\nxxxxx0000000000000000000000000000000000000xxxx\r\nxxxxxxx000000000000000000000000000000000xxxxxx\r\nxxxxxxx0000000000000000000000000000000000xxxxx\r\nxxxxxxx000000000000000000000000000000000xxxxxx\r\nxxxxxxxxx0000000000000000000000000000000xxxxxx\r\nxxxxxxxxxx00000000000000000000000000000xxxxxxx\r\nxxxxxxxxxxx0000000000000000000000000000xxxxxxx\r\nxxxxxxxxxxxxx00000000000000000000000000xxxxxxx\r\nxxxxxxxxxxxxxx0000000000000000000000000xxxxxxx\r\nxxxxxxxxxxxxxxx00000000000000000000000xxxxxxxx\r\nxxxxxxxxxxxxxxx00000000000000000000000xxxxxxxx\r\nxxxxxxxxxxxxxxxx0xxxx000000000000000000xxxxxxx\r\nxxxxxxxxxxxxxxxxxxxxxx000000000000000xxxxxxxxx\r\nxxxxxxxxxxxxxxxxxxxxxx0000000000000xxxxxxxxxxx\r\nxxxxxxxxxxxxxxxxxxxxxx000000000000xxxxxxxxxxxx\r\nxxxxxxxxxxxxxxxxxxxxxxx000000xxxxxxxxxxxxxxxxx"
		};

		public static readonly RoomModel Model_snowwar2 = new RoomModel (37, "model_snowwar2") {
			Door = new Vector (0, 10, 0),
			DoorOrientation = 2,
			ClubOnly = false,
			Heightmap = "xxxxxxxxxxxxxxxxxxxxxx\r\nxxxxxx00000000xxxxxxxx\r\nxxxxx0000000000xxxxxxx\r\nxxxx0000000000000xxxxx\r\nxxx000000000000000xxxx\r\nxx00000000000000000xxx\r\nx0000000000000000000xx\r\nx0000000000000000000xx\r\nx00000000000000000000x\r\nx00000000000000000000x\r\nx000000000000000000000\r\nx000000000000000000000\r\nx000000000000000000000\r\nx000000000000000000000\r\nxx00000000000000000000\r\nxx00000000000000000000\r\nxxx0000000000000000000\r\nxxx0000000000000000000\r\nxxxx000000000000000000\r\nxxxxx0000000000000000x\r\nxxxxxxx000000000000xxx\r\nxxxxxxx0x00000000xxxxx\r\nxxxxxxxx0x000000xxxxxx\r\nxxxxxxxxxxxxxxxxxxxxxx"
		};

		public bool ClubOnly { get; protected set; }
		// TODO Enum!
		public int DoorOrientation { get; protected set; }

		public Vector Door { get; protected set; }

		public string Heightmap { get; protected set; }

		private RoomModel (int value, string displayName) : base (value, displayName)
		{
			
		}
	}
}