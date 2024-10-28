﻿using SQLite;

namespace Counter.Models
{
    public class Counter
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
        public string Color { get; set; }
        public int startValue { get; set; }
    }
}
