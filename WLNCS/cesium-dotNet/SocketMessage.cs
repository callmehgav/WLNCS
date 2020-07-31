﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cesium_dotNet
{
    public class SocketMessage<T>
    {
        public string MessageType { get; set; }
        public T Payload { get; set; }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}