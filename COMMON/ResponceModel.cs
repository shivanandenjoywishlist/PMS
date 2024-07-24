﻿using System;
using System.Collections.Generic;

namespace COMMON
{
    public class ResponseModel<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public List<string> Errors { get; set; }

        public ResponseModel()
        {
            Errors = new List<string>();
        }
    }

    public class JsonModel
    {
        public JsonModel() { }
        public JsonModel(object responseData, string message, int statusCode)
        {
            Data = responseData;
            Message = message;
            StatusCode = statusCode;
        }
        public string AccessToken { get; set; }

        public object Data { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }



    }
}
