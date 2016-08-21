using System;
using System.Globalization;
using Yupi.Model.Domain.Components;
using Headspring;

namespace Yupi.Model.Domain
{
	public class RoomModel : Enumeration<RoomModel>
	{
		public static readonly RoomModel Model_a = new RoomModel (1, "model_a") {
			Door = new Vector3D (3, 5, 0),
			DoorOrientation = 2,
			ClubOnly = false,
			Heightmap = "xxxxxxxxxxxx\rxxxx00000000\rxxxx00000000\rxxxx00000000\rxxxx00000000\rxxx000000000\rxxxx00000000\rxxxx00000000\rxxxx00000000\rxxxx00000000\rxxxx00000000\rxxxx00000000\rxxxx00000000\rxxxx00000000\rxxxxxxxxxxxx\rxxxxxxxxxxxx"
		};

		public static readonly RoomModel Model_b = new RoomModel (2, "model_b") {
			Door = new Vector3D (0, 5, 0),
			DoorOrientation = 2,
			ClubOnly = false,
			Heightmap = "xxxxxxxxxxxx\rxxxxx0000000\rxxxxx0000000\rxxxxx0000000\rxxxxx0000000\r000000000000\rx00000000000\rx00000000000\rx00000000000\rx00000000000\rx00000000000\rxxxxxxxxxxxx\rxxxxxxxxxxxx\rxxxxxxxxxxxx\rxxxxxxxxxxxx\rxxxxxxxxxxxx"
		};

		public static readonly RoomModel Model_c = new RoomModel (3, "model_c") {
			Door = new Vector3D (4, 7, 0),
			DoorOrientation = 2,
			ClubOnly = false,
			Heightmap = "xxxxxxxxxxxx\rxxxxxxxxxxxx\rxxxxxxxxxxxx\rxxxxxxxxxxxx\rxxxxxxxxxxxx\rxxxxx000000x\rxxxxx000000x\rxxxx0000000x\rxxxxx000000x\rxxxxx000000x\rxxxxx000000x\rxxxxxxxxxxxx\rxxxxxxxxxxxx\rxxxxxxxxxxxx\rxxxxxxxxxxxx\rxxxxxxxxxxxx"
		};

		public static readonly RoomModel Model_d = new RoomModel (4, "model_d") {
			Door = new Vector3D (4, 7, 0),
			DoorOrientation = 2,
			ClubOnly = false,
			Heightmap = "xxxxxxxxxxxx\rxxxxx000000x\rxxxxx000000x\rxxxxx000000x\rxxxxx000000x\rxxxxx000000x\rxxxxx000000x\rxxxx0000000x\rxxxxx000000x\rxxxxx000000x\rxxxxx000000x\rxxxxx000000x\rxxxxx000000x\rxxxxx000000x\rxxxxx000000x\rxxxxxxxxxxxx"
		};

		public static readonly RoomModel Model_e = new RoomModel (5, "model_e") {
			Door = new Vector3D (1, 5, 0),
			DoorOrientation = 2,
			ClubOnly = false,
			Heightmap = "xxxxxxxxxxxx\rxxxxxxxxxxxx\rxxxxxxxxxxxx\rxx0000000000\rxx0000000000\rx00000000000\rxx0000000000\rxx0000000000\rxx0000000000\rxx0000000000\rxx0000000000\rxxxxxxxxxxxx\rxxxxxxxxxxxx\rxxxxxxxxxxxx\rxxxxxxxxxxxx\rxxxxxxxxxxxx"
		};

		public static readonly RoomModel Model_f = new RoomModel (6, "model_f") {
			Door = new Vector3D (2, 5, 0),
			DoorOrientation = 2,
			ClubOnly = false,
			Heightmap = "xxxxxxxxxxxx\rxxxxxxx0000x\rxxxxxxx0000x\rxxx00000000x\rxxx00000000x\rxx000000000x\rxxx00000000x\rx0000000000x\rx0000000000x\rx0000000000x\rx0000000000x\rxxxxxxxxxxxx\rxxxxxxxxxxxx\rxxxxxxxxxxxx\rxxxxxxxxxxxx\rxxxxxxxxxxxx"
		};

		public static readonly RoomModel Model_g = new RoomModel (7, "model_g") {
			Door = new Vector3D (1, 7, 1),
			DoorOrientation = 2,
			ClubOnly = true,
			Heightmap = "xxxxxxxxxxxx\rxxxxxxxxxxxx\rxxxxxxx00000\rxxxxxxx00000\rxxxxxxx00000\rxx1111000000\rxx1111000000\rx11111000000\rxx1111000000\rxx1111000000\rxxxxxxx00000\rxxxxxxx00000\rxxxxxxx00000\rxxxxxxxxxxxx\rxxxxxxxxxxxx\rxxxxxxxxxxxx\rxxxxxxxxxxxx"
		};

		public static readonly RoomModel Model_h = new RoomModel (8, "model_h") {
			Door = new Vector3D (4, 4, 1),
			DoorOrientation = 2,
			ClubOnly = true,
			Heightmap = "xxxxxxxxxxxx\rxxxxxxxxxxxx\rxxxxx111111x\rxxxxx111111x\rxxxx1111111x\rxxxxx111111x\rxxxxx111111x\rxxxxx000000x\rxxxxx000000x\rxxx00000000x\rxxx00000000x\rxxx00000000x\rxxx00000000x\rxxxxxxxxxxxx\rxxxxxxxxxxxx\rxxxxxxxxxxxx"
		};

		public static readonly RoomModel Model_i = new RoomModel (9, "model_i") {
			Door = new Vector3D (0, 10, 0),
			DoorOrientation = 2,
			ClubOnly = false,
			Heightmap = "xxxxxxxxxxxxxxxxx\rx0000000000000000\rx0000000000000000\rx0000000000000000\rx0000000000000000\rx0000000000000000\rx0000000000000000\rx0000000000000000\rx0000000000000000\rx0000000000000000\r00000000000000000\rx0000000000000000\rx0000000000000000\rx0000000000000000\rx0000000000000000\rx0000000000000000\rx0000000000000000\rx0000000000000000\rx0000000000000000\rx0000000000000000\rx0000000000000000\rx0000000000000000\rx0000000000000000\rx0000000000000000\rx0000000000000000\rx0000000000000000\rx0000000000000000\rxxxxxxxxxxxxxxxxx"
		};

