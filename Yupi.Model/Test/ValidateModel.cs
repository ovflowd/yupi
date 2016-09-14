using System;
using Yupi.Model.Domain;

namespace Yupi.Model
{
	public class ValidateModel
	{
		// TODO Implement as Model Validator (Room Model Extensions)
		public static void ValidateDoor ()
		{
			foreach (RoomModel model in RoomModel.GetAll()) {
				string[] rows = model.Heightmap.Split ('\r');
				if (rows [(int)model.Door.Y] [(int)model.Door.X] == 'x') {

					Console.WriteLine (model.DisplayName);
					PrintFix (rows, model);

				} else if (rows [(int)model.Door.Y] [(int)model.Door.X] != rows [(int)model.Door.Y] [(int)model.Door.X + 1]) {
					Console.WriteLine ("Invalid door height: " + model.DisplayName + " should be [ " + rows [(int)model.Door.Y] [(int)model.Door.X + 1] + "]");
					PrintFix (rows, model);
				}
			}
		}

		private static void PrintFix (string[] rows, RoomModel model)
		{
			char[] doorRow = rows [(int)model.Door.Y].ToCharArray ();

			doorRow [(int)model.Door.X] = rows [(int)model.Door.Y] [(int)model.Door.X + 1];

			for (int i = 0; i < rows.Length; i++) {
				if (i == model.Door.X) {
					Console.WriteLine (doorRow);
				} else {
					Console.WriteLine (rows [i]);
				}
			}
		}
	}
}

