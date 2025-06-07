using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;
using Newtonsoft.Json;
using Tuscany.Models;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace Tuscany.Utility
{
    public class LiqpayService
    {
        private static readonly string _privateKey;
        private static readonly string _publicKey;

        static LiqpayService()
        {
            var config = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("utilitySettings.json", optional: false, reloadOnChange: true)
               .Build();

            _privateKey = config["LiqPayPrivateKey"];
            _publicKey = config["LiqPayPublicKey"];


            _publicKey = "sandbox_i53604402195";
            _privateKey = "sandbox_BNtsFcc3MKW2djc4BHgUDHmpl1uCa1cG73gGOD3g";
        }

        public static LiqpayViewModel GetLiqpayModel(int orderId, decimal amount, string? tourName = null)
        {
            // Fill data for submit them to the view
            var signatureSource = new LiqpayCheckoutViewModel
            {
                PublicKey = _publicKey,
                Version = 3,
                Action = "pay",
                Amount = amount,
                Currency = "UAH",
                Description = $"Order{(tourName is null ? $"{orderId}" : $" \"{tourName}\"")} payment",
                OrderId = $"tuscany_{orderId}_{Guid.NewGuid()}",
                Sandbox = 1,

                ResultUrl = "https://localhost:7065/postOrder",
            };
            var jsonString = JsonConvert.SerializeObject(signatureSource);
            var dataHash = Convert.ToBase64String(Encoding.UTF8.GetBytes(jsonString));
            var signatureHash = GetSignature(dataHash);

            // Data for send to view
            var model = new LiqpayViewModel
            {
                Data = dataHash,
                Signature = signatureHash,
                OrderId = orderId,
            };
            return model;
        }

        /// <summary>
        /// Generate liqpay signature
        /// </summary>
        /// <param name="data">Json string with parameters for Liqpay</param>
        /// <returns>liqpay signature</returns>
        public static string GetSignature(string data)
        {
            return Convert.ToBase64String(SHA1.Create()
                .ComputeHash(Encoding.UTF8.GetBytes(_privateKey + data + _privateKey)));
        }
    }
}