		public static readonly RoomModel Model_j = new RoomModel (10, "model_j") {
			Door = new Vector3D (0, 10, 0),
			DoorOrientation = 2,
			ClubOnly = false,
			Heightmap = "xxxxxxxxxxxxxxxxxxxxx\rxxxxxxxxxxx0000000000\rxxxxxxxxxxx0000000000\rxxxxxxxxxxx0000000000\rxxxxxxxxxxx0000000000\rxxxxxxxxxxx0000000000\rxxxxxxxxxxx0000000000\rx00000000000000000000\rx00000000000000000000\rx00000000000000000000\r000000000000000000000\rx00000000000000000000\rx00000000000000000000\rx00000000000000000000\rx00000000000000000000\rx00000000000000000000\rx00000000000000000000\rx0000000000xxxxxxxxxx\rx0000000000xxxxxxxxxx\rx0000000000xxxxxxxxxx\rx0000000000xxxxxxxxxx\rx0000000000xxxxxxxxxx\rx0000000000xxxxxxxxxx\rxxxxxxxxxxxxxxxxxxxxx"
		};

		public static readonly RoomModel Model_k = new RoomModel (11, "model_k") {
			Door = new Vector3D (0, 13, 0),
			DoorOrientation = 2,
			ClubOnly = false,
			Heightmap = "xxxxxxxxxxxxxxxxxxxxxxxxx\rxxxxxxxxxxxxxxxxx00000000\rxxxxxxxxxxxxxxxxx00000000\rxxxxxxxxxxxxxxxxx00000000\rxxxxxxxxxxxxxxxxx00000000\rxxxxxxxxx0000000000000000\rxxxxxxxxx0000000000000000\rxxxxxxxxx0000000000000000\rxxxxxxxxx0000000000000000\rx000000000000000000000000\rx000000000000000000000000\rx000000000000000000000000\rx000000000000000000000000\r0000000000000000000000000\rx000000000000000000000000\rx000000000000000000000000\rx000000000000000000000000\rxxxxxxxxx0000000000000000\rxxxxxxxxx0000000000000000\rxxxxxxxxx0000000000000000\rxxxxxxxxx0000000000000000\rxxxxxxxxx0000000000000000\rxxxxxxxxx0000000000000000\rxxxxxxxxx0000000000000000\rxxxxxxxxx0000000000000000\rxxxxxxxxx0000000000000000\rxxxxxxxxx0000000000000000\rxxxxxxxxxxxxxxxxxxxxxxxxx"
		};

		public static readonly RoomModel Model_l = new RoomModel (12, "model_l") {
			Door = new Vector3D (0, 16, 0),
			DoorOrientation = 2,
			ClubOnly = false,
			Heightmap = "xxxxxxxxxxxxxxxxxxxxx\rx00000000000000000000\rx00000000000000000000\rx00000000000000000000\rx00000000000000000000\rx00000000000000000000\rx00000000000000000000\rx00000000000000000000\rx00000000000000000000\rx00000000xxxx00000000\rx00000000xxxx00000000\rx00000000xxxx00000000\rx00000000xxxx00000000\rx00000000xxxx00000000\rx00000000xxxx00000000\rx00000000xxxx00000000\r000000000xxxx00000000\rx00000000xxxx00000000\rx00000000xxxx00000000\rx00000000xxxx00000000\rx00000000xxxx00000000\rxxxxxxxxxxxxxxxxxxxxx"
		};

		public static readonly RoomModel Model_m = new RoomModel (13, "model_m") {
			Door = new Vector3D (0, 15, 0),
			DoorOrientation = 2,
			ClubOnly = false,
			Heightmap = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxx\rxxxxxxxxxxx00000000xxxxxxxxxx\rxxxxxxxxxxx00000000xxxxxxxxxx\rxxxxxxxxxxx00000000xxxxxxxxxx\rxxxxxxxxxxx00000000xxxxxxxxxx\rxxxxxxxxxxx00000000xxxxxxxxxx\rxxxxxxxxxxx00000000xxxxxxxxxx\rxxxxxxxxxxx00000000xxxxxxxxxx\rxxxxxxxxxxx00000000xxxxxxxxxx\rxxxxxxxxxxx00000000xxxxxxxxxx\rxxxxxxxxxxx00000000xxxxxxxxxx\rx0000000000000000000000000000\rx0000000000000000000000000000\rx0000000000000000000000000000\rx0000000000000000000000000000\r00000000000000000000000000000\rx0000000000000000000000000000\rx0000000000000000000000000000\rx0000000000000000000000000000\rxxxxxxxxxxx00000000xxxxxxxxxx\rxxxxxxxxxxx00000000xxxxxxxxxx\rxxxxxxxxxxx00000000xxxxxxxxxx\rxxxxxxxxxxx00000000xxxxxxxxxx\rxxxxxxxxxxx00000000xxxxxxxxxx\rxxxxxxxxxxx00000000xxxxxxxxxx\rxxxxxxxxxxx00000000xxxxxxxxxx\rxxxxxxxxxxx00000000xxxxxxxxxx\rxxxxxxxxxxx00000000xxxxxxxxxx\rxxxxxxxxxxx00000000xxxxxxxxxx\rxxxxxxxxxxxxxxxxxxxxxxxxxxxxx"
		};

		public static readonly RoomModel Model_n = new RoomModel (14, "model_n") {
			Door = new Vector3D (0, 16, 0),
			DoorOrientation = 2,
			ClubOnly = false,
			Heightmap = "xxxxxxxxxxxxxxxxxxxxx\rx00000000000000000000\rx00000000000000000000\rx00000000000000000000\rx00000000000000000000\rx00000000000000000000\rx00000000000000000000\rx000000xxxxxxxx000000\rx000000x000000x000000\rx000000x000000x000000\rx000000x000000x000000\rx000000x000000x000000\rx000000x000000x000000\rx000000x000000x000000\rx000000xxxxxxxx000000\rx00000000000000000000\r000000000000000000000\rx00000000000000000000\rx00000000000000000000\rx00000000000000000000\rx00000000000000000000\rxxxxxxxxxxxxxxxxxxxxx"
		};

