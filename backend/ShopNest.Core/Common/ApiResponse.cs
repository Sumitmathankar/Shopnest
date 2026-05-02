using System;
using System.Collections.Generic;
using System.Text;

namespace ShopNest.Core.Common
{
    public class ApiResponse<T>
    {
        public T? Result { get; set; }
        public List<string> Errors { get; set; } = new();
        public bool IsSuccess => (Errors?.Count ?? 0) == 0;
        public string? Message { get; set; } 

        // Helper methods to make the Service code cleaner
        public static ApiResponse<T> Success(T result, string? message = null)
        {
            return new ApiResponse<T>
            {
                Result = result,
                Message = message
            };
        }
        public static ApiResponse<T> Failure(List<string> errors) => new() { Errors = errors };
    }
}
