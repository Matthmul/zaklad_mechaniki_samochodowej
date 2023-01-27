namespace ZakladMEchanikiSamochodowejTests
{
    [Apartment(ApartmentState.STA)]
    public class Tests
    {
        private ZakladMechanikiSamochodowej.Client.AddData addData;
        private ZakladMechanikiSamochodowej.Client.HomeClient homeClient;

        [SetUp]
        public void Setup()
        {
            addData = new ZakladMechanikiSamochodowej.Client.AddData();
            homeClient= new ZakladMechanikiSamochodowej.Client.HomeClient();
            ZakladMechanikiSamochodowej.Properties.Settings.Default.UserName = "aa";

        }

        [Test]
        public void TestAddDataPhoneNumber()
        {
            bool result = addData.IsValidPhoneNumber("123456789");
            Assert.IsTrue(result);
        }

        [Test]
        public void TestAddDataEmailAdress()
        {
            bool result = addData.IsValidEmailAddress("123@wp.pl");
            Assert.IsTrue(result);
        }

        [Test]
        public void TestHomeCLientCHeckUserState()
        {
            bool result = homeClient.checkUserState(ZakladMechanikiSamochodowej.Properties.Settings.Default.UserName);
            Assert.IsTrue(result);
        }
    }
}