		public static readonly RoomModel Model_o = new RoomModel (15, "model_o") {
			Door = new Vector3D (0, 18, 1),
			DoorOrientation = 2,
			ClubOnly = true,
			Heightmap = "xxxxxxxxxxxxxxxxxxxxxxxxx\rxxxxxxxxxxxxx11111111xxxx\rxxxxxxxxxxxxx11111111xxxx\rxxxxxxxxxxxxx11111111xxxx\rxxxxxxxxxxxxx11111111xxxx\rxxxxxxxxxxxxx11111111xxxx\rxxxxxxxxxxxxx11111111xxxx\rxxxxxxxxxxxxx11111111xxxx\rxxxxxxxxxxxxx00000000xxxx\rxxxxxxxxx0000000000000000\rxxxxxxxxx0000000000000000\rxxxxxxxxx0000000000000000\rxxxxxxxxx0000000000000000\rxxxxxxxxx0000000000000000\rxxxxxxxxx0000000000000000\rx111111100000000000000000\rx111111100000000000000000\rx111111100000000000000000\r1111111100000000000000000\rx111111100000000000000000\rx111111100000000000000000\rx111111100000000000000000\rx111111100000000000000000\rxxxxxxxxx0000000000000000\rxxxxxxxxx0000000000000000\rxxxxxxxxx0000000000000000\rxxxxxxxxx0000000000000000\rxxxxxxxxxxxxxxxxxxxxxxxxx"
		};

		public static readonly RoomModel Model_p = new RoomModel (16, "model_p") {
			Door = new Vector3D (0, 23, 2),
			DoorOrientation = 2,
			ClubOnly = true,
			Heightmap = "xxxxxxxxxxxxxxxxxxx\rxxxxxxx222222222222\rxxxxxxx222222222222\rxxxxxxx222222222222\rxxxxxxx222222222222\rxxxxxxx222222222222\rxxxxxxx222222222222\rxxxxxxx22222222xxxx\rxxxxxxx11111111xxxx\rx222221111111111111\rx222221111111111111\rx222221111111111111\rx222221111111111111\rx222221111111111111\rx222221111111111111\rx222221111111111111\rx222221111111111111\rx2222xx11111111xxxx\rx2222xx00000000xxxx\rx2222xx000000000000\rx2222xx000000000000\rx2222xx000000000000\rx2222xx000000000000\r22222xx000000000000\rx2222xx000000000000\rxxxxxxxxxxxxxxxxxxx"
		};

		public static readonly RoomModel Model_q = new RoomModel (17, "model_q") {
			Door = new Vector3D (10, 4, 2),
			DoorOrientation = 2,
			ClubOnly = true,
			Heightmap = "xxxxxxxxxxxxxxxxxxx\rxxxxxxxxxxx22222222\rxxxxxxxxxxx22222222\rxxxxxxxxxxx22222222\rxxxxxxxxxx222222222\rxxxxxxxxxxx22222222\rxxxxxxxxxxx22222222\rx222222222222222222\rx222222222222222222\rx222222222222222222\rx222222222222222222\rx222222222222222222\rx222222222222222222\rx2222xxxxxxxxxxxxxx\rx2222xxxxxxxxxxxxxx\rx2222211111xx000000\rx222221111110000000\rx222221111110000000\rx2222211111xx000000\rxx22xxx1111xxxxxxxx\rxx11xxx1111xxxxxxxx\rx1111xx1111xx000000\rx1111xx111110000000\rx1111xx111110000000\rx1111xx1111xx000000\rxxxxxxxxxxxxxxxxxxx"
		};

		public static readonly RoomModel Model_r = new RoomModel (18, "model_r") {
			Door = new Vector3D (10, 4, 3),
			DoorOrientation = 2,
			ClubOnly = true,
			Heightmap = "xxxxxxxxxxxxxxxxxxxxxxxxx\rxxxxxxxxxxx33333333333333\rxxxxxxxxxxx33333333333333\rxxxxxxxxxxx33333333333333\rxxxxxxxxxx333333333333333\rxxxxxxxxxxx33333333333333\rxxxxxxxxxxx33333333333333\rxxxxxxx333333333333333333\rxxxxxxx333333333333333333\rxxxxxxx333333333333333333\rxxxxxxx333333333333333333\rxxxxxxx333333333333333333\rxxxxxxx333333333333333333\rx4444433333xxxxxxxxxxxxxx\rx4444433333xxxxxxxxxxxxxx\rx44444333333222xx000000xx\rx44444333333222xx000000xx\rxxx44xxxxxxxx22xx000000xx\rxxx33xxxxxxxx11xx000000xx\rxxx33322222211110000000xx\rxxx33322222211110000000xx\rxxxxxxxxxxxxxxxxx000000xx\rxxxxxxxxxxxxxxxxx000000xx\rxxxxxxxxxxxxxxxxx000000xx\rxxxxxxxxxxxxxxxxx000000xx\rxxxxxxxxxxxxxxxxxxxxxxxxx"
		};
				
		public static readonly RoomModel Model_t = new RoomModel (19, "model_t") {
			Door = new Vector3D (0, 3, 2),
			DoorOrientation = 2,
			ClubOnly = true,
			Heightmap = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxx\rx222222222222222222222222222x\rx222222222222222222222222222x\r2222222222222222222222222222x\rx222222222222222222222222222x\rx2222xxxxxx222222xxxxxxx2222x\rx2222xxxxxx111111xxxxxxx2222x\rx2222xx111111111111111xx2222x\rx2222xx111111111111111xx2222x\rx2222xx11xxx1111xxxx11xx2222x\rx2222xx11xxx0000xxxx11xx2222x\rx22222111x00000000xx11xx2222x\rx22222111x00000000xx11xx2222x\rx22222111x00000000xx11xx2222x\rx22222111x00000000xx11xx2222x\rx22222111x00000000xx11xx2222x\rx22222111x00000000xx11xx2222x\rx2222xx11xxxxxxxxxxx11xx2222x\rx2222xx11xxxxxxxxxxx11xx2222x\rx2222xx111111111111111xx2222x\rx2222xx111111111111111xx2222x\rx2222xxxxxxxxxxxxxxxxxxx2222x\rx2222xxxxxxxxxxxxxxxxxxx2222x\rx222222222222222222222222222x\rx222222222222222222222222222x\rx222222222222222222222222222x\rx222222222222222222222222222x\rxxxxxxxxxxxxxxxxxxxxxxxxxxxxx"
		};

