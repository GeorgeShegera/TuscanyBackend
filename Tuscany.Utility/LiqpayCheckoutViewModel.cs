﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Tuscany.Utility
{
    internal class LiqpayCheckoutViewModel
    {
        // Required parameters
        [JsonProperty("version")]
        public int Version { get; set; }

        [JsonProperty("public_key")]
        public string PublicKey { get; set; }

        [JsonProperty("action")]
        public string Action { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("order_id")]
        public string OrderId { get; set; }

        // Optional parameters
        [JsonProperty("expired_date")]
        public string ExpiredDate { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("paytypes")]
        public string PayTypes { get; set; }

        [JsonProperty("result_url")]
        public string ResultUrl { get; set; }

        [JsonProperty("server_url")]
        public string ServerUrl { get; set; }

        [JsonProperty("verifycode")]
        public string VerifyCode { get; set; }


        /// <summary>
        /// Test mode: 1 - test, 0 - production (in test mode money isn't withdrawn)
        /// </summary>
        [JsonProperty("sandbox")]
        public int Sandbox { get; set; }
    }
}
