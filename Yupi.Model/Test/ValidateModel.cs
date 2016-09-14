// ---------------------------------------------------------------------------------
// <copyright file="ValidateModel.cs" company="https://github.com/sant0ro/Yupi">
//   Copyright (c) 2016 Claudio Santoro, TheDoctor
// </copyright>
// <license>
//   Permission is hereby granted, free of charge, to any person obtaining a copy
//   of this software and associated documentation files (the "Software"), to deal
//   in the Software without restriction, including without limitation the rights
//   to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//   copies of the Software, and to permit persons to whom the Software is
//   furnished to do so, subject to the following conditions:
//
//   The above copyright notice and this permission notice shall be included in
//   all copies or substantial portions of the Software.
//
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//   IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//   FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//   AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//   LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//   OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//   THE SOFTWARE.
// </license>
// ---------------------------------------------------------------------------------
namespace Yupi.Model
{
    using System;

    using Yupi.Model.Domain;

    public class ValidateModel
    {
        #region Methods

        // TODO Implement as Model Validator (Room Model Extensions)
        public static void ValidateDoor()
        {
            foreach (RoomModel model in RoomModel.GetAll())
            {
                string[] rows = model.Heightmap.Split('\r');
                if (rows[(int) model.Door.Y][(int) model.Door.X] == 'x')
                {
                    Console.WriteLine(model.DisplayName);
                    PrintFix(rows, model);
                }
                else if (rows[(int) model.Door.Y][(int) model.Door.X] != rows[(int) model.Door.Y][(int) model.Door.X + 1])
                {
                    Console.WriteLine("Invalid door height: " + model.DisplayName + " should be [ " +
                                      rows[(int) model.Door.Y][(int) model.Door.X + 1] + "]");
                    PrintFix(rows, model);
                }
            }
        }

        private static void PrintFix(string[] rows, RoomModel model)
        {
            char[] doorRow = rows[(int) model.Door.Y].ToCharArray();

            doorRow[(int) model.Door.X] = rows[(int) model.Door.Y][(int) model.Door.X + 1];

            for (int i = 0; i < rows.Length; i++)
            {
                if (i == model.Door.X)
                {
                    Console.WriteLine(doorRow);
                }
                else
                {
                    Console.WriteLine(rows[i]);
                }
            }
        }

        #endregion Methods
    }
}