		public static readonly RoomModel Model_u = new RoomModel (20, "model_u") {
			Door = new Vector3D (0, 17, 1),
			DoorOrientation = 2,
			ClubOnly = true,
			Heightmap = "xxxxxxxxxxxxxxxxxxxxxxxx\rx1111100000000000000000x\rx1111100000000000000000x\rx1111100000000000000000x\rx1111100000000000000000x\rx1111100000000000000000x\rx1111100000000000000000x\rx1111100000000000000000x\rx1111100000000000000000x\rx1111100000000000000000x\rx1111100000000000000000x\rx1111100000000000000000x\rx1111100000000000000000x\rx1111100000000000000000x\rx1111100000000000000000x\rx1111100000000000000000x\rx1111100000000000000000x\r11111100000000000000000x\rx1111100000000000000000x\rx1111100000000000000000x\rx1111100000000000000000x\rx1111100000000000000000x\rx1111100000000000000000x\rx1111100000000000000000x\rx1111100000000000000000x\rx1111100000000000000000x\rx1111100000000000000000x\rx1111100000000000000000x\rx1111100000000000000000x\rx1111100000000000000000x\rx1111100000000000000000x\rx1111100000000000000000x\rx1111100000000000000000x\rx1111100000000000000000x\rx1111100000000000000000x\rxxxxxxxxxxxxxxxxxxxxxxxx"
		};

		public static readonly RoomModel Model_v = new RoomModel (21, "model_v") {
			Door = new Vector3D (0, 3, 2),
			DoorOrientation = 2,
			ClubOnly = true,
			Heightmap = "xxxxxxxxxxxxxxxxxxxx\rx222221111111111111x\rx222221111111111111x\r2222221111111111111x\rx222221111111111111x\rx222221111111111111x\rx222221111111111111x\rxxxxxxxx1111xxxxxxxx\rxxxxxxxx0000xxxxxxxx\rx000000x0000x000000x\rx000000x0000x000000x\rx00000000000x000000x\rx00000000000x000000x\rx000000000000000000x\rx000000000000000000x\rxxxxxxxx00000000000x\rx000000x00000000000x\rx000000x0000xxxxxxxx\rx00000000000x000000x\rx00000000000x000000x\rx00000000000x000000x\rx00000000000x000000x\rxxxxxxxx0000x000000x\rx000000x0000x000000x\rx000000x0000x000000x\rx000000000000000000x\rx000000000000000000x\rx000000000000000000x\rx000000000000000000x\rxxxxxxxxxxxxxxxxxxxx"
		};

		public static readonly RoomModel Model_w = new RoomModel (22, "model_w") {
			Door = new Vector3D (0, 3, 2),
			DoorOrientation = 2,
			ClubOnly = true,
			Heightmap = "xxxxxxxxxxxxxxxxxxxxxxxxxxx\rx2222xx1111111111xx11111111\rx2222xx1111111111xx11111111\r222222111111111111111111111\rx22222111111111111111111111\rx22222111111111111111111111\rx22222111111111111111111111\rx2222xx1111111111xx11111111\rx2222xx1111111111xx11111111\rx2222xx1111111111xxxx1111xx\rx2222xx1111111111xxxx0000xx\rxxxxxxx1111111111xx00000000\rxxxxxxx1111111111xx00000000\rx22222111111111111000000000\rx22222111111111111000000000\rx22222111111111111000000000\rx22222111111111111000000000\rx2222xx1111111111xx00000000\rx2222xx1111111111xx00000000\rx2222xxxx1111xxxxxxxxxxxxxx\rx2222xxxx0000xxxxxxxxxxxxxx\rx2222x0000000000xxxxxxxxxxx\rx2222x0000000000xxxxxxxxxxx\rx2222x0000000000xxxxxxxxxxx\rx2222x0000000000xxxxxxxxxxx\rx2222x0000000000xxxxxxxxxxx\rx2222x0000000000xxxxxxxxxxx"
		};

		public static readonly RoomModel Model_x = new RoomModel (23, "model_x") {
			Door = new Vector3D (0, 12, 0),
			DoorOrientation = 2,
			ClubOnly = true,
			Heightmap = "xxxxxxxxxxxxxxxxxxxx\rx000000000000000000x\rx000000000000000000x\rx000000000000000000x\rx000000000000000000x\rx000000000000000000x\rx000000000000000000x\rxxx00xxx0000xxx00xxx\rx000000x0000x000000x\rx000000x0000x000000x\rx000000x0000x000000x\rx000000x0000x000000x\r0000000x0000x000000x\rx000000x0000x000000x\rx000000x0000x000000x\rx000000x0000x000000x\rx000000x0000x000000x\rx000000x0000x000000x\rx000000xxxxxx000000x\rx000000000000000000x\rx000000000000000000x\rx000000000000000000x\rx000000000000000000x\rx000000000000000000x\rx000000000000000000x\rxxxxxxxxxxxxxxxxxxxx"
		};

		public static readonly RoomModel Model_y = new RoomModel (24, "model_y") {
			Door = new Vector3D (0, 3, 0),
			DoorOrientation = 2,
			ClubOnly = true,
			Heightmap = "xxxxxxxxxxxxxxxxxxxxxxxxxxxx\rx00000000xx0000000000xx0000x\rx00000000xx0000000000xx0000x\r000000000xx0000000000xx0000x\rx00000000xx0000000000xx0000x\rx00000000xx0000xx0000xx0000x\rx00000000xx0000xx0000xx0000x\rx00000000xx0000xx0000000000x\rx00000000xx0000xx0000000000x\rxxxxx0000xx0000xx0000000000x\rxxxxx0000xx0000xx0000000000x\rxxxxx0000xx0000xxxxxxxxxxxxx\rxxxxx0000xx0000xxxxxxxxxxxxx\rx00000000xx0000000000000000x\rx00000000xx0000000000000000x\rx00000000xx0000000000000000x\rx00000000xx0000000000000000x\rx0000xxxxxxxxxxxxxxxxxx0000x\rx0000xxxxxxxxxxxxxxxxxx0000x\rx00000000000000000000000000x\rx00000000000000000000000000x\rx00000000000000000000000000x\rx00000000000000000000000000x\rxxxxxxxxxxxxxxxxxxxxxxxxxxxx"
		};

