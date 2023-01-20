namespace ZakladMechanikiSamochodowej.Database.DatabaseModels
{
    public partial class Order
    {
        public Order(int clientId, string brand, string model, string nrVIN, int productionYear, string registrationNumber, int engineCapacity, OrderState orderState)
        {
            ClientId = clientId;
            Brand = brand;
            Model = model;
            NrVIN = nrVIN;
            ProductionYear = productionYear;
            RegistrationNumber = registrationNumber;
            EngineCapacity = engineCapacity;
            OrderState = orderState;
        }

        public int Id { get; set; }
        public int ClientId { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public bool Fix { get; set; } = false;
        public bool Review { get; set; } = false;
        public bool Assembly { get; set; } = false;
        public bool TechnicalConsultation { get; set; } = false;
        public bool Training { get; set; } = false;
        public bool OrderingParts { get; set; } = false;
        public string NrVIN { get; set; }
        public int ProductionYear { get; set; }
        public string RegistrationNumber { get; set; }
        public int EngineCapacity { get; set; }
        public OrderState OrderState { get; set; }
    }
}
