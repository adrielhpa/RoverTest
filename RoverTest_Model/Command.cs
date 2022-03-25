namespace RoverTest.Model
{
    public class Command
    {
        public int PlateauHeight { get; set; }
        public int PlateauWidth { get; set; }
        public int PositionHeight { get; set; }
        public int PositionWidth { get; set; }
        public string PositionDirection { get; set; }
        public string MovementCommand { get; set; }
        public bool IsValid { get; set; } = true;
        public string Error { get; set; } = string.Empty;
        public virtual Command AfterCommand { get; set; }
    }
}