		public static readonly RoomModel Model_z = new RoomModel (25, "model_z") {
			Door = new Vector3D (0, 9, 0),
			DoorOrientation = 2,
			ClubOnly = true,
			Heightmap = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx\rxxxxxxxxxxx00000000000000000000\rxxxxxxxxxxx00000000000000000000\rxxxxxxxxxxx00000000000000000000\rx00000000xx00000000000000000000\rx00000000xx00000000000000000000\rx00000000xx00000000000000000000\rx00000000xx00000000000000000000\rx00000000xx00000000000000000000\r000000000xx00000000000000000000\rx00000000xx00000000000000000000\rx00000000xx00000000000000000000\rx00000000xx00000000000000000000\rx00000000xx00000000000000000000\rx00000000xx00000000000000000000\rx00000000xx00000000000000000000\rxxxxxxxxxxx00000000000000000000\rxxxxxxxxxxx00000000000000000000\rxxxxxxxxxxx00000000000000000000\rxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx"
		};

		public static readonly RoomModel Model_0 = new RoomModel (26, "model_0") {
			Door = new Vector3D (0, 4, 0),
			DoorOrientation = 2,
			ClubOnly = true,
			Heightmap = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx\rx00000000xx00000000xx00000000xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx\rx00000000xx00000000xx00000000xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx\rx00000000xx00000000xx00000000xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx0000\r000000000xx00000000xx00000000xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx0000\rx00000000xx00000000xx00000000xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx0000\rx00000000xx00000000xx00000000xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx0000\rx00000000xx00000000xx00000000xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx\rx00000000xx00000000xx00000000xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx\rxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx"
		};

		public static readonly RoomModel Model_1 = new RoomModel (27, "model_1") {
			Door = new Vector3D (0, 10, 10),
			DoorOrientation = 2,
			ClubOnly = true,
			Heightmap = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx\rxeeeeeeeeeeeeeeeedcba9888888888888\rxeeeeeeeeeeeeeeeexxxxxx88888888888\rxeeeeeeeeeeeeeeeexxxxxx88888888888\rxeeeeeeeeeeeeeeeexxxxxx88888888888\rxeeeeeeeeeeeeeeeexxxxxx88888888888\rxdxxxxxxxxxxxxxxxxxxxxx88888888888\rxcxxxxxxxxxxxxxxxxxxxxx88888888888\rxbxxxxxxxxxxxxxxxxxxxxx88888888888\rxaxxxxxxxxxxxxxxxxxxxxx88888888888\raaaaaaaaaaaaaaaaaxxxxxxxxxxxxxxxxx\rxaaaaaaaaaaaaaaaaxxxxxxxxxxxxxxxxx\rxaaaaaaaaaaaaaaaaxxxxxxxxxxxxxxxxx\rxaaaaaaaaaaaaaaaaxxxx6666666666666\rxaaaaaaaaaaaaaaaaxxxx6666666666666\rxaaaaaaaaaaaaaaaaxxxx6666666666666\rxaaaaaaaaaaaaaaaaxxxx6666666666666\rxaaaaaaaaaaaaaaaaxxxx6666666666666\rxaaaaaaaaaaaaaaaa98766666666666666\rxaaaaaaaaaaaaaaaaxxxxxxxxxxxx5xxxx\rxaaaaaaaaaaaaaaaaxxxxxxxxxxxx4xxxx\rxaaaaaaaaaaaaaaaaxxxxxxxxxxxx3xxxx\rxaaaaaaaaaaaaaaaaxxx3333333333xxxx\rxaaaaaaaaaaaaaaaaxxx3333333333xxxx\rxaaaaaaaaaaaaaaaaxxx3333333333xxxx\rxaaaaaaaaaaaaaaaaxxx3333333333xxxx\rxaaaaaaaaaaaaaaaaxxx3333333333xxxx\rxaaaaaaaaaaaaaaaaxxx3333333333xxxx\rxaaaaaaaaaaaaaaaaxxx3333333333xxxx\rxaaaaaaaaaaaaaaaaxxx3333333333xxxx\rxaaaaaaaaaaaaaaaaxxx3333333333xxxx\rxaaaaaaaaaaaaaaaaxxx3333333333xxxx\rxxxxxxxxxxxxxxxx9xxx3333333333xxxx\rxxxxxxxxxxxxxxxx8xxx3333333333xxxx\rxxxxxxxxxxxxxxxx7xxx3333333333xxxx\rxxx777777777xxxx6xxx3333333333xxxx\rxxx777777777xxxx5xxxxxxxxxxxxxxxxx\rxxx777777777xxxx4xxxxxxxxxxxxxxxxx\rxxx777777777xxxx3xxxxxxxxxxxxxxxxx\rxxx777777777xxxx2xxxxxxxxxxxxxxxxx\rxfffffffffxxxxxx1xxxxxxxxxxxxxxxxx\rxfffffffffxxxxxx111111111111111111\rxfffffffffxxxxxx111111111111111111\rxfffffffffxxxxxx111111111111111111\rxfffffffffxxxxxx111111111111111111\rxfffffffffxxxxxx111111111111111111\rxfffffffffxxxxxx111111111111111111\rxxxxxxxxxxxxxxxx111111111111111111\rxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx"
		};

