namespace EcommerceDemo.Models
    {
    public class ResponseModel<T>
        {
        public int statusCode { get; set; }
        public string messages { get; set; }
        public T? data { get; set; }
      
        }
    }
