namespace ZakladMechanikiSamochodowej.Database.DatabaseModels
{
    public partial class Cars
    {
        public Cars(string carModel, string brand)
        {
            CarModel = carModel;
            Brand = brand;
        }

        public int Id { get; set; }
        public string CarModel { get; set; }
        public string Brand { get; set; }
    }
}
