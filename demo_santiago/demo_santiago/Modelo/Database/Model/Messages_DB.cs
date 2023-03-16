using System;
using SQLite;

namespace demo_santiago
{
	public class Messages_DB
	{
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }
		public string Message { get; set; }
        public string Date_Created { get; set; }
    }
}

