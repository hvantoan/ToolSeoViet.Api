﻿using Newtonsoft.Json;
using System.Collections.Generic;

namespace ToolSeoViet.Services.Models {

    public class BaseResponse {
        public bool Success { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }

        public static BaseResponse Ok() {
            return new() { Success = true };
        }

        public static BaseResponse Fail(string message = null) {
            return new() { Message = message };
        }
    }

    public class BaseResponse<T> : BaseResponse {

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public T Data { get; set; }

        public static BaseResponse<T> Ok(T data) {
            return new() { Success = true, Data = data };
        }
    }

    public class BaseListData<T> {
        public List<T> Items { get; set; }
        public int Count { get; set; }
    }
}