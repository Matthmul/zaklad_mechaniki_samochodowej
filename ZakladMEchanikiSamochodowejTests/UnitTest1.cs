using System.Windows.Controls;
using ZakladMechanikiSamochodowej.Admin;
using ZakladMechanikiSamochodowej.Database.DatabaseActions;
using ZakladMechanikiSamochodowej.Database.DatabaseModels;

namespace ZakladMEchanikiSamochodowejTests
{
    [TestFixture, Apartment(ApartmentState.STA)]
    public class Tests
    {
        private ZakladMechanikiSamochodowej.Client.AddData addData;
        private ZakladMechanikiSamochodowej.Client.HomeClient homeClient;

        [SetUp]
        public void Setup()
        {
            addData = new ZakladMechanikiSamochodowej.Client.AddData();
            homeClient = new ZakladMechanikiSamochodowej.Client.HomeClient();

        }

        // ======================================
        //         AddData Class Tests
        // ======================================
        [Test]
        public void TestValidPhoneNumbersRegex()
        {
            bool validPhoneNumber1 = addData.IsValidPhoneNumber("123456789");
            bool validPhoneNumber2 = addData.IsValidPhoneNumber("134360083");
            bool validPhoneNumber3 = addData.IsValidPhoneNumber("997-997-997");

            Assert.IsTrue(validPhoneNumber1);
            Assert.IsTrue(validPhoneNumber2);
            Assert.IsTrue(validPhoneNumber3);
        }

        [Test]
        public void TestInvalidPhoneNumbersRegex()
        {
            bool invalidPhoneNumber1 = addData.IsValidPhoneNumber("123-a345-c456");
            bool invalidPhoneNumber2 = addData.IsValidPhoneNumber("abc123456");
            bool invalidPhoneNumber3 = addData.IsValidPhoneNumber("997997997112");

            Assert.IsFalse(invalidPhoneNumber1);
            Assert.IsFalse(invalidPhoneNumber2);
            Assert.IsFalse(invalidPhoneNumber3);
        }

        [Test]
        public void TestValidEmailAdressRegex()
        {
            bool validEmail1 = addData.IsValidEmailAddress("123@wp.pl");
            bool validEmail2 = addData.IsValidEmailAddress("www.piatek@onet.com");

            Assert.IsTrue(validEmail1);
            Assert.IsTrue(validEmail2);
        }

        [Test]
        public void TestInvalidEmailAdressRegex()
        {
            bool invalidEmail1 = addData.IsValidEmailAddress("123.wp.pl");
            bool invalidEmail2 = addData.IsValidEmailAddress("@www@onet.com");

            Assert.IsFalse(invalidEmail1);
            Assert.IsFalse(invalidEmail2);
        }

        [Test]
        public void TestHomeClientCheckUserState()
        {
            String userName = "aa";

            bool result = homeClient.checkUserState(userName);
            Assert.IsTrue(result);
        }

        // ======================================
        //     TestAccountEdition Class Tests
        // ======================================
        [Test]
        public void TestAccountEdition()
        {
            //given
            User user = new User("Test", "Testowe");
            user.PhoneNumber = "1234567890";
            user.EmialAddress = "test@testowy.pl";

            //when
            AccountEdition accountEdition = new AccountEdition(user);

            //then
            Assert.That(accountEdition.txtUsername.Text, Is.EqualTo("Test"));
            Assert.That(accountEdition.txtPassword.Password.ToString(), Is.EqualTo("Testowe"));
            Assert.That(accountEdition.txtPhoneNumber.Text, Is.EqualTo("1234567890"));
            Assert.That(accountEdition.txtEmail.Text, Is.EqualTo("test@testowy.pl"));
        }

        // ======================================
        //         OrderTableActions Tests
        // ======================================

        [TestFixture, Apartment(ApartmentState.STA)]
        public class OrderTableActionsTests
        {
            private User user;
            private Order order;
            private int idUser;

            [SetUp]
            public void Setup()
            {
                user = new User("Test", "Test");
                LoginTableActions.SaveUser(user);
                idUser = LoginTableActions.TryGetUserByName("Test").Id;

                order = new Order(idUser, "Audi", "Q7", "XYZ234XY223", 2022, "KR123456", 3000, OrderState.NEW);
                OrdersTableActions.SaveOrder(order);
            }

            [TearDown]
            public void TearDown()
            {
                OrdersTableActions.RemoveOrder(order);
                LoginTableActions.RemoveUser(user);
            }

            [Test]
            public void TestOrdersTableLastOrderAction()
            {
                List<Order> order = OrdersTableActions.GetLastOrder(idUser);
                Assert.That(order[0].Brand, Is.EqualTo("Audi"));
                Assert.IsFalse(order[0].Fix);

            }

            [Test]
            public void TestAllUserOrdersAction()
            {
                List<Order> orders = OrdersTableActions.GetAllUserOrders(idUser);
                Assert.That(orders.Count, Is.EqualTo(1));
            }

            [Test]
            public void TestOrdersByOrderStateAction()
            {
                List<Order> ordersNew = OrdersTableActions.GetOrdersByOrderState(OrderState.NEW);
                Assert.That(ordersNew.Count, Is.GreaterThanOrEqualTo(1));
            }
        }

        // ======================================
        //         LoginTableActions Tests
        // ======================================

        [TestFixture, Apartment(ApartmentState.STA)]
        public class LoginTableActionsTests
        {
            private User testUser;

            [SetUp]
            public void Setup()
            {
                testUser = new User("Test", "Test");
                LoginTableActions.SaveUser(testUser);
            }

            [TearDown]
            public void TearDown()
            {
                LoginTableActions.RemoveUser(testUser);
            }

            [Test]
            public void TestGetUserByIdAndName()
            {
                User userByName = LoginTableActions.TryGetUserByName("Test");
                Assert.AreEqual(userByName.Username, testUser.Username);

                User userById = LoginTableActions.GetUserById(userByName.Id);
                Assert.AreEqual(userById.Username, testUser.Username);

            }

            [Test]
            public void TestAcceptedUsers()
            {
                LoginTableActions.AcceptUser(testUser);
                List<User> acceptedUsers = LoginTableActions.GetAllAcceptedUsers();

                Assert.IsNotNull(acceptedUsers.Find(el => el.Username == testUser.Username));
            }

        }
    }
}