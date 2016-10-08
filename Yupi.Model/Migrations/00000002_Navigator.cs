// ---------------------------------------------------------------------------------
// <copyright file="00000001_Navigator.cs" company="https://github.com/sant0ro/Yupi">
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
using System;
using FluentMigrator;
using Yupi.Model.Domain;

namespace Yupi.Model.Db.Migrations
{
    [Migration (00000002)]
    public class Navigator : Migration
    {
        public override void Down ()
        {
            for (int i = 0; i <= 11; ++i) {
                Delete.FromTable ("NavigatorCategory").Row (new { Caption = "${roomevent_type_" + i + "}" });
            }

            Delete.FromTable ("NavigatorCategory").Row (new { Caption = "Staffs" });

            Delete.FromTable ("NavigatorCategory").Row (new { Caption = "No category" });
        }

        public override void Up () {
            for (int i = 0; i <= 11; ++i) {
                InsertCategory<PromotionNavigatorCategory> ("${roomevent_type_" + i + "}");
            }
            // TODO Should be string in external_texts!
            InsertCategory<PromotionNavigatorCategory> ("Staffs", 6);

            InsertCategory<FlatNavigatorCategory> ("No category");
        }

        private void InsertCategory<T> (string caption, int minRank = 1) where T : NavigatorCategory
        {
            Insert.IntoTable ("NavigatorCategory").Row (new {
                discriminator = typeof(T).FullName,
                Caption = caption,
                IsImage = false,
                IsOpened = false,
                MinRank = minRank,
                Visible = 1,
            });
        }
    }
}
