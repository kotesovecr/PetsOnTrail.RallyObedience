﻿using SQLite;
using SQLiteNetExtensions.Attributes;

namespace RallyObedienceApp.Persistency.Models;

public class ParkourItem
{
    [PrimaryKey]
    public string ID { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    [OneToMany(CascadeOperations = CascadeOperation.All)]
    public List<PositionDto> Positions { get; set; } = new();
}

public class PositionDto
{
    [PrimaryKey, AutoIncrement]
    public int ID { get; set; }

    [ForeignKey(typeof(ParkourItem))]
    public string ParkourID { get; set; } = string.Empty;

    [OneToMany(CascadeOperations = CascadeOperation.All)]
    public List<PositionExercises> Exercises { get; set; } = new();

    public double Top { get; set; }
    public double Left { get; set; }
    public double Rotation { get; set; }
}

public class PositionExercises
{
    [PrimaryKey, AutoIncrement]
    public int ID { get; set; }

    [ForeignKey(typeof(PositionDto))]
    public int PositionID { get; set; }

    public string ExerciseId { get; set; } = string.Empty;
}
