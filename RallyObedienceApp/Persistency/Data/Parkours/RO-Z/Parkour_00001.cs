using RallyObedienceApp.Persistency.Models;

namespace RallyObedienceApp.Persistency.Data.Parkours.RO_Z;

public static class Parkour_00001
{
    public static ParkourItem Create() => new ParkourItem
    {
        ID = "RO-Z-00001",
        Author = "Shrek",
        Category = "Z",
        Description = "Z 00001",
        Positions = new List<PositionDto>
        {
            new PositionDto
            {
                Exercises = new List<PositionExercises>
                {
                    new PositionExercises
                    {
                        PositionID = 1,
                        ExerciseId = "Start"
                    }
                },
                ID = 1,
                Left = 2.0,
                ParkourID = "RO-Z-00001",
                Rotation = 0.0,
                Top = 12.0
            },
            new PositionDto
            {
                Exercises = new List<PositionExercises>
                {
                    new PositionExercises
                    {
                        Number = "1",
                        PositionID = 2,
                        ExerciseId = "Z-001"
                    },
                    new PositionExercises
                    {
                        PositionID = 2,
                        ExerciseId = "D0a"
                    },
                    new PositionExercises
                    {
                        PositionID = 2,
                        ExerciseId = "D0b"
                    },
                    new PositionExercises
                    {
                        Number = "2",
                        PositionID = 2,
                        ExerciseId = "Z-002"
                    },
                    new PositionExercises
                    {
                        Number = "3",
                        PositionID = 2,
                        ExerciseId = "Z-003"
                    },
                    new PositionExercises
                    {
                        PositionID = 2,
                        ExerciseId = "D0c"
                    },
                },
                ID = 2,
                Left = 2.0,
                ParkourID = "RO-Z-00001",
                Rotation = 0.0,
                Top = 7.0
            },
            new PositionDto
            {
                Exercises = new List<PositionExercises>
                {
                    new PositionExercises
                    {
                        PositionID = 3,
                        ExerciseId = "Finish"
                    }
                },
                ID = 3,
                Left = 2.0,
                ParkourID = "RO-Z-00001",
                Rotation = 0.0,
                Top = 2.0
            }
        }
    };
}
