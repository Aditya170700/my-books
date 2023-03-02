﻿using System;
using Newtonsoft.Json;

namespace MyBooks.Data.Views
{
	public class ErrorViews
	{
		public int StatusCode { get; set; }
		public string Message { get; set; }
		public string Path { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}

