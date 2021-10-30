using System;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using Otus.Teaching.PromoCodeFactoryGrpcService;

namespace Otus.Teaching.PromoCodeFactoryGrpcClient
{
    class Program
    {
        static void Main()
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Customer.CustomerClient(channel);

            Console.WriteLine("All customer");
            GetCustomers(client).Wait();

            Console.WriteLine("Get customer id");
            GetCustomer(client, "a6c8c6b1-4349-45b0-ab31-244740aaf0f0");

            var customerModel = new CustomerModel() {
                Id = "a6c8c6b1-4349-45b0-ab31-244740aaf0f0",
                FirstName = "New",
                PreferenceId = "76324c47-68d2-472d-abb8-33cfa8cc0c84"
            };
            
            Console.WriteLine("New customer");
            CreateCustomer(client, customerModel);
            GetCustomers(client).Wait();

            Console.WriteLine("Edit customer");
            customerModel.FirstName = "Update";
            EditCustomers(client, customerModel);
            GetCustomers(client).Wait();

            Console.WriteLine("Delete customer");
            DeleteCustomer(client, "a6c8c6b1-4349-45b0-ab31-244740aaf0f0");
            GetCustomers(client).Wait();


            Console.ReadKey();
        }

        public static async Task GetCustomers(Customer.CustomerClient client)
        {
            using var clientData = client.GetCustomers(new Empty());
            while (await clientData.ResponseStream.MoveNext(new System.Threading.CancellationToken()))
            {
                var thisProduct = clientData.ResponseStream.Current;
                Console.WriteLine($"id: {thisProduct.Id} FirstName: {thisProduct.FirstName}");
            }
        }
        
        public async static void GetCustomer(Customer.CustomerClient client, string Id)
        {
            var customer = new CustomerRequest {Id = Id };

            using var response = client.GetCustomer(customer);
            while (await response.ResponseStream.MoveNext(new System.Threading.CancellationToken()))
            {
                var thisProduct = response.ResponseStream.Current;
                Console.WriteLine($"id: {thisProduct.Id}");
            }
        }
        
        public static void CreateCustomer(Customer.CustomerClient client, CustomerModel customerModel)
        {
           client.CreateCustomer(customerModel);
        }

        public static void EditCustomers(Customer.CustomerClient client, CustomerModel customerModel)
        {
             client.EditCustomers(customerModel);
        }

        public static void DeleteCustomer(Customer.CustomerClient client, string Id)
        {
            var customer = new CustomerRequest { Id = Id };
            client.DeleteCustomers(customer);
        }
    }

}