		public static readonly RoomModel Model_2 = new RoomModel (28, "model_2") {
			Door = new Vector3D (0, 15, 14),
			DoorOrientation = 2,
			ClubOnly = false,
			Heightmap = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxx\rxjjjjjjjjjjjjjx0000xxxxxxxxxx\rxjjjjjjjjjjjjjx0000xxxxxxxxxx\rxjjjjjjjjjjjjjx0000xxxxxxxxxx\rxjjjjjjjjjjjjjx0000xxxxxxxxxx\rxjjjjjjjjjjjjjx0000xxxxxxxxxx\rxjjjjjjjjjjjjjx0000xxxxxxxxxx\rxjjjjjjjjjjjjjx0000xxxxxxxxxx\rxjjjjjjjjjjjjjx0000xxxxxxxxxx\rxxxxxxxxxxxxiix0000xxxxxxxxxx\rxxxxxxxxxxxxhhx0000xxxxxxxxxx\rxxxxxxxxxxxxggx0000xxxxxxxxxx\rxxxxxxxxxxxxffx0000xxxxxxxxxx\rxxxxxxxxxxxxeex0000xxxxxxxxxx\rxeeeeeeeeeeeeex0000xxxxxxxxxx\reeeeeeeeeeeeeex0000xxxxxxxxxx\rxeeeeeeeeeeeeex0000xxxxxxxxxx\rxeeeeeeeeeeeeex0000xxxxxxxxxx\rxeeeeeeeeeeeeex0000xxxxxxxxxx\rxeeeeeeeeeeeeex0000xxxxxxxxxx\rxeeeeeeeeeeeeex0000xxxxxxxxxx\rxeeeeeeeeeeeeex0000xxxxxxxxxx\rxeeeeeeeeeeeeex0000xxxxxxxxxx\rxeeeeeeeeeeeeex0000xxxxxxxxxx\rxxxxxxxxxxxxddx00000000000000\rxxxxxxxxxxxxccx00000000000000\rxxxxxxxxxxxxbbx00000000000000\rxxxxxxxxxxxxaax00000000000000\rxaaaaaaaaaaaaax00000000000000\rxaaaaaaaaaaaaax00000000000000\rxaaaaaaaaaaaaax00000000000000\rxaaaaaaaaaaaaax00000000000000\rxaaaaaaaaaaaaax00000000000000\rxaaaaaaaaaaaaax00000000000000\rxaaaaaaaaaaaaax00000000000000\rxaaaaaaaaaaaaax00000000000000\rxaaaaaaaaaaaaax00000000000000\rxaaaaaaaaaaaaax00000000000000\rxxxxxxxxxxxx99x0000xxxxxxxxxx\rxxxxxxxxxxxx88x0000xxxxxxxxxx\rxxxxxxxxxxxx77x0000xxxxxxxxxx\rxxxxxxxxxxxx66x0000xxxxxxxxxx\rxxxxxxxxxxxx55x0000xxxxxxxxxx\rxxxxxxxxxxxx44x0000xxxxxxxxxx\rx4444444444444x0000xxxxxxxxxx\rx4444444444444x0000xxxxxxxxxx\rx4444444444444x0000xxxxxxxxxx\rx4444444444444x0000xxxxxxxxxx\rx4444444444444x0000xxxxxxxxxx\rx4444444444444x0000xxxxxxxxxx\rx4444444444444x0000xxxxxxxxxx\rx4444444444444x0000xxxxxxxxxx\rx4444444444444x0000xxxxxxxxxx\rx4444444444444x0000xxxxxxxxxx\rxxxxxxxxxxxx33x0000xxxxxxxxxx\rxxxxxxxxxxxx22x0000xxxxxxxxxx\rxxxxxxxxxxxx11x0000xxxxxxxxxx\rxxxxxxxxxxxx00x0000xxxxxxxxxx\rx000000000000000000xxxxxxxxxx\rx000000000000000000xxxxxxxxxx\rx000000000000000000xxxxxxxxxx\rx000000000000000000xxxxxxxxxx\rx000000000000000000xxxxxxxxxx\rx000000000000000000xxxxxxxxxx\rx000000000000000000xxxxxxxxxx\rx000000000000000000xxxxxxxxxx\rxxxxxxxxxxxxxxxxxxxxxxxxxxxxx\rxxxxxxxxxxxxxxxxxxxxxxxxxxxxx"
		};

		public static readonly RoomModel Model_3 = new RoomModel (29, "model_3") {
			Door = new Vector3D (0, 10, 0),
			DoorOrientation = 2,
			ClubOnly = true,
			Heightmap = "xxxxxxxxxxxxxxxxx\rxxx0000000000000x\rxxx0000000000000x\rxxx0000000000000x\rxxx0000000000000x\rxxx0000000000000x\rxxx0000000000000x\rx000000000000000x\rx000000000000000x\rx000000000000000x\r0000000000000000x\rx000000000000000x\rx000000000000000x\rx000000000000000x\rxxxxxxxxxxxxxxxxx"
		};

		public static readonly RoomModel Model_4 = new RoomModel (30, "model_4") {
			Door = new Vector3D (0, 10, 0),
			DoorOrientation = 2,
			ClubOnly = true,
			Heightmap = "xxxxxxxxxxxxxxxxxxxxx\rxxxxxxxxxaaaaaaaaaaax\rxxxxxxxxxaaaaaaaaaaax\rxxxxxxxxxaaaaaaaaaaax\rxxxxxxxxxaaaaaaaaaaax\rx00000000xxxxxaaaaaax\rx00000000xxxxxaaaaaax\rx00000000xxxxxaaaaaax\rx00000000xxxxxaaaaaax\rx0000000000000aaaaaax\r00000000000000aaaaaax\rx0000000000000aaaaaax\rx0000000000000aaaaaax\rx0000000000000xxxxxxx\rx0000000000000xxxxxxx\rx0000000000000xxxxxxx\rx0000000000000xxxxxxx\rx0000000000000xxxxxxx\rx0000000000000xxxxxxx\rxxxxxxxxxxxxxxxxxxxxx"
		};

		public static readonly RoomModel Model_5 = new RoomModel (31, "model_5") {
			Door = new Vector3D (0, 10, 0),
			DoorOrientation = 2,
			ClubOnly = false,
			Heightmap = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx\rx00000000000000000000000000000000x\rx00000000000000000000000000000000x\rx00000000000000000000000000000000x\rx00000000000000000000000000000000x\rx00000000000000000000000000000000x\rx00000000000000000000000000000000x\rx00000000000000000000000000000000x\rx00000000000000000000000000000000x\rx00000000000000000000000000000000x\r000000000000000000000000000000000x\rx00000000000000000000000000000000x\rx00000000000000000000000000000000x\rx00000000000000000000000000000000x\rx00000000000000000000000000000000x\rx00000000000000000000000000000000x\rx00000000000000000000000000000000x\rx00000000000000000000000000000000x\rx00000000000000000000000000000000x\rx00000000000000000000000000000000x\rx00000000000000000000000000000000x\rx00000000000000000000000000000000x\rx00000000000000000000000000000000x\rx00000000000000000000000000000000x\rx00000000000000000000000000000000x\rx00000000000000000000000000000000x\rx00000000000000000000000000000000x\rx00000000000000000000000000000000x\rx00000000000000000000000000000000x\rx00000000000000000000000000000000x\rx00000000000000000000000000000000x\rx00000000000000000000000000000000x\rx00000000000000000000000000000000x\rxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx"
		};

