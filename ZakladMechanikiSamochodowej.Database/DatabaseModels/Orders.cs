namespace ZakladMechanikiSamochodowej.Database.DatabaseModels
{
    public partial class Orders
    {
        public int Id { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public bool Fix { get; set; } = false;
        public bool Review { get; set; } = false;
        public bool Assembly { get; set; } = false;
        public bool TechnicalConsultation { get; set; } = false;
        public bool Training { get; set; } = false;
        public bool OrderingParts { get; set; } = false;
        public string? NrVIN { get; set; }
        public int ProductionYear { get; set; }
        public string? RegistrationNumber { get; set; }
        public int EngineCapacity { get; set; }
        public OrderState OrderState { get; set; }

        public virtual ICollection<Users> User { get; } = new List<Users>();
    }
}