		public static readonly RoomModel Model_6 = new RoomModel (32, "model_6") {
			Door = new Vector3D (0, 15, 0),
			DoorOrientation = 2,
			ClubOnly = false,
			Heightmap = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx\rx222222222x000000000000000000000000xxxx\rx222222222x000000000000000000000000xxxx\rx222222222x000000000000000000000000xxxx\rx222222222x000000000000000000000000xxxx\rx222222222x000000000000000000000000xxxx\rx222222222x000000000000000000000000xxxx\rx222222222x000000000000000000000000xxxx\rx222222222x000000000000000000000000xxxx\rx222222222x00000000xxxxxxxx00000000xxxx\rx11xxxxxxxx00000000xxxxxxxx00000000xxxx\rx00x000000000000000xxxxxxxx00000000xxxx\rx00x000000000000000xxxxxxxx00000000xxxx\rx000000000000000000xxxxxxxx00000000xxxx\rx000000000000000000xxxxxxxx00000000xxxx\r0000000000000000000xxxxxxxx00000000xxxx\rx000000000000000000xxxxxxxx00000000xxxx\rx00x000000000000000xxxxxxxx00000000xxxx\rx00x000000000000000xxxxxxxx00000000xxxx\rx00xxxxxxxxxxxxxxxxxxxxxxxx00000000xxxx\rx00xxxxxxxxxxxxxxxxxxxxxxxx00000000xxxx\rx00x0000000000000000000000000000000xxxx\rx00x0000000000000000000000000000000xxxx\rx0000000000000000000000000000000000xxxx\rx0000000000000000000000000000000000xxxx\rx0000000000000000000000000000000000xxxx\rx0000000000000000000000000000000000xxxx\rx00x0000000000000000000000000000000xxxx\rx00x0000000000000000000000000000000xxxx\rxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx\rxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx\rxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx"
		};

		public static readonly RoomModel Model_7 = new RoomModel (33, "model_7") {
			Door = new Vector3D (0, 17, 0),
			DoorOrientation = 2,
			ClubOnly = false,
			Heightmap = "xxxxxxxxxxxxxxxxxxxxxxxxx\rx222222xx00000000xxxxxxxx\rx222222xx00000000xxxxxxxx\rx2222221000000000xxxxxxxx\rx2222221000000000xxxxxxxx\rx222222xx00000000xxxxxxxx\rx222222xx00000000xxxxxxxx\rx222222xxxxxxxxxxxxxxxxxx\rx222222xkkkkkkxxiiiiiiiix\rx222222xkkkkkkxxiiiiiiiix\rx222222xkkkkkkjiiiiiiiiix\rx222222xkkkkkkjiiiiiiiiix\rx222222xkkkkkkxxiiiiiiiix\rxxx11xxxkkkkkkxxiiiiiiiix\rxxx00xxxkkkkkkxxxxxxxxxxx\rx000000xkkkkkkxxxxxxxxxxx\rx000000xkkkkkkxxxxxxxxxxx\r0000000xkkkkkkxxxxxxxxxxx\rx000000xkkkkkkxxxxxxxxxxx\rx000000xkkkkkkxxxxxxxxxxx\rx000000xxxjjxxxxxxxxxxxxx\rx000000xxxiixxxxxxxxxxxxx\rx000000xiiiiiixxxxxxxxxxx\rxxxxxxxxiiiiiixxxxxxxxxxx\rxxxxxxxxiiiiiixxxxxxxxxxx\rxxxxxxxxiiiiiixxxxxxxxxxx\rxxxxxxxxiiiiiixxxxxxxxxxx\rxxxxxxxxiiiiiixxxxxxxxxxx\rxxxxxxxxiiiiiixxxxxxxxxxx\rxxxxxxxxiiiiiixxxxxxxxxxx"
		};

		public static readonly RoomModel Model_8 = new RoomModel (34, "model_8") {
			Door = new Vector3D (0, 15, 5),
			DoorOrientation = 2,
			ClubOnly = false,
			Heightmap = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx\rx5555555555555555555555555xxxxxxxxx\rx5555555555555555555555555xxxxxxxxx\rx5555555555555555555555555xxxxxxxxx\rx5555555555555555555555555xxxxxxxxx\rx5555555555555555555555555xxxxxxxxx\rx5555555555555555555555555xxxxxxxxx\rx5555555555xxxxxxxxxxxxxxxxxxxxxxxx\rx55555555554321000000000000000000xx\rx55555555554321000000000000000000xx\rx5555555555xxxxx00000000000000000xx\rx555555x44x0000000000000000000000xx\rx555555x33x0000000000000000000000xx\rx555555x22x0000000000000000000000xx\rx555555x11x0000000000000000000000xx\r5555555x00x0000000000000000000000xx\rx555555x0000000000000000000000000xx\rx555555x0000000000000000000000000xx\rx555555x0000000000000000000000000xx\rx555555x0000000000000000000000000xx\rx555555x0000000000000000000000000xx\rx555555x0000000000000000000000000xx\rx555555x0000000000000000000000000xx\rx555555x0000000000000000000000000xx\rx555555x0000000000000000000000000xx\rx555555x0000000000000000000000000xx\rxxxxxxxx0000000000000000000000000xx\rxxxxxxxx0000000000000000000000000xx\rxxxxxxxx0000000000000000000000000xx\rxxxxxxxx0000000000000000000000000xx\rxxxxxxxx0000000000000000000000000xx\rxxxxxxxx0000000000000000000000000xx\rxxxxxxxx0000000000000000000000000xx\rxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx"
		};

		public static readonly RoomModel Model_9 = new RoomModel (35, "model_9") {
			Door = new Vector3D (0, 17, 0),
			DoorOrientation = 2,
			ClubOnly = false,
			Heightmap = "xxxxxxxxxxxxxxxxxxxxxxxx\rx0000000000000000000000x\rx0000000000000000000000x\rx0000000000000000000000x\rx0000000000000000000000x\rx0000000000000000000000x\rx0000000000000000000000x\rx0000000000000000000000x\rx0000000000000000000000x\rx0000000000000000000000x\rx0000000000000000000000x\rx0000000000000000000000x\rx0000000000000000000000x\rx0000000000000000000000x\rx0000000000000000000000x\rx0000000000000000000000x\rx0000000000000000000000x\r00000000000000000000000x\rx0000000000000000000000x\rx0000000000000000000000x\rx0000000000000000000000x\rx0000000000000000000000x\rx0000000000000000000000x\rx0000000000000000000000x\rx0000000000000000000000x\rx0000000000000000000000x\rx0000000000000000000000x\rx0000000000000000000000x\rx0000000000000000000000x\rx0000000000000000000000x\rx0000000000000000000000x\rx0000000000000000000000x\rx0000000000000000000000x\rx0000000000000000000000x\rx0000000000000000000000x\rx0000000000000000000000x\rx0000000000000000000000x\rx0000000000000000000000x\rx0000000000000000000000x\rx0000000000000000000000x\rx0000000000000000000000x\rx0000000000000000000000x\rx0000000000000000000000x\rxxxxxxxxxxxxxxxxxxxxxxxx"
		};

		public static readonly RoomModel Model_snowwar1 = new RoomModel (36, "model_snowwar1") {
			Door = new Vector3D (0, 16, 0),
			DoorOrientation = 2,
			ClubOnly = false,
			Heightmap = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx\rxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx\rxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx\rxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx\rxxxxxxxxxxx0000000000000000xxxxxxxxxxxxxxxxxxx\rxxxxxxxxxx00000000000000000xxxxxxxxxxxxxxxxxxx\rxxxxxxxx00000000000000000000xxxxxxxxxxxxxxxxxx\rxxxxxxx0000000000000000000000xxxxxxxxxxxxxxxxx\rxxxxxx000000000000000000000000xxxxxxxxxxxxxxxx\rxxxxx0000000000000000000000000000xxxxxxxxxxxxx\rxxxx000000000000000000000000000000xxxxxxxxxxxx\rxxx00000000000000000000000000000000xxxxxxxxxxx\rxxx0000000000000000000000000000000000xxxxxxxxx\rxx000000000000000000000000000000000000xxxxxxxx\rxx0000000000000000000000000000000000000xxxxxxx\rxx0000000000000000000000000000000000000xxxxxxx\rx00000000000000000000000000000000000000xxxxxxx\rxx00000000000000000000000000000000000000xxxxxx\rx0000000000000000000000000000000000000000xxxxx\rx00000000000000000000000000000000000000000xxxx\rx00000000000000000000000000000000000000000xxxx\rx000000000000000000000000000000000000000000xxx\rxx000000000000000000000000000000000000000000xx\rxx00000000000000000000000000000000000000000000\rxx00000000000000000000000000000000000000000000\rxx00000000000000000000000000000000000000000000\rxx00000000000000000000000000000000000000000000\rxx00000000000000000000000000000000000000000000\rxxx0000000000000000000000000000000000000000000\rxxxx00000000000000000000000000000000000000xx0x\rxxxxx0000000000000000000000000000000000000xxxx\rxxxxxxx000000000000000000000000000000000xxxxxx\rxxxxxxx0000000000000000000000000000000000xxxxx\rxxxxxxx000000000000000000000000000000000xxxxxx\rxxxxxxxxx0000000000000000000000000000000xxxxxx\rxxxxxxxxxx00000000000000000000000000000xxxxxxx\rxxxxxxxxxxx0000000000000000000000000000xxxxxxx\rxxxxxxxxxxxxx00000000000000000000000000xxxxxxx\rxxxxxxxxxxxxxx0000000000000000000000000xxxxxxx\rxxxxxxxxxxxxxxx00000000000000000000000xxxxxxxx\rxxxxxxxxxxxxxxx00000000000000000000000xxxxxxxx\rxxxxxxxxxxxxxxxx0xxxx000000000000000000xxxxxxx\rxxxxxxxxxxxxxxxxxxxxxx000000000000000xxxxxxxxx\rxxxxxxxxxxxxxxxxxxxxxx0000000000000xxxxxxxxxxx\rxxxxxxxxxxxxxxxxxxxxxx000000000000xxxxxxxxxxxx\rxxxxxxxxxxxxxxxxxxxxxxx000000xxxxxxxxxxxxxxxxx"
		};

		public static readonly RoomModel Model_snowwar2 = new RoomModel (37, "model_snowwar2") {
			Door = new Vector3D (0, 10, 0),
			DoorOrientation = 2,
			ClubOnly = false,
			Heightmap = "xxxxxxxxxxxxxxxxxxxxxx\rxxxxxx00000000xxxxxxxx\rxxxxx0000000000xxxxxxx\rxxxx0000000000000xxxxx\rxxx000000000000000xxxx\rxx00000000000000000xxx\rx0000000000000000000xx\rx0000000000000000000xx\rx00000000000000000000x\rx00000000000000000000x\rx000000000000000000000\rx000000000000000000000\rx000000000000000000000\rx000000000000000000000\rxx00000000000000000000\rxx00000000000000000000\rxxx0000000000000000000\rxxx0000000000000000000\rxxxx000000000000000000\rxxxxx0000000000000000x\rxxxxxxx000000000000xxx\rxxxxxxx0x00000000xxxxx\rxxxxxxxx0x000000xxxxxx\rxxxxxxxxxxxxxxxxxxxxxx"
		};

		public bool ClubOnly { get; protected set; }
		// TODO Enum!
		public int DoorOrientation { get; protected set; }

		public Vector3D Door { get; protected set; }

		public string Heightmap { get; protected set; }

		private RoomModel (int value, string displayName) : base (value, displayName)
		{
			
		}
	